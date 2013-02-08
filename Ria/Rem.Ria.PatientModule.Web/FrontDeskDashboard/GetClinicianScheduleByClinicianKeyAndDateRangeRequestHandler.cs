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
using System.Collections.ObjectModel;
using System.Linq;
using Agatha.Common;
using AutoMapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for handling get clinician schedule by clinician key and date range request.
    /// </summary>
    public class GetClinicianScheduleByClinicianKeyAndDateRangeRequestHandler :
        NHibernateSessionRequestHandler<GetClinicianScheduleByClinicianKeyAndDateRangeRequest, GetClinicianScheduleByClinicianKeyAndDateRangeResponse>
    {
        #region Constants and Fields

        private readonly IStaffRepository _staffRepository;
        private readonly IVisitRepository _visitRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetClinicianScheduleByClinicianKeyAndDateRangeRequestHandler"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public GetClinicianScheduleByClinicianKeyAndDateRangeRequestHandler ( IVisitRepository visitRepository, IStaffRepository staffRepository )
        {
            _visitRepository = visitRepository;
            _staffRepository = staffRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetClinicianScheduleByClinicianKeyAndDateRangeRequest request )
        {
            var clinicianKey = request.ClinicianKey;
            var startDate = request.StartDate;
            var endDate = request.EndDate;
            var slotSizeInMinutes = request.SlotSizeInMinutes;
            var beginTime = request.BeginTime;
            var endTime = request.EndTime;

            var potentialTimeSlots = BuildFullScheduleForDate ( startDate, slotSizeInMinutes, beginTime, endTime );
            var totalDays = ( int )( ( endDate.Date - startDate.Date ).TotalDays );
            var totalAppointments = totalDays == 0 ? potentialTimeSlots.Count : totalDays * potentialTimeSlots.Count;

            var visits = _visitRepository.GetVisitsByClinicianAndDateRange ( clinicianKey, startDate, endDate );
            var clinician = _staffRepository.GetByKey ( clinicianKey );
            var clinicianScheduleDto = new ClinicianScheduleDto
                {
                    ClinicianFirstName = clinician.StaffProfile.StaffName.First,
                    ClinicianLastName = clinician.StaffProfile.StaffName.Last,
                    ClinicianKey = clinician.Key,
                    TotalAppointments = totalAppointments,
                    AvailableAppointments = totalAppointments - visits.Count
                };

            var scheduledVisits = from scheduledVisit in visits
                                                                   select new ClinicianAppointmentDto
                                                                       {
                                                                           Key = scheduledVisit.Key,
                                                                           ClinicianKey = clinician.Key,
                                                                           PatientKey = scheduledVisit.ClinicalCase.Patient.Key,
                                                                           PatientFirstName = scheduledVisit.ClinicalCase.Patient.Name.First,
                                                                           PatientLastName = scheduledVisit.ClinicalCase.Patient.Name.Last,
                                                                           AppointmentEndDateTime =
                                                                               scheduledVisit.AppointmentDateTimeRange.EndDateTime,
                                                                           AppointmentStartDateTime =
                                                                               scheduledVisit.AppointmentDateTimeRange.StartDateTime,
                                                                           VisitStatus = new LookupValueDto
                                                                               {
                                                                                   WellKnownName = scheduledVisit.VisitStatus.WellKnownName,
                                                                                   Key = scheduledVisit.VisitStatus.Key,
                                                                                   Name = scheduledVisit.VisitStatus.Name
                                                                               },
                                                                           VisitTemplateName = scheduledVisit.Name,
                                                                           PatientAlerts =
                                                                               scheduledVisit.ClinicalCase.Patient.Alerts.Select (
                                                                                   Mapper.Map<PatientAlert, PatientAlertDto> ).ToList ()
                                                                       };
            clinicianScheduleDto.ClinicianAppointments =
                new ObservableCollection<ClinicianAppointmentDto> ( scheduledVisits );

            var response = CreateTypedResponse ();
            response.ClinicianScheduleDto = clinicianScheduleDto;

            return response;
        }

        #endregion

        #region Methods

        private static IList<TimeSlotDto> BuildFullScheduleForDate (
            DateTime date,
            int slotSizeInMinutes,
            DateTime beginTime,
            DateTime endTime )
        {
            IList<TimeSlotDto> timeSlotDtos = new List<TimeSlotDto> ();

            var endSlotTime = new DateTime (
                date.Year,
                date.Month,
                date.Day,
                endTime.Hour,
                endTime.Minute,
                endTime.Second,
                0 );

            for (
                var slotTime = new DateTime (
                    date.Year,
                    date.Month,
                    date.Day,
                    beginTime.Hour,
                    beginTime.Minute,
                    beginTime.Second,
                    0 );
                slotTime < endSlotTime;
                slotTime = slotTime.AddMinutes ( slotSizeInMinutes ) )
            {
                var timeSlotDto = new TimeSlotDto
                    {
                        StartTime = slotTime,
                        EndTime = slotTime.AddMinutes ( slotSizeInMinutes - 1 )
                    };
                timeSlotDtos.Add ( timeSlotDto );
            }

            return timeSlotDtos;
        }

        #endregion
    }
}
