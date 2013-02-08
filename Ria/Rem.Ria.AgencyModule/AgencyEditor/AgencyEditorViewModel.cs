using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.AgencyEditor;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.AgencyModule.AgencyEditor
{
    /// <summary>
    /// View Model for Class for editing agency.
    /// </summary>
    public class AgencyEditorViewModel : PanelEditorViewModel<AgencyDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserDialogService _userDialogService;

        private long _agencyKey;
        private ObservableCollection<AgencyDisplayNameDto> _agencyList;
        private bool _isCreateMode;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyEditorViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public AgencyEditorViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the agency list.
        /// </summary>
        /// <value>The agency list.</value>
        public ObservableCollection<AgencyDisplayNameDto> AgencyList
        {
            get { return _agencyList; }
            set { ApplyPropertyChange ( ref _agencyList, () => AgencyList, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is create mode.
        /// </summary>
        public bool IsCreateMode
        {
            get { return _isCreateMode; }
            private set { ApplyPropertyChange ( ref _isCreateMode, () => IsCreateMode, value ); }
        }

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
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
            var agencyKey = parameters.GetValue<long> ( "AgencyKey" );
            return _agencyKey == agencyKey;
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
            IsCreateMode = parameters.GetValue<bool> ( "IsCreate" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetAllAgencyDisplayNamesRequest () );
            requestDispatcher.Add ( new GetAgencyByKeyRequest { Key = _agencyKey } );
            requestDispatcher
                .AddLookupValuesRequest ( "AgencyPhoneType" )
                .AddLookupValuesRequest ( "AgencyAddressType" )
                .AddLookupValuesRequest ( "AgencyContactType" )
                .AddLookupValuesRequest ( "AgencyEmailAddressType" )
                .AddLookupValuesRequest ( "AgencyIdentifierType" )
                .AddLookupValuesRequest ( "AgencyType" )
                .AddLookupValuesRequest ( "Country" )
                .AddLookupValuesRequest ( "CountyArea" )
                .AddLookupValuesRequest ( "GeographicalRegion" )
                .AddLookupValuesRequest ( "OrganizationCharacteristic" )
                .AddLookupValuesRequest ( "StateProvince" );
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
            RaiseAgencyChangedEvent ();
        }

        private void GetAgencyByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAgencyByKeyResponse> ();
            EditingDto = response.AgencyDto;
            RemoveCurrentAgencyFromAgencyList ();
        }

        private void GetAllAgenciesCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAllAgencyDisplayNamesResponse> ();

            var agencies = response.Agencies;

            AgencyList = new ObservableCollection<AgencyDisplayNameDto> ( agencies );
            RemoveCurrentAgencyFromAgencyList ();
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            LookupValueLists = lookupValueLists;
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetAllAgenciesCompleted ( receivedResponses );
            GetAgencyByKeyCompleted ( receivedResponses );
            GetLookupValuesCompleted ( receivedResponses );
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Agency editor initialization failed", UserDialogServiceOptions.Ok );
        }

        private void RaiseAgencyChangedEvent ()
        {
            _eventAggregator.GetEvent<AgencyChangedEvent> ().Publish (
                new AgencyChangedEventArgs
                    {
                        Sender = this,
                        AgencyKey = EditingDto.Key
                    } );
        }

        private void RemoveCurrentAgencyFromAgencyList ()
        {
            if ( EditingDto != null && AgencyList != null )
            {
                var agencyToRemove = AgencyList.FirstOrDefault ( a => a.Key == EditingDto.Key );
                if ( agencyToRemove != null )
                {
                    AgencyList.Remove ( agencyToRemove );
                }
            }
        }

        #endregion
    }
}
