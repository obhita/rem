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

using System.Collections.Generic;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for Dast10 class.
    /// </summary>
    public class Dast10ViewModel : ActivityViewModelBase<Dast10Dto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private bool _canViewGuidance;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dast10ViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public Dast10ViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, eventAggregator, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _popupService = popupService;
            _eventAggregator = eventAggregator;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ShowGuidanceCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => ShowGuidanceCommand, ExecuteShowGuidanceCommand, CanExecuteShowGuidanceCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance can view guidance.
        /// </summary>
        /// <value><c>true</c> if this instance can view guidance; otherwise, <c>false</c>.</value>
        public bool CanViewGuidance
        {
            get { return _canViewGuidance; }
            set { ApplyPropertyChange ( ref _canViewGuidance, () => CanViewGuidance, value ); }
        }

        /// <summary>
        /// Gets the show guidance command.
        /// </summary>
        public ICommand ShowGuidanceCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
        protected override ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory )
        {
            return CommandFactoryHelper.CreateHelper ( this, commandFactory );
        }

        /// <summary>
        /// Loads the activity.
        /// </summary>
        /// <param name="activityKey">The activity key.</param>
        protected override void LoadActivity ( long activityKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<Dast10Dto> { Key = activityKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( GetDast10Completed, HandleRequestDispatcherException );
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );

            //Publish visit changed event.
            var visitChangedEventArgs = new VisitChangedEventArgs
                {
                    Sender = this,
                    VisitKey = EditingDto.VisitKey,
                    ClinicianKey = EditingDto.ClinicianKey,
                    PatientKey = EditingDto.PatientKey,
                    VisitStartDateTime = EditingDto.AppointmentStartDateTime
                };
            _eventAggregator.GetEvent<VisitChangedEvent> ().Publish ( visitChangedEventArgs );

            var activityChangedEventArgs = new ActivityChangedEventArgs
                {
                    Sender = this,
                    VisitKey = EditingDto.VisitKey,
                    ClinicianKey = EditingDto.ClinicianKey,
                    PatientKey = EditingDto.PatientKey
                };
            _eventAggregator.GetEvent<ActivityChangedEvent> ().Publish ( activityChangedEventArgs );

            if ( EditingDto.IsNidaDrugQuestionnaireCreated )
            {
                _userDialogService.ShowDialog (
                    "The following activity item(s) have been created: \nNidaDrugQuestionnaire", "Notification", UserDialogServiceOptions.Ok );
            }

            SetCanViewGuidanceFlag ();
        }

        private bool CanExecuteShowGuidanceCommand ( object value )
        {
            return EditingDto != null && EditingDto.SeverityScore != null && EditingDto.Dast10Result != null;
        }

        private void ExecuteShowGuidanceCommand ( object value )
        {
            _popupService.ShowPopup (
                "Dast10ResultView",
                null,
                "DAST-10 Guidance",
                new[] { new KeyValuePair<string, string> ( "Dast10Result", EditingDto.Dast10Result.GetValueOrDefault ().ToString () ) } );
        }

        private void GetDast10Completed ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<Dast10Dto>> ();
            EditingDto = response.DataTransferObject;
            SetCanViewGuidanceFlag ();
            IsLoading = false;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Error Loading DAST-10", UserDialogServiceOptions.Ok );
        }

        private void SetCanViewGuidanceFlag ()
        {
            CanViewGuidance = ( EditingDto != null && EditingDto.SeverityScore != null && EditingDto.Dast10Result != null );
        }

        #endregion
    }
}
