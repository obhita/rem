#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.BillingModule.Web.BillingOfficeEditor;
using Rem.Ria.BillingModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.BillingModule.BillingAdministrationWorkspace
{
    /// <summary>
    /// View Model for BillingAdministrationWorkspace class.
    /// </summary>
    public class BillingAdministrationWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly INavigationService _navigationService;
        private readonly IUserDialogService _userDialogService;
        private long _agencyKey;
        private long _billingOfficeKey;

        private BillingOfficeSummaryDto _billingOfficeSummaryDto;
        private bool _isCreateMode;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingAdministrationWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public BillingAdministrationWorkspaceViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper (
                this, commandFactory );

            GoToDashboardCommand = commandFactoryHelper.BuildDelegateCommand ( () => GoToDashboardCommand, ExecuteGoToDashboard );
            GoToPayorDashboardCommand = commandFactoryHelper.BuildDelegateCommand (
                () => GoToPayorDashboardCommand, ExecuteGoToPayorDashboard, CanExecuteGoToPayorDashboard );
            GoToProfileCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => GoToProfileCommand, ExecuteGoToProfile );

            eventAggregator.GetEvent<BillingOfficeChangedEvent> ().Subscribe ( HandleBillingOfficeChanged );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the billing office primary address.
        /// </summary>
        public BillingOfficeAddressDto BillingOfficePrimaryAddress
        {
            get
            {
                if ( BillingOfficeSummary != null )
                {
                    return _billingOfficeSummaryDto.Addresses.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the billing office primary phone.
        /// </summary>
        public BillingOfficePhoneDto BillingOfficePrimaryPhone
        {
            get
            {
                if ( BillingOfficeSummary != null )
                {
                    return _billingOfficeSummaryDto.PhoneNumbers.FirstOrDefault ();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the billing office summary.
        /// </summary>
        /// <value>The billing office summary.</value>
        public BillingOfficeSummaryDto BillingOfficeSummary
        {
            get { return _billingOfficeSummaryDto; }
            set
            {
                _billingOfficeSummaryDto = value;
                RaisePropertyChanged ( () => BillingOfficeSummary );
            }
        }

        /// <summary>
        /// Gets the go to dashboard command.
        /// </summary>
        public ICommand GoToDashboardCommand { get; private set; }

        /// <summary>
        /// Gets the go to payor dashboard command.
        /// </summary>
        public ICommand GoToPayorDashboardCommand { get; private set; }

        /// <summary>
        /// Gets the go to profile command.
        /// </summary>
        public ICommand GoToProfileCommand { get; private set; }

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get
            {
                if ( BillingOfficeSummary != null )
                {
                    return BillingOfficeSummary.Name;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Administrative; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the billing office changed.
        /// </summary>
        /// <param name="eventArgs">The <see cref="Rem.Ria.BillingModule.BillingOfficeChangedEventArgs"/> instance containing the event data.</param>
        public void HandleBillingOfficeChanged ( BillingOfficeChangedEventArgs eventArgs )
        {
            if ( eventArgs.BillingOfficeKey == _billingOfficeKey )
            {
                LoadBillingOfficeSummary ( _billingOfficeKey );
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Actives the changed.
        /// </summary>
        protected override void ActiveChanged ()
        {
            if ( IsActive )
            {
                _navigationService.Navigate ( "HeaderControlRegion", "MainAgencyQuickPickersView" );
            }
            base.ActiveChanged ();
        }

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var billingOfficeKey = parameters.GetValue<long> ( "BillingOfficeKey" );
            return billingOfficeKey == _billingOfficeKey || ( billingOfficeKey == default( long ) && _agencyKey != default( long ) );
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
            _billingOfficeKey = parameters.GetValue<long> ( "BillingOfficeKey" );
            _isCreateMode = parameters.GetValue<bool> ( "IsCreate" );
            LoadBillingOfficeSummary ( _billingOfficeKey );
            if ( _billingOfficeKey != default( long ) )
            {
                HandleDefaultNavigation ();
            }
        }

        private bool CanExecuteGoToPayorDashboard ()
        {
            return _billingOfficeKey != default( long );
        }

        private void ExecuteGoToDashboard ()
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "BillingAdministrationDashboardView",
                null,
                new[] { new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ) } );
        }

        private void ExecuteGoToPayorDashboard ()
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "BillingAdministrationDashboardView",
                "GoToPayorDashboard",
                new[] { new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ) } );
        }

        private void ExecuteGoToProfile ( object obj )
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                "BillingOfficeEditorView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", "false" )
                    } );
        }

        private void GetSummaryByDtoRequestCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<DtoResponse<BillingOfficeSummaryDto>> ();
            BillingOfficeSummary = response.DataTransferObject;

            RaisePropertyChanged ( () => BillingOfficePrimaryAddress );
            RaisePropertyChanged ( () => BillingOfficePrimaryPhone );
            RaisePropertyChanged ( () => HeaderContext );

            if ( _billingOfficeKey == default ( long ) )
            {
                _billingOfficeKey = BillingOfficeSummary.Key;
                HandleDefaultNavigation ();
            }
        }

        private void HandleDefaultNavigation ()
        {
            if ( _isCreateMode )
            {
                _navigationService.Navigate (
                    RegionManager,
                    "WorkspaceContentScopedRegion",
                    "BillingOfficeEditorView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ),
                            new KeyValuePair<string, string> ( "IsCreate", "true" )
                        } );
            }
            else
            {
                _navigationService.Navigate (
                    RegionManager,
                    "WorkspaceContentScopedRegion",
                    "BillingAdministrationDashboardView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () )
                        } );
            }

            ( GoToPayorDashboardCommand as VirtualDelegateCommand ).RaiseCanExecuteChanged ();
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Loading Billing Office Summary", UserDialogServiceOptions.Ok );
        }

        private void LoadBillingOfficeSummary ( long billingOfficeSummaryKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            if ( billingOfficeSummaryKey != default( long ) )
            {
                requestDispatcher.Add ( new GetDtoRequest<BillingOfficeSummaryDto> { Key = billingOfficeSummaryKey } );
            }
            else
            {
                _agencyKey = CurrentUserContext.Agency.Key;
                requestDispatcher.Add ( new GetBillingOfficeSummaryByAgencyKeyRequest { AgencyKey = CurrentUserContext.Agency.Key } );
            }

            requestDispatcher.ProcessRequests ( GetSummaryByDtoRequestCompleted, HandleRequestDispatcherException );
            IsLoading = true;
        }

        #endregion
    }
}
