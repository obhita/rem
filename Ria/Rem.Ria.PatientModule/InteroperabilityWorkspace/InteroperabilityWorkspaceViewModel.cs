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
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.InteroperabilityWorkspace
{
    /// <summary>
    /// View Model for InteroperabilityWorkspace class.
    /// </summary>
    public class InteroperabilityWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private const string InteroperabilityWorkspaceRegion1 = "InteroperabilityWorkspaceRegion1";
        private const string InteroperabilityWorkspaceRegion2 = "InteroperabilityWorkspaceRegion2";
        private const string InteroperabilityWorkspaceRegion3 = "InteroperabilityWorkspaceRegion3";

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly INavigationService _navigationService;
        private readonly IUserDialogService _userDialogService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InteroperabilityWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public InteroperabilityWorkspaceViewModel (
            IAccessControlManager accessControlManager,
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ExportC32ToPopHealthCommand = commandFactoryHelper.BuildDelegateCommand ( () => ExportC32ToPopHealthCommand, ExecuteExportC32ToPopHealth );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the export C32 to pop health command.
        /// </summary>
        public ICommand ExportC32ToPopHealthCommand { get; private set; }

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get { return "Interoperability Workspace"; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Administrative; }
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
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            _navigationService.Navigate ( RegionManager, InteroperabilityWorkspaceRegion1, "SingleConceptView" );
            _navigationService.Navigate ( RegionManager, InteroperabilityWorkspaceRegion1, "TerminologyVocabularyView" );
            _navigationService.Navigate(RegionManager, InteroperabilityWorkspaceRegion2, "NewCropView");
            _navigationService.Navigate(RegionManager, InteroperabilityWorkspaceRegion2, "DroolsRestTestView");
        }

        private void ExecuteExportC32ToPopHealth ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new PostC32ToPopHealtheRequest () );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( PostC32ToPopHealtheRequestRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Interoperability Workspace operation failed.", UserDialogServiceOptions.Ok );
        }

        private void PostC32ToPopHealtheRequestRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<PostC32ToPopHealthResponse> ();
            _userDialogService.ShowDialog (
                string.Format ( "Message from PopHealth:\n{0}", response.Message ), "Export C32 to PopHealth", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
