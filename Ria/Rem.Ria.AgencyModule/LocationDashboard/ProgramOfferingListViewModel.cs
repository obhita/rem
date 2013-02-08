using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.LocationDashboard;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.LocationDashboard
{
    #region Nested type: ShowOption

    /// <summary>
    /// Show Options
    /// </summary>
    public enum ShowOption
    {
        /// <summary>
        /// Show all option
        /// </summary>
        ShowAll,

        /// <summary>
        /// Show Active option
        /// </summary>
        ShowActive
    }

    #endregion

    /// <summary>
    /// View Model for ProgramOfferingList class.
    /// </summary>
    public class ProgramOfferingListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly Predicate<object> _filter;
        private readonly PagedCollectionViewWrapper<ProgramOfferingDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly DelegateCommand _showActiveOnlyCommand;
        private readonly DelegateCommand _showAllCommand;
        private readonly IUserDialogService _userDialogService;
        private long _locationKey;

        private IList<ProgramOfferingDto> _programOfferingList;

        private ShowOption _showOption;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingListViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public ProgramOfferingListViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IUserDialogService userDialogService,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<ProgramOfferingDto> ();
            _userDialogService = userDialogService;
            _popupService = popupService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            _showAllCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowAllCommand, ExecuteShowAll );
            _showActiveOnlyCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowActiveOnlyCommand, ExecuteShowActiveOnly );
            ToggleActiveIndicatorCommand = commandFactoryHelper.BuildDelegateCommand<ProgramOfferingDto> (
                () => ToggleActiveIndicatorCommand, ExecuteToggleActiveIndicator );
            ShowProgramOfferingAddCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => ShowProgramOfferingAddCommand, ExecuteShowProgramOfferingAddCommand );
            ShowProgramOfferingEditCommand = commandFactoryHelper.BuildDelegateCommand<ProgramOfferingDto> (
                () => ShowProgramOfferingEditCommand, ExecuteShowProgramOfferingEditCommand );
            DeleteProgramOfferingCommand = commandFactoryHelper.BuildDelegateCommand<ProgramOfferingDto> (
                () => DeleteProgramOfferingCommand, ExecuteDeleteProgramOfferingCommand );

            _showOption = ShowOption.ShowActive;
            _filter = FilterByActiveStatus;
            InitializeGroupingDescriptions ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the delete program offering command.
        /// </summary>
        public ICommand DeleteProgramOfferingCommand { get; private set; }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<ProgramOfferingDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets the show active only command.
        /// </summary>
        public ICommand ShowActiveOnlyCommand
        {
            get { return _showActiveOnlyCommand; }
        }

        /// <summary>
        /// Gets the show all command.
        /// </summary>
        public ICommand ShowAllCommand
        {
            get { return _showAllCommand; }
        }

        /// <summary>
        /// Gets the show program offering add command.
        /// </summary>
        public ICommand ShowProgramOfferingAddCommand { get; private set; }

        /// <summary>
        /// Gets the show program offering edit command.
        /// </summary>
        public ICommand ShowProgramOfferingEditCommand { get; private set; }

        /// <summary>
        /// Gets the toggle active indicator command.
        /// </summary>
        public ICommand ToggleActiveIndicatorCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "LocationKey" );
            return key == 0 || key == _locationKey;
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
            GetProgramOfferingsByLocationAsync ( _locationKey );
        }

        private void ExecuteDeleteProgramOfferingCommand ( ProgramOfferingDto programOfferingDto )
        {
            var result = _userDialogService.ShowDialog (
                "Are you sure you want to delete?", "Confirmation", UserDialogServiceOptions.OkCancel );

            if ( result == UserDialogServiceResult.Ok )
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( new DeleteProgramOfferingRequest { ProgramOfferingKey = programOfferingDto.Key, LocationKey = _locationKey } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleDeleteProgramOfferingCompleted, HandleDeleteProgramOfferingException );
            }
        }

        private void ExecuteShowActiveOnly ()
        {
            if ( _showOption == ShowOption.ShowActive )
            {
                return;
            }
            _showOption = ShowOption.ShowActive;

            PagedCollectionViewWrapper.SetFilter ( _filter );
        }

        private void ExecuteShowAll ()
        {
            if ( _showOption == ShowOption.ShowAll )
            {
                return;
            }
            _showOption = ShowOption.ShowAll;

            PagedCollectionViewWrapper.SetFilter ( _filter );
        }

        private void ExecuteShowProgramOfferingAddCommand ( object dataContext )
        {
            _popupService.ShowPopup (
                "ProgramOfferingEditorView",
                "Create",
                "Edit Program Offering",
                new[] { new KeyValuePair<string, string> ( "LocationKey", _locationKey.ToString () ) },
                true,
                ProgramOfferingsClosed );
        }

        private void ExecuteShowProgramOfferingEditCommand ( ProgramOfferingDto programOfferingDto )
        {
            _popupService.ShowPopup (
                "ProgramOfferingEditorView",
                "Edit",
                "Edit Program Offering",
                new[]
                    {
                        new KeyValuePair<string, string> ( "LocationKey", _locationKey.ToString () ),
                        new KeyValuePair<string, string> ( "ProgramOfferingKey", programOfferingDto.Key.ToString () ),
                    },
                true,
                ProgramOfferingsClosed );
        }

        private void ExecuteToggleActiveIndicator ( ProgramOfferingDto programOfferingDto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SaveDtoRequest<ProgramOfferingDto> { DataTransferObject = programOfferingDto } );
            requestDispatcher.ProcessRequests ( HandleSaveProgramOfferingCompleted, HandleSaveProgramException );
        }

        private bool FilterByActiveStatus ( object obj )
        {
            var returnValue = true;
            var programOfferingDto = obj as ProgramOfferingDto;

            if ( programOfferingDto != null )
            {
                if ( _showOption == ShowOption.ShowActive )
                {
                    returnValue = programOfferingDto.Profile.Active;
                }
            }

            return returnValue;
        }

        private void GetProgramOfferingsByLocationAsync ( long locationKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetProgramOfferingsByLocationRequest { LocationKey = locationKey } );
            requestDispatcher.ProcessRequests ( HandleGetProgramOfferingsByLocationCompleted, HandleGetProgramsByLocationException );
            IsLoading = true;
        }

        private void HandleDeleteProgramOfferingCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<DeleteProgramOfferingResponse> ();
            var dto = response.DataTransferObject;

            IsLoading = false;

            if ( dto.HasErrors )
            {
                var message = string.Empty;
                foreach ( var dataErrorInfo in dto.DataErrorInfoCollection )
                {
                    message += dataErrorInfo.ErrorLevel + ": " + dataErrorInfo.Message + Environment.NewLine;
                }

                _userDialogService.ShowDialog ( message, "Could not delete", UserDialogServiceOptions.Ok );
            }
            else
            {
                _programOfferingList = new List<ProgramOfferingDto> ( response.ProgramOfferingDtos );
                PagedCollectionViewWrapper.WrapInPagedCollectionView ( _programOfferingList, _filter );
            }
        }

        private void HandleDeleteProgramOfferingException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not delete Program Offering.", UserDialogServiceOptions.Ok );
        }

        private void HandleGetProgramOfferingsByLocationCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<GetProgramOfferingsByLocationResponse> ();
            _programOfferingList = new ObservableCollection<ProgramOfferingDto> ( response.ProgramOfferingDtos );
            _pagedCollectionViewWrapper.WrapInPagedCollectionView ( _programOfferingList, _filter );
        }

        private void HandleGetProgramsByLocationException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get Program Offering list failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleSaveProgramException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save Program Offering.", UserDialogServiceOptions.Ok );
        }

        private void HandleSaveProgramOfferingCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            GetProgramOfferingsByLocationAsync ( _locationKey );
        }

        private void InitializeGroupingDescriptions ()
        {
            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription ( "Profile.Program.ProgramCategory", "Program Category" ) );

            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription ( "Profile.Program.GenderSpecification", "Gender Specification" ) );

            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription ( "Profile.Program.AgeGroup", "Age Group" ) );

            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription ( "Profile.Program.TreatmentApproach", "Approach" ) );
        }

        private void ProgramOfferingsClosed ()
        {
            GetProgramOfferingsByLocationAsync ( _locationKey );
        }

        #endregion
    }
}
