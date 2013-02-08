using System;
using System.Collections;
using Rem.Infrastructure.Service.DataTransferObject;
using Telerik.Windows.Controls.ScheduleView;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Data transfer object for LocationWorkHour class.
    /// </summary>
    public partial class LocationWorkHourDto : IAppointment
    {
        #region Constants and Fields

        private LocationWorkHourDto _cacheObject;

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
            get
            {
                var startOfWeek = GetStartOfWeek ();
                return new DateTime (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    startOfWeek.AddDays ( ( int )DayOfWeek ).Day,
                    EndTime.Value.Hours,
                    EndTime.Value.Minutes,
                    EndTime.Value.Seconds );
            }

            set
            {
                DayOfWeek = value.DayOfWeek;
                EndTime = value - value.Date;
            }
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
                throw new NotImplementedException ();
            }
        }

        /// <summary>
        /// Gets the collection containing the resources, associated with the appointment.
        /// </summary>
        public IList Resources
        {
            get { return new ResourceItemCollection (); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.DateTime"/> value determining the start date and time of the <see cref="T:Telerik.Windows.Controls.ScheduleView.IAppointment"/>.
        /// </summary>
        /// <value>The start.</value>
        public DateTime Start
        {
            get
            {
                var startOfWeek = GetStartOfWeek ();
                return new DateTime (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    startOfWeek.AddDays ( ( int )DayOfWeek ).Day,
                    StartTime.Value.Hours,
                    StartTime.Value.Minutes,
                    StartTime.Value.Seconds );
            }

            set
            {
                DayOfWeek = value.DayOfWeek;
                StartTime = value - value.Date;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.String"/> value representing the subject of the <see cref="T:Telerik.Windows.Controls.ScheduleView.IAppointment"/> object.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject
        {
            get { return string.Format ( "{0} - {1}", Start.ToShortTimeString (), End.ToShortTimeString () ); }
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

        #endregion

        #region Public Methods

        /// <summary>
        /// Causes the object to enter editing mode.
        /// </summary>
        public void BeginEdit ()
        {
            _cacheObject = new LocationWorkHourDto ();
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
            var newDto = new LocationWorkHourDto ();
            newDto.CopyFrom ( this );
            return newDto;
        }

        /// <summary>
        /// Copies from.
        /// </summary>
        /// <param name="other">The other.</param>
        public void CopyFrom ( IAppointment other )
        {
            if ( other is LocationWorkHourDto )
            {
                var otherDto = other as LocationWorkHourDto;
                Key = otherDto.Key;
                DayOfWeek = otherDto.DayOfWeek;
                StartTime = otherDto.StartTime;
                EndTime = otherDto.EndTime;
            }
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
            if ( other is LocationWorkHourDto )
            {
                return Equals ( other as KeyedDataTransferObject );
            }
            return false;
        }

        #endregion

        #region Methods

        private DateTime GetStartOfWeek ()
        {
            var diff = DateTime.Now.DayOfWeek - DayOfWeek.Sunday;
            if ( diff < 0 )
            {
                diff += 7;
            }

            return DateTime.Now.AddDays ( -1 * diff ).Date;
        }

        #endregion
    }
}
