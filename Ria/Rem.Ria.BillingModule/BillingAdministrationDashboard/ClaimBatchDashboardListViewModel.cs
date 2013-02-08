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
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.BillingModule.Web.BillingAdministrationDashboard;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.BillingModule.BillingAdministrationDashboard
{
    /// <summary>
    /// View Model for ClaimBatchList class.
    /// </summary>
    public class ClaimBatchDashboardListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private long _billingOfficeKey;

        private IEnumerable<ClaimSummaryDto> _claimList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimBatchDashboardListViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        public ClaimBatchDashboardListViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            PageController = new PageController ( HandlePageChanged ) { PageSize = 50 };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the claim list.
        /// </summary>
        /// <value>The claim list.</value>
        public IEnumerable<ClaimSummaryDto> ClaimList
        {
            get { return _claimList; }
            set
            {
                _claimList = value;
                RaisePropertyChanged ( () => ClaimList );
            }
        }

        /// <summary>
        /// Gets the page controller.
        /// </summary>
        public PageController PageController { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Refreshes the list.
        /// </summary>
        public void RefreshList ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new GetOpenClaimSummaryListByBillingOfficeKeyRequest
                    {
                        BillingOfficeKey = _billingOfficeKey,
                        PageSize = PageController.PageSize,
                        PageIndex = PageController.PageIndex
                    } );
            requestDispatcher.ProcessRequests ( HandleGetClaimSummaryListCompleted, HandleError );
            IsLoading = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance can navigate to default command with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance can navigate to default command with the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var billingOfficeKey = parameters.GetValue<long> ( "BillingOfficeKey" );
            return _billingOfficeKey == billingOfficeKey;
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
            RefreshList ();
        }

        private void HandleError ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error getting claims.", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleGetClaimSummaryListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<PagedClaimSummaryListDto>> ();
            ClaimList = response.DataTransferObject.ClaimSummaryList;
            PageController.PageIndex = response.DataTransferObject.PageIndex;
            PageController.PageSize = response.DataTransferObject.PageSize;
            PageController.TotalCount = response.DataTransferObject.TotalCount;
            IsLoading = false;
        }

        private void HandlePageChanged ( int page )
        {
            RefreshList ();
        }

        #endregion
    }
}
