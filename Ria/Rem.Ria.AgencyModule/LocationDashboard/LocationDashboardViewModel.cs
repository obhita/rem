using System.Collections.Generic;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;

namespace Rem.Ria.AgencyModule.LocationDashboard
{
    /// <summary>
    /// View Model for LocationDashboard class.
    /// </summary>
    public class LocationDashboardViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly INavigationService _navigationService;
        private long _locationKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDashboardViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public LocationDashboardViewModel (
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _navigationService = navigationService;
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
            _locationKey = parameters.GetValue<long> ( "LocationKey" );

            _navigationService.Navigate (
                RegionManager,
                "LocationDashboardProgramsRegion",
                "ProgramOfferingListView",
                null,
                new[] { new KeyValuePair<string, string> ( "LocationKey", _locationKey.ToString () ) } );
        }

        #endregion
    }
}
