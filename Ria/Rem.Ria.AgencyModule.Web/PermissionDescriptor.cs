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
using Rem.Ria.AgencyModule.Web.AgencyDashboard;
using Rem.Ria.AgencyModule.Web.AgencyEditor;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.LocationDashboard;
using Rem.Ria.AgencyModule.Web.LocationEditor;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;
using Rem.Ria.AgencyModule.Web.QuickPickers;
using Rem.Ria.AgencyModule.Web.RoleManagement;
using Rem.Ria.AgencyModule.Web.Service;
using Rem.Ria.AgencyModule.Web.StaffEditor;
using Rem.Ria.AgencyModule.Web.StaffSearch;
using Rem.Ria.Infrastructure.Web.Permissions;

namespace Rem.Ria.AgencyModule.Web
{
    /// <summary>
    /// Class for descripting permission.
    /// </summary>
    public class PermissionDescriptor : IPermissionDescriptor
    {
        #region Constants and Fields

        private readonly ResourceList _resources = new ResourceListBuilder ()

            //Basic User Access Permission
            .AddResource<GetDtoRequest<StaffLocationAssignmentDto>> ( InfrastructurePermission.AccessUserInterfacePermission )

            // Agency Permissions
            .AddResource<GetDtoRequest<AgencyAddressAndPhoneDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyPhoneDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyAddressesAndPhonesDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyAliasDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyCharacteristicDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyContactDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyContactsDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyDisplayNameDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyEmailAddressDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyFaqDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyFaqsDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyIdentifierDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyIdentifiersDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencyProfileDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<AgencySummaryDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetAgencyByKeyRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetAgencyNamesByKeywordRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetAgencySummaryByKeyRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetAllAgencyDisplayNamesRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetAllProgramsByAgencyRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GetDtoRequest<ProgramDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<SaveDtoRequest<AgencyAddressAndPhoneDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyPhoneDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyAddressesAndPhonesDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyAliasDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyCharacteristicDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyContactDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyContactsDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyEmailAddressDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyFaqDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyFaqsDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyIdentifierDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyIdentifiersDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencyProfileDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<AgencySummaryDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<CreateNewAgencyRequest> ( AgencyPermission.AgencyEditPermission )
            .AddResource<SaveDtoRequest<ProgramDto>> ( AgencyPermission.AgencyEditPermission )
            .AddResource<DeleteProgramRequest> ( AgencyPermission.AgencyEditPermission )


            // Location Permissions
            .AddResource<GetDtoRequest<LocationDisplayNameDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationSearchResultDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationSummaryDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationAddressAndPhoneDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationPhoneDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationAddressesAndPhonesDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationContactDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationContactsDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationEmailAddressDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationIdentifierDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationIdentifiersDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationOperationScheduleDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationOperationSchedulesDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationProfileDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<LocationWorkHourDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetLocationNamesByKeywordRequest> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetLocationsByAgencyRequest> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetProgramsForLocationKeyRequest> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetDtoRequest<ProgramOfferingDto>> ( AgencyPermission.LocationViewPermission )
            .AddResource<GetProgramOfferingsByLocationRequest> ( AgencyPermission.LocationViewPermission )
            .AddResource<SaveDtoRequest<LocationDisplayNameDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationSearchResultDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationSummaryDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationAddressAndPhoneDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationPhoneDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationAddressesAndPhonesDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationContactDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationContactsDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationEmailAddressDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationIdentifierDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationIdentifiersDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationOperationScheduleDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationOperationSchedulesDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationProfileDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<LocationWorkHourDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<CreateNewLocationRequest> ( AgencyPermission.LocationEditPermission )
            .AddResource<SaveDtoRequest<ProgramOfferingProfileDto>> ( AgencyPermission.LocationEditPermission )
            .AddResource<DeleteProgramOfferingRequest> ( AgencyPermission.LocationEditPermission )

            // Staff Permissions
            .AddResource<GetDtoRequest<StaffAddressDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffAddressSummaryDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffAddressesDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffApprovedLocationDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffCertificationDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffChecklistItemDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffCollegeDegreeDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffCredentialsDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffEventDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffHRDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffIdentifierDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffIdentifierSummaryDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffIdentifiersDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffLanguageDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffLicenseDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffNameDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffPhoneNumberDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffPhoneNumbersDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffPhoneSummaryDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffPhotoDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffProfileDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffSummaryDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<StaffTrainingCourseDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetStaffNamesByKeywordRequest> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetStaffsByAgencyRequest> ( AgencyPermission.StaffViewPermission )

            //TODO: Temporarily set these request permissions
            .AddResource<GetDtoRequest<StaffSystemRolesDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<JobFunctionSystemRolesDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<TaskGroupSystemRolesDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<TaskAndTaskGroupSystemRolesDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<TaskSystemRolesDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<GetDtoRequest<SystemPermissionsDto>> ( AgencyPermission.StaffViewPermission )
            .AddResource<SaveDtoRequest<StaffSystemRolesDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<AddStaffTaskGroupRolesRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<AddStaffTaskRolesRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<ReviseStaffJobFunctionRoleRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<RemoveStaffTaskGroupRoleRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<RemoveStaffTaskRoleRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<RemoveStaffJobFunctionRoleRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffAddressDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffAddressSummaryDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffAddressesDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffApprovedLocationDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffCertificationDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffChecklistItemDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffCollegeDegreeDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffCredentialsDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffEventDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffHRDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffIdentifierDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffIdentifierSummaryDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffIdentifiersDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffLanguageDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffLicenseDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffLocationAssignmentDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffNameDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffPhoneNumberDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffPhoneNumbersDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffPhoneSummaryDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffPhotoDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffProfileDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffSummaryDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<SaveDtoRequest<StaffTrainingCourseDto>> ( AgencyPermission.StaffEditPermission )
            .AddResource<CreateNewStaffRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<LinkSystemAccountRequest> ( AgencyPermission.StaffEditPermission )
            .AddResource<CreateSystemAccountRequest> ( AgencyPermission.StaffEditPermission )

            // Lab Results Permissions
            .AddResource<SaveLabResultRequest> ( AgencyPermission.LabResultViewPermission )
            .AddResource<GetAgencyStaffsByKeywordRequest> ( AgencyPermission.LabResultEditPermission )

            //TODO: Role Management Permission: Temporarily using Agency permissions
            .AddResource<GetDtoRequest<SystemRoleDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<SaveDtoRequest<SystemRoleDto>> ( AgencyPermission.AgencyViewPermission )
            .AddResource<CreateSystemRoleRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<DeleteJobFunctionSystemRoleRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GrantSystemRoleRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<RevokeSystemRoleRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<CloneSystemRoleRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<RenameSystemRoleRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<GrantSystemPermissionRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<RevokeSystemPermissionRequest> ( AgencyPermission.AgencyViewPermission )
            .AddResource<DeleteTaskOrTaskGroupRequest> ( AgencyPermission.AgencyViewPermission );

        #endregion

        // Meaningful Use Objectives Permissions

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
