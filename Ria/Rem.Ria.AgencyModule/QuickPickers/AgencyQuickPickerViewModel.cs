using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.QuickPickers;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.AgencyModule.QuickPickers
{
    /// <summary>
    /// View Model for Class for picking agency quick.
    /// </summary>
    public class AgencyQuickPickerViewModel : QuickPickerViewModelBase
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private bool _isLoading;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private string _newAgencyName;
        private LookupValueDto _newAgencyType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyQuickPickerViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public AgencyQuickPickerViewModel (
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

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.AddLookupValuesRequest ( "AgencyType" );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleLookupCompleted, HandleLookupException );
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
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value><c>true</c> if this instance is loading; otherwise, <c>false</c>.</value>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { ApplyPropertyChange ( ref _isLoading, () => IsLoading, value ); }
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
        /// Gets or sets the new name of the agency.
        /// </summary>
        /// <value>The new name of the agency.</value>
        public string NewAgencyName
        {
            get { return _newAgencyName; }
            set
            {
                ApplyPropertyChange ( ref _newAgencyName, () => NewAgencyName, value );
                ( ClearCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
                ( AddCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
            }
        }

        /// <summary>
        /// Gets or sets the new type of the agency.
        /// </summary>
        /// <value>The new type of the agency.</value>
        public LookupValueDto NewAgencyType
        {
            get { return _newAgencyType; }
            set
            {
                ApplyPropertyChange ( ref _newAgencyType, () => NewAgencyType, value );
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
                new GetAgencyNamesByKeywordRequest
                    {
                        SearchCriteria = searchCriteria,
                        PageIndex = pageIndex,
                        PageSize = pageSize
                    } );
            requestDispatcher.ProcessRequests ( HandleGetAgencyNamesByKeywordCompleted, HandleGetLocationNamesByKeywordException );
        }

        private bool CanExecuteAddCommand ()
        {
            return !string.IsNullOrEmpty ( NewAgencyName ) && NewAgencyType != null;
        }

        private bool CanExecuteClearCommand ()
        {
            return !string.IsNullOrEmpty ( NewAgencyName ) && NewAgencyType != null;
        }

        private void ExecuteAddCommand ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new CreateNewAgencyRequest { AgencyName = NewAgencyName, AgencyType = NewAgencyType } );
            requestDispatcher.ProcessRequests ( HandleCreateNewAgencyCompleted, HandleCreateNewAgencyException );

            ExecuteClearCommand ();
        }

        private void ExecuteClearCommand ()
        {
            NewAgencyName = null;
            NewAgencyType = null;
        }

        private void HandleCreateNewAgencyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<AgencyDisplayNameDto>> ();
            var result = response.DataTransferObject;

            if ( SearchCommunicater.ItemAddedCommand != null && SearchCommunicater.ItemAddedCommand.CanExecute ( result.Key ) )
            {
                SearchCommunicater.ItemAddedCommand.Execute ( result.Key );
            }
        }

        private void HandleCreateNewAgencyException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Adding new Agency Failed", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAgencyNamesByKeywordCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAgencyNamesByKeywordResponse> ();
            var searchedCriteria = response.SearchedCriteria;
            var result = response.PagedAgencyNameSearchResultDto;

            if ( searchedCriteria == QuickSearchCriteria )
            {
                ResultsReceived ( result.PagedList, result.PageIndex, result.PageSize, result.TotalCount );
            }
        }

        private void HandleGetLocationNamesByKeywordException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        private void HandleLookupCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void HandleLookupException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Getting Agency Types Failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
