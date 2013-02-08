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

namespace Rem.Ria.BillingModule
{
    /// <summary>
    /// View Model for VisitForBillingList class.
    /// </summary>
    public class VisitForBillingListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly PagedCollectionViewWrapper<VisitForBillingDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _currentVisitKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitForBillingListViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public VisitForBillingListViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitForBillingListViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public VisitForBillingListViewModel (
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

            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<VisitForBillingDto> ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SendToBilling = commandFactoryHelper.BuildDelegateCommand<object> (
                () => SendToBilling, ExecuteSendToBillingCommand, CanExecuteSendToBilling );
            SendToBillingDirectly = commandFactoryHelper.BuildDelegateCommand<object> (
                () => SendToBillingDirectly, ExecuteSendToBillingCommandDirectly, CanExecuteSendToBilling );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<VisitForBillingDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets the send to billing.
        /// </summary>
        public ICommand SendToBilling { get; private set; }

        /// <summary>
        /// Gets the send to billing directly.
        /// </summary>
        public ICommand SendToBillingDirectly { get; private set; }

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
            var visitList = new List<VisitForBillingDto>
                {
                    new VisitForBillingDto
                        {
                            DisplayName = "Established Patient - Primary Care - Adult",
                            Key = 13000,
                            PatientName = "Henry Levin",
                            Status = "Reviewed",
                            VisitDate = DateTime.Parse ( "2010-10-10" )
                        },
                     new VisitForBillingDto
                        {
                            DisplayName = "Established Patient - Primary Care - Adult",
                            Key = 13001,
                            PatientName = "Henry Levin",
                            Status = "Reviewed",
                            VisitDate = DateTime.Parse ( "2010-10-11" )
                        },
                    new VisitForBillingDto
                        {
                            DisplayName = "Established Patient - Primary Care - Adult",
                            Key = 421000,
                            PatientName = "Payne Feit",
                            Status = "Reviewed",
                            VisitDate = DateTime.Parse ( "2010-10-14" )
                        },
                    new VisitForBillingDto
                        {
                            DisplayName = "New Established Patient - Primary Care - Adult",
                            Key = 100010081003,
                            PatientName = "Tad Young",
                            Status = "Reviewed",
                            VisitDate = DateTime.Parse ( "2011-03-15" )
                        }
                };

            PagedCollectionViewWrapper.WrapInPagedCollectionView ( visitList );
            IsLoading = false;
        }

        private bool CanExecuteSendToBilling ( object value )
        {
            return true;
        }

        private void ExecuteSendToBillingCommand ( object value )
        {
            _currentVisitKey = ( long )value;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SendToBillingRequest { VisitKey = ( long )value, VisBus = true } );
            requestDispatcher.ProcessRequests ( HandleSendToBillingCompleted, HandleSendToBillingException );
        }

        private void ExecuteSendToBillingCommandDirectly ( object value )
        {
            _currentVisitKey = ( long )value;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SendToBillingRequest { VisitKey = ( long )value, VisBus = false } );
            requestDispatcher.ProcessRequests ( HandleSendToBillingCompleted, HandleSendToBillingException );
        }

        private void HandleSendToBillingCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<SendToBillingResponse> ();
            IsLoading = false;
        }

        private void HandleSendToBillingException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not send the visit information to billing module.", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
