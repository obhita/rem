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
using System.Windows;
using System.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for picking time span.
    /// </summary>
    [TemplatePart ( Name = "PART_YearsTextBox", Type = typeof( TextBox ) )]
    [TemplatePart ( Name = "PART_MonthsTextBox", Type = typeof( TextBox ) )]
    public class TimeSpanPicker : Control
    {
        #region Constants and Fields

        private const double NoOfDaysInAMonth = 30.417; //  ( 365 / 12 );
        private const int NoOfDaysInAnYear = 365;
        private bool _isYearChangeFromTimeSpanEvent;
        private bool _isMonthChangeFromTimeSpanEvent;

        /// <summary>
        /// Dependency Property for MonthsProperty Property.
        /// </summary>
        public static readonly DependencyProperty MonthsProperty =
            DependencyProperty.Register (
                "Months",
                typeof( int? ),
                typeof( TimeSpanPicker ),
                new PropertyMetadata ( null, OnMonthsChanged ) );

        /// <summary>
        /// Dependency Property for TimeSpanProperty Property.
        /// </summary>
        public static readonly DependencyProperty TimeSpanProperty =
            DependencyProperty.Register (
                "TimeSpan",
                typeof( TimeSpan? ),
                typeof( TimeSpanPicker ),
                new PropertyMetadata ( null, OnTimeSpanChanged ) );

        /// <summary>
        /// Dependency Property for YearsProperty Property.
        /// </summary>
        public static readonly DependencyProperty YearsProperty =
            DependencyProperty.Register (
                "Years",
                typeof( int? ),
                typeof( TimeSpanPicker ),
                new PropertyMetadata ( null, OnYearsChanged ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanPicker"/> class.
        /// </summary>
        public TimeSpanPicker ()
        {
            DefaultStyleKey = typeof( TimeSpanPicker );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the months.
        /// </summary>
        /// <value>The months.</value>
        public int? Months
        {
            get { return ( int? )GetValue ( MonthsProperty ); }
            set { SetValue ( MonthsProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the time span.
        /// </summary>
        /// <value>The time span.</value>
        public TimeSpan? TimeSpan
        {
            get { return ( TimeSpan? )GetValue ( TimeSpanProperty ); }
            set { SetValue ( TimeSpanProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the years.
        /// </summary>
        /// <value>The years.</value>
        public int? Years
        {
            get { return ( int? )GetValue ( YearsProperty ); }
            set { SetValue ( YearsProperty, value ); }
        }

        #endregion
      

        #region Methods

        private static int GetMonths ( TimeSpan timeSpan )
        {
            int months;

            if ( timeSpan.TotalDays >= NoOfDaysInAnYear )
            {
                var daysLeft = ( timeSpan.TotalDays % NoOfDaysInAnYear );
                var result = daysLeft / NoOfDaysInAMonth;
                months = Convert.ToInt32 ( ( Math.Round ( result, 0 ) ) );
            }
            else
            {
                var result = timeSpan.TotalDays / NoOfDaysInAMonth;
                months = Convert.ToInt32 ( ( Math.Round ( result, 0 ) ) );
            }
            return months;
        }

        private static TimeSpan GetTimeSpan ( int years, int months )
        {
            var result = ( years * NoOfDaysInAnYear ) + ( months * NoOfDaysInAMonth );

            return  new TimeSpan ( Convert.ToInt32 ( Math.Round ( result, 0 ) ), 0, 0, 0 );
        }

        private static int GetYears ( TimeSpan timeSpan )
        {
            var years = 0;

            if ( timeSpan.TotalDays >= NoOfDaysInAnYear )
            {
                years = ( int )( timeSpan.TotalDays / NoOfDaysInAnYear );
            }
            return years;
        }

        private static void OnMonthsChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var yearsMonthsControl = d as TimeSpanPicker;

            if ( yearsMonthsControl == null )
            {
                return;
            }

            var months = ( int? )e.NewValue;

            if ( yearsMonthsControl.Years.GetValueOrDefault () == 0 && months.GetValueOrDefault () == 0 )
            {
                if (months.HasValue || yearsMonthsControl.Years.HasValue)
                { 
                    // One of year or month is not null --> 0
                    if (!yearsMonthsControl._isMonthChangeFromTimeSpanEvent)
                    {
                        yearsMonthsControl.TimeSpan = new TimeSpan ( 0 );
                    }
                }
                else
                {
                    // Both year and Month are null --> null
                    yearsMonthsControl.TimeSpan = null;
                }
            }
            else
            {
                if (!yearsMonthsControl._isMonthChangeFromTimeSpanEvent)
                {
                    yearsMonthsControl.TimeSpan = GetTimeSpan(yearsMonthsControl.Years.GetValueOrDefault(), months.GetValueOrDefault());
                }
            }

            if (yearsMonthsControl._isMonthChangeFromTimeSpanEvent)
            {
                yearsMonthsControl._isMonthChangeFromTimeSpanEvent = false;
            }
        }

        private static void OnTimeSpanChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var yearsMonthsControl = d as TimeSpanPicker;

            if ( yearsMonthsControl != null )
            {
                var timeSpan = ( TimeSpan? )e.NewValue;

                if (timeSpan.HasValue)
                {
                    yearsMonthsControl._isYearChangeFromTimeSpanEvent = true;
                    yearsMonthsControl.Years = GetYears ( timeSpan.Value );

                    yearsMonthsControl._isMonthChangeFromTimeSpanEvent = true;
                    yearsMonthsControl.Months = GetMonths ( timeSpan.Value );
                }
                else
                {
                    yearsMonthsControl._isYearChangeFromTimeSpanEvent = true;
                    yearsMonthsControl.Years = null;

                    yearsMonthsControl._isMonthChangeFromTimeSpanEvent = true;
                    yearsMonthsControl.Months = null;
                }
            }
        }

        private static void OnYearsChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var yearsMonthsControl = d as TimeSpanPicker;

            if ( yearsMonthsControl == null )
            {
                return;
            }

            var years = ( int? )e.NewValue;

            if ( years.GetValueOrDefault () == 0 && yearsMonthsControl.Months.GetValueOrDefault () == 0 )
            {
                if (years.HasValue || yearsMonthsControl.Months.HasValue)
                {
                    // One of year or month is not null --> 0
                    if (!yearsMonthsControl._isYearChangeFromTimeSpanEvent)
                    {
                        yearsMonthsControl.TimeSpan = new TimeSpan ( 0 );
                    }
                }
                else
                {
                    // Both year and Month are null --> null
                    yearsMonthsControl.TimeSpan = null;
                }
            }
            else
            {
                if (!yearsMonthsControl._isYearChangeFromTimeSpanEvent)
                {
                    yearsMonthsControl.TimeSpan = GetTimeSpan ( years.GetValueOrDefault (), yearsMonthsControl.Months.GetValueOrDefault () );
                }
            }

            if (yearsMonthsControl._isYearChangeFromTimeSpanEvent)
            {
                yearsMonthsControl._isYearChangeFromTimeSpanEvent = false;
            }
        }
        #endregion
    }
}
