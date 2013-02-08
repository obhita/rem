using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// View Model for the HealthProidersDirectory Screen.
    /// </summary>
    public class HealthProvidersDirectoryViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _dispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupService _popupService;

        private ObservableCollection<Tuple<string, string>> _directories;
        private ObservableCollection<HealthProviderEntryDto> _providers;
        private Tuple<string, string> _selectedDirectory;
        private HealthProviderEntryDto _selectedProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthProvidersDirectoryViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="dispatcherFactory">The dispatcher factory.</param>
        public HealthProvidersDirectoryViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IPopupService popupService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory dispatcherFactory )
            : base ( accessControlManager, commandFactory )
        {
            _popupService = popupService;
            _eventAggregator = eventAggregator;

            // [Agatha] Dispatcher factory
            _dispatcherFactory = dispatcherFactory;

            // Using the Command Factory Helper, build all the commands used by this ViewModel. 
            // This is required because the generated command(s) are [Castle] Dynamic Proxies, which run special logic (i.e.: Run Rule Engine Rules)
            // before the Execute Action is called. See Pillar.FluentRuleEngine.CheckRulesInterceptor for an example. 
            CommandFactoryHelper<HealthProvidersDirectoryViewModel> commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            // UI Commands
            DoneCommand = commandFactoryHelper.BuildDelegateCommand ( () => DoneCommand, ExecuteDoneCommand );

            // Navigation Command 

            // Event Aggregator Events Subscription

            Providers = new ObservableCollection<HealthProviderEntryDto>
                {
                    new HealthProviderEntryDto
                        {
                            FirstName = "John",
                            LastName = "Snow",
                            Mail = "jsnow@mydomain.com",
                            OrganizationName = "IniTech",
                            Specialization = "Cardiologist",
                            TelephoneNumber = "(123)456-7890"
                        }
                };

            Directories = new ObservableCollection<Tuple<string, string>>
                {
                    new Tuple<string, string> ( "EHRDoctors", "nhin.medibridge.net" )
                };

            SelectedDirectory = Directories.First ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the directories.
        /// </summary>
        /// <value>
        /// The directories.
        /// </value>
        public ObservableCollection<Tuple<string, string>> Directories
        {
            get { return _directories; }

            set { ApplyPropertyChange ( ref _directories, () => Directories, value ); }
        }
       
        /// <summary>
        /// Gets the Done command.
        /// </summary>
        public ICommand DoneCommand { get; private set; }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public ObservableCollection<HealthProviderEntryDto> Providers
        {
            get { return _providers; }
            set { ApplyPropertyChange ( ref _providers, () => Providers, value ); }
        }

        /// <summary>
        /// Gets or sets the selected directory.
        /// </summary>
        /// <value>
        /// The selected directory.
        /// </value>
        public Tuple<string, string> SelectedDirectory
        {
            get { return _selectedDirectory; }
            set { ApplyPropertyChange ( ref _selectedDirectory, () => SelectedDirectory, value ); }
        }
       
        /// <summary>
        /// Gets or sets the selected provider.
        /// </summary>
        /// <value>
        /// The selected provider.
        /// </value>
        public virtual HealthProviderEntryDto SelectedProvider
        {
            get { return _selectedProvider; }
            set { ApplyPropertyChange ( ref _selectedProvider, () => SelectedProvider, value ); }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>
        /// A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/>
        /// </returns>
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
            DispatchGetHealthcareProvidersDirectoryEntriesRequest ();
        }

        private void DispatchGetHealthcareProvidersDirectoryEntriesRequest ()
        {
            IAsyncRequestDispatcher dispatcher = _dispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.Add ( new GetHealthcareProviderDirectoryEntriesRequest () );
            dispatcher.ProcessRequests ( RequestCompleted, HandleRequestDispatcherException );
        }

        private void ExecuteDoneCommand ()
        {
            // TODO: Event Aggregator

            if (SelectedProvider != null)
            {
                _eventAggregator
                    .GetEvent<DirectAddressRecipientSelectedEvent> ()
                    .Publish (
                        new DirectAddressRecipientSelectedArgs { Address = SelectedProvider.Mail, DisplayName = SelectedProvider.DisplayName } );
            }

            _popupService.ClosePopup ( "HealthProvidersDirectoryView");
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
        }

        /// <summary>
        /// Handles the Response of the HealthProvidersRequestHandler 
        /// </summary>
        /// <param name="responses">The responses.</param>
        private void RequestCompleted ( ReceivedResponses responses )
        {
            var response = responses.Get<GetHealthcareProviderDirectoryEntriesResponse> ();
            Providers = new ObservableCollection<HealthProviderEntryDto> ( response.Providers );
        }

        #endregion
    }
}
