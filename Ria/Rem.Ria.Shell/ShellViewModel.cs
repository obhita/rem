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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.Shell
{
    /// <summary>
    /// View Model for Shell class.
    /// </summary>
    public class ShellViewModel : ViewModelBase
    {
        #region Constants and Fields

        private const string AgencyWorkspaceView = "AgencyWorkspaceView";
        private const string InteroperabilityWorkspaceView = "InteroperabilityWorkspaceView";
        private const string PatientAccessHistoryView = "PatientAccessHistoryView";
        private const string WorkspacesRegion = "WorkspacesRegion";
        private readonly INavigationService _navigationService;
        private readonly IRegionManager _regionManager;
        private readonly IRegionNavigationService _regionNavigationService;
        private readonly ISignOffService _signOffService;
        private readonly IUserDialogService _userDialogService;
        private bool _hasBillingOffice = false;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="signOffService">The sign off service.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionNavigationService">The region navigation service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ShellViewModel (
            IAccessControlManager accessControlManager,
            ISignOffService signOffService,
            IUserDialogService userDialogService,
            IRegionManager regionManager,
            IRegionNavigationService regionNavigationService,
            INavigationService navigationService,
            ICommandFactory commandFactory,
            IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _signOffService = signOffService;
            _userDialogService = userDialogService;
            _regionNavigationService = regionNavigationService;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            OpenMessageCenterWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand(
                () => OpenMessageCenterWorkspaceCommand, ExecuteOpenMessageCenterWorkspace);
            OpenPatientAccessHistoryWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand (
                () => OpenPatientAccessHistoryWorkspaceCommand, ExecuteOpenPatientAccessHistoryWorkspace );
            OpenInteroperabilityWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand (
                () => OpenInteroperabilityWorkspaceCommand, ExecuteOpenInteroperabilityWorkspace );
            OpenAgencyWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand (
                () => OpenAgencyWorkspaceCommand, ExecuteOpenAgencyWorkspaceCommand );
            OpenPatientListCommand = commandFactoryHelper.BuildDelegateCommand ( () => OpenPatientListCommand, ExecuteOpenPatientListCommand );
            OpenPatientReminderCommand = commandFactoryHelper.BuildDelegateCommand (
                () => OpenPatientReminderCommand, ExecuteOpenPatientReminderCommand );
            OpenMuObjectivesCommand = commandFactoryHelper.BuildDelegateCommand ( () => OpenMuObjectivesCommand, ExecuteOpenMuObjectivesCommand );
            OpenCdsEditorCommand = commandFactoryHelper.BuildDelegateCommand ( () => OpenCdsEditorCommand, ExecuteOpenCdsEditorCommand );
            OpenReportsWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand (
                () => OpenReportsWorkspaceCommand, ExecuteOpenReportsWorkspaceCommand );
            OpenRoleManagementWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand (
                () => OpenRoleManagementWorkspaceCommand, ExecuteOpenRoleManagementWorkspaceCommand );
            OpenBillingWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand(
                () => OpenBillingWorkspaceCommand, ExecuteOpenBillingWorkspaceCommand);
            OpenBillingAdministrationWorkspaceCommand = commandFactoryHelper.BuildDelegateCommand(
                () => OpenBillingAdministrationWorkspaceCommand, ExecuteOpenBillingAdministrationWorkspaceCommand, () => _hasBillingOffice);
            LogoutCommand = commandFactoryHelper.BuildDelegateCommand ( () => LogoutCommand, ExecuteLogout );

            _regionNavigationService.NavigationFailed += RegionNavigationFailed;

            ApplyContextChanges = true;

            _regionManager.RegisterViewWithRegion ( "WorkspacesRegion", typeof( HomePageView ) );

            eventAggregator.GetEvent<HasBillingOfficeEvent>().Subscribe(HandleHasBillingOfficeEvent, true);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the logout command.
        /// </summary>
        public ICommand LogoutCommand { get; private set; }

        /// <summary>
        /// Gets or sets the open agency workspace command.
        /// </summary>
        /// <value>The open agency workspace command.</value>
        public ICommand OpenAgencyWorkspaceCommand { get; set; }

        /// <summary>
        /// Gets the open CDS editor command.
        /// </summary>
        public ICommand OpenCdsEditorCommand { get; private set; }

        /// <summary>
        /// Gets or sets the open interoperability workspace command.
        /// </summary>
        /// <value>The open interoperability workspace command.</value>
        public ICommand OpenInteroperabilityWorkspaceCommand { get; set; }

        /// <summary>
        /// Gets the open mu objectives command.
        /// </summary>
        public ICommand OpenMuObjectivesCommand { get; private set; }

        /// <summary>
        /// Gets or sets the open message center workspace command.
        /// </summary>
        public ICommand OpenMessageCenterWorkspaceCommand { get; set; }

        /// <summary>
        /// Gets or sets the open patient access history workspace command.
        /// </summary>
        /// <value>The open patient access history workspace command.</value>
        public ICommand OpenPatientAccessHistoryWorkspaceCommand { get; set; }

        /// <summary>
        /// Gets the open patient list command.
        /// </summary>
        public ICommand OpenPatientListCommand { get; private set; }

        /// <summary>
        /// Gets the open patient reminder command.
        /// </summary>
        public ICommand OpenPatientReminderCommand { get; private set; }

        /// <summary>
        /// Gets the open reports workspace command.
        /// </summary>
        public ICommand OpenReportsWorkspaceCommand { get; private set; }

        /// <summary>
        /// Gets the open role management workspace command.
        /// </summary>
        public ICommand OpenRoleManagementWorkspaceCommand { get; private set; }

        /// <summary>
        /// Gets the open billing workspace command.
        /// </summary>
        public ICommand OpenBillingWorkspaceCommand { get; private set; }

        /// <summary>
        /// Gets the open billing administration workspace command.
        /// </summary>
        public ICommand OpenBillingAdministrationWorkspaceCommand { get; private set; }

        /// <summary>
        /// Gets the version display.
        /// </summary>
        public string VersionDisplay
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly ().GetCustomAttributes ( typeof( AssemblyInformationalVersionAttribute ), false );
                return attributes.Length == 0
                           ? string.Empty
                           : ( ( AssemblyInformationalVersionAttribute )attributes[0] ).InformationalVersion;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the has billing office event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void HandleHasBillingOfficeEvent(EventArgs e )
        {
            _hasBillingOffice = true;
            (OpenBillingAdministrationWorkspaceCommand as DelegateCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Executes the open agency workspace command.
        /// </summary>
        public void ExecuteOpenAgencyWorkspaceCommand ()
        {
            _navigationService.Navigate (
                WorkspacesRegion,
                AgencyWorkspaceView,
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "AgencyKey", CurrentUserContext.Agency.Key.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", false.ToString () )
                    } );
        }

        /// <summary>
        /// Executes the open interoperability workspace.
        /// </summary>
        public void ExecuteOpenInteroperabilityWorkspace ()
        {
            _navigationService.Navigate ( WorkspacesRegion, InteroperabilityWorkspaceView );
        }

        /// <summary>
        /// Executes the open patient access history workspace.
        /// </summary>
        public void ExecuteOpenPatientAccessHistoryWorkspace ()
        {
            _navigationService.Navigate ( WorkspacesRegion, PatientAccessHistoryView, "SearchPatientAccessHistory" );
        }

        /// <summary>
        /// Executes the open message center workspace.
        /// </summary>
        public void ExecuteOpenMessageCenterWorkspace()
        {
            _navigationService.Navigate(WorkspacesRegion, "MessageCenterWorkspaceView");
        }

        #endregion

        #region Methods

        private static void RegionNavigationFailed(object sender, RegionNavigationFailedEventArgs e)
        {
            throw e.Error;
        }

        private void ExecuteLogout()
        {
            var result = _userDialogService.ShowDialog (
                "Are you sure that you want to logout?",
                "Logout Request",
                UserDialogServiceOptions.OkCancel );

            if ( result == UserDialogServiceResult.Ok )
            {
                _signOffService.SignOff ();
            }
        }

        private void ExecuteOpenCdsEditorCommand()
        {
            _navigationService.Navigate ( WorkspacesRegion, "CdsRuleEditorView" );
        }

        private void ExecuteOpenMuObjectivesCommand()
        {
            _navigationService.Navigate ( WorkspacesRegion, "MUObjectivesView" );
        }

        private void ExecuteOpenPatientListCommand()
        {
            _navigationService.Navigate ( WorkspacesRegion, "PatientListView" );
        }

        private void ExecuteOpenPatientReminderCommand()
        {
            _navigationService.Navigate ( WorkspacesRegion, "PatientReminderView" );
        }

        private void ExecuteOpenReportsWorkspaceCommand()
        {
            _navigationService.Navigate ( WorkspacesRegion, "ReportsWorkspaceView" );
        }

        private void ExecuteOpenRoleManagementWorkspaceCommand()
        {
            _navigationService.Navigate ( WorkspacesRegion, "RoleManagementWorkspaceView" );
        }

        private void ExecuteOpenBillingWorkspaceCommand()
        {
            _navigationService.Navigate(WorkspacesRegion, "BillingWorkspaceView");
        }

        private void ExecuteOpenBillingAdministrationWorkspaceCommand()
        {
            _navigationService.Navigate(WorkspacesRegion, "BillingAdministrationWorkspaceView");
        }

        #endregion
    }
}
