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

using Pillar.Security.AccessControl;
using Rem.Ria.BillingModule.BillingAdministrationDashboard;
using Rem.Ria.BillingModule.BillingAdministrationWorkspace;
using Rem.Ria.BillingModule.BillingOfficeEditor;
using Rem.Ria.BillingModule.PayorEditor;
using Rem.Ria.Infrastructure.Web.Permissions;

namespace Rem.Ria.BillingModule
{
    /// <summary>
    /// Class for describing client permission.
    /// </summary>
    public class ClientPermissionDescriptor : IPermissionDescriptor
    {
        #region Constants and Fields

        private readonly ResourceList _resources = new ResourceListBuilder ()
            .AddResource<BillingWorkspaceView> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<BillingWorkspaceViewModel> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<ClaimBatchListView> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<ClaimBatchListViewModel> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<VisitForBillingListView> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<VisitForBillingListViewModel> ( BillingPermission.BillingWorkspaceViewPermission )

            //Billing Office Permissions
            .AddResource<BillingOfficeEditorViewModel> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<BillingOfficeEditorView> ( BillingPermission.BillingOfficeViewPermission )
            .AddResource<BillingAdministrationWorkspaceView> ( BillingPermission.BillingOfficeViewPermission )
            .AddResource<BillingAdministrationWorkspaceViewModel> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<BillingAdministrationDashboardView> ( BillingPermission.BillingOfficeViewPermission )
            .AddResource<BillingAdministrationDashboardViewModel> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<CreateBillingOfficeButtonView> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<CreateBillingOfficeButtonViewModel> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<CreateBillingOfficeView> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<CreateBillingOfficeViewModel> ( BillingPermission.BillingOfficeEditPermission )

            //Payor Permissions
            .AddResource<PayorDashboardView> ( BillingPermission.PayorViewPermission )
            .AddResource<PayorDashboardViewModel> ( BillingPermission.PayorViewPermission )
            .AddResource<PayorEditorView> ( BillingPermission.PayorViewPermission )
            .AddResource<PayorListView> ( BillingPermission.PayorViewPermission )
            .AddResource<PayorEditorViewModel> ( BillingPermission.PayorEditPermission )
            .AddResource<PayorListViewModel> ( BillingPermission.PayorEditPermission )

            //Payor type -- share permissions with payor
            .AddResource<PayorTypeEditorView> ( BillingPermission.PayorViewPermission )
            .AddResource<PayorTypeListView> ( BillingPermission.PayorViewPermission )
            .AddResource<PayorTypeEditorViewModel> ( BillingPermission.PayorEditPermission )
            .AddResource<PayorTypeListViewModel> ( BillingPermission.PayorEditPermission )

            //Claims Permissions
            .AddResource<ClaimsDashboardView> ( BillingPermission.ClaimsViewPermission )
            .AddResource<ClaimsDashboardViewModel> ( BillingPermission.ClaimsEditPermission )
            .AddResource<ClaimBatchDashboardListView> ( BillingPermission.ClaimsViewPermission )
            .AddResource<ClaimBatchDashboardListViewModel> ( BillingPermission.ClaimsEditPermission )
            .AddResource<ClaimErrorsListView> ( BillingPermission.ClaimsViewPermission )
            .AddResource<ClaimErrorsListViewModel> ( BillingPermission.ClaimsEditPermission )
            .AddResource<ClaimOnHoldListView> ( BillingPermission.ClaimsViewPermission )
            .AddResource<ClaimOnHoldListViewModel> ( BillingPermission.ClaimsEditPermission );

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
