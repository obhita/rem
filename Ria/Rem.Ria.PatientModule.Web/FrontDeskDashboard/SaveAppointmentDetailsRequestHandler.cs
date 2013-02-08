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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for handling save appointment details request.
    /// </summary>
    public class SaveAppointmentDetailsRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<AppointmentDetailsDto>, DtoResponse<AppointmentDetailsDto>, AppointmentDetailsDto, Visit>
    {
        #region Constants and Fields

        private readonly IActivitySchedulerService _activitySchedulerService;
        private readonly IClinicalCaseRepository _clinicalCaseRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IVisitFactory _visitFactory;
        private readonly IVisitTemplateRepository _visitTemplateRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAppointmentDetailsRequestHandler"/> class.
        /// </summary>
        /// <param name="staffRepository">The staff repository.</param>
        /// <param name="clinicalCaseRepository">The clinical case repository.</param>
        /// <param name="visitTemplateRepository">The visit template repository.</param>
        /// <param name="locationRepository">The location repository.</param>
        /// <param name="visitFactory">The visit factory.</param>
        /// <param name="activitySchedulerService">The activity scheduler service.</param>
        public SaveAppointmentDetailsRequestHandler (
            IStaffRepository staffRepository,
            IClinicalCaseRepository clinicalCaseRepository,
            IVisitTemplateRepository visitTemplateRepository,
            ILocationRepository locationRepository,
            IVisitFactory visitFactory,
            IActivitySchedulerService activitySchedulerService )
        {
            _staffRepository = staffRepository;
            _clinicalCaseRepository = clinicalCaseRepository;
            _visitTemplateRepository = visitTemplateRepository;
            _locationRepository = locationRepository;
            _visitFactory = visitFactory;
            _activitySchedulerService = activitySchedulerService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The data trasfer object.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.VisitModule.Visit"/></returns>
        protected override Visit CreateNew ( AppointmentDetailsDto dto )
        {
            var staff = _staffRepository.GetByKey ( dto.ClinicianKey.Value );
            var clinicalCase =
                _clinicalCaseRepository.GetActiveClinicalCaseByPatient ( dto.PatientKey );
            var visitTemplate =
                _visitTemplateRepository.GetByKey ( dto.VisitTemplateKey.Value );
            var location = _locationRepository.GetByKey ( dto.Location.Key );

            var visit = _visitFactory.CreateVisit (
                staff,
                new DateTimeRange ( dto.AppointmentStartDateTime.Value, dto.AppointmentEndDateTime.Value ),
                clinicalCase,
                visitTemplate,
                location );

            foreach ( var visitTemplateActivityType in visitTemplate.ActivityTypes )
            {
                _activitySchedulerService.ScheduleActivity ( visit.Key, visitTemplateActivityType.ActivityType );

                ////// TODO: We do not currently have full support for this feature.  Eventually we 
                //////       need to enable activity factories to register themselves with the base 
                //////       activity service so that we can schedule activities by type.  For now 
                //////       we will hard code the one supported activity type called VitalSigns.
                ////if ( visitTemplateActivityType.ActivityType.WellKnownName == WellKnownNames.VisitModule.ActivityType.VitalSign )
                ////{
                ////    var vitalSign = _vitalSignFactory.CreateVitalSign ( visit );

                ////    if ( !vitalSign.PersistenceRuleContext.CheckRules () )
                ////    {
                ////        MappingHelper.MapRuleViolationsToDataErrorInfo ( dto, vitalSign );
                ////    }
                ////}
                ////else
                ////{
                ////    throw new SystemException ( "Unknown activity type: " +
                ////                                visitTemplateActivityType.ActivityType.Name );
                ////}
            }

            return visit;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( AppointmentDetailsDto dto, Visit entity )
        {
            if ( dto.ClinicianKey != entity.Staff.Key )
            {
                var staff = _staffRepository.GetByKey ( dto.ClinicianKey.Value );
                entity.ReassignAppointment ( staff );
            }
            entity.RescheduleAppointment(new DateTimeRange(dto.AppointmentStartDateTime.Value, dto.AppointmentEndDateTime.Value));

            var location = _locationRepository.GetByKey(dto.Location.Key);
            if (location.Key != entity.ServiceLocation.Key)
            {
                entity.ChangeServiceLocation ( location );
            }

            return true;
        }

        #endregion
    }
}
