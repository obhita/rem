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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for History for social.
    /// </summary>
    public class SocialHistoryViewModel : ActivityViewModelBase<SocialHistoryDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly Dictionary<bool, string> _choices = new Dictionary<bool, string> { { true, "Yes" }, { false, "No" } };
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private bool _isFreezeFocused;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private string _patientFirstName;
        private long _patientKey;
        private string _patientLastName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialHistoryViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public SocialHistoryViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            IPopupService popupService,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, eventAggregator, commandFactory )
        {
            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SmokingWillingToQuitCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => SmokingWillingToQuitCommand, ExecuteSmokingWillingToQuitCommand, CanExecuteSmokingWillingToQuitCommand );
            ScheduleFollowupCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => ScheduleFollowupCommand, ExecuteScheduleFollowupCommand, CanExecuteScheduleFollowupCommand );

            _userDialogService = userDialogService;
            _popupService = popupService;
            _navigationService = navigationService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _eventAggregator = eventAggregator;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.AddLookupValuesRequest ( "SmokingStatus" );
            requestDispatcher.ProcessRequests ( GetLookupsCompleted, HandleRequestDispatcherException );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the choices.
        /// </summary>
        public Dictionary<bool, string> Choices
        {
            get { return _choices; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is freeze focused.
        /// </summary>
        /// <value><c>true</c> if this instance is freeze focused; otherwise, <c>false</c>.</value>
        public bool IsFreezeFocused
        {
            get { return _isFreezeFocused; }
            set { ApplyPropertyChange ( ref _isFreezeFocused, () => IsFreezeFocused, value ); }
        }

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets the schedule followup command.
        /// </summary>
        public ICommand ScheduleFollowupCommand { get; private set; }

        /// <summary>
        /// Gets the smoking willing to quit command.
        /// </summary>
        public ICommand SmokingWillingToQuitCommand { get; private set; }

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
            requestDispatcher.Add ( new GetDtoRequest<SocialHistoryDto> ( activityKey ) );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( GetSocialHistoryByActivityKeyCompleted, HandleRequestDispatcherException );
        }

        /// <summary>
        /// Navigates to edit activity command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToEditActivityCommand ( KeyValuePair<string, string>[] parameters )
        {
            _patientKey = parameters.GetValue<long> ( "PatientKey" );
            _patientFirstName = parameters.GetValue<string> ( "PatientFirstName" );
            _patientLastName = parameters.GetValue<string> ( "PatientLastName" );
            base.NavigateToEditActivityCommand ( parameters );
            IsFreezeFocused = false;
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );

            var visitChangedEventArgs = new VisitChangedEventArgs
                {
                    Sender = this,
                    VisitKey = EditingDto.VisitKey,
                    ClinicianKey = EditingDto.ClinicianKey,
                    PatientKey = EditingDto.PatientKey,
                    VisitStartDateTime = EditingDto.AppointmentStartDateTime
                };

            _eventAggregator.GetEvent<VisitChangedEvent> ().Publish ( visitChangedEventArgs );

            var createdActivityNames = GetCreatedActivityNames ();

            if ( createdActivityNames.Count <= 0 )
            {
                return;
            }

            var activityChangedEventArgs = new ActivityChangedEventArgs
                {
                    Sender = this,
                    VisitKey = EditingDto.VisitKey,
                    ClinicianKey = EditingDto.ClinicianKey,
                    PatientKey = EditingDto.PatientKey
                };
            _eventAggregator.GetEvent<ActivityChangedEvent> ().Publish ( activityChangedEventArgs );

            var nameBuilder = new StringBuilder ();
            var last = createdActivityNames.Last ();
            createdActivityNames.ForEach (
                name =>
                    {
                        if ( name != last )
                        {
                            nameBuilder.AppendFormat ( "{0}, ", name );
                        }
                        else
                        {
                            nameBuilder.Append ( name );
                        }
                    } );

            var msg = "The following activity item(s) have been created: " + nameBuilder;

            _userDialogService.ShowDialog ( msg, "Notification", UserDialogServiceOptions.Ok );
        }

        private bool CanExecuteScheduleFollowupCommand ( object value )
        {
            return EditingDto != null;
        }

        private bool CanExecuteSmokingWillingToQuitCommand ( object value )
        {
            return EditingDto != null;
        }

        private void ExecuteScheduleFollowupCommand ( object value )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "FrontDeskDashboardView",
                "CreateAppointment",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ),
                        new KeyValuePair<string, string> ( "PatientFirstName", _patientFirstName ),
                        new KeyValuePair<string, string> ( "PatientLastName", _patientLastName ),
                    } );
        }

        private void ExecuteSmokingWillingToQuitCommand ( object value )
        {
            IsFreezeFocused = true;
            if ( value != null )
            {
                if ( ( bool )value )
                {
                    _popupService.ShowPopup ( "SocialHistorySmokingCessationAdvisoryView", null, "Smoking Cessation Advice" );
                }
            }
        }

        private IList<string> GetCreatedActivityNames ()
        {
            IList<string> names = new List<string> ();

            if ( EditingDto.IsAuditCCreated )
            {
                names.Add ( "Audit-C" );
            }
            if ( EditingDto.IsDast10Created )
            {
                names.Add ( "DAST-10" );
            }
            if ( EditingDto.IsPhq9Created )
            {
                names.Add ( "PHQ-9" );
            }

            return names;
        }

        private void GetLookupsCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void GetSocialHistoryByActivityKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<SocialHistoryDto>> ();
            if ( EditingDto != null )
            {
                EditingDto.PropertyChanged -= HandlePropertyChanged;
            }
            EditingDto = response.DataTransferObject;
            if ( EditingDto != null )
            {
                EditingDto.PropertyChanged += HandlePropertyChanged;
            }
            IsLoading = false;
        }

        private void HandlePropertyChanged ( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => EditingDto.SmokingStatusAreYouWillingToQuitIndicator ) )
            {
                ExecuteSmokingWillingToQuitCommand ( EditingDto.SmokingStatusAreYouWillingToQuitIndicator );
            }
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Social History operation failed.", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
