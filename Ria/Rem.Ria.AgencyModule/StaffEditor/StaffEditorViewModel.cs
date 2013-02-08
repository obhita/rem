using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Collections;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.StaffEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.AgencyModule.StaffEditor
{
    /// <summary>
    /// View Model for Class for editing staff.
    /// </summary>
    public class StaffEditorViewModel : PanelEditorViewModel<StaffDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IUserDialogService _userDialogService;
        private bool _isCreateMode;
        private bool _isEditMode;
        private bool _isStaffAccountAccessLoading;

        private IList<SystemRoleDto> _jobFunctionSystemRoleLookupList;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private long _staffKey;
        private PagedCollectionView _taskGroupSystemRoleLookupList;
        private PagedCollectionView _taskSystemRoleLookupList;

        private ObservableCollection<StaffSystemRoleDto> _totalSelectedStaffSystemRoleList = new ObservableCollection<StaffSystemRoleDto> ();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffEditorViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public StaffEditorViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;

            _eventAggregator.GetEvent<StaffPhotoUploadedEvent> ().Subscribe (
                StaffPhotoUploadedEventHandler,
                ThreadOption.UIThread,
                false,
                null );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            AddPhotoCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => AddPhotoCommand, ExecuteAddPhotoCommand, CanExecuteAddPhotoCommand );
            DeletePhotoCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => DeletePhotoCommand, ExecuteDeletePhotoCommand, CanExecuteDeletePhotoCommand );
            SetPrimaryLocationCommand = commandFactoryHelper.BuildDelegateCommand<LocationDisplayNameDto> (
                () => SetPrimaryLocationCommand, ExecuteSetPrimaryLocationCommand );
            SelectedLocationChangedCommand = commandFactoryHelper.BuildDelegateCommand<LocationDisplayNameDto> (
                () => SelectedLocationChangedCommand, ExecuteSelectedLocationChangedCommand );
            SelectedJobFunctionChangedCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => SelectedJobFunctionChangedCommand, ExecuteSelectedJobFunctionChangedCommand );
            AddToAccountCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => AddToAccountCommand, ExecuteAddToAccountCommand );
            DeleteSystemRoleCommand = commandFactoryHelper.BuildDelegateCommand<StaffSystemRoleDto> (
                () => DeleteSystemRoleCommand, ExecuteDeleteSystemRoleCommand );

            AddSystemAccountCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => AddSystemAccountCommand, ExecuteAddSystemAccountCommand, CanExecuteAddSystemAccountCommand );

            LinkSystemAccountCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => LinkSystemAccountCommand, ExecuteLinkSystemAccountCommand, CanExecuteLinkSystemAccountCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the add photo command.
        /// </summary>
        /// <value>The add photo command.</value>
        public ICommand AddPhotoCommand { get; set; }

        /// <summary>
        /// Gets or sets the add system account command.
        /// </summary>
        /// <value>The add system account command.</value>
        public ICommand AddSystemAccountCommand { get; set; }

        /// <summary>
        /// Gets or sets the add to account command.
        /// </summary>
        /// <value>The add to account command.</value>
        public ICommand AddToAccountCommand { get; set; }

        /// <summary>
        /// Gets or sets the delete photo command.
        /// </summary>
        /// <value>The delete photo command.</value>
        public ICommand DeletePhotoCommand { get; set; }

        /// <summary>
        /// Gets or sets the delete system role command.
        /// </summary>
        /// <value>The delete system role command.</value>
        public ICommand DeleteSystemRoleCommand { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is create mode.
        /// </summary>
        public bool IsCreateMode
        {
            get { return _isCreateMode; }
            private set { ApplyPropertyChange ( ref _isCreateMode, () => IsCreateMode, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is edit mode.
        /// </summary>
        /// <value><c>true</c> if this instance is edit mode; otherwise, <c>false</c>.</value>
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { ApplyPropertyChange ( ref _isEditMode, () => IsEditMode, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is staff account access loading.
        /// </summary>
        /// <value><c>true</c> if this instance is staff account access loading; otherwise, <c>false</c>.</value>
        public bool IsStaffAccountAccessLoading
        {
            get { return _isStaffAccountAccessLoading; }
            set { ApplyPropertyChange ( ref _isStaffAccountAccessLoading, () => IsStaffAccountAccessLoading, value ); }
        }

        /// <summary>
        /// Gets the job function system role lookup list.
        /// </summary>
        public IList<SystemRoleDto> JobFunctionSystemRoleLookupList
        {
            get { return _jobFunctionSystemRoleLookupList; }
            private set { ApplyPropertyChange ( ref _jobFunctionSystemRoleLookupList, () => JobFunctionSystemRoleLookupList, value ); }
        }

        /// <summary>
        /// Gets or sets the link system account command.
        /// </summary>
        /// <value>The link system account command.</value>
        public ICommand LinkSystemAccountCommand { get; set; }

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets or sets the selected job function changed command.
        /// </summary>
        /// <value>The selected job function changed command.</value>
        public ICommand SelectedJobFunctionChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the selected location changed command.
        /// </summary>
        /// <value>The selected location changed command.</value>
        public ICommand SelectedLocationChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the selected location changed command command.
        /// </summary>
        /// <value>The selected location changed command command.</value>
        public ICommand SelectedLocationChangedCommandCommand { get; set; }

        /// <summary>
        /// Gets or sets the set primary location command.
        /// </summary>
        /// <value>The set primary location command.</value>
        public ICommand SetPrimaryLocationCommand { get; set; }

        /// <summary>
        /// Gets the task group system role lookup list.
        /// </summary>
        public PagedCollectionView TaskGroupSystemRoleLookupList
        {
            get { return _taskGroupSystemRoleLookupList; }
            private set { ApplyPropertyChange ( ref _taskGroupSystemRoleLookupList, () => TaskGroupSystemRoleLookupList, value ); }
        }

        /// <summary>
        /// Gets the task system role lookup list.
        /// </summary>
        public PagedCollectionView TaskSystemRoleLookupList
        {
            get { return _taskSystemRoleLookupList; }
            private set { ApplyPropertyChange ( ref _taskSystemRoleLookupList, () => TaskSystemRoleLookupList, value ); }
        }

        /// <summary>
        /// Gets the total selected staff system role list.
        /// </summary>
        public ObservableCollection<StaffSystemRoleDto> TotalSelectedStaffSystemRoleList
        {
            get { return _totalSelectedStaffSystemRoleList; }
            private set { ApplyPropertyChange ( ref _totalSelectedStaffSystemRoleList, () => TotalSelectedStaffSystemRoleList, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Staffs the photo uploaded event handler.
        /// </summary>
        /// <param name="staffPhotoUploadedEventArgs">The <see cref="Rem.Ria.AgencyModule.StaffEditor.StaffPhotoUploadedEventArgs"/> instance containing the event data.</param>
        public void StaffPhotoUploadedEventHandler (
            StaffPhotoUploadedEventArgs staffPhotoUploadedEventArgs )
        {
            if ( EditingDto.StaffPhoto == null )
            {
                EditingDto.StaffPhoto = new StaffPhotoDto ();
            }
            EditingDto.StaffPhoto.Picture = staffPhotoUploadedEventArgs.Picture;
            EditingDto.StaffPhoto.Key = _staffKey;
            IsEditMode = false;
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
            var staffKey = parameters.GetValue<long> ( "StaffKey" );
            return _staffKey == staffKey;
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
            _staffKey = parameters.GetValue<long> ( "StaffKey" );
            IsCreateMode = parameters.GetValue<bool> ( "IsCreate" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<StaffDto> { Key = _staffKey } );
            requestDispatcher.Add ( new GetDtoRequest<JobFunctionSystemRolesDto> () );
            requestDispatcher.Add ( new GetDtoRequest<TaskSystemRolesDto> () );
            requestDispatcher.Add ( new GetDtoRequest<TaskGroupSystemRolesDto> () );
            requestDispatcher
                .AddLookupValuesRequest ( "Suffix" )
                .AddLookupValuesRequest ( "Prefix" )
                .AddLookupValuesRequest ( "Gender" )
                .AddLookupValuesRequest ( "Language" )
                .AddLookupValuesRequest ( "LanguageFluency" )
                .AddLookupValuesRequest ( "StaffType" )
                .AddLookupValuesRequest ( "StaffAddressType" )
                .AddLookupValuesRequest ( "StaffIdentifierType" )
                .AddLookupValuesRequest ( "StateProvince" )
                .AddLookupValuesRequest ( "StaffPhoneType" )
                .AddLookupValuesRequest ( "CollegeDegree" )
                .AddLookupValuesRequest ( "License" )
                .AddLookupValuesRequest ( "Certification" )
                .AddLookupValuesRequest ( "TrainingCourse" )
                .AddLookupValuesRequest ( "StaffChecklistItemType" )
                .AddLookupValuesRequest ( "StaffEventType" )
                .AddLookupValuesRequest ( "EmploymentType" );

            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );
            RaiseStaffChangedEvent ();
        }

        private static bool CanExecuteAddPhotoCommand ( object arg )
        {
            return true;
        }

        private bool CanExecuteAddSystemAccountCommand ( object arg )
        {
            return true;
        }

        private bool CanExecuteDeletePhotoCommand ( object arg )
        {
            return true;
        }

        private bool CanExecuteLinkSystemAccountCommand ( object arg )
        {
            return true;
        }

        private void DetermineSystemAccountPanelStatus ()
        {
            if ( EditingDto.SystemAccount != null )
            {
                return;
            }

            //Populate the Username field as well as the email address from the staff 
            var displayNameFormat = "{0} {1}";
            var usernameFormat = "{0}.{1}";

            var username = string.Format (
                usernameFormat,
                EditingDto.StaffProfile.FirstName.ToLower ( CultureInfo.InvariantCulture ).Trim (),
                EditingDto.StaffProfile.LastName.ToLower ( CultureInfo.InvariantCulture ).Trim () );

            var displayname = string.Format ( displayNameFormat, EditingDto.StaffProfile.FirstName, EditingDto.StaffProfile.LastName );

            EditingDto.SystemAccount = new SystemAccountDto
                {
                    DisplayName = displayname,
                    Username = username,
                    EmailAddress = EditingDto.StaffProfile.EmailAddress,
                };

            EditingDto.IsNewSystemAccountMode = true;
        }

        private void ExecuteAddPhotoCommand ( object obj )
        {
            IsEditMode = true;
            _navigationService.Navigate (
                "ModalPopupRegion",
                "UploadPhotoView",
                null,
                new[] { new KeyValuePair<string, string> ( "StaffKey", _staffKey.ToString () ) } );
        }

        private void ExecuteAddSystemAccountCommand ( object obj )
        {
            EditingDto.SystemAccount.ClearAllDataErrorInfo ();

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new CreateSystemAccountRequest { SystemAccount = EditingDto.SystemAccount, StaffKey = EditingDto.Key } );
            requestDispatcher.ProcessRequests ( HandleCreateSystemAccountRequestCompleted, HandleCreateSystemAccountRequestException );
        }

        private void ExecuteAddToAccountCommand ( object obj )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

            //only handle adding, not deleting, which will be handled in the delete command
            var taskSytemRoleKeysToBeAdded = ( from systemRoleDto in _taskSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> ()
                                                      where systemRoleDto.IsSelectable && systemRoleDto.IsSelected
                                                      select systemRoleDto.Key ).ToList ();
            if ( taskSytemRoleKeysToBeAdded.Count > 0 )
            {
                requestDispatcher.Add ( new AddStaffTaskRolesRequest ( EditingDto.Key, taskSytemRoleKeysToBeAdded ) );
                requestDispatcher.ProcessRequests ( HandleAddStaffTaskRolesCompleted, HandleAddStaffTaskRolesException );
            }

            var taskGroupSytemRoleKeysToBeAdded =
                ( from systemRoleDto in _taskGroupSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> ()
                  where systemRoleDto.IsSelectable && systemRoleDto.IsSelected
                  select systemRoleDto.Key ).ToList ();
            if ( taskGroupSytemRoleKeysToBeAdded.Count > 0 )
            {
                requestDispatcher.Add ( new AddStaffTaskRolesRequest ( EditingDto.Key, taskGroupSytemRoleKeysToBeAdded ) );
                requestDispatcher.ProcessRequests ( HandleAddStaffTaskGroupRolesCompleted, HandleAddStaffTaskGroupRolesException );
            }
            IsStaffAccountAccessLoading = true;
        }

        private void ExecuteDeletePhotoCommand ( object obj )
        {
            EditingDto.StaffPhoto.Picture = null;
            EditingDto.StaffPhoto.Key = _staffKey;
        }

        private void ExecuteDeleteSystemRoleCommand ( StaffSystemRoleDto obj )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            if ( obj.SystemRole.SystemRoleType == SystemRoleType.Task )
            {
                requestDispatcher.Add ( new RemoveStaffTaskRoleRequest ( EditingDto.Key, obj.SystemRole.Key ) );
                requestDispatcher.ProcessRequests ( HandleRemoveStaffTaskRoleCompleted, HandleRemoveStaffSystemRoleException );
            }
            else
            {
                requestDispatcher.Add ( new RemoveStaffTaskGroupRoleRequest ( EditingDto.Key, obj.SystemRole.Key ) );
                requestDispatcher.ProcessRequests ( HandleRemoveStaffTaskGroupRoleCompleted, HandleRemoveStaffSystemRoleException );
            }
            IsStaffAccountAccessLoading = true;
        }

        private void ExecuteLinkSystemAccountCommand ( object obj )
        {
            EditingDto.SystemAccount.ClearAllDataErrorInfo ();

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new LinkSystemAccountRequest { SystemAccount = EditingDto.SystemAccount, StaffKey = EditingDto.Key } );
            requestDispatcher.ProcessRequests ( LinkSystemAccountRequestCompleted, HandleLinkSystemAccountRequestException );
        }

        private void ExecuteSelectedJobFunctionChangedCommand ( SystemRoleDto systemRoleDto )
        {
            var jobFunctionRole = EditingDto.SystemRoles.JobFunctionRole;

            if ( jobFunctionRole == null && systemRoleDto != null )
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( new ReviseStaffJobFunctionRoleRequest ( EditingDto.Key, systemRoleDto.Key ) );
                requestDispatcher.ProcessRequests ( HandleReviseJobFunctionCompleted, HandleSaveJobFunctionException );
                IsStaffAccountAccessLoading = true;
            }

            if ( jobFunctionRole != null && systemRoleDto != EditingDto.SystemRoles.JobFunctionRole.SystemRole )
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                if ( systemRoleDto != null )
                {
                    requestDispatcher.Add ( new ReviseStaffJobFunctionRoleRequest ( EditingDto.Key, systemRoleDto.Key ) );
                    requestDispatcher.ProcessRequests ( HandleReviseJobFunctionCompleted, HandleSaveJobFunctionException );
                }
                else
                {
                    requestDispatcher.Add ( new RemoveStaffJobFunctionRoleRequest ( EditingDto.Key ) );
                    requestDispatcher.ProcessRequests ( HandleRemoveJobFunctionCompleted, HandleSaveJobFunctionException );
                }
                IsStaffAccountAccessLoading = true;
            }
        }

        private void ExecuteSelectedLocationChangedCommand ( LocationDisplayNameDto obj )
        {
            if ( EditingDto.LocationAssignment != null )
            {
                var approvedLocation = new StaffApprovedLocationDto ();

                if ( obj != null )
                {
                    approvedLocation.Location.DisplayName = obj.DisplayName;
                }

                if ( EditingDto.LocationAssignment.Locations == null )
                {
                    EditingDto.LocationAssignment.Locations = new SoftDeleteObservableCollection<StaffApprovedLocationDto> ();
                }

                EditingDto.LocationAssignment.Locations.Add ( approvedLocation );
            }
        }

        private void ExecuteSetPrimaryLocationCommand ( LocationDisplayNameDto obj )
        {
            if ( EditingDto.LocationAssignment != null )
            {
                var approvedLocation = new StaffApprovedLocationDto ();

                if ( obj != null )
                {
                    approvedLocation.Location.DisplayName = obj.DisplayName;
                }

                EditingDto.LocationAssignment.PrimaryLocation = approvedLocation;
            }
        }

        private void GetJobFunctionSystemRoleListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<JobFunctionSystemRolesDto>> ();
            JobFunctionSystemRoleLookupList = response.DataTransferObject.SystemRoles;
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void GetStaffByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<StaffDto>> ();
            EditingDto = response.DataTransferObject;

            DetermineSystemAccountPanelStatus ();

            //RemoveCurrentAgencyFromAgencyList();
        }

        private void GetSystemRoleResponse ( StaffSystemRolesDto staffSystemRolesDto, List<DataErrorInfo> validationErrors )
        {
            if ( validationErrors.Count () == 0 )
            {
                EditingDto.SystemRoles = staffSystemRolesDto;
                RefreshSystemRoleLists ();
            }
            else
            {
                foreach ( var validationError in validationErrors )
                {
                    EditingDto.SystemRoles.AddDataErrorInfo ( validationError );
                }
            }
        }

        private void GetTaskGroupSystemRoleListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<TaskGroupSystemRolesDto>> ();
            TaskGroupSystemRoleLookupList = new PagedCollectionView ( response.DataTransferObject.SystemRoles );
        }

        private void GetTaskSystemRoleListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<TaskSystemRolesDto>> ();
            TaskSystemRoleLookupList = new PagedCollectionView ( response.DataTransferObject.SystemRoles );
        }

        private void HandleAddStaffTaskGroupRolesCompleted ( ReceivedResponses receivedResponses )
        {
            IsStaffAccountAccessLoading = false;
            var response = receivedResponses.Get<AddStaffTaskGroupRolesResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();
            GetSystemRoleResponse ( response.StaffSystemRoles, validationErrors );
        }

        private void HandleAddStaffTaskGroupRolesException ( ExceptionInfo exceptionInfo )
        {
            IsStaffAccountAccessLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Staff editor add task group roles failed", UserDialogServiceOptions.Ok );
        }

        private void HandleAddStaffTaskRolesCompleted ( ReceivedResponses receivedResponses )
        {
            IsStaffAccountAccessLoading = false;
            var response = receivedResponses.Get<AddStaffTaskRolesResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();
            GetSystemRoleResponse ( response.StaffSystemRoles, validationErrors );
        }

        private void HandleAddStaffTaskRolesException ( ExceptionInfo exceptionInfo )
        {
            IsStaffAccountAccessLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Staff editor add task roles failed", UserDialogServiceOptions.Ok );
        }

        private void HandleCreateSystemAccountRequestCompleted ( ReceivedResponses obj )
        {
            var response = ( CreateSystemAccountResponse )obj.Responses.First ();
            EditingDto.SystemAccount = response.SystemAccount;
            EditingDto.IsLinkedToSystemAccount = true;

            if ( response.SystemAccount.DataErrorInfoCollection.Count () == 0 )
            {
                _userDialogService.ShowDialog ( "The System Account has been created Successfully.", "Success", UserDialogServiceOptions.Ok );
            }
        }

        private void HandleCreateSystemAccountRequestException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "System Account Creation failed", UserDialogServiceOptions.Ok );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetStaffByKeyCompleted ( receivedResponses );
            GetLookupValuesCompleted ( receivedResponses );
            GetJobFunctionSystemRoleListCompleted ( receivedResponses );
            GetTaskSystemRoleListCompleted ( receivedResponses );
            GetTaskGroupSystemRoleListCompleted ( receivedResponses );
            RefreshSystemRoleLists ();
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Staff editor initialization failed", UserDialogServiceOptions.Ok );
        }

        private void HandleLinkSystemAccountRequestException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "System Account Linking failed", UserDialogServiceOptions.Ok );
        }

        private void HandleRemoveJobFunctionCompleted ( ReceivedResponses receivedResponses )
        {
            IsStaffAccountAccessLoading = false;
            var response = receivedResponses.Get<RemoveStaffJobFunctionRoleResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();
            GetSystemRoleResponse ( response.StaffSystemRoles, validationErrors );
        }

        private void HandleRemoveStaffSystemRoleException ( ExceptionInfo exceptionInfo )
        {
            IsStaffAccountAccessLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Staff editor remove task or task group role failed", UserDialogServiceOptions.Ok );
        }

        private void HandleRemoveStaffTaskGroupRoleCompleted ( ReceivedResponses receivedResponses )
        {
            IsStaffAccountAccessLoading = false;
            var response = receivedResponses.Get<RemoveStaffTaskGroupRoleResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();
            GetSystemRoleResponse ( response.StaffSystemRoles, validationErrors );
        }

        private void HandleRemoveStaffTaskRoleCompleted ( ReceivedResponses receivedResponses )
        {
            IsStaffAccountAccessLoading = false;
            var response = receivedResponses.Get<RemoveStaffTaskRoleResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();
            GetSystemRoleResponse ( response.StaffSystemRoles, validationErrors );
        }

        private void HandleReviseJobFunctionCompleted ( ReceivedResponses receivedResponses )
        {
            IsStaffAccountAccessLoading = false;
            var response = receivedResponses.Get<ReviseStaffJobFunctionRoleResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();
            GetSystemRoleResponse ( response.StaffSystemRoles, validationErrors );
        }

        private void HandleSaveJobFunctionException ( ExceptionInfo exceptionInfo )
        {
            IsStaffAccountAccessLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Staff editor save job function failed", UserDialogServiceOptions.Ok );
        }

        private void LinkSystemAccountRequestCompleted ( ReceivedResponses obj )
        {
            var response = ( LinkSystemAccountResponse )obj.Responses.First ();
            EditingDto.SystemAccount = response.SystemAccount;

            if ( response.SystemAccount.DataErrorInfoCollection.Count () == 0 )
            {
                EditingDto.IsLinkedToSystemAccount = true;
                _userDialogService.ShowDialog ( "The System Account has been created Successfully.", "Success", UserDialogServiceOptions.Ok );
            }
        }

        private void RaiseStaffChangedEvent ()
        {
            _eventAggregator.GetEvent<StaffChangedEvent> ().Publish ( new StaffChangedEventArgs { Sender = this, StaffKey = EditingDto.Key } );
        }

        private void RefreshSystemRoleLists ()
        {
            RefreshTaskGroupSystemRoeLookupList ();
            RefreshTaskSystemRoeLookupList ();

            RefreshTotalSelectedStaffSystemRoleList ();
        }

        private void RefreshTaskGroupSystemRoeLookupList ()
        {
            foreach ( var systemRoleDto in TaskGroupSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> () )
            {
                systemRoleDto.IsSelectable = true;
                systemRoleDto.IsSelected = false;
                foreach ( var staffSystemRoleDto in EditingDto.SystemRoles.TaskGroupRoles )
                {
                    if ( systemRoleDto.Key == staffSystemRoleDto.SystemRole.Key )
                    {
                        systemRoleDto.IsSelectable = false;
                        systemRoleDto.IsSelected = true;
                        break;
                    }
                }
            }

            TaskGroupSystemRoleLookupList.Filter = ( p => ( p as SystemRoleDto ).IsSelectable );
            TaskGroupSystemRoleLookupList.Refresh ();
        }

        private void RefreshTaskSystemRoeLookupList ()
        {
            foreach ( var systemRoleDto in TaskSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> () )
            {
                systemRoleDto.IsSelectable = true;
                systemRoleDto.IsSelected = false;
                foreach ( var staffSystemRoleDto in EditingDto.SystemRoles.TaskRoles )
                {
                    if ( systemRoleDto.Key == staffSystemRoleDto.SystemRole.Key )
                    {
                        systemRoleDto.IsSelectable = false;
                        systemRoleDto.IsSelected = true;
                        break;
                    }
                }
            }

            TaskSystemRoleLookupList.Filter = ( p => ( p as SystemRoleDto ).IsSelectable );
            TaskSystemRoleLookupList.Refresh ();
        }

        private void RefreshTotalSelectedStaffSystemRoleList ()
        {
            TotalSelectedStaffSystemRoleList = new ObservableCollection<StaffSystemRoleDto> ();
            TotalSelectedStaffSystemRoleList.AddRange ( EditingDto.SystemRoles.TaskRoles );
            TotalSelectedStaffSystemRoleList.AddRange ( EditingDto.SystemRoles.TaskGroupRoles );
        }

        #endregion
    }
}
