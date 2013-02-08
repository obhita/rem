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
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.CdsRuleService;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.CdsAlerts
{
    /// <summary>
    /// CdsAlertService class.
    /// </summary>
    public class CdsAlertService : ICdsAlertService
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly IUserDialogService _userDialogService;
        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CdsAlertService"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public CdsAlertService (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            INotificationService notificationService,
            IEventAggregator eventAggregator )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _notificationService = notificationService;
            _eventAggregator = eventAggregator;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks the rules.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        public void CheckRules ( long patientKey )
        {
            _patientKey = patientKey;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new CheckCdsRulesRequest { PatientKey = patientKey } );
            requestDispatcher.ProcessRequests ( CheckRulesCompleted, HandleDispatcherException );
        }

        #endregion

        #region Methods

        private void CheckRulesCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<CheckCdsRulesResponse> ();

            foreach ( var alert in response.Alerts )
            {
                var uriQuery = new UriQuery
                    {
                        { "Key", alert.Key.ToString () },
                        { "Name", alert.Name },
                        { "Note", alert.Note },
                        { "CdsIdentifier", alert.CdsIdentifier }
                    };
                var uri = new Uri ( "CdsAlertView" + uriQuery, UriKind.Relative );
                _notificationService.ShowNotification ( uri );
            }
            RaisePatientChangedEvent ( _patientKey );
        }

        private void HandleDispatcherException ( ExceptionInfo exceptionInfox )
        {
            _userDialogService.ShowDialog ( exceptionInfox.Message, "Failed checking CDS rules", UserDialogServiceOptions.Ok );
        }

        private void RaisePatientChangedEvent ( long patientKey )
        {
            _eventAggregator.GetEvent<PatientChangedEvent> ().Publish ( new PatientChangedEventArgs { Sender = this, PatientKey = patientKey } );
        }

        #endregion
    }
}
