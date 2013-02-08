#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Agatha.Common;
using AutoMapper;
using C32Gen;
using Health.Direct.Xdm;
using Ionic.Zip;
using Pillar.Common.Collections;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Mail;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientEditor;
using Rem.WellKnownNames;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// QueryPatientByDocumentRequestHandler class.
    /// </summary>
    public class QueryPatientByDocumentRequestHandler : NHibernateSessionRequestHandler<QueryPatientByDocumentRequest, QueryPatientByDocumentResponse>
    {
        #region Constants and Fields

        private readonly ICodedConceptLookupBaseRepository _codedConceptLookupBaseRepository;
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly IImapMailMessageFetcher _imapMessageFetcher;
        private readonly IPatientRepository _patientRepository;
        private readonly IC32Builder _c32Builder;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryPatientByDocumentRequestHandler"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="codedConceptLookupBaseRepository">The coded concept lookup base repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        /// <param name="c32Builder">The C32 builder.</param>
        /// <param name="imapMessageFetcher">The imap message fetcher.</param>
        public QueryPatientByDocumentRequestHandler (
            IPatientRepository patientRepository,
            ICodedConceptLookupBaseRepository codedConceptLookupBaseRepository,
            ILookupValueRepository lookupValueRepository,
            IC32Builder c32Builder,
            IImapMailMessageFetcher imapMessageFetcher )
        {
            _patientRepository = patientRepository;
            _codedConceptLookupBaseRepository = codedConceptLookupBaseRepository;
            _lookupValueRepository = lookupValueRepository;
            _c32Builder = c32Builder;
            _imapMessageFetcher = imapMessageFetcher;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( QueryPatientByDocumentRequest request )
        {
            var mailAttachmentDto = request.MailAttachmentPatientDocument;

            var mailAttachment = GetMailAttachment ( mailAttachmentDto );

            var patientSearchResultDto = GetPatientSearchResultDto ( mailAttachment );

            QueryPatientByDocumentResponse response = CreateTypedResponse ();
            response.PatientSearchResult = patientSearchResultDto;
            return response;
        }

        #endregion

        #region Methods

        private MailAttachment GetMailAttachment ( MailAttachmentPatientDocumentDto documentDto )
        {
            var message = _imapMessageFetcher.FetchMessage ( documentDto.MailFolderName, documentDto.MailId );

            if ( message == null || message.Attachments.Count == 0 )
            {
                throw new ApplicationException ( "Couldn't find the attachment." );
            }

            var mailAttachment = message.Attachments[0];

            return mailAttachment;
        }

        private PatientSearchResultDto GetPatientSearchResultDto ( MailAttachment mailAttachment )
        {
            var patientSearchResultDto = new PatientSearchResultDto ();

            var queryCriteria = new PatientQueryCriteria ();

            var contentBytes = mailAttachment.ContentBytes;
            var fileName = mailAttachment.FileName;

            if ( fileName.ToLower ().EndsWith ( ".zip" ) )
            {
                queryCriteria = GetQueryCriteriaFromXDM ( contentBytes );
            }
            else if ( fileName.ToLower ().EndsWith ( ".xml" ) )
            {
                queryCriteria = GetQueryCriteriaFromC32 ( contentBytes );
            }
            else
            {
                throw new ApplicationException ( "Unsupported file type :" + fileName );
            }

            if ( queryCriteria.IsNullCriteria () )
            {
                return patientSearchResultDto;
            }

            var result = _patientRepository.FindPatientsByAdvancedSearch (
                queryCriteria.FirstName,
                queryCriteria.MiddleName,
                queryCriteria.LastName,
                queryCriteria.GenderWellKnownName,
                queryCriteria.BirthDate,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                0,
                100 );

            var patientSearchResultDtos = Mapper.Map<IList<Patient>, IList<PatientSearchResultDto>> ( result.Item3 );
            var firstOrDefault = patientSearchResultDtos.FirstOrDefault ();
            if ( firstOrDefault != null )
            {
                patientSearchResultDto = firstOrDefault;
            }
            else
            {
                patientSearchResultDto = CreatePatientSearchResultDtoFromQueryCriteria ( queryCriteria );
            }

            return patientSearchResultDto;
        }

        private PatientQueryCriteria GetQueryCriteriaFromXDM ( byte[] contentBytes )
        {
            var patientQueryCriteria = new PatientQueryCriteria ();

            var packager = XDMZipPackager.Default;
            var xdmFile = ZipFile.Read ( contentBytes, null, Encoding.UTF8 );

            var documentPackage = packager.Unpackage ( xdmFile );

            if ( documentPackage == null || documentPackage.Documents == null || documentPackage.Documents.FirstOrDefault () == null )
            {
                return patientQueryCriteria;
            }

            patientQueryCriteria = GetQueryCriteriaFromC32 ( documentPackage.Documents.FirstOrDefault ().DocumentString );

            return patientQueryCriteria;
        }

        private PatientQueryCriteria GetQueryCriteriaFromC32 ( byte[] contentBytes )
        {
            var patientQueryCriteria = new PatientQueryCriteria ();

            var contentString = Encoding.UTF8.GetString ( contentBytes );

            patientQueryCriteria = GetQueryCriteriaFromC32 ( contentString );

            return patientQueryCriteria;
        }

        private PatientQueryCriteria GetQueryCriteriaFromC32 ( string content )
        {
            var patientQueryCriteria = new PatientQueryCriteria ();

            if ( string.IsNullOrEmpty ( content ) )
            {
                return patientQueryCriteria;
            }
            var c32Dto = _c32Builder.BuildC32Dto ( content );

            if ( c32Dto == null || c32Dto.Header == null || c32Dto.Header.PersonalInfo == null || c32Dto.Header.PersonalInfo.PatientInfo == null
                 || c32Dto.Header.PersonalInfo.PatientInfo.PersonInfo == null )
            {
                return patientQueryCriteria;
            }

            var personAddress = c32Dto.Header.PersonalInfo.PatientInfo.PersonAddress;
            if ( personAddress != null )
            {
                patientQueryCriteria.City = personAddress.City;
                patientQueryCriteria.AddressLineOne = personAddress.StreetAddressLines == null
                                                          ? null
                                                          : personAddress.StreetAddressLines.FirstOrDefault ();
                patientQueryCriteria.ZipCode = personAddress.PostalCode;
                patientQueryCriteria.StateWellKnownName = personAddress.State;
            }

            var personPhone = c32Dto.Header.PersonalInfo.PatientInfo.PersonPhone;
            if ( personPhone != null )
            {
                patientQueryCriteria.Phone = personPhone.Value;
            }

            var personInfo = c32Dto.Header.PersonalInfo.PatientInfo.PersonInfo;
            if ( personInfo.PersonName != null )
            {
                patientQueryCriteria.FirstName = personInfo.PersonName.Given;
                patientQueryCriteria.LastName = personInfo.PersonName.Family;
            }

            if ( personInfo.Gender != null && !string.IsNullOrEmpty ( personInfo.Gender.Code ) )
            {
                var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode ( typeof( AdministrativeGender ), personInfo.Gender.Code );

                if ( lookup != null )
                {
                    patientQueryCriteria.GenderWellKnownName = lookup.WellKnownName;
                }
            }

            if ( personInfo.PersonDateOfBirth != null && !string.IsNullOrEmpty ( personInfo.PersonDateOfBirth.Value ) )
            {
                var birthDate = DateTime.MinValue;

                DateTime.TryParseExact (
                    personInfo.PersonDateOfBirth.Value,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out birthDate );

                if ( birthDate != DateTime.MinValue )
                {
                    patientQueryCriteria.BirthDate = birthDate;
                }
            }

            return patientQueryCriteria;
        }

        private PatientSearchResultDto CreatePatientSearchResultDtoFromQueryCriteria ( PatientQueryCriteria queryCriteria )
        {
            var patientSearchResultDto = new PatientSearchResultDto
                {
                    Addresses = new SoftDeleteObservableCollection<PatientAddressDto> (),
                    PhoneNumbers = new SoftDeleteObservableCollection<PatientPhoneDto> (),
                    FirstName = queryCriteria.FirstName,
                    LastName = queryCriteria.LastName,
                    MiddleName = queryCriteria.MiddleName,
                    BirthDate = queryCriteria.BirthDate
                };

            if (!string.IsNullOrEmpty(queryCriteria.GenderWellKnownName))
            {
                var patientGender = _lookupValueRepository.GetLookupByWellKnownName<PatientGender> ( queryCriteria.GenderWellKnownName );
                if ( patientGender != null )
                {
                    patientSearchResultDto.PatientGender = Mapper.Map<LookupBase, LookupValueDto> ( patientGender );
                }
            }

            if ( !string.IsNullOrEmpty ( queryCriteria.Phone ) )
            {
                var patientPhoneType =
                    _lookupValueRepository.GetLookupByWellKnownName<PatientPhoneType> ( WellKnownNames.PatientModule.PatientPhoneType.Other );
                var patietnPhoneTypeDto = Mapper.Map<LookupBase, LookupValueDto> ( patientPhoneType );
                var patientPhoneDto = new PatientPhoneDto
                    {
                        PatientPhoneType = patietnPhoneTypeDto,
                        PhoneNumber = queryCriteria.Phone
                    };
                patientSearchResultDto.PhoneNumbers.Add ( patientPhoneDto );
            }

            if ( !string.IsNullOrEmpty ( queryCriteria.AddressLineOne ) || !string.IsNullOrEmpty ( queryCriteria.City )
                 || !string.IsNullOrEmpty ( queryCriteria.StateWellKnownName ) || !string.IsNullOrEmpty ( queryCriteria.ZipCode ) )
            {
                var patientAddressType =
                    _lookupValueRepository.GetLookupByWellKnownName<PatientAddressType> ( WellKnownNames.PatientModule.PatientAddressType.Other );
                var patientAddressTypeDto = Mapper.Map<LookupBase, LookupValueDto> ( patientAddressType );
                var patientAddressDto = new PatientAddressDto
                    {
                        FirstStreetAddress = queryCriteria.AddressLineOne,
                        PostalCode = queryCriteria.ZipCode,
                        CityName = queryCriteria.City,
                        PatientAddressType = patientAddressTypeDto
                    };

                if ( !string.IsNullOrEmpty ( queryCriteria.StateWellKnownName ) )
                {
                    var state = _lookupValueRepository.GetLookupByWellKnownName<StateProvince> ( queryCriteria.StateWellKnownName );
                    if ( state != null )
                    {
                        patientAddressDto.StateProvince = Mapper.Map<LookupBase, LookupValueDto> ( state );
                    }
                }

                patientSearchResultDto.Addresses.Add ( patientAddressDto );
            }

            return patientSearchResultDto;
        }

        #endregion

        #region Struct

        private struct PatientQueryCriteria
        {
            public string FirstName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }

            public string GenderWellKnownName { get; set; }

            public DateTime? BirthDate { get; set; }

            public string AddressLineOne { get; set; }

            public string City { get; set; }

            public string StateWellKnownName { get; set; }

            public string ZipCode { get; set; }

            public string Phone { get; set; }

            public bool IsNullCriteria ()
            {
                return string.IsNullOrEmpty ( FirstName ) &&
                       string.IsNullOrEmpty ( MiddleName ) &&
                       string.IsNullOrEmpty ( LastName ) &&
                       string.IsNullOrEmpty ( GenderWellKnownName ) &&
                       !BirthDate.HasValue;
            }
        }

        #endregion
    }
}
