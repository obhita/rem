using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.QuickPickers;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.QuickPickers
{
    /// <summary>
    /// View Model for Class for picking location quick.
    /// </summary>
    public class LocationQuickPickerViewModel : QuickPickerViewModelBase
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private string _newLocationName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationQuickPickerViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public LocationQuickPickerViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            ICommandFactory commandFactory )
            : base ( commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ClearCommand = commandFactoryHelper.BuildDelegateCommand ( () => ClearCommand, ExecuteClearCommand, CanExecuteClearCommand );
            AddCommand = commandFactoryHelper.BuildDelegateCommand ( () => AddCommand, ExecuteAddCommand, CanExecuteAddCommand );

            ApplyContextChanges = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the add command.
        /// </summary>
        public ICommand AddCommand { get; private set; }

        /// <summary>
        /// Gets the clear command.
        /// </summary>
        public ICommand ClearCommand { get; private set; }

        /// <summary>
        /// Gets or sets the new name of the location.
        /// </summary>
        /// <value>The new name of the location.</value>
        public string NewLocationName
        {
            get { return _newLocationName; }
            set
            {
                ApplyPropertyChange ( ref _newLocationName, () => NewLocationName, value );
                ( ClearCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
                ( AddCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
            }
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
        /// Does the search.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        protected override void DoSearch ( string searchCriteria, int pageIndex, int pageSize )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new GetLocationNamesByKeywordRequest
                    {
                        SearchCriteria = searchCriteria,
                        PageIndex = pageIndex,
                        PageSize = pageSize
                    } );
            requestDispatcher.ProcessRequests ( HandleGetLocationNamesByKeywordCompleted, HandleGetLocationNamesByKeywordException );
        }

        private bool CanExecuteAddCommand ()
        {
            return !string.IsNullOrEmpty ( NewLocationName );
        }

        private bool CanExecuteClearCommand ()
        {
            return !string.IsNullOrEmpty ( NewLocationName );
        }

        private void ExecuteAddCommand ()
        {
            var agencyKey = CurrentUserContext.Agency.Key;
            var locationName = NewLocationName;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new CreateNewLocationRequest { AgencyKey = agencyKey, LocationName = locationName } );
            requestDispatcher.ProcessRequests ( HandleCreateNewLocationCompleted, HandleCreateNewLocationException );

            ExecuteClearCommand ();
        }

        private void ExecuteClearCommand ()
        {
            NewLocationName = null;
        }

        private void HandleCreateNewLocationCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<CreateNewLocationResponse> ();
            var result = response.LocationDisplayNameDto;

            if ( SearchCommunicater.ItemAddedCommand != null && SearchCommunicater.ItemAddedCommand.CanExecute ( result.Key ) )
            {
                SearchCommunicater.ItemAddedCommand.Execute ( result.Key );
            }
        }

        private void HandleCreateNewLocationException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Adding new location Failed", UserDialogServiceOptions.Ok );
        }

        private void HandleGetLocationNamesByKeywordCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetLocationNamesByKeywordResponse> ();
            var searchedCriteria = response.SearchedCriteria;
            var result = response.PagedLocationNameSearchResultDto;

            if ( searchedCriteria == QuickSearchCriteria )
            {
                ResultsReceived ( result.PagedList, result.PageIndex, result.PageSize, result.TotalCount );
            }
        }

        private void HandleGetLocationNamesByKeywordException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
