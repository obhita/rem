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

using Agatha.Common;
using AutoMapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.ClinicalCaseEditor
{
    /// <summary>
    /// Class for handling create new clinical case request.
    /// </summary>
    public class CreateNewClinicalCaseRequestHandler :
        NHibernateSessionRequestHandler<CreateNewClinicalCaseRequest, CreateNewClinicalCaseResponse>
    {
        #region Constants and Fields

        private readonly IClinicalCaseFactory _clinicalCaseFactory;
        private readonly ILocationRepository _locationRepository;
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly IPatientRepository _patientRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewClinicalCaseRequestHandler"/> class.
        /// </summary>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="locationRepository">The location repository.</param>
        /// <param name="clinicalCaseFactory">The clinical case factory.</param>
        public CreateNewClinicalCaseRequestHandler (
            ILookupValueRepository lookupValueRepository,
            IPatientRepository patientRepository,
            ILocationRepository locationRepository,
            IClinicalCaseFactory clinicalCaseFactory )
        {
            _lookupValueRepository = lookupValueRepository;
            _patientRepository = patientRepository;
            _locationRepository = locationRepository;
            _clinicalCaseFactory = clinicalCaseFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( CreateNewClinicalCaseRequest request )
        {
            var patient = _patientRepository.GetByKey ( request.PatientKey );
            var location = _locationRepository.GetByKey ( request.LocationKey );
            var clinicalCaseStatus = _lookupValueRepository.GetLookupByWellKnownName<ClinicalCaseStatus> (
                WellKnownNames.ClinicalCaseModule.ClinicalCaseStatus.CaseInitiation );

            var entity = _clinicalCaseFactory.CreateClinicalCase (
                patient, new ClinicalCaseProfileBuilder ().WithInitialLocation ( location ) );
            entity.UpdateStatus ( clinicalCaseStatus );

            var response = CreateTypedResponse ();
            response.ClinicalCaseDto = new ClinicalCaseDto
                {
                    Key = entity.Key,
                    ClinicalCaseProfile = new ClinicalCaseProfileDto
                        {
                            Key = entity.Key,
                            PatientKey = entity.Patient.Key,
                            PatientFullName = entity.Patient.Name.First + " " + entity.Patient.Name.Last,
                            InitialLocation = Mapper.Map<Location, LocationDisplayNameDto> ( entity.ClinicalCaseProfile.InitialLocation ),
                            ClinicalCaseNumber = entity.ClinicalCaseNumber
                        },
                    ClinicalCaseStatus = new ClinicalCaseStatusDto
                        {
                            ClinicalCaseStatus = Mapper.Map<ClinicalCaseStatus, LookupValueDto> ( entity.ClinicalCaseStatus )
                        }
                };

            return response;
        }

        #endregion
    }
}
