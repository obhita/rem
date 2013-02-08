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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Agatha.Common;
using Pillar.Common.Collections;
using Pillar.Domain.Primitives;
using Pillar.Domain.Event;
using Pillar.Common.Utility;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Mail;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientEditor;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// CreatePatientImportDocumentRequestHandler class.
    /// </summary>
    public class CreatePatientImportDocumentRequestHandler :
        NHibernateSessionRequestHandler<CreatePatientImportDocumentRequest, CreatePatientImportDocumentResponse>
    {
        #region Constants and Fields

        private readonly IImapMailMessageFetcher _imapMessageFetcher;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientFactory _patientFactory;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly IPatientDocumentFactory _patientDocumentFactory;
        private readonly IPatientDocumentRepository _patientDocumentRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePatientImportDocumentRequestHandler"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="patientFactory">The patient factory.</param>
        /// <param name="agencyRepository">The agency repository.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="patientDocumentFactory">The patient document factory.</param>
        /// <param name="patientDocumentRepository">The patient document repository.</param>
        /// <param name="imapMessageFetcher">The imap message fetcher.</param>
        public CreatePatientImportDocumentRequestHandler (
            IPatientRepository patientRepository,
            IPatientFactory patientFactory,
            IAgencyRepository agencyRepository,
            IDtoToDomainMappingHelper mappingHelper,
            IPatientDocumentFactory patientDocumentFactory,
            IPatientDocumentRepository patientDocumentRepository,
            IImapMailMessageFetcher imapMessageFetcher )
        {
            _patientRepository = patientRepository;
            _patientFactory = patientFactory;
            _agencyRepository = agencyRepository;
            _mappingHelper = mappingHelper;
            _patientDocumentFactory = patientDocumentFactory;
            _patientDocumentRepository = patientDocumentRepository;
            _imapMessageFetcher = imapMessageFetcher;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( CreatePatientImportDocumentRequest request )
        {
            var mailAttachmentDto = request.MailAttachmentPatientDocument;
            var patientSearchResult = request.PatientSearchResult;
            var agencyKey = request.AgencyKey;

            var patient = SavePatient ( patientSearchResult, agencyKey );

            ImportDocument ( patient, mailAttachmentDto );

            var profileDto = AutoMapper.Mapper.Map<Patient, PatientProfileDto> ( patient );

            CreatePatientImportDocumentResponse response = CreateTypedResponse ();
            response.PatientProfile = profileDto;
            return response;
        }

        #endregion

        #region Methods

        private Patient SavePatient ( PatientSearchResultDto patientSearchResult, long agencyKey )
        {
            CheckPhoneAndAddressRequiredFields(patientSearchResult);

            var agency = _agencyRepository.GetByKey ( agencyKey );

            var personName = new PersonName (
                patientSearchResult.PrefixName,
                patientSearchResult.FirstName,
                patientSearchResult.MiddleName,
                patientSearchResult.LastName,
                patientSearchResult.SuffixName );

            var patientGender = ( patientSearchResult.PatientGender == null )
                                    ? null
                                    : _mappingHelper.MapLookupField<PatientGender> ( patientSearchResult.PatientGender );
            var patientProfile = new PatientProfileBuilder ()
                .WithBirthDate ( patientSearchResult.BirthDate )
                .WithPatientGender ( patientGender )
                .Build ();

            var patient = _patientFactory.CreatePatient ( agency, personName, patientProfile );

            PopulatePatientAddresses ( patient, patientSearchResult );
            PopulatePatientPhones ( patient, patientSearchResult );
            
            _patientRepository.MakePersistent(patient);

            return patient;
        }

        #region Address

        private void PopulatePatientAddresses ( Patient patient, PatientSearchResultDto patientSearchResult )
        {
            new AggregateNodeCollectionMapper<PatientAddressDto, Patient, PatientAddress> (
                new SoftDeleteObservableCollection<PatientAddressDto> ( patientSearchResult.Addresses ), patient, patient.Addresses )
                .MapRemovedItem ( RemovePatientAddress )
                .MapAddedItem ( AddPatientAddress )
                .MapChangedItem ( ChangePatientAddress )
                .Map ();
        }

        private static void RemovePatientAddress ( PatientAddressDto patientAddressDto, Patient patient, PatientAddress patientAddress )
        {
            patient.RemoveAddress ( patientAddress );
        }

        private void AddPatientAddress ( PatientAddressDto patientAddressDto, Patient patient )
        {
            var addressType = _mappingHelper.MapLookupField<PatientAddressType> ( patientAddressDto.PatientAddressType );
            var countyAreaLookup = _mappingHelper.MapLookupField<CountyArea> ( patientAddressDto.CountyArea );
            var stateProvinceLookup = _mappingHelper.MapLookupField<StateProvince> ( patientAddressDto.StateProvince );
            var countryLookup = _mappingHelper.MapLookupField<Country> ( patientAddressDto.Country );

            var address = new AddressBuilder ()
                .WithFirstStreetAddress ( patientAddressDto.FirstStreetAddress )
                .WithSecondStreetAddress ( patientAddressDto.SecondStreetAddress )
                .WithCityName ( patientAddressDto.CityName )
                .WithCountyArea ( countyAreaLookup )
                .WithStateProvince ( stateProvinceLookup )
                .WithCountry ( countryLookup )
                .WithPostalCode (
                    string.IsNullOrWhiteSpace ( patientAddressDto.PostalCode ) ? null : new PostalCode ( patientAddressDto.PostalCode ) )
                .Build ();

            var patientAddress = new PatientAddressBuilder ()
                .WithPatientAddressType ( addressType )
                .WithAddress ( address )
                .WithConfidentialIndicator ( patientAddressDto.ConfidentialIndicator )
                .WithYearsOfStayNumber ( patientAddressDto.YearsOfStayNumber )
                .Build ();

            patient.AddAddress ( patientAddress );
        }

        private void ChangePatientAddress ( PatientAddressDto patientAddressDto, Patient patient, PatientAddress patientAddress )
        {
            RemovePatientAddress ( patientAddressDto, patient, patientAddress );
            AddPatientAddress ( patientAddressDto, patient );
        }

        #endregion

        #region Phones

        private void PopulatePatientPhones ( Patient patient, PatientSearchResultDto patientSearchResult )
        {
            new AggregateNodeCollectionMapper<PatientPhoneDto, Patient, PatientPhone> (
                new SoftDeleteObservableCollection<PatientPhoneDto> ( patientSearchResult.PhoneNumbers ), patient, patient.PhoneNumbers )
                .MapRemovedItem ( RemovePatientPhone )
                .MapAddedItem ( AddPatientPhone )
                .MapChangedItem ( ChangePatientPhone )
                .Map ();
        }

        private static void RemovePatientPhone ( PatientPhoneDto patientPhoneDto, Patient patient, PatientPhone patientPhone )
        {
            patient.RemovePhoneNumber ( patientPhone );
        }

        private void AddPatientPhone ( PatientPhoneDto patientPhoneDto, Patient patient )
        {
            var patientPhoneType = _mappingHelper.MapLookupField<PatientPhoneType> ( patientPhoneDto.PatientPhoneType );

            var patientPhone = new PatientPhone (
                patientPhoneType,
                patientPhoneDto.PhoneNumber,
                patientPhoneDto.PhoneExtensionNumber,
                patientPhoneDto.ConfidentialIndicator );

            patient.AddPhoneNumber ( patientPhone );
        }

        private void ChangePatientPhone ( PatientPhoneDto patientPhoneDto, Patient patient, PatientPhone patientPhone )
        {
            RemovePatientPhone ( patientPhoneDto, patient, patientPhone );
            AddPatientPhone ( patientPhoneDto, patient );
        }

        #endregion

        private void ImportDocument ( Patient patient, MailAttachmentPatientDocumentDto dto )
        {
            var message = _imapMessageFetcher.FetchMessage ( dto.MailFolderName, dto.MailId );

            if ( message == null || message.Attachments.Count == 0 )
            {
                throw new ApplicationException ( "Couldn't find the attachment." );
            }

            //TODO: Right now, only get the first attachment
            dto.Document = message.Attachments[0].ContentBytes;
            dto.FileName = message.Attachments[0].FileName;

            var patientDocumentType = _mappingHelper.MapLookupField<PatientDocumentType> ( dto.PatientDocumentType );
            var patientDocument = _patientDocumentFactory.CreatePatientDocument ( patient, patientDocumentType, dto.Document, dto.FileName );

            var clinicalDateRange = new DateRange ( dto.ClinicalStartDate, dto.ClinicalEndDate );

            patientDocument.ReviseClinicalDateRange ( clinicalDateRange );
            patientDocument.ReviseDescription ( dto.Description );
            patientDocument.ReviseDocumentProviderName ( dto.DocumentProviderName );
            patientDocument.ReviseOtherDocumentTypeName ( dto.OtherDocumentTypeName );

            _patientDocumentRepository.MakePersistent ( patientDocument );
        }

        private void CheckPhoneAndAddressRequiredFields(PatientSearchResultDto patientSearchResult)
        {
            var sb = new StringBuilder ();

            var phoneRequiredFieldMissing = from p in patientSearchResult.PhoneNumbers
                                   where p.PatientPhoneType == null || string.IsNullOrEmpty(p.PhoneNumber) 
                                   select p;
            if (phoneRequiredFieldMissing.Count() > 0)
            {
                sb.Append("Phone Type and Phone Number are all required");
                sb.Append(Environment.NewLine);
            }

            var regex = new Regex ( @"^\d{5}(-\d{4})?$" );
            var addressRequiredFieldMissing = from a in patientSearchResult.Addresses
                                              where (a.PatientAddressType == null || string.IsNullOrEmpty(a.PostalCode) || a.StateProvince == null || string.IsNullOrEmpty(a.CityName) || string.IsNullOrEmpty(a.FirstStreetAddress))
                                              || (!string.IsNullOrEmpty(a.PostalCode) && !regex.IsMatch ( a.PostalCode ))
                                     select a;
            if (addressRequiredFieldMissing.Count() > 0)
            {
                sb.Append("Address Type, First Street Address, City Name, State Province and PostalCode are all required. And also please make sure the foramt of Post Code is correct.");
                sb.Append ( Environment.NewLine );
            }

            var errors = sb.ToString();
            if (!string.IsNullOrEmpty(errors))
            {
                throw new ArgumentException(string.Format("Please fix the following errors before trying to save:{0}{1}", Environment.NewLine, errors));
            }
        }

        #endregion
    }
}
