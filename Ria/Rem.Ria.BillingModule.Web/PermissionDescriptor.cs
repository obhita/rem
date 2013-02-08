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
using Rem.Infrastructure.Service;
using Rem.Ria.BillingModule.Web.BillingAdministrationDashboard;
using Rem.Ria.BillingModule.Web.BillingOfficeEditor;
using Rem.Ria.BillingModule.Web.Common;
using Rem.Ria.BillingModule.Web.PayorEditor;
using Rem.Ria.BillingModule.Web.Service;
using Rem.Ria.Infrastructure.Web.Permissions;

namespace Rem.Ria.BillingModule.Web
{
    /// <summary>
    /// Class for describing client permission.
    /// </summary>
    public class PermissionDescriptor : IPermissionDescriptor
    {
        #region Constants and Fields

        private readonly ResourceList _resources = new ResourceListBuilder ()
            .AddResource<GetClaimBatchDisplayNamesByClaimBatchStatusRequest> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<GenerateHcc837ProfessionalRequest> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<SendToBillingRequest> ( BillingPermission.BillingWorkspaceViewPermission )
            .AddResource<ResetTestDataRequest> ( BillingPermission.BillingWorkspaceViewPermission )

            //Billing Office Permissions
            .AddResource<GetDtoRequest<BillingOfficeDto>> ( BillingPermission.BillingOfficeViewPermission )
            .AddResource<SaveDtoRequest<BillingOfficeProfileDto>> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<SaveDtoRequest<BillingOfficeAddressesDto>> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<SaveDtoRequest<BillingOfficePhonesDto>> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<GetDtoRequest<BillingOfficeSummaryDto>> ( BillingPermission.BillingOfficeViewPermission )
            .AddResource<GetDtoRequest<BillingOfficeProfileDto>> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<GetDtoRequest<BillingOfficeAddressesDto>> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<GetDtoRequest<BillingOfficePhonesDto>> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<CreateBillingOfficeRequest> ( BillingPermission.BillingOfficeEditPermission )
            .AddResource<CheckBillingOfficeExistsByAgencyKeyRequest> ( InfrastructurePermission.AccessUserInterfacePermission )
            .AddResource<GetBillingOfficeSummaryByAgencyKeyRequest> ( BillingPermission.BillingOfficeViewPermission )

            //Payor Permissions
            .AddResource<GetDtoRequest<PayorDto>> ( BillingPermission.PayorViewPermission )
            .AddResource<GetDtoRequest<PayorProfileDto>> ( BillingPermission.PayorEditPermission )
            .AddResource<GetDtoRequest<PayorAddressesDto>> ( BillingPermission.PayorEditPermission )
            .AddResource<GetDtoRequest<PayorPhoneNumbersDto>> ( BillingPermission.PayorEditPermission )
            .AddResource<SaveDtoRequest<PayorProfileDto>> ( BillingPermission.PayorEditPermission )
            .AddResource<SaveDtoRequest<PayorAddressesDto>> ( BillingPermission.PayorEditPermission )
            .AddResource<SaveDtoRequest<PayorPhoneNumbersDto>> ( BillingPermission.PayorEditPermission )
            .AddResource<GetPayorsByBillingOfficeKeyRequest> ( BillingPermission.PayorViewPermission )

            //Payor type : share permission with payor
            .AddResource<GetDtoRequest<PayorTypeDto>> ( BillingPermission.PayorEditPermission )
            .AddResource<GetPayorTypesByBillingOfficeKeyRequest> ( BillingPermission.PayorViewPermission )
            //.AddResource<GetDtoRequest<PayorTypeDto>> ( BillingPermission.PayorViewPermission )
            .AddResource<SaveDtoRequest<PayorTypeDto>> ( BillingPermission.PayorEditPermission )

            //Billing Administration Dashboard Permissions
            .AddResource<GetClaimBatchListSummaryRequest> ( BillingPermission.BillingOfficeViewPermission )
            .AddResource<GetOpenClaimSummaryListByBillingOfficeKeyRequest> ( BillingPermission.BillingOfficeViewPermission );

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
