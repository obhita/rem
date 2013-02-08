using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.Login
{
    /// <summary>
    /// View Model for LoginInformation class.
    /// </summary>
    public class LoginInformationViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly ICurrentUserContextService _currentUserContextService;
        private readonly IUserDialogService _userDialogService;
        private string _agencyName;
        private bool _initialized;
        private long _locationKey;
        private string _userName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInformationViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="currentUserContextService">The current user context service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public LoginInformationViewModel (
            IAccessControlManager accessControlManager,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            ICurrentUserContextService currentUserContextService,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _currentUserContextService = currentUserContextService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SelectedLocationChangedCommand = commandFactoryHelper.BuildDelegateCommand<LocationDisplayNameDto> (
                () => SelectedLocationChangedCommand, ExecuteSelectedLocationChangedCommand );

            eventAggregator.GetEvent<StaffChangedEvent> ().Subscribe ( OnStaffChanged, ThreadOption.PublisherThread, false, FilterStaffChanged );

            ApplyContextChanges = true;
            ContextChanged += OnContextChanged;
            UpdateProperties ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the agency.
        /// </summary>
        public string AgencyName
        {
            get { return _agencyName; }
            private set { ApplyPropertyChange ( ref _agencyName, () => AgencyName, value ); }
        }

        /// <summary>
        /// Gets or sets the location key.
        /// </summary>
        /// <value>The location key.</value>
        public long LocationKey
        {
            get { return _locationKey; }
            set { ApplyPropertyChange ( ref _locationKey, () => LocationKey, value ); }
        }

        /// <summary>
        /// Gets the selected location changed command.
        /// </summary>
        public ICommand SelectedLocationChangedCommand { get; private set; }

        /// <summary>
        /// Gets the staff approved locations.
        /// </summary>
        public IEnumerable<LocationDisplayNameDto> StaffApprovedLocations { get; private set; }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            private set { ApplyPropertyChange ( ref _userName, () => UserName, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the staff changed.
        /// </summary>
        /// <param name="staffChangedEventArgs">The <see cref="Rem.Ria.AgencyModule.StaffChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterStaffChanged ( StaffChangedEventArgs staffChangedEventArgs )
        {
            return CurrentUserContext.Staff.Key == staffChangedEventArgs.StaffKey;
        }

        /// <summary>
        /// Raises the <see cref="E:StaffChanged"/> event.
        /// </summary>
        /// <param name="staffChangedEventArgs">The <see cref="Rem.Ria.AgencyModule.StaffChangedEventArgs"/> instance containing the event data.</param>
        public void OnStaffChanged ( StaffChangedEventArgs staffChangedEventArgs )
        {
            UpdateLocationAssignments ();
        }

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
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
        }

        private void ExecuteSelectedLocationChangedCommand ( LocationDisplayNameDto locationDisplayNameDto )
        {
            _currentUserContextService.ChangeContext ( new LocationContext ( locationDisplayNameDto.Key, locationDisplayNameDto.DisplayName ) );
        }

        private void HandleGetLocationsCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<StaffLocationAssignmentDto>> ();

            StaffApprovedLocations =
                response.DataTransferObject.Locations.Select ( l => new LocationDisplayNameDto { Key = l.Key, DisplayName = l.Location.DisplayName } );
            RaisePropertyChanged ( () => StaffApprovedLocations );

            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Staff editor initialization failed", UserDialogServiceOptions.Ok );
        }

        private void OnContextChanged ( object sender, EventArgs e )
        {
            UpdateProperties ();
        }

        private void UpdateLocationAssignments ()
        {
            IsLoading = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<StaffLocationAssignmentDto> { Key = CurrentUserContext.Staff.Key } );
            requestDispatcher.ProcessRequests ( HandleGetLocationsCompleted, HandleInitializationException );
        }

        private void UpdateProperties ()
        {
            if ( CurrentUserContext != null )
            {
                AgencyName = CurrentUserContext.Agency.DisplayName;
                LocationKey = CurrentUserContext.Location.Key;
                UserName = CurrentUserContext.Staff.FullName;

                if ( !_initialized )
                {
                    _initialized = true;
                    UpdateLocationAssignments ();
                }
            }
        }

        #endregion
    }
}
