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
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Practices.Unity;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// View for scheduling appointment.
    /// </summary>
    public partial class AppointmentSchedulerView : UserControl
    {
        #region Constants and Fields

        private const string TIMEFORMAT = "hh:mm tt";
        private readonly DispatcherTimer _timer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentSchedulerView"/> class.
        /// </summary>
        public AppointmentSchedulerView ()
        {
            InitializeComponent ();

            _timer = new DispatcherTimer ();
            txtTime.Text = DateTime.Now.ToString ( TIMEFORMAT );
            if ( DateTime.Now.Minute + 1 < 60 )
            {
                _timer.Interval =
                    ( new DateTime (
                          DateTime.Now.Year,
                          DateTime.Now.Month,
                          DateTime.Now.Day,
                          DateTime.Now.Hour,
                          DateTime.Now.Minute + 1,
                          0 ) - DateTime.Now );
            }
            else
            {
                _timer.Interval =
                    ( new DateTime (
                          DateTime.Now.Year,
                          DateTime.Now.Month,
                          DateTime.Now.Day,
                          DateTime.Now.Hour + 1,
                          0,
                          0 ) - DateTime.Now );
            }
            _timer.Tick += Timer_Tick;
            _timer.Start ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentSchedulerView"/> class.
        /// </summary>
        /// <param name="appointmentSchedulerViewModel">The appointment scheduler view model.</param>
        [InjectionConstructor]
        public AppointmentSchedulerView ( AppointmentSchedulerViewModel appointmentSchedulerViewModel )
            : this ()
        {
            DataContext = appointmentSchedulerViewModel;
        }

        #endregion

        #region Methods

        private void Timer_Tick ( object sender, EventArgs e )
        {
            txtTime.Text = DateTime.Now.ToString ( TIMEFORMAT );
            _timer.Interval = TimeSpan.FromMinutes ( 1 );
        }

        #endregion
    }
}
