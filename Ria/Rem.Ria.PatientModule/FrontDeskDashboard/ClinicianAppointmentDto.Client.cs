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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rem.Infrastructure.Service.DataTransferObject;
using Telerik.Windows.Controls.ScheduleView;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Data transfer object for ClinicianAppointment class.
    /// </summary>
    public partial class ClinicianAppointmentDto : IAppointment
    {
        #region Constants and Fields

        private ClinicianAppointmentDto _cacheObject;

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when <see cref="T:Telerik.Windows.Controls.ScheduleView.RecurrencePattern"/> status is changed.
        /// </summary>
        public event EventHandler RecurrenceRuleChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="T:System.DateTime"/> value determining the end date and time of the <see cref="T:Telerik.Windows.Controls.ScheduleView.IAppointment"/>.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime End
        {
            get { return AppointmentEndDateTime; }
            set
            {
                AppointmentEndDateTime = value;
                RaisePropertyChanged ( () => End );
            }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get { return PatientFirstName + " " + PatientLastName; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has alert.
        /// </summary>
        public bool HasAlert
        {
            get { return PatientAlerts.Count () > 0; }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Boolean"/> value indicating if the current <see cref="T:Telerik.Windows.Controls.ScheduleView.IAppointment"/> is an All-day one.
        /// </summary>
        /// <value><c>true</c> if this instance is all day event; otherwise, <c>false</c>.</value>
        public bool IsAllDayEvent
        {
            get { return false; }
            set { }
        }

        /// <summary>
        /// Gets or sets the location key.
        /// </summary>
        /// <value>The location key.</value>
        public long LocationKey { get; set; }

        /// <summary>
        /// Gets or sets the recurrence rule.
        /// </summary>
        /// <value>The recurrence rule.</value>
        public IRecurrenceRule RecurrenceRule
        {
            get { return null; }
            set
            {
                if ( RecurrenceRuleChanged != null )
                {
                    RecurrenceRuleChanged ( this, new EventArgs () );
                }
            }
        }

        /// <summary>
        /// Gets the collection containing the resources, associated with the appointment.
        /// </summary>
        public IList Resources
        {
            get
            {
                if ( VisitStatus == null )
                {
                    return new List<object> ();
                }
                else
                {
                    return new List<object> ( new object[] { VisitStatus } );
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.DateTime"/> value determining the start date and time of the <see cref="T:Telerik.Windows.Controls.ScheduleView.IAppointment"/>.
        /// </summary>
        /// <value>The start.</value>
        public DateTime Start
        {
            get { return AppointmentStartDateTime; }
            set
            {
                AppointmentStartDateTime = value;
                RaisePropertyChanged ( () => Start );
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.String"/> value representing the subject of the <see cref="T:Telerik.Windows.Controls.ScheduleView.IAppointment"/> object.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject
        {
            get { return PatientFirstName + " " + PatientLastName; }
            set { }
        }

        /// <summary>
        /// Gets or sets the appointment time zone.
        /// </summary>
        /// <value>The appointment time zone.</value>
        public TimeZoneInfo TimeZone
        {
            get { return TimeZoneInfo.Local; }
            set { }
        }

        /// <summary>
        /// Gets or sets the visit template key.
        /// </summary>
        /// <value>The visit template key.</value>
        public long? VisitTemplateKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the visit type.
        /// </summary>
        /// <value>The name of the visit type.</value>
        public string VisitTypeName
        {
            get { return VisitTemplateName; }
            set { }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Causes the object to enter editing mode.
        /// </summary>
        public void BeginEdit ()
        {
            _cacheObject = new ClinicianAppointmentDto ();
            _cacheObject.CopyFrom ( this );
        }

        /// <summary>
        /// Causes the object to leave editing mode and revert to the previous, unedited value.
        /// </summary>
        public void CancelEdit ()
        {
            CopyFrom ( _cacheObject );
        }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns>A <see cref="Telerik.Windows.Controls.ScheduleView.IAppointment"/></returns>
        public IAppointment Copy ()
        {
            IAppointment copy = new ClinicianAppointmentDto ();
            copy.CopyFrom ( this );
            return copy;
        }

        /// <summary>
        /// Copies from.
        /// </summary>
        /// <param name="other">The other.</param>
        public void CopyFrom ( IAppointment other )
        {
            var otherDto = other as ClinicianAppointmentDto;
            Key = otherDto.Key;
            PatientKey = otherDto.PatientKey;
            ClinicianKey = otherDto.ClinicianKey;
            VisitStatus = otherDto.VisitStatus;
            AppointmentStartDateTime = otherDto.AppointmentStartDateTime;
            AppointmentEndDateTime = otherDto.AppointmentEndDateTime;
            PatientFirstName = otherDto.PatientFirstName;
            PatientLastName = otherDto.PatientLastName;
        }

        /// <summary>
        /// Causes the object to leave editing mode and commit the edited value.
        /// </summary>
        public void EndEdit ()
        {
            _cacheObject = null;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( IAppointment other )
        {
            if ( other is ClinicianAppointmentDto )
            {
                return Equals ( other as KeyedDataTransferObject );
            }
            return false;
        }

        #endregion
    }
}
