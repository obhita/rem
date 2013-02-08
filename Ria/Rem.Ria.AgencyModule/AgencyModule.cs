using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Bootstrapper;
using Rem.Ria.AgencyModule.AgencyDashboard;
using Rem.Ria.AgencyModule.AgencyEditor;
using Rem.Ria.AgencyModule.AgencyWorkspace;
using Rem.Ria.AgencyModule.LocationDashboard;
using Rem.Ria.AgencyModule.LocationEditor;
using Rem.Ria.AgencyModule.LocationWorkspace;
using Rem.Ria.AgencyModule.Login;
using Rem.Ria.AgencyModule.MUObjectivesWorkspace;
using Rem.Ria.AgencyModule.ProgramEditor;
using Rem.Ria.AgencyModule.ProgramOfferingEditor;
using Rem.Ria.AgencyModule.QuickPickers;
using Rem.Ria.AgencyModule.RoleManagement;
using Rem.Ria.AgencyModule.StaffEditor;
using Rem.Ria.AgencyModule.StaffSearch;
using Rem.Ria.AgencyModule.StaffWorkspace;
using Rem.Ria.Infrastructure;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.AgencyModule
{
    /// <summary>
    /// AgencyModule class.
    /// </summary>
    public class AgencyModule : IModule
    {
        #region Constants and Fields

        private const string LoginControlRegion = "HeaderMiddleRegion";
        private readonly IAccessControlManager _accessControlManager;
        private readonly IUnityContainer _container;
        private readonly IMetadataService _metadataService;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="container">The container.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="metadataService">The metadata service.</param>
        public AgencyModule (
            IRegionManager regionManager,
            IUnityContainer container,
            IAccessControlManager accessControlManager,
            IMetadataService metadataService )
        {
            _regionManager = regionManager;
            _container = container;
            _accessControlManager = accessControlManager;
            _metadataService = metadataService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize ()
        {
            RegisterServices ();

            RegisterAgencyWorkspaceScreens ();
            RegisterHomePageScreens ();
            RegisterRoleManagementWorkspaceScreens ();
            RegisterRuleCollections ();
        }

        #endregion

        #region Methods

        private void RegisterAgencyWorkspaceScreens ()
        {
            _container.RegisterType<object, AgencyWorkspaceView> ( "AgencyWorkspaceView" );
            _container.RegisterType<object, AgencyDashboardView> ( "AgencyDashboardView" );
            _container.RegisterType<object, ProgramListView> ( "ProgramListView" );
            _container.RegisterType<object, EditProgramView> ( "EditProgramView" );

            _container.RegisterType<object, AgencyEditorView> ( "AgencyEditorView" );
            _container.RegisterType<object, StaffWorkspaceView> ( "StaffWorkspaceView" );
            _container.RegisterType<object, StaffEditorView> ( "StaffEditorView" );

            _container.RegisterType<object, LocationWorkspaceView> ( "LocationWorkspaceView" );
            _container.RegisterType<object, LocationDashboardView> ( "LocationDashboardView" );
            _container.RegisterType<object, ProgramOfferingListView> ( "ProgramOfferingListView" );
            _container.RegisterType<object, ProgramOfferingEditorView> ( "ProgramOfferingEditorView" );

            _container.RegisterType<object, LocationEditorView> ( "LocationEditorView" );

            _container.RegisterType<object, UploadLabResultView> ( "UploadLabResultView" );
            _container.RegisterType<object, MUObjectivesView> ( "MUObjectivesView" );
            _container.RegisterType<object, SaveLabResultView> ( "SaveLabResultView" );
            _regionManager.RegisterViewWithRegion (
                "StaffSearch",
                () => _container.Resolve<StaffSearchView> () );

            _regionManager.RegisterViewWithRegion (
                "AgencyQuickPicker",
                () => _container.Resolve<AgencyQuickPickerView> () );
            _regionManager.RegisterViewWithRegion (
                "LocationQuickPicker",
                () => _container.Resolve<LocationQuickPickerView> () );
            _regionManager.RegisterViewWithRegion (
                "StaffQuickPicker",
                () => _container.Resolve<StaffQuickPickerView> () );
            _container.RegisterType<object, UploadPhotoView> ( "UploadPhotoView" );

            _container.RegisterType<object, MainAgencyQuickPickersView> ( "MainAgencyQuickPickersView", new ContainerControlledLifetimeManager () );
        }

        private void RegisterHomePageScreens ()
        {
            _regionManager.RegisterViewWithRegion ( LoginControlRegion, () => _container.Resolve<LoginInformationView> () );
        }

        private void RegisterRoleManagementWorkspaceScreens ()
        {
            _container.RegisterType<object, RoleManagementWorkspaceView> ( "RoleManagementWorkspaceView" );
            _container.RegisterType<object, JobFunctionListView> ( "JobFunctionListView" );
            _container.RegisterType<object, EditJobFunctionView> ( "EditJobFunctionView" );
            _container.RegisterType<object, TaskAndTakGroupListView> ( "TaskAndTakGroupListView" );
            _container.RegisterType<object, EditTaskAndTaskGroupView> ( "EditTaskAndTaskGroupView" );
        }

        private void RegisterRuleCollections ()
        {
            _container.RegisterType<IRuleCollection<EditProgramViewModel>, EditProgramViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
            _container.RegisterType<IRuleCollection<ProgramOfferingEditorViewModel>, ProgramOfferingEditorViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
        }

        private void RegisterServices ()
        {
            _accessControlManager.RegisterPermissionDescriptor (
                new ClientPermissionDescriptor () );
            _metadataService.LoadMetadataForModule ( this );

            var genericsToApply = KnownTypeHelper.GetGenerics ( typeof( Const ).Assembly );
            KnownTypeHelper.RegisterNonGenericRequestsAndResponses ( typeof( AgencyModule ).Assembly );
            KnownTypeHelper.RegisterGenerics ( genericsToApply, typeof( AgencyModule ).Assembly );
        }

        #endregion
    }
}
