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
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Bootstrapper;
using Rem.Ria.BillingModule.BillingAdministrationDashboard;
using Rem.Ria.BillingModule.BillingAdministrationWorkspace;
using Rem.Ria.BillingModule.BillingOfficeEditor;
using Rem.Ria.BillingModule.PayorEditor;
using Rem.Ria.BillingModule.Web.BillingOfficeEditor;
using Rem.Ria.Infrastructure;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.View;

namespace Rem.Ria.BillingModule
{
    /// <summary>
    /// The Billing Module class.
    /// </summary>
    public class BillingModule : IModule
    {
        #region Constants and Fields

        private readonly IAccessControlManager _accessControlManager;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private long _agencyKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingModule"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="container">The container.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="currentUserContextService">The current user context service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BillingModule (
            IAccessControlManager accessControlManager,
            IUnityContainer container,
            IRegionManager regionManager,
            ICurrentUserContextService currentUserContextService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IEventAggregator eventAggregator )
        {
            _accessControlManager = accessControlManager;
            _container = container;
            _regionManager = regionManager;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _eventAggregator = eventAggregator;

            //This is temporary until the main navigation dropdown is fixed
            currentUserContextService.RegisterForContext (
                ( u, b ) =>
                    {
                        if ( u != null )
                        {
                            _agencyKey = u.Agency.Key;
                            HandleCheckBillingOffice ();
                        }
                    },
                true );
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the check billing office.
        /// </summary>
        public void HandleCheckBillingOffice ()
        {

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new CheckBillingOfficeExistsByAgencyKeyRequest { AgencyKey = _agencyKey } );
            requestDispatcher.ProcessRequests (
                responses =>
                    {
                        System.Threading.Thread.Sleep ( 5000 );
                        var response = responses.Get<CheckBillingOfficeExistsByAgencyKeyResponse> ();
                        if ( response.HasBillingOffice )
                        {
                            _eventAggregator.GetEvent<HasBillingOfficeEvent> ().Publish ( new EventArgs () );
                        }
                    },
                info => { } );
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize ()
        {
            RegisterServices ();

            RegisterBillingWorkspaceScreens ();

            RegisterRuleCollections ();
        }

        #endregion

        #region Methods

        private void RegisterBillingWorkspaceScreens ()
        {
            _container.RegisterType<object, BillingWorkspaceView> ( "BillingWorkspaceView" );
            _container.RegisterType<object, ClaimBatchListView> ( "ClaimBatchListView" );
            _container.RegisterType<object, VisitForBillingListView> ( "VisitForBillingListView" );
            _container.RegisterType<object, BillingOfficeEditorView> ( "BillingOfficeEditorView" );
            _container.RegisterType<object, CreateBillingOfficeView> ( "CreateBillingOfficeView" );
            _container.RegisterType<object, BillingAdministrationWorkspaceView> ( "BillingAdministrationWorkspaceView" );
            _container.RegisterType<object, BillingAdministrationDashboardView> ( "BillingAdministrationDashboardView" );
            _container.RegisterType<object, PayorEditorView> ( "PayorEditorView" );
            _container.RegisterType<object, PayorListView> ( "PayorListView" );
            _container.RegisterType<object, PayorTypeEditorView> ( "PayorTypeEditorView" );
            _container.RegisterType<object, PayorTypeListView> ( "PayorTypeListView" );
            _container.RegisterType<object, PayorDashboardView> ( "PayorDashboardView" );
            _container.RegisterType<object, ClaimsDashboardView> ( "ClaimsDashboardView" );
            _container.RegisterType<object, ClaimBatchDashboardListView> ( "ClaimBatchDashboardListView" );
            _container.RegisterType<object, ClaimErrorsListView> ( "ClaimErrorsListView" );
            _container.RegisterType<object, ClaimOnHoldListView> ( "ClaimOnHoldListView" );

            _regionManager.RegisterViewWithRegion (
                "AgencyToolsRegion", () => new CreateBillingOfficeButtonView ( _container.Resolve<CreateBillingOfficeButtonViewModel> () ) );
        }

        private void RegisterRuleCollections ()
        {
            _container.RegisterType<IRuleCollection<PayorEditorViewModel>, PayorEditorViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
            _container.RegisterType<IRuleCollection<PayorTypeEditorViewModel>, PayorTypeEditorViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
        }

        private void RegisterServices ()
        {
            var genericsToApply = KnownTypeHelper.GetGenerics ( typeof( Const ).Assembly );
            KnownTypeHelper.RegisterNonGenericRequestsAndResponses ( typeof( BillingModule ).Assembly );
            KnownTypeHelper.RegisterGenerics ( genericsToApply, typeof( BillingModule ).Assembly );

            _accessControlManager.RegisterPermissionDescriptor (
                new ClientPermissionDescriptor () );
        }

        #endregion
    }
}
