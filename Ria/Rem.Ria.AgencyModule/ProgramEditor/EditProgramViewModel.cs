using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.AgencyDashboard;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.AgencyModule.ProgramEditor
{
    /// <summary>
    /// View Model for EditProgram class.
    /// </summary>
    public class EditProgramViewModel : PanelEditorViewModel<ProgramDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;

        private bool _isCreate;

        private Dictionary<string, IList<LookupValueDto>> _lookupValueLists;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProgramViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        public EditProgramViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory,
            IPopupService popupService )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _eventAggregator = eventAggregator;
            _popupService = popupService;
            _userDialogService = userDialogService;

            CreateCommand = NavigationCommandManager.BuildCommand ( () => CreateCommand, NavigateToCreateCommand );
            EditCommand = NavigationCommandManager.BuildCommand ( () => EditCommand, NavigateToEditCommand );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SaveCommand = commandFactoryHelper.BuildDelegateCommand<KeyedDataTransferObject> (
                () => SaveCommand,
                dto =>
                    {
                        var name = PropertyUtil.ExtractPropertyName ( () => EditingDto );
                        if ( dto != null && EditingDto.GetType () != dto.GetType () )
                        {
                            name = EditingDto.GetType ().GetProperties ().First ( pi => pi.PropertyType == dto.GetType () ).Name;
                        }
                        return name;
                    },
                ExecuteSaveCommand,
                CanExecuteSaveCommand );

            var ruleExecutor = new NotifyPropertyChangedRuleExecutor<EditProgramViewModel, IDataTransferObject> ();
            ruleExecutor.AddRunAllRulesProperty ( vm => vm.EditingDto );
            ruleExecutor.WatchSubject ( this );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the create command.
        /// </summary>
        public INavigationCommand CreateCommand { get; private set; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        public INavigationCommand EditCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is create.
        /// </summary>
        /// <value><c>true</c> if this instance is create; otherwise, <c>false</c>.</value>
        public bool IsCreate
        {
            get { return _isCreate; }
            set { ApplyPropertyChange ( ref _isCreate, () => IsCreate, value ); }
        }

        /// <summary>
        /// Gets or sets the lookup value lists.
        /// </summary>
        /// <value>The lookup value lists.</value>
        public Dictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
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
        /// Executes the cancel command.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        protected override void ExecuteCancelCommand ( KeyedDataTransferObject dto )
        {
            if ( IsCreate )
            {
                _popupService.ClosePopup ( "EditProgramView" );
            }
            else
            {
                base.ExecuteCancelCommand ( dto );
            }
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );

            if ( EditingDto != null )
            {
                _eventAggregator.GetEvent<ProgramChangedEvent> ().Publish (
                    new ProgramChangedEventArgs { Sender = this, AgencyKey = EditingDto.AgencyKey } );
            }
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;
            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void GetPatientContactByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<ProgramDto>> ();
            EditingDto = response.DataTransferObject;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Program Editor Initialization Failed.", UserDialogServiceOptions.Ok );
        }

        private void NavigateToCreateCommand ( KeyValuePair<string, string>[] parameters )
        {
            var agencyKey = parameters.GetValue<long> ( "AgencyKey" );
            EditingDto = new ProgramDto { AgencyKey = agencyKey };
            IsCreate = true;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            RequestLookups ( requestDispatcher );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( NavigatedToRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void NavigateToEditCommand ( KeyValuePair<string, string>[] parameters )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            var programKey = parameters.GetValue<long> ( "ProgramKey" );
            requestDispatcher.Add ( new GetDtoRequest<ProgramDto> { Key = programKey } );
            RequestLookups ( requestDispatcher );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( NavigatedToRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void NavigatedToRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            if ( !IsCreate )
            {
                GetPatientContactByKeyCompleted ( receivedResponses );
            }
            GetLookupValuesCompleted ( receivedResponses );
            IsLoading = false;
        }

        private void RequestLookups ( IAsyncRequestDispatcher requestDispatcher )
        {
            requestDispatcher.AddLookupValuesRequest ( "CapacityType" );
            requestDispatcher.AddLookupValuesRequest ( "ProgramCategory" );
            requestDispatcher.AddLookupValuesRequest ( "AgeGroup" );
            requestDispatcher.AddLookupValuesRequest ( "GenderSpecification" );
            requestDispatcher.AddLookupValuesRequest ( "TreatmentApproach" );
        }

        #endregion
    }
}
