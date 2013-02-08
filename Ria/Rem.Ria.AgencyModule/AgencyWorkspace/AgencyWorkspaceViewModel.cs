using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.AgencyWorkspace
{
    /// <summary>
    /// View Model for AgencyWorkspace class.
    /// </summary>
    public class AgencyWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IUserDialogService _userDialogService;
        private long _agencyKey;

        private AgencySummaryDto _agencySummaryDto;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public AgencyWorkspaceViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            UploadLabResultCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => UploadLabResultCommand, ExecuteUploadLabResultCommand );
            GoToDashboardCommand = commandFactoryHelper.BuildDelegateCommand ( () => GoToDashboardCommand, ExecuteGoToDashboard );
            GoToProfileCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => GoToProfileCommand, ExecuteGoToProfile );

            _eventAggregator.GetEvent<AgencyChangedEvent> ().Subscribe (
                AgencyChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterAgencyChangedEvents );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the agency key.
        /// </summary>
        public long AgencyKey
        {
            get { return _agencyKey; }
            private set { ApplyPropertyChange ( ref _agencyKey, () => AgencyKey, value ); }
        }

        /// <summary>
        /// Gets the agency primary address.
        /// </summary>
        public AgencyAddressAndPhoneDto AgencyPrimaryAddress
        {
            get
            {
                if ( AgencySummary != null )
                {
                    return _agencySummaryDto.AddressesAndPhones.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the agency primary contact.
        /// </summary>
        public AgencyContactDto AgencyPrimaryContact
        {
            get
            {
                if ( AgencySummary != null )
                {
                    return _agencySummaryDto.AgencyContacts.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the agency primary phone.
        /// </summary>
        public AgencyPhoneDto AgencyPrimaryPhone
        {
            get
            {
                if ( AgencyPrimaryAddress != null )
                {
                    return AgencyPrimaryAddress.PhoneNumbers.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the agency summary.
        /// </summary>
        /// <value>The agency summary.</value>
        public AgencySummaryDto AgencySummary
        {
            get { return _agencySummaryDto; }
            set
            {
                _agencySummaryDto = value;
                RaisePropertyChanged ( () => AgencySummary );
            }
        }

        /// <summary>
        /// Gets the go to dashboard command.
        /// </summary>
        public ICommand GoToDashboardCommand { get; private set; }

        /// <summary>
        /// Gets the go to profile command.
        /// </summary>
        public ICommand GoToProfileCommand { get; private set; }

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get
            {
                if ( AgencySummary != null )
                {
                    return AgencySummary.DisplayName;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Administrative; }
        }

        /// <summary>
        /// Gets the upload lab result command.
        /// </summary>
        public ICommand UploadLabResultCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Agencies the changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.AgencyChangedEventArgs"/> instance containing the event data.</param>
        public void AgencyChangedEventHandler ( AgencyChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded ( () => LoadAgencySummary ( _agencySummaryDto.Key ) );
        }

        /// <summary>
        /// Executes the upload lab result command.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void ExecuteUploadLabResultCommand ( object obj )
        {
            _navigationService.Navigate ( "ModalPopupRegion", "UploadLabResultView" );
        }

        /// <summary>
        /// Filters the agency changed events.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.AgencyChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterAgencyChangedEvents ( AgencyChangedEventArgs obj )
        {
            return _agencySummaryDto != null && _agencySummaryDto.Key == obj.AgencyKey;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Actives the changed.
        /// </summary>
        protected override void ActiveChanged ()
        {
            if ( IsActive )
            {
                _navigationService.Navigate ( "HeaderControlRegion", "MainAgencyQuickPickersView" );
            }
            base.ActiveChanged ();
        }

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var agencyKey = parameters.GetValue<long> ( "AgencyKey" );
            return agencyKey == _agencyKey;
        }

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
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            AgencyKey = parameters.GetValue<long> ( "AgencyKey" );
            var isCreateMode = parameters.GetValue<bool> ( "IsCreate" );
            LoadAgencySummary ( _agencyKey );
            if ( isCreateMode )
            {
                _navigationService.Navigate (
                    RegionManager,
                    "WorkspaceContentScopedRegion",
                    "AgencyEditorView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "AgencyKey", _agencyKey.ToString () ),
                            new KeyValuePair<string, string> ( "IsCreate", "true" )
                        } );
            }
            else
            {
                _navigationService.Navigate (
                    RegionManager,
                    "WorkspaceContentScopedRegion",
                    "AgencyDashboardView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "AgencyKey", _agencyKey.ToString () )
                        } );
            }
        }

        private void ExecuteGoToDashboard ()
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "AgencyDashboardView",
                null,
                new[] { new KeyValuePair<string, string> ( "AgencyKey", _agencyKey.ToString () ) } );
        }

        private void ExecuteGoToProfile ( object obj )
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "AgencyEditorView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "AgencyKey", _agencyKey.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", "false" )
                    } );
        }

        private void GetSummaryByDtoRequestCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<GetAgencySummaryByKeyResponse> ();
            AgencySummary = response.AgencySummaryDto;

            RaisePropertyChanged ( () => AgencyPrimaryAddress );
            RaisePropertyChanged ( () => AgencyPrimaryContact );
            RaisePropertyChanged ( () => AgencyPrimaryPhone );
            RaisePropertyChanged ( () => HeaderContext );
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Loading Agency Summary", UserDialogServiceOptions.Ok );
        }

        private void LoadAgencySummary ( long agencySummaryKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetAgencySummaryByKeyRequest { Key = agencySummaryKey } );

            // TODO: Implement standard exception handler.  maybe in the base class viewmodel
            requestDispatcher.ProcessRequests ( GetSummaryByDtoRequestCompleted, HandleRequestDispatcherException );
            IsLoading = true;
        }

        #endregion
    }
}
