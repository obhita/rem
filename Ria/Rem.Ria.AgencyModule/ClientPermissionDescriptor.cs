using Pillar.Security.AccessControl;
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
using Rem.Ria.Infrastructure.Web.Permissions;

namespace Rem.Ria.AgencyModule
{
    /// <summary>
    /// Class for descripting client permission.
    /// </summary>
    public class ClientPermissionDescriptor : IPermissionDescriptor
    {
        #region Constants and Fields

        private readonly ResourceList _resources = new ResourceListBuilder ()

            // Agency Permissions
            .AddResource<AgencyEditorView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<AgencyWorkspaceView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<MainAgencyQuickPickersView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<AgencyQuickPickerView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<AgencyDashboardView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<ProgramListView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<EditProgramView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<AgencyEditorViewModel> ( AgencyPermission.AgencyEditPermission )
            .AddResource<AgencyWorkspaceViewModel> ( AgencyPermission.AgencyEditPermission )
            .AddResource<MainAgencyQuickPickersViewModel> ( AgencyPermission.AgencyEditPermission )
            .AddResource<AgencyQuickPickerViewModel> ( AgencyPermission.AgencyEditPermission )
            .AddResource<AgencyDashboardViewModel> ( AgencyPermission.AgencyEditPermission )
            .AddResource<ProgramListViewModel> ( AgencyPermission.AgencyEditPermission )
            .AddResource<EditProgramViewModel> ( AgencyPermission.AgencyEditPermission )

            // Role Management Permissions
            //TODO: Temporarily using AgencyViewPermission
            .AddResource<RoleManagementWorkspaceView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<JobFunctionListView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<EditJobFunctionView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<TaskAndTakGroupListView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<EditTaskAndTaskGroupView> ( AgencyPermission.AgencyViewPermission )
            .AddResource<RoleManagementWorkspaceViewModel> ( AgencyPermission.AgencyViewPermission )
            .AddResource<JobFunctionListViewModel> ( AgencyPermission.AgencyViewPermission )
            .AddResource<EditJobFunctionViewModel> ( AgencyPermission.AgencyViewPermission )
            .AddResource<TaskAndTakGroupListViewModel> ( AgencyPermission.AgencyViewPermission )
            .AddResource<EditTaskAndTaskGroupViewModel> ( AgencyPermission.AgencyViewPermission )

            // Location Permissions
            .AddResource<LocationEditorView> ( AgencyPermission.LocationViewPermission )
            .AddResource<LocationWorkspaceView> ( AgencyPermission.LocationViewPermission )
            .AddResource<LocationQuickPickerView> ( AgencyPermission.LocationViewPermission )
            .AddResource<LocationDashboardView> ( AgencyPermission.LocationViewPermission )
            .AddResource<ProgramOfferingListView> ( AgencyPermission.LocationViewPermission )
            .AddResource<ProgramOfferingEditorView> ( AgencyPermission.LocationViewPermission )
            .AddResource<LocationEditorViewModel> ( AgencyPermission.LocationEditPermission )
            .AddResource<LocationWorkspaceViewModel> ( AgencyPermission.LocationEditPermission )
            .AddResource<LocationQuickPickerViewModel> ( AgencyPermission.LocationEditPermission )
            .AddResource<LocationDashboardViewModel> ( AgencyPermission.LocationEditPermission )
            .AddResource<ProgramOfferingListViewModel> ( AgencyPermission.LocationEditPermission )
            .AddResource<ProgramOfferingEditorViewModel> ( AgencyPermission.LocationEditPermission )

            // Staff Permissions
            .AddResource<StaffEditorView> ( AgencyPermission.StaffViewPermission )
            .AddResource<StaffWorkspaceView> ( AgencyPermission.StaffViewPermission )
            .AddResource<StaffQuickPickerView> ( AgencyPermission.StaffViewPermission )
            .AddResource<UploadPhotoView> ( AgencyPermission.StaffViewPermission )
            .AddResource<StaffSearchView> ( AgencyPermission.StaffViewPermission )
            .AddResource<StaffEditorViewModel> ( AgencyPermission.StaffEditPermission )
            .AddResource<StaffWorkspaceViewModel> ( AgencyPermission.StaffEditPermission )
            .AddResource<StaffQuickPickerViewModel> ( AgencyPermission.StaffEditPermission )
            .AddResource<UploadPhotoViewModel> ( AgencyPermission.StaffEditPermission )
            .AddResource<StaffSearchViewModel> ( AgencyPermission.StaffEditPermission )

            // Lab Results Permissions
            .AddResource<SaveLabResultView> ( AgencyPermission.LabResultViewPermission )
            .AddResource<SaveLabResultViewModel> ( AgencyPermission.LabResultEditPermission )
            .AddResource<UploadLabResultViewModel> ( AgencyPermission.UploadLabResultPermission )
            .AddResource<UploadLabResultView> ( AgencyPermission.UploadLabResultPermission )

            // Meaningful Use Objectives Permissions
            .AddResource<MUObjectivesView> ( AgencyPermission.MeaningfulUseObjectivesViewPermission )
            .AddResource<MUObjectivesViewModel> ( AgencyPermission.MeaningfulUseObjectivesViewPermission )

            //Login Information Permissions
            .AddResource<LoginInformationViewModel> ( AgencyPermission.LoginInformationViewPermission );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the resources.
        /// </summary>
        public ResourceList Resources
        {
            get { return _resources; }
        }

        #endregion
    }
}
