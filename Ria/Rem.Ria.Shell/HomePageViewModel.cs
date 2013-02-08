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
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.Shell
{
    /// <summary>
    /// View Model for HomePage class.
    /// </summary>
    public class HomePageViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        public HomePageViewModel (
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory,
            IPopupService popupService)
            : base ( accessControlManager, commandFactory )
        {
            _navigationService = navigationService;
            _popupService = popupService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get { return "Home Page"; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Default; }
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
                _navigationService.Navigate ( "HeaderControlRegion", "MainPatientSearchView" );
            }
            base.ActiveChanged ();
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
            var commandName = parameters.GetValue<string> ( "CommandName" );
            _popupService.ShowPopup ( "AppointmentDetailsView", commandName, "Appointment Details", parameters );
        }

        #endregion
    }
}
