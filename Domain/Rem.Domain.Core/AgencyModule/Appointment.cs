#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The Appointment represents a a fixed mutual agreement for a meeting or engagement. The Appointment is associated with a time range.
    /// </summary>
    public class Appointment : AuditableAggregateRootBase
    {
        private DateTimeRange _appointmentDateTimeRange;
        private string _note;
        private Staff _staff;
        private string _subjectDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment"/> class.
        /// </summary>
        protected Appointment ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment"/> class.
        /// </summary>
        /// <param name="staff">The staff.</param>
        /// <param name="appointmentDateTimeRange">The appointment date time range.</param>
        protected internal Appointment ( Staff staff, DateTimeRange appointmentDateTimeRange )
        {
            Check.IsNotNull ( staff, "Staff is required." );
            Check.IsNotNull ( appointmentDateTimeRange, "AppointmentDateTimeRange is required." );

            _staff = staff;
            _appointmentDateTimeRange = appointmentDateTimeRange;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment"/> class.
        /// </summary>
        /// <param name="staff">
        /// The staff.
        /// </param>
        /// <param name="appointmentDateTimeRange">
        /// The appointment date time range.
        /// </param>
        /// <param name="subjectDescription">
        /// The subject description.
        /// </param>
        protected internal Appointment ( Staff staff, DateTimeRange appointmentDateTimeRange, string subjectDescription )
            : this ( staff, appointmentDateTimeRange )
        {
            _subjectDescription = subjectDescription;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment"/> class.
        /// </summary>
        /// <param name="staff">
        /// The staff.
        /// </param>
        /// <param name="appointmentDateTimeRange">
        /// The appointment date time range.
        /// </param>
        /// <param name="subjectDescription">
        /// The subject description.
        /// </param>
        /// <param name="appointmentNote">
        /// The appointment note.
        /// </param>
        protected internal Appointment ( Staff staff, DateTimeRange appointmentDateTimeRange, string subjectDescription, string appointmentNote )
            : this ( staff, appointmentDateTimeRange )
        {
            _subjectDescription = subjectDescription;
            _note = appointmentNote;
        }

        /// <summary>
        /// Gets Staff.
        /// </summary>
        [NotNull]
        public virtual Staff Staff
        {
            get { return _staff; }
            private set { ApplyPropertyChange ( ref _staff, () => Staff, value ); }
        }

        /// <summary>
        /// Gets AppointmentDateTimeRange.
        /// </summary>
        [NotNull]
        public virtual DateTimeRange AppointmentDateTimeRange
        {
            get { return _appointmentDateTimeRange; }
            private set { ApplyPropertyChange ( ref _appointmentDateTimeRange, () => AppointmentDateTimeRange, value ); }
        }

        /// <summary>
        /// Gets SubjectDescription.
        /// </summary>
        public virtual string SubjectDescription
        {
            get { return _subjectDescription; }
            private set { ApplyPropertyChange ( ref _subjectDescription, () => SubjectDescription, value ); }
        }

        /// <summary>
        /// Gets Note.
        /// </summary>
        public virtual string Note
        {
            get { return _note; }
            private set { ApplyPropertyChange ( ref _note, () => Note, value ); }
        }

        /// <summary>
        /// The reassign appointment.
        /// </summary>
        /// <param name="staff">
        /// The staff.
        /// </param>
        public virtual void ReassignAppointment ( Staff staff )
        {
            Check.IsNotNull ( staff, "Staff is required." );

            // TODO: Create validator for if staff is available.
            if ( Staff == null || !Staff.Equals ( staff ) )
            {
                Staff = staff;
            }
        }

        /// <summary>
        /// The reschedule appointment.
        /// </summary>
        /// <param name="appointmentDateTimeRange">
        /// The appointment date time range.
        /// </param>
        public virtual void RescheduleAppointment ( DateTimeRange appointmentDateTimeRange )
        {
            Check.IsNotNull ( appointmentDateTimeRange, "AppointmentDateTimeRange is required." );
            if ( AppointmentDateTimeRange != null && !AppointmentDateTimeRange.Equals ( appointmentDateTimeRange ) )
            {
                AppointmentDateTimeRange = appointmentDateTimeRange;
            }
        }

        /// <summary>
        /// The revise subject description.
        /// </summary>
        /// <param name="subjectDescription">
        /// The subject description.
        /// </param>
        public virtual void ReviseSubjectDescription ( string subjectDescription )
        {
            SubjectDescription = subjectDescription;
        }

        /// <summary>
        /// The revise note.
        /// </summary>
        /// <param name="appointmentNote">
        /// The appointment note.
        /// </param>
        public virtual void ReviseNote ( string appointmentNote )
        {
            Note = appointmentNote;
        }
    }
}
