using System.Collections.Generic;
using System.Windows.Input;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;

namespace Rem.Ria.AgencyModule
{
    /// <summary>
    /// View Model for MainAgencyQuickPickers class.
    /// </summary>
    public class MainAgencyQuickPickersViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly INavigationService _navigationService;
        private ISearchResultDto _agencyItem;
        private ISearchResultDto _locationItem;
        private ISearchResultDto _staffItem;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainAgencyQuickPickersViewModel"/> class.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainAgencyQuickPickersViewModel (
            ICommandFactory commandFactory,
            IAccessControlManager accessControlManager,
            INavigationService navigationService )
            : base ( accessControlManager, commandFactory )
        {
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            GoToAgencyCommand = commandFactoryHelper.BuildDelegateCommand<ISearchResultDto> (
                () => GoToAgencyCommand, ExecuteGoToAgencyCommand, CanExecuteGoToAgencyCommand );
            AddAgencyCommand = commandFactoryHelper.BuildDelegateCommand<long?> (
                () => AddAgencyCommand, ExecuteAddAgencyCommand, CanExecuteAddAgencyCommand );
            GoToLocationCommand = commandFactoryHelper.BuildDelegateCommand<ISearchResultDto> (
                () => GoToLocationCommand, ExecuteGoToLocationCommand, CanExecuteGoToLocationCommand );
            AddLocationCommand = commandFactoryHelper.BuildDelegateCommand<long?> (
                () => AddLocationCommand, ExecuteAddLocationCommand, CanExecuteAddLocationCommand );
            GoToStaffCommand = commandFactoryHelper.BuildDelegateCommand<ISearchResultDto> (
                () => GoToStaffCommand, ExecuteGoToStaffCommand, CanExecuteGoToStaffCommand );
            AddStaffCommand = commandFactoryHelper.BuildDelegateCommand<long?> (
                () => AddStaffCommand, ExecuteAddStaffCommand, CanExecuteAddStaffCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the add agency command.
        /// </summary>
        public ICommand AddAgencyCommand { get; private set; }

        /// <summary>
        /// Gets the add location command.
        /// </summary>
        public ICommand AddLocationCommand { get; private set; }

        /// <summary>
        /// Gets the add staff command.
        /// </summary>
        public ICommand AddStaffCommand { get; private set; }

        /// <summary>
        /// Gets or sets the agency item.
        /// </summary>
        /// <value>The agency item.</value>
        public ISearchResultDto AgencyItem
        {
            get { return _agencyItem; }
            set { ApplyPropertyChange ( ref _agencyItem, () => AgencyItem, value ); }
        }

        /// <summary>
        /// Gets the go to agency command.
        /// </summary>
        public ICommand GoToAgencyCommand { get; private set; }

        /// <summary>
        /// Gets the go to location command.
        /// </summary>
        public ICommand GoToLocationCommand { get; private set; }

        /// <summary>
        /// Gets the go to staff command.
        /// </summary>
        public ICommand GoToStaffCommand { get; private set; }

        /// <summary>
        /// Gets or sets the location item.
        /// </summary>
        /// <value>The location item.</value>
        public ISearchResultDto LocationItem
        {
            get { return _locationItem; }
            set { ApplyPropertyChange ( ref _locationItem, () => LocationItem, value ); }
        }

        /// <summary>
        /// Gets or sets the staff item.
        /// </summary>
        /// <value>The staff item.</value>
        public ISearchResultDto StaffItem
        {
            get { return _staffItem; }
            set { ApplyPropertyChange ( ref _staffItem, () => StaffItem, value ); }
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

        private static bool CanExecuteAddAgencyCommand ( long? agencyId )
        {
            return agencyId.HasValue;
        }

        private static bool CanExecuteAddLocationCommand ( long? locationId )
        {
            return locationId.HasValue;
        }

        private static bool CanExecuteAddStaffCommand ( long? staffId )
        {
            return staffId.HasValue;
        }

        private static bool CanExecuteGoToAgencyCommand ( ISearchResultDto item )
        {
            return item != null;
        }

        private static bool CanExecuteGoToLocationCommand ( ISearchResultDto item )
        {
            return item != null;
        }

        private static bool CanExecuteGoToStaffCommand ( ISearchResultDto item )
        {
            return item != null;
        }

        private void ExecuteAddAgencyCommand ( long? agencyId )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "AgencyWorkspaceView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "AgencyKey", agencyId.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", true.ToString () )
                    } );
        }

        private void ExecuteAddLocationCommand ( long? locationId )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "LocationWorkspaceView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "LocationKey", locationId.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", true.ToString () )
                    } );
        }

        private void ExecuteAddStaffCommand ( long? staffId )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "StaffWorkspaceView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "StaffKey", staffId.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", true.ToString () )
                    } );
        }

        private void ExecuteGoToAgencyCommand ( ISearchResultDto item )
        {
            AgencyItem = item;
            _navigationService.Navigate (
                "WorkspacesRegion",
                "AgencyWorkspaceView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "AgencyKey", item.Key.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", false.ToString () )
                    } );
            AgencyItem = null;
        }

        private void ExecuteGoToLocationCommand ( ISearchResultDto item )
        {
            LocationItem = item;
            _navigationService.Navigate (
                "WorkspacesRegion",
                "LocationWorkspaceView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "LocationKey", item.Key.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", false.ToString () )
                    } );
            LocationItem = null;
        }

        private void ExecuteGoToStaffCommand ( ISearchResultDto item )
        {
            StaffItem = item;
            _navigationService.Navigate (
                "WorkspacesRegion",
                "StaffWorkspaceView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "StaffKey", item.Key.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", false.ToString () )
                    } );
            StaffItem = null;
        }

        #endregion
    }
}
