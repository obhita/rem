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

using System.Windows.Input;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Web.Permissions;

namespace Rem.Ria.Shell
{
    /// <summary>
    /// Class for descripting client permission.
    /// </summary>
    public class ClientPermissionDescriptor : IPermissionDescriptor
    {
        // Define resource as hierarchy. 
        // Define resource as hierarchy. The account must have the permission for the leaf resource to access the leaf. But not necessarily have the permission for the trunk.

        #region Constants and Fields

        private readonly ResourceList _resources = new ResourceListBuilder ()
            .AddResource<ShellViewModel> (
                InfrastructurePermission.AccessUserInterfacePermission,
                rlb =>
                rlb.AddResource (
                    PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenAgencyWorkspaceCommand ),
                    AgencyPermission.AgencyViewPermission )
                    .AddResource(
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand>(p => p.OpenMessageCenterWorkspaceCommand),
                        PatientPermission.FrontDeskDashboardViewPermission)
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenPatientAccessHistoryWorkspaceCommand ),
                        PatientPermission.PatientAccessHistoryViewPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenInteroperabilityWorkspaceCommand ),
                        PatientPermission.InteroperabilityWorkspaceViewPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenPatientListCommand ),
                        PatientPermission.PatientListViewPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenPatientReminderCommand ),
                        PatientPermission.PatientRemindersViewPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenMuObjectivesCommand ),
                        AgencyPermission.MeaningfulUseObjectivesViewPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenCdsEditorCommand ),
                        PatientPermission.CdsRulesViewPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenReportsWorkspaceCommand ),
                        ReportPermission.ReportAccessPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenRoleManagementWorkspaceCommand ),
                        AgencyPermission.AgencyViewPermission )
                    .AddResource (
                        PropertyUtil.ExtractPropertyName<ShellViewModel, ICommand> ( p => p.OpenBillingWorkspaceCommand ),
                        BillingPermission.BillingWorkspaceViewPermission )
            );

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
