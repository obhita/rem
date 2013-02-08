using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Pillar.Common.Extension;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.QuickPickers;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.QuickPickers
{
    /// <summary>
    /// View Model for Class for picking staff quick.
    /// </summary>
    public class StaffQuickPickerViewModel : QuickPickerViewModelBase
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private string _firstName;
        private bool? _hasErrors;
        private bool _isMIRequired;
        private string _lastName;
        private string _middleInitial;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffQuickPickerViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public StaffQuickPickerViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            ICommandFactory commandFactory )
            : base ( commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _isMIRequired = false;

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
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                ApplyPropertyChange ( ref _firstName, () => FirstName, value );
                ( ClearCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
                ( AddCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
            }
        }

        /// <summary>
        /// Gets or sets the has errors.
        /// </summary>
        /// <value>The has errors.</value>
        public bool? HasErrors
        {
            get { return _hasErrors; }
            set
            {
                _hasErrors = value;
                RaisePropertyChanged ( () => HasErrors );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is MI required.
        /// </summary>
        /// <value><c>true</c> if this instance is MI required; otherwise, <c>false</c>.</value>
        public bool IsMIRequired
        {
            get { return _isMIRequired; }
            set { ApplyPropertyChange ( ref _isMIRequired, () => IsMIRequired, value ); }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                ApplyPropertyChange ( ref _lastName, () => LastName, value );
                ( ClearCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
                ( AddCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
            }
        }

        /// <summary>
        /// Gets or sets the middle initial.
        /// </summary>
        /// <value>The middle initial.</value>
        public string MiddleInitial
        {
            get { return _middleInitial; }
            set
            {
                ApplyPropertyChange ( ref _middleInitial, () => MiddleInitial, value );
                if ( value != null )
                {
                    IsMIRequired = false;
                    ( ClearCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
                    ( AddCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
                }
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
                new GetStaffNamesByKeywordRequest
                    {
                        SearchCriteria = searchCriteria,
                        PageIndex = pageIndex,
                        PageSize = pageSize
                    } );
            requestDispatcher.ProcessRequests ( HandleGetStaffNamesByKeywordCompleted, HandleGetStaffNamesByKeywordException );
        }

        private bool CanExecuteAddCommand ()
        {
            return !string.IsNullOrEmpty ( FirstName ) && !string.IsNullOrEmpty ( LastName ) && !IsMIRequired;
        }

        private bool CanExecuteClearCommand ()
        {
            return !string.IsNullOrEmpty ( FirstName ) && !string.IsNullOrEmpty ( LastName );
        }

        private void ExecuteAddCommand ()
        {
            var agencyKey = CurrentUserContext.Agency.Key;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new CreateNewStaffRequest
                    {
                        AgencyKey = agencyKey,
                        FirstName = FirstName,
                        MiddleInitial = MiddleInitial,
                        LastName = LastName
                    } );
            requestDispatcher.ProcessRequests ( HandleCreateNewStaffCompleted, HandleCreateNewStaffException );

            ExecuteClearCommand ();
        }

        private void ExecuteClearCommand ()
        {
            FirstName = null;
            LastName = null;
        }

        private void HandleCreateNewStaffCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<CreateNewStaffResponse> ();
            var result = response.StaffNameDto;

            if ( result.HasErrors )
            {
                FirstName = result.FirstName;
                LastName = result.LastName;
                MiddleInitial = result.MiddleInitial;

                var errors = new StringBuilder ();
                IsMIRequired = true;
                result.DataErrorInfoCollection.ForEach ( error => errors.AppendLine ( error.Message ) );
                HasErrors = true;
                _userDialogService.ShowDialog ( errors.ToString (), "Add Staff", UserDialogServiceOptions.Ok );
            }
            else
            {
                HasErrors = false;
                if ( SearchCommunicater.ItemAddedCommand != null && SearchCommunicater.ItemAddedCommand.CanExecute ( result.Key ) )
                {
                    SearchCommunicater.ItemAddedCommand.Execute ( result.Key );
                }
            }
        }

        private void HandleCreateNewStaffException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Adding new Staff Failed", UserDialogServiceOptions.Ok );
        }

        private void HandleGetStaffNamesByKeywordCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetStaffNamesByKeywordResponse> ();
            var searchedCriteria = response.SearchedCriteria;
            var result = response.PagedStaffNameSearchResultDto;

            if ( searchedCriteria == QuickSearchCriteria )
            {
                ResultsReceived ( result.PagedList, result.PageIndex, result.PageSize, result.TotalCount );
            }
        }

        private void HandleGetStaffNamesByKeywordException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
