using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.AgencyDashboard;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.ProgramOfferingEditor
{
    /// <summary>
    /// View Model for Class for editing program offering.
    /// </summary>
    public class ProgramOfferingEditorViewModel : PanelEditorViewModel<ProgramOfferingDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private bool _isCreate;

        private ObservableCollection<ProgramDto> _programs;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingEditorViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        public ProgramOfferingEditorViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IPopupService popupService )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _popupService = popupService;

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
        /// Gets or sets the programs.
        /// </summary>
        /// <value>The programs.</value>
        public ObservableCollection<ProgramDto> Programs
        {
            get { return _programs; }
            set { ApplyPropertyChange ( ref _programs, () => Programs, value ); }
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
                _popupService.ClosePopup ( "ProgramOfferingEditorView" );
            }
            else
            {
                base.ExecuteCancelCommand ( dto );
            }
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Program Offering Editor Initialization Failed.", UserDialogServiceOptions.Ok );
        }

        private void NavigateToCreateCommand ( KeyValuePair<string, string>[] parameters )
        {
            var locationDisplayName = parameters.GetValue<string> ( "LocationDisplayName" );
            var locationKey = parameters.GetValue<long> ( "LocationKey" );
            EditingDto = new ProgramOfferingDto ();
            EditingDto.Profile = new ProgramOfferingProfileDto
                {
                    Location = new LocationDisplayNameDto { Key = locationKey, DisplayName = locationDisplayName }
                };
            IsCreate = true;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            IsLoading = true;
            requestDispatcher.Add ( new GetProgramsForLocationKeyRequest { LocationKey = locationKey } );
            requestDispatcher.ProcessRequests ( NavigatedToRequestDispatcherCompleted, HandleRequestDispatcherException );

            StartRuleWatch ();
        }

        private void NavigateToEditCommand ( KeyValuePair<string, string>[] parameters )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            var locationKey = parameters.GetValue<long> ( "LocationKey" );
            var programOfferingKey = parameters.GetValue<long> ( "ProgramOfferingKey" );
            requestDispatcher.Add ( new GetDtoRequest<ProgramOfferingDto> { Key = programOfferingKey } );
            IsLoading = true;
            requestDispatcher.Add ( new GetProgramsForLocationKeyRequest { LocationKey = locationKey } );
            requestDispatcher.ProcessRequests ( NavigatedToRequestDispatcherCompleted, HandleRequestDispatcherException );

            StartRuleWatch ();
        }

        private void NavigatedToRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            if ( !IsCreate )
            {
                var programOffering = receivedResponses.Get<DtoResponse<ProgramOfferingDto>> ();
                EditingDto = programOffering.DataTransferObject;
            }

            var programs = receivedResponses.Get<GetProgramsForLocationKeyResponse> ();
            Programs = new ObservableCollection<ProgramDto> ( programs.Programs );
            IsLoading = false;
        }

        private void StartRuleWatch ()
        {
            var ruleExecutor = new NotifyPropertyChangedRuleExecutor<ProgramOfferingEditorViewModel, IDataTransferObject> ();
            ruleExecutor.AddRunAllRulesProperty ( vm => vm.EditingDto );
            ruleExecutor.IgnoreProperty ( vm => vm.Programs );
            ruleExecutor.WatchSubject ( this );
        }

        #endregion
    }
}
