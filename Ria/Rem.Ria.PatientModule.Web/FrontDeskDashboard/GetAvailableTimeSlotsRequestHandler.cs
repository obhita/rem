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
using System.Linq;
using Agatha.Common;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for handling get available time slots request.
    /// </summary>
    public class GetAvailableTimeSlotsRequestHandler :
        NHibernateSessionRequestHandler<GetAvailableTimeSlotsRequest, GetAvailableTimeSlotsResponse>
    {
        #region Constants and Fields

        private readonly IAppointmentRepository _appointmentRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableTimeSlotsRequestHandler"/> class.
        /// </summary>
        /// <param name="appointmentRepository">The appointment repository.</param>
        public GetAvailableTimeSlotsRequestHandler ( IAppointmentRepository appointmentRepository )
        {
            _appointmentRepository = appointmentRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetAvailableTimeSlotsRequest request )
        {
            var beginTime = request.BeginTime ?? new DateTime ( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0, 0 );
            var endTime = request.EndTime ?? new DateTime ( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0, 0 );
            var potentialTimeSlots = BuildFullScheduleForDate (
                request.Date,
                request.SlotSizeInMinutes ?? 60,
                beginTime,
                endTime );
            var availableTimeslots = new List<TimeSlotDto> ();

            var appointments = _appointmentRepository
                .GetAppointmentsByClinicianAndDate ( request.ClinicianKey, request.Date )
                .Where ( x => x.Key != request.AppointmentKey ) // exclude currrent appointment
                .ToList ();

            foreach ( var potentialTimeSlot in potentialTimeSlots )
            {
                var slot = potentialTimeSlot;

                var timeslotFull = appointments.Any (
                    appointment =>
                    ( slot.StartTime >= appointment.AppointmentDateTimeRange.StartDateTime &&
                      slot.StartTime <= appointment.AppointmentDateTimeRange.EndDateTime ) ||
                    ( slot.EndTime >= appointment.AppointmentDateTimeRange.StartDateTime &&
                      slot.EndTime <= appointment.AppointmentDateTimeRange.EndDateTime ) );

                if ( !timeslotFull )
                {
                    availableTimeslots.Add ( potentialTimeSlot );
                }
            }

            var response = CreateTypedResponse ();
            response.TimeSlots = availableTimeslots;
            return response;
        }

        #endregion

        #region Methods

        private static IEnumerable<TimeSlotDto> BuildFullScheduleForDate (
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
