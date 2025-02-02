﻿#region License

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
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.BillingModule.Web.Claim;
using Rem.Ria.BillingModule.Web.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.WellKnownNames;
using Rem.WellKnownNames.BillingModule;

namespace Rem.Ria.BillingModule
{
    /// <summary>
    /// View Model for ClaimBatchList class.
    /// </summary>
    public class ClaimBatchListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly PagedCollectionViewWrapper<ClaimBatchDisplayNameDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _currentClaimBatchKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimBatchListViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public ClaimBatchListViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimBatchListViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public ClaimBatchListViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _popupService = popupService;
            _userDialogService = userDialogService;

            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<ClaimBatchDisplayNameDto> ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            DownloadHealthCareClaim837ProfessionalCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => DownloadHealthCareClaim837ProfessionalCommand,
                ExecuteDownloadHealthCareClaim837ProfessionalCommand,
                CanExecuteDownloadHealthCareClaim837ProfessionalCommand );

            RefreshCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => RefreshCommand, ExecuteRefreshCommand );
            ResetTestDataCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => ResetTestDataCommand, ExecuteResetTestDataCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the download health care claim837 professional command.
        /// </summary>
        public ICommand DownloadHealthCareClaim837ProfessionalCommand { get; private set; }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<ClaimBatchDisplayNameDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        public ICommand RefreshCommand { get; private set; }

        /// <summary>
        /// Gets the reset test data command.
        /// </summary>
        public ICommand ResetTestDataCommand { get; private set; }

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
            GetAllClaimBatches ();
        }

        private bool CanExecuteDownloadHealthCareClaim837ProfessionalCommand ( object value )
        {
            return true;
        }

        private void ExecuteDownloadHealthCareClaim837ProfessionalCommand ( object value )
        {
            _currentClaimBatchKey = ( long )value;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GenerateHcc837ProfessionalRequest { ClaimBatchKey = ( long )value } );
            requestDispatcher.ProcessRequests ( HandleGenerateHcc837ProfessionalCompleted, HandleGenerateHcc837ProfessionalException );
        }

        private void ExecuteRefreshCommand ( object value )
        {
            GetAllClaimBatches ();
        }

        private void ExecuteResetTestDataCommand ( object obj )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new ResetTestDataRequest () );
            requestDispatcher.ProcessRequests ( HandleResetTestDataCompleted, HandleResetTestDataException );
        }

        private void GetAllClaimBatches ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetClaimBatchDisplayNamesByClaimBatchStatusRequest () );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleClaimBatchesCompleted, HandleGetAllClaimBatchesException );
        }

        private void HandleClaimBatchesCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetClaimBatchDisplayNamesByClaimBatchStatusResponse> ();

            var claimBatchList = new List<ClaimBatchDisplayNameDto> ( response.ClaimBatchDisplayNames );
            PagedCollectionViewWrapper.WrapInPagedCollectionView ( claimBatchList );
            IsLoading = false;
        }

        private void HandleGenerateHcc837ProfessionalCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GenerateHcc837ProfessionalResponse> ();

            var relativePath = string.Format (
                "../{0}?{1}={2}&{3}={4}",
                HttpHandlerPaths.BillingModuleHttpHandlerPath,
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.RequestName ),
                HttpUtility.UrlEncode ( HttpHandlerRequestNames.DownloadHealthCareClaim837ProfessionalDocument ),
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.ClaimBatchKey ),
                _currentClaimBatchKey );

            var uri = Application.Current.Host.Source != null ? new Uri ( Application.Current.Host.Source, relativePath ) : new Uri ( "blank:" );
            HtmlPage.Window.Navigate ( uri, "_blank" );
        }

        private void HandleGenerateHcc837ProfessionalException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog (
                ex.Message, "Could not generate health care claim 837 professional message.", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAllClaimBatchesException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not get all Claim batches.", UserDialogServiceOptions.Ok );
        }

        private void HandleResetTestDataCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<ResetTestDataResponse> ();
            GetAllClaimBatches ();
            IsLoading = false;
        }

        private void HandleResetTestDataException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not reset test data.", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
