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
using Rem.Ria.BillingModule.Web.Common;
using Rem.Ria.BillingModule.Web.PayorEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.BillingModule.BillingAdministrationDashboard
{
    /// <summary>
    /// View Model for PayorTypeList class.
    /// </summary>
    public class PayorTypeListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _billingOfficeKey;
        private IEnumerable<PayorTypeDto> _payorTypeList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorTypeListViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public PayorTypeListViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IUserDialogService userDialogService,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _popupService = popupService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ShowPayorTypeAddCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowPayorTypeAddCommand, ExecuteShowPayorTypeAddCommand );
            ShowPayorTypeEditCommand = commandFactoryHelper.BuildDelegateCommand<PayorTypeDto> (
                () => ShowPayorTypeEditCommand, ExecuteShowPayorTypeEditCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the payor type list.
        /// </summary>
        /// <value>The payor type list.</value>
        public IEnumerable<PayorTypeDto> PayorTypeList
        {
            get { return _payorTypeList; }
            set
            {
                _payorTypeList = value;
                RaisePropertyChanged ( () => PayorTypeList );
            }
        }

        /// <summary>
        /// Gets the show program add command.
        /// </summary>
        public ICommand ShowPayorTypeAddCommand { get; private set; }

        /// <summary>
        /// Gets the show program edit command.
        /// </summary>
        public ICommand ShowPayorTypeEditCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "BillingOfficeKey" );
            return key == 0 || key == _billingOfficeKey;
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
            GetPayorTypesByBillingOfficeAsync ( _billingOfficeKey );
        }

        private void ExecuteShowPayorTypeAddCommand ()
        {
            _popupService.ShowPopup (
                "PayorTypeEditorView",
                null,
                "Add a Payor Type",
                new[]
                    {
                        new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", true.ToString () )
                    },
                true,
                () => GetPayorTypesByBillingOfficeAsync ( _billingOfficeKey ) );
        }

        private void ExecuteShowPayorTypeEditCommand ( PayorTypeDto payorTypeDto )
        {
            _popupService.ShowPopup (
                "PayorTypeEditorView",
                null,
                "Edit Payor Type",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PayorTypeKey", payorTypeDto.Key.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreate", false.ToString () )
                    },
                true,
                () => GetPayorTypesByBillingOfficeAsync ( _billingOfficeKey ) );
        }

        private void GetPayorTypesByBillingOfficeAsync ( long billingOfficeKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetPayorTypesByBillingOfficeKeyRequest { BillingOfficeKey = billingOfficeKey } );
            requestDispatcher.ProcessRequests ( HandleGetPayorTypesCompleted, HandleGetPayorTypesException );
            IsLoading = true;
        }

        private void HandleGetPayorTypesCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<GetPayorTypesByBillingOfficeKeyResponse> ();
            PayorTypeList = response.PayorTypes;
        }

        private void HandleGetPayorTypesException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get Payor Types list failed.", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
