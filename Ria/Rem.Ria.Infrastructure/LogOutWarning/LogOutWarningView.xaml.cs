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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Rem.Ria.Infrastructure.LogOutWarning
{
    /// <summary>
    /// LogOutWarningView provides an logout warning prompt.
    /// </summary>
    public partial class LogOutWarningView : ChildWindow, INotifyPropertyChanged
    {
        #region Constants and Fields

        private int _secondsRemaining;
        private string _warningTimeLeft;
        private DispatcherTimer warningPromptTimer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOutWarningView"/> class.
        /// </summary>
        /// <param name="warningCountdownSeconds">The warning countdown seconds.</param>
        public LogOutWarningView ( int warningCountdownSeconds )
        {
            InitializeComponent ();
            DataContext = this;
            _secondsRemaining = warningCountdownSeconds;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the warning time left.
        /// </summary>
        /// <value>The warning time left.</value>
        public string WarningTimeLeft
        {
            get { return _warningTimeLeft; }

            set
            {
                _warningTimeLeft = value;
                OnPropertyChanged ( "WarningTimeLeft" );
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the Tick event for the warning countdown timer.
        /// </summary>
        /// <param name="o">The source of the event.</param>
        /// <param name="sender">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void EachTick ( object o, EventArgs sender )
        {
            WarningTimeLeft = ( _secondsRemaining-- ).ToString ();

            if ( _secondsRemaining > 0 )
            {
                return;
            }

            warningPromptTimer.Stop ();
            warningPromptTimer.Tick -= EachTick;
        }

        /// <summary>
        /// Starts the warning countdown timer that fires at one second intervals.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="sender">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        public void StartTimer ( object o, RoutedEventArgs sender )
        {
            warningPromptTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds ( 1 ) };
            warningPromptTimer.Tick += EachTick;
            warningPromptTimer.Start ();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged ( string propertyName )
        {
            var handler = PropertyChanged;

            if ( handler != null )
            {
                handler ( this, new PropertyChangedEventArgs ( propertyName ) );
            }
        }

        private void CancelButtonClick ( object sender, RoutedEventArgs e )
        {
            DialogResult = false;
        }

        private void OKButtonClick ( object sender, RoutedEventArgs e )
        {
            DialogResult = true;
        }

        #endregion
    }
}
