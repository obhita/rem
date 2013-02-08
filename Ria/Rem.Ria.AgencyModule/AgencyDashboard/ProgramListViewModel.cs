using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.AgencyDashboard;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.AgencyDashboard
{
    #region Nested type: ShowOption

    /// <summary>
    /// Show Options
    /// </summary>
    public enum ShowOption
    {
        /// <summary>
        /// Show All Option
        /// </summary>
        ShowAll,

        /// <summary>
        /// Show Active Option
        /// </summary>
        ShowActive
    }

    #endregion

    /// <summary>
    /// View Model for ProgramList class.
    /// </summary>
    public class ProgramListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly Predicate<object> _filter;
        private readonly PagedCollectionViewWrapper<ProgramDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly DelegateCommand _showActiveOnlyCommand;
        private readonly DelegateCommand _showAllCommand;
        private readonly IUserDialogService _userDialogService;
        private long _agencyKey;

        private IList<ProgramDto> _programList;

        private ShowOption _showOption;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramListViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public ProgramListViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IUserDialogService userDialogService,
            IPopupService popupService,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<ProgramDto> ();
            _userDialogService = userDialogService;
            _popupService = popupService;
            _eventAggregator = eventAggregator;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            _showAllCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowAllCommand, ExecuteShowAll );
            _showActiveOnlyCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowActiveOnlyCommand, ExecuteShowActiveOnly );
            ToggleActiveIndicatorCommand = commandFactoryHelper.BuildDelegateCommand<ProgramDto> (
                () => ToggleActiveIndicatorCommand, ExecuteToggleActiveIndicator );
            ShowProgramAddCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => ShowProgramAddCommand, ExecuteShowProgramAddCommand );
            ShowProgramEditCommand = commandFactoryHelper.BuildDelegateCommand<ProgramDto> (
                () => ShowProgramEditCommand, ExecuteShowProgramEditCommand );
            DeleteProgramCommand = commandFactoryHelper.BuildDelegateCommand<ProgramDto> ( () => DeleteProgramCommand, ExecuteDeleteProgramCommand );

            _showOption = ShowOption.ShowActive;
            _filter = FilterByActiveStatus;
            InitializeGroupingDescriptions ();

            _eventAggregator.GetEvent<ProgramChangedEvent> ().Subscribe (
                ProgramChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterProgramChangedEvents );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the delete program command.
        /// </summary>
        public ICommand DeleteProgramCommand { get; private set; }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<ProgramDto> PagedCollectionViewWrapper
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
        /// Gets the show program add command.
        /// </summary>
        public ICommand ShowProgramAddCommand { get; private set; }

        /// <summary>
        /// Gets the show program edit command.
        /// </summary>
        public ICommand ShowProgramEditCommand { get; private set; }

        /// <summary>
        /// Gets the toggle active indicator command.
        /// </summary>
        public ICommand ToggleActiveIndicatorCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the program changed events.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.ProgramChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterProgramChangedEvents ( ProgramChangedEventArgs obj )
        {
            return obj.AgencyKey == _agencyKey;
        }

        /// <summary>
        /// Programs the changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.AgencyModule.ProgramChangedEventArgs"/> instance containing the event data.</param>
        public void ProgramChangedEventHandler ( ProgramChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded ( () => GetAllProgramsByAgencyAsync ( _agencyKey ) );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "AgencyKey" );
            return key == 0 || key == _agencyKey;
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
            _agencyKey = parameters.GetValue<long> ( "AgencyKey" );
            GetAllProgramsByAgencyAsync ( _agencyKey );
        }

        private void ExecuteDeleteProgramCommand ( ProgramDto programDto )
        {
            var result = _userDialogService.ShowDialog (
                "Are you sure you want to delete?", "Confirmation", UserDialogServiceOptions.OkCancel );

            if ( result == UserDialogServiceResult.Ok )
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( new DeleteProgramRequest { ProgramKey = programDto.Key, AgencyKey = _agencyKey } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleDeleteProgramCompleted, HandleDeleteProgramException );
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

        private void ExecuteShowProgramAddCommand ( object dataContext )
        {
            _popupService.ShowPopup (
                "EditProgramView",
                "Create",
                "Edit Program",
                new[] { new KeyValuePair<string, string> ( "AgencyKey", _agencyKey.ToString () ) },
                true );
        }

        private void ExecuteShowProgramEditCommand ( ProgramDto programDto )
        {
            _popupService.ShowPopup (
                "EditProgramView",
                "Edit",
                "Edit Program",
                new[] { new KeyValuePair<string, string> ( "ProgramKey", programDto.Key.ToString () ) },
                true );
        }

        private void ExecuteToggleActiveIndicator ( ProgramDto programDto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SaveDtoRequest<ProgramDto> { DataTransferObject = programDto } );
            requestDispatcher.ProcessRequests ( HandleSaveProgramCompleted, HandleSaveProgramException );
        }

        private bool FilterByActiveStatus ( object obj )
        {
            var returnValue = true;
            var programDto = obj as ProgramDto;

            if ( programDto != null )
            {
                if ( _showOption == ShowOption.ShowActive )
                {
                    returnValue = programDto.Active;
                }
            }

            return returnValue;
        }

        private void GetAllProgramsByAgencyAsync ( long agencyKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetAllProgramsByAgencyRequest { AgencyKey = agencyKey } );
            requestDispatcher.ProcessRequests ( HandleGetAllProgramsByAgencyCompleted, HandleGetAllProgramsByAgencyException );
            IsLoading = true;
        }

        private void HandleDeleteProgramCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<DeleteProgramResponse> ();
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
                _programList = new List<ProgramDto> ( response.ProgramDtos );
                PagedCollectionViewWrapper.WrapInPagedCollectionView ( _programList, _filter );
            }
        }

        private void HandleDeleteProgramException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not delete program.", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAllProgramsByAgencyCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<GetAllProgramsByAgencyResponse> ();
            _programList = new ObservableCollection<ProgramDto> ( response.ProgramDtos );
            _pagedCollectionViewWrapper.WrapInPagedCollectionView ( _programList, _filter );
        }

        private void HandleGetAllProgramsByAgencyException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get Programs list failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleSaveProgramCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            GetAllProgramsByAgencyAsync ( _agencyKey );
        }

        private void HandleSaveProgramException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save program.", UserDialogServiceOptions.Ok );
        }

        private void InitializeGroupingDescriptions ()
        {
            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<ProgramDto, object> ( p => p.ProgramCategory ), "Program Category" ) );

            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<ProgramDto, object> ( p => p.GenderSpecification ), "Gender Specification" ) );

            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<ProgramDto, object> ( p => p.AgeGroup ), "Age Group" ) );

            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<ProgramDto, object> ( p => p.TreatmentApproach ), "Approach" ) );
        }

        #endregion
    }
}
