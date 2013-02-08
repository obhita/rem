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
using Agatha.Common;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for handling update clinician appointment request.
    /// </summary>
    public class UpdateClinicianAppointmentRequestHandler :
        NHibernateSessionRequestHandler<UpdateClinicianAppointmentRequest, UpdateClinicianAppointmentResponse>
    {
        #region Constants and Fields

        private readonly IStaffRepository _staffRepository;
        private readonly IVisitRepository _visitRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateClinicianAppointmentRequestHandler"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public UpdateClinicianAppointmentRequestHandler ( IVisitRepository visitRepository, IStaffRepository staffRepository )
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
        public override Response Handle ( UpdateClinicianAppointmentRequest request )
        {
            var clinicianAppointmentDto = request.ClinicianAppointmentDto;
            var clinicianKey = request.ClinicianKey;

            var visit = _visitRepository.GetByKey ( clinicianAppointmentDto.Key );
            if ( visit == null )
            {
                throw new ArgumentException (
                    "Could not find Visit Associated with ClinicianAppoitmentDto with key:" +
                    clinicianAppointmentDto.Key );
            }

            Staff newStaff = null;
            if ( clinicianKey != visit.Staff.Key )
            {
                newStaff = _staffRepository.GetByKey ( clinicianKey );
                if ( newStaff == null )
                {
                    throw new ArgumentException ( "Could not find Staff with key:" + clinicianKey );
                }
            }

            if ( visit.AppointmentDateTimeRange.StartDateTime != clinicianAppointmentDto.AppointmentStartDateTime
                 || visit.AppointmentDateTimeRange.EndDateTime != clinicianAppointmentDto.AppointmentEndDateTime )
            {
                visit.RescheduleAppointment (
                    new DateTimeRange ( clinicianAppointmentDto.AppointmentStartDateTime, clinicianAppointmentDto.AppointmentEndDateTime ) );
            }

            if ( newStaff != null )
            {
                visit.ReassignAppointment ( newStaff );
            }

            clinicianAppointmentDto = new ClinicianAppointmentDto
                {
                    Key = visit.Key,
                    PatientKey = visit.ClinicalCase.Patient.Key,
                    PatientFirstName = visit.ClinicalCase.Patient.Name.First,
                    PatientLastName = visit.ClinicalCase.Patient.Name.Last,
                    AppointmentEndDateTime = visit.AppointmentDateTimeRange.EndDateTime,
                    AppointmentStartDateTime = visit.AppointmentDateTimeRange.StartDateTime,
                    VisitStatus = new LookupValueDto
                        {
                            WellKnownName = visit.VisitStatus.WellKnownName,
                            Key = visit.VisitStatus.Key,
                            Name = visit.VisitStatus.Name
                        },
                    VisitTemplateName = visit.Name
                };

            var response = CreateTypedResponse ();
            response.ClinicianAppointmentDto = clinicianAppointmentDto;

            return response;
        }

        #endregion
    }
}
