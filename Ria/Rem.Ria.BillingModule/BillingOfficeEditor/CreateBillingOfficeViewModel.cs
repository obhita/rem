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
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.BillingModule.Web.BillingOfficeEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;

namespace Rem.Ria.BillingModule.BillingOfficeEditor
{
    /// <summary>
    /// View Model for CreateBillingOffice class.
    /// </summary>
    public class CreateBillingOfficeViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _agencyKey;
        private StaffContext _currentStaff;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBillingOfficeViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="currentUserContextService">The current user context service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public CreateBillingOfficeViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            ICurrentUserContextService currentUserContextService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            INavigationService navigationService,
            IUserDialogService userDialogService,
            IPopupService popupService,
            IEventAggregator eventAggregator )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _navigationService = navigationService;
            _userDialogService = userDialogService;
            _popupService = popupService;
            _eventAggregator = eventAggregator;
            currentUserContextService.RegisterForContext ( ( u, b ) => { _currentStaff = u.Staff; } );

            var commandFactoryHelper = CreateCommandFactoryHelper ( commandFactory );
            CreateCommand = commandFactoryHelper.BuildDelegateCommand ( () => CreateCommand, ExecuteCreateCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the create command.
        /// </summary>
        public ICommand CreateCommand { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

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
            _agencyKey = parameters.GetValue<long> ( "AgencyKey" );
        }

        private void ExecuteCreateCommand ()
        {
            IsLoading = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new CreateBillingOfficeRequest { AgencyKey = _agencyKey, StaffKey = _currentStaff.Key, Name = Name } );
            requestDispatcher.ProcessRequests ( HandleCreateCompleted, HandleCreateException );
        }

        private void HandleCreateCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<DtoResponse<BillingOfficeDto>> ();
            var billingOfficeDto = response.DataTransferObject;
            if ( billingOfficeDto.HasErrors )
            {
                var errorBuilder = new StringBuilder ();
                foreach ( var error in billingOfficeDto.DataErrorInfoCollection )
                {
                    errorBuilder.Append ( error.Message + "\n" );
                }
                _userDialogService.ShowDialog ( errorBuilder.ToString (), "Failed Creating Billing Office", UserDialogServiceOptions.Ok );
            }
            else
            {
                _navigationService.Navigate (
                    "WorkspacesRegion",
                    "BillingAdministrationWorkspaceView",
                    null,
                    new KeyValuePair<string, string> ( "BillingOfficeKey", billingOfficeDto.Key.ToString () ),
                    new KeyValuePair<string, string> ( "IsCreate", true.ToString () ) );
                _popupService.ClosePopup ( "CreateBillingOfficeView" );
                _eventAggregator.GetEvent<HasBillingOfficeEvent> ().Publish ( new EventArgs () );
            }
        }

        private void HandleCreateException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Failed Creating Billing Office", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
