using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.StaffWorkspace
{
    /// <summary>
    /// View Model for StaffWorkspace class.
    /// </summary>
    public class StaffWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IUserDialogService _userDialogService;
        private StaffSummaryDto _staffDto;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public StaffWorkspaceViewModel (
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

            _eventAggregator.GetEvent<StaffChangedEvent> ().Subscribe (
                StaffChangedEventHandler,
                ThreadOption.PublisherThread,
                false,
                FilterStaffChangedEvents );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string EmailAddress
        {
            get
            {
                if ( StaffDto != null )
                {
                    return StaffDto.EmailAddress;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get
            {
                if ( StaffDto != null )
                {
                    return StaffDto.LastName + ", " + StaffDto.FirstName + " " + StaffDto.ProfessionalCredentialNote;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the staff dto.
        /// </summary>
        /// <value>The staff dto.</value>
        public StaffSummaryDto StaffDto
        {
            get { return _staffDto; }
            set
            {
                _staffDto = value;
                RaisePropertyChanged ( () => StaffDto );
            }
        }

        /// <summary>
        /// Gets the staff primary address.
        /// </summary>
        public StaffAddressSummaryDto StaffPrimaryAddress
        {
            get
            {
                if ( StaffDto != null )
                {
                    return StaffDto.Addresses.FirstOrDefault ( a => a.StaffAddressType.WellKnownName != "H" );
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the staff primary phone.
        /// </summary>
        public StaffPhoneSummaryDto StaffPrimaryPhone
        {
            get
            {
                if ( StaffDto != null )
                {
                    return StaffDto.PhoneNumbers.FirstOrDefault ();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Staff; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the staff changed events.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.StaffChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterStaffChangedEvents ( StaffChangedEventArgs obj )
        {
            return _staffDto != null && _staffDto.Key == obj.StaffKey;
        }

        /// <summary>
        /// Staffs the changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.StaffChangedEventArgs"/> instance containing the event data.</param>
        public void StaffChangedEventHandler ( StaffChangedEventArgs obj )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<StaffSummaryDto> { Key = obj.StaffKey } );
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
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
            var staffKey = parameters.GetValue<long> ( "StaffKey" );
            return StaffDto.Key == staffKey;
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
            var staffKey = parameters.GetValue<long> ( "StaffKey" );
            var isCreateMode = parameters.GetValue<bool> ( "IsCreate" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<StaffSummaryDto> { Key = staffKey } );
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );

            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "StaffEditorView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "StaffKey", staffKey.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", isCreateMode.ToString () )
                    } );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<StaffSummaryDto>> ();
            StaffDto = response.DataTransferObject;

            RaisePropertyChanged ( () => StaffPrimaryAddress );
            RaisePropertyChanged ( () => StaffPrimaryPhone );
            RaisePropertyChanged ( () => EmailAddress );
            RaisePropertyChanged ( () => HeaderContext );
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Staff Workspace initialization failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
