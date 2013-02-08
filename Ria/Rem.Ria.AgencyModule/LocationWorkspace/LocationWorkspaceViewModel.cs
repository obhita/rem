using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.LocationEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.LocationWorkspace
{
    /// <summary>
    /// View Model for LocationWorkspace class.
    /// </summary>
    public class LocationWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IUserDialogService _userDialogService;
        private long _locationKey;

        private LocationSummaryDto _locationSummaryDto;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public LocationWorkspaceViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;

            _eventAggregator.GetEvent<LocationChangedEvent> ().Subscribe (
                LocationChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterLocationChangedEvents );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            GoToDashboardCommand = commandFactoryHelper.BuildDelegateCommand ( () => GoToDashboardCommand, ExecuteGoToDashboard );
            GoToProfileCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => GoToProfileCommand, ExecuteGoToProfile );
        }

        #endregion

        #region Public Properties

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
                if ( LocationSummary != null )
                {
                    return LocationSummary.DisplayName;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the location primary address.
        /// </summary>
        public LocationAddressAndPhoneDto LocationPrimaryAddress
        {
            get
            {
                if ( LocationSummary != null )
                {
                    return _locationSummaryDto.LocationAddressesAndPhones.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the location primary contact.
        /// </summary>
        public LocationContactDto LocationPrimaryContact
        {
            get
            {
                if ( LocationSummary != null )
                {
                    return _locationSummaryDto.LocationContacts.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the location primary phone.
        /// </summary>
        public LocationPhoneDto LocationPrimaryPhone
        {
            get
            {
                if ( LocationPrimaryAddress != null )
                {
                    return LocationPrimaryAddress.PhoneNumbers.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the location summary.
        /// </summary>
        /// <value>The location summary.</value>
        public LocationSummaryDto LocationSummary
        {
            get { return _locationSummaryDto; }
            set
            {
                _locationSummaryDto = value;
                RaisePropertyChanged ( () => LocationSummary );
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Locations; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the location changed events.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.LocationChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterLocationChangedEvents ( LocationChangedEventArgs obj )
        {
            return _locationSummaryDto != null && _locationSummaryDto.Key == obj.LocationKey;
        }

        /// <summary>
        /// Locations the changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.LocationChangedEventArgs"/> instance containing the event data.</param>
        public void LocationChangedEventHandler ( LocationChangedEventArgs obj )
        {
            Deployment.Current.Dispatcher.BeginInvoke (
                () =>
                    {
                        var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                        requestDispatcher.Add ( new GetDtoRequest<LocationSummaryDto> { Key = obj.LocationKey } );
                        requestDispatcher.ProcessRequests ( HandleGetLocationSummaryDtoCompleted, HandleGetLocationSummaryDtoException );
                    } );
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
            var locationKey = parameters.GetValue<long> ( "LocationKey" );
            return LocationSummary.Key == locationKey;
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
            _locationKey = parameters.GetValue<long> ( "LocationKey" );
            var isCreateMode = parameters.GetValue<bool> ( "IsCreate" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<LocationSummaryDto> { Key = _locationKey } );
            requestDispatcher.ProcessRequests ( HandleGetLocationSummaryDtoCompleted, HandleGetLocationSummaryDtoException );
            IsLoading = true;

            if ( isCreateMode )
            {
                _navigationService.Navigate (
                    RegionManager,
                    "WorkspaceContentScopedRegion",
                    "LocationEditorView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "LocationKey", _locationKey.ToString () ),
                            new KeyValuePair<string, string> ( "IsCreate", "true" )
                        } );
            }
            else
            {
                _navigationService.Navigate (
                    RegionManager,
                    "WorkspaceContentScopedRegion",
                    "LocationDashboardView",
                    null,
                    new[] { new KeyValuePair<string, string> ( "LocationKey", _locationKey.ToString () ) } );
            }
        }

        private void ExecuteGoToDashboard ()
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "LocationDashboardView",
                null,
                new[] { new KeyValuePair<string, string> ( "LocationKey", _locationKey.ToString () ) } );
        }

        private void ExecuteGoToProfile ( object obj )
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "LocationEditorView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "LocationKey", _locationKey.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", "false" )
                    } );
        }

        private void HandleGetLocationSummaryDtoCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<LocationSummaryDto>> ();

            LocationSummary = response.DataTransferObject;
            RaisePropertyChanged ( () => LocationPrimaryAddress );
            RaisePropertyChanged ( () => LocationPrimaryContact );
            RaisePropertyChanged ( () => LocationPrimaryPhone );
            RaisePropertyChanged ( () => HeaderContext );
            IsLoading = false;
        }

        private void HandleGetLocationSummaryDtoException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not get the selected dto", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
