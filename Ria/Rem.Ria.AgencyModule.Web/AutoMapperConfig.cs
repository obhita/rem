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

using Pillar.Common.Bootstrapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service.DataTransferObject.Mapping;
using Rem.Ria.AgencyModule.Web.AgencyDashboard;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.LocationEditor;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;

namespace Rem.Ria.AgencyModule.Web
{
    /// <summary>
    /// AutoMapperConfig class.
    /// </summary>
    public class AutoMapperConfig : IBootstrapperTask
    {
        #region Public Methods

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute ()
        {
            CreateProgramsDtoConfig ();

            CreateStaffSearchResultDtoConfig ();
            CreateAgencyProfileConfig ();
            CreateAgencyDtoConfig ();
            CreateLocationDtoConfig ();
            CreateLocationSummaryDtoConfig ();
            CreateStaffCredentialsDto ();
            CreateStaffDtoConfig ();

            CreateStaffSummaryAddressDtoConfig ();
            CreateStaffSummaryPhoneDtoConfig ();
            CreateStaffSummaryDtoConfig ();

            CreateAgencyAddressConfig ();
            CreateAgencyPhoneConfig ();
            CreateAgencyContactConfig ();
            CreateAgencyIdentifierConfig ();
            CreateAgencyCharacteristicsConfig ();
            CreateAgencyFrequentlyAskedQuestionsConfig ();
            CreateAgencySummaryConfig ();

            CreateProgramOfferingConfig ();
        }

        #endregion

        #region Methods

        private static void CreateAgencyAddressConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Staff, StaffPhoneNumbersDto> ();
            AutoMapperSetup.CreateMapToEditableDto<StaffPhone, StaffPhoneNumberDto> ()
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.Phone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.Phone.PhoneExtensionNumber ) );

            AutoMapperSetup.CreateMapToEditableDto<Staff, StaffAddressesDto> ();
            AutoMapperSetup.CreateMapToEditableDto<StaffAddress, StaffAddressDto> ()
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.Address.FirstStreetAddress ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.Address.SecondStreetAddress ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.Address.CityName ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.Address.CountyArea ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.Address.StateProvince ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.Address.PostalCode.Code ) );

            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencyAddressesAndPhonesDto> ()
                .ForMember ( dest => dest.AddressesAndPhones, opt => opt.MapFrom ( src => src.AddressesAndPhones ) );

            AutoMapperSetup.CreateMapToEditableDto<AgencyAddressAndPhone, AgencyAddressAndPhoneDto> ()
                .ForMember ( dest => dest.AgencyAddressType, opt => opt.MapFrom ( src => src.AgencyAddress.AgencyAddressType ) )
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.AgencyAddress.Address.FirstStreetAddress ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.AgencyAddress.Address.SecondStreetAddress ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.AgencyAddress.Address.CityName ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.AgencyAddress.Address.CountyArea ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.AgencyAddress.Address.StateProvince ) )
                .ForMember ( dest => dest.Country, opt => opt.MapFrom ( src => src.AgencyAddress.Address.Country ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.AgencyAddress.Address.PostalCode.Code ) );
        }

        private static void CreateAgencyCharacteristicsConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<AgencyCharacteristic, AgencyCharacteristicDto> ();
        }

        private static void CreateAgencyContactConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencyContactsDto> ();
            AutoMapperSetup.CreateMapToEditableDto<AgencyContact, AgencyContactDto> ();
        }

        private static void CreateAgencyDtoConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencyProfileDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencyDto> ()
                .ForMember ( dest => dest.AgencyProfile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.AddressesAndPhones, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.AgencyIdentifiers, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.AgencyContacts, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.AgencyFrequentlyAskedQuestions, opt => opt.MapFrom ( src => src ) );
        }

        private static void CreateAgencyFrequentlyAskedQuestionsConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencyFaqsDto> ().ForMember (
                dest => dest.AgencyFaqs, opt => opt.MapFrom ( src => src.AgencyFrequentlyAskedQuestions ) );
            AutoMapperSetup.CreateMapToEditableDto<AgencyFrequentlyAskedQuestion, AgencyFaqDto> ();
        }

        private static void CreateAgencyIdentifierConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Staff, StaffIdentifiersDto> ();
            AutoMapperSetup.CreateMapToEditableDto<StaffIdentifier, StaffIdentifierDto> ()
                .ForMember (
                    dest => dest.StartDate,
                    opt =>
                    opt.MapFrom (
                        src => src.EffectiveDateRange != null ? src.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt =>
                    opt.MapFrom ( src => src.EffectiveDateRange != null ? src.EffectiveDateRange.EndDate : null ) );
            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencyIdentifiersDto> ();
            AutoMapperSetup.CreateMapToEditableDto<AgencyIdentifier, AgencyIdentifierDto> ()
                .ForMember (
                    dest => dest.StartDate,
                    opt =>
                    opt.MapFrom (
                        src => src.EffectiveDateRange != null ? src.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt =>
                    opt.MapFrom ( src => src.EffectiveDateRange != null ? src.EffectiveDateRange.EndDate : null ) );
        }

        private static void CreateAgencyPhoneConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<AgencyPhone, AgencyPhoneDto> ()
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.Phone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.Phone.PhoneExtensionNumber ) )
                .ForMember ( dest => dest.AgencyPhoneType, opt => opt.MapFrom ( src => src.AgencyPhoneType ) );
        }

        private static void CreateAgencyProfileConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<AgencyEmailAddress, AgencyEmailAddressDto> ()
                .ForMember ( dest => dest.EmailAddress, opt => opt.MapFrom ( src => src.EmailAddress == null ? null : src.EmailAddress.Address ) );

            AutoMapperSetup.CreateMapToEditableDto<AgencyAlias, AgencyAliasDto> ();

            AutoMapperSetup.CreateMapToEditableDto<Agency, AgencyProfileDto> ()
                .ForMember (
                    dest => dest.StartDate,
                    opt => opt.MapFrom ( src => src.AgencyProfile.EffectiveDateRange != null ? src.AgencyProfile.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt => opt.MapFrom ( src => src.AgencyProfile.EffectiveDateRange != null ? src.AgencyProfile.EffectiveDateRange.EndDate : null ) )
                .ForMember ( dest => dest.LegalName, opt => opt.MapFrom ( src => src.AgencyProfile.AgencyName.LegalName ) )
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.AgencyProfile.AgencyName.DisplayName ) )
                .ForMember ( dest => dest.DoingBusinessAsName, opt => opt.MapFrom ( src => src.AgencyProfile.AgencyName.DoingBusinessAsName ) )
                .ForMember ( dest => dest.AgencyType, opt => opt.MapFrom ( src => src.AgencyProfile.AgencyType ) )
                .ForMember ( dest => dest.GeographicalRegion, opt => opt.MapFrom ( src => src.AgencyProfile.GeographicalRegion ) )
                .ForMember ( dest => dest.WebsiteUrlName, opt => opt.MapFrom ( src => src.AgencyProfile.WebsiteUrlName ) );
        }

        private static void CreateAgencySummaryConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencySummaryDto> ()
                .ForMember ( dest => dest.AddressesAndPhones, opt => opt.MapFrom ( src => src.AddressesAndPhones ) )
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.AgencyProfile.AgencyName.DisplayName ) );

            AutoMapperSetup.CreateMapToAbstractDto<Agency, AgencyDisplayNameDto> ()
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.AgencyProfile.AgencyName.DisplayName ) );
        }

        private static void CreateLocationDtoConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Location, LocationProfileDto> ()
                .ForMember (
                    dest => dest.StartDate,
                    opt =>
                    opt.MapFrom (
                        src => src.LocationProfile.EffectiveDateRange != null ? src.LocationProfile.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt =>
                    opt.MapFrom ( src => src.LocationProfile.EffectiveDateRange != null ? src.LocationProfile.EffectiveDateRange.EndDate : null ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.LocationProfile.LocationName.Name ) )
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.LocationProfile.LocationName.DisplayName ) )
                .ForMember ( dest => dest.WebsiteUrlName, opt => opt.MapFrom ( src => src.LocationProfile.WebsiteUrlName ) )
                .ForMember ( dest => dest.GeographicalRegion, opt => opt.MapFrom ( src => src.LocationProfile.GeographicalRegion ) );

            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationContactsDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationIdentifiersDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationOperationSchedulesDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationDto> ()
                .ForMember ( dest => dest.LocationProfile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.LocationAddressesAndPhones, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.LocationIdentifiers, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.LocationContacts, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.LocationOperationSchedules, opt => opt.MapFrom ( src => src ) );
            AutoMapperSetup.CreateMapToEditableDto<LocationEmailAddress, LocationEmailAddressDto> ()
                .ForMember ( dest => dest.EmailAddress, opt => opt.MapFrom ( src => src.EmailAddress == null ? null : src.EmailAddress.Address ) );

            AutoMapperSetup.CreateMapToEditableDto<LocationAddressAndPhone, LocationAddressAndPhoneDto> ()
                .ForMember ( dest => dest.ConfidentialIndicator, opt => opt.MapFrom ( src => src.LocationAddress.ConfidentialIndicator ) )
                .ForMember ( dest => dest.LocationAddressType, opt => opt.MapFrom ( src => src.LocationAddress.LocationAddressType ) )
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.LocationAddress.Address.FirstStreetAddress ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.LocationAddress.Address.SecondStreetAddress ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.LocationAddress.Address.CityName ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.LocationAddress.Address.CountyArea ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.LocationAddress.Address.StateProvince ) )
                .ForMember ( dest => dest.Country, opt => opt.MapFrom ( src => src.LocationAddress.Address.Country ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.LocationAddress.Address.PostalCode.Code ) );

            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationAddressesAndPhonesDto> ();

            AutoMapperSetup.CreateMapToEditableDto<LocationPhone, LocationPhoneDto> ()
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.Phone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.Phone.PhoneExtensionNumber ) );

            AutoMapperSetup.CreateMapToEditableDto<LocationOperationSchedule, LocationOperationScheduleDto> ();
            AutoMapperSetup.CreateMapToEditableDto<LocationIdentifier, LocationIdentifierDto> ()
                .ForMember (
                    dest => dest.StartDate,
                    opt =>
                    opt.MapFrom (
                        src => src.EffectiveDateRange != null ? src.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt =>
                    opt.MapFrom ( src => src.EffectiveDateRange != null ? src.EffectiveDateRange.EndDate : null ) );
            AutoMapperSetup.CreateMapToEditableDto<LocationContact, LocationContactDto> ()
                .ForMember (
                    dest => dest.EffectiveStartDate,
                    opt =>
                    opt.MapFrom (
                        src => src.EffectiveDateRange != null ? src.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EffectiveEndDate,
                    opt =>
                    opt.MapFrom ( src => src.EffectiveDateRange != null ? src.EffectiveDateRange.EndDate : null ) );
            AutoMapperSetup.CreateMapToEditableDto<LocationWorkHour, LocationWorkHourDto> ()
                .ForMember (
                    dest => dest.StartTime, opt => opt.MapFrom ( src => src.WorkHourTimeRange != null ? src.WorkHourTimeRange.StartTime : null ) )
                .ForMember (
                    dest => dest.EndTime, opt => opt.MapFrom ( src => src.WorkHourTimeRange != null ? src.WorkHourTimeRange.EndTime : null ) );
        }

        private static void CreateLocationSummaryDtoConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationDisplayNameDto> ()
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.LocationProfile.LocationName.DisplayName ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.LocationProfile.LocationName.Name ) );

            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationSummaryDto> ()
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.LocationProfile.LocationName.DisplayName ) )
                .ForMember ( dest => dest.LocationAddressesAndPhones, opt => opt.MapFrom ( src => src.LocationAddressesAndPhones ) );

            AutoMapperSetup.CreateMapToAbstractDto<Location, LocationSearchResultDto> ()
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.LocationProfile.LocationName.DisplayName ) );
        }

        private static void CreateProgramOfferingConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<ProgramOffering, ProgramOfferingDto> ()
                .ForMember ( dest => dest.Profile, opt => opt.MapFrom ( src => src ) );
            AutoMapperSetup.CreateMapToEditableDto<ProgramOffering, ProgramOfferingProfileDto> ();
        }

        private static void CreateProgramsDtoConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Program, ProgramDto> ();
            AutoMapperSetup.CreateMapToEditableDto<Program, ProgramDto> ()
                .ForMember ( dest => dest.AgeGroup, opt => opt.MapFrom ( src => src.ProgramCharacteristics.AgeGroup ) )
                .ForMember ( dest => dest.GenderSpecification, opt => opt.MapFrom ( src => src.ProgramCharacteristics.GenderSpecification ) )
                .ForMember ( dest => dest.TreatmentApproach, opt => opt.MapFrom ( src => src.ProgramCharacteristics.TreatmentApproach ) )
                .ForMember ( dest => dest.ProgramCategory, opt => opt.MapFrom ( src => src.ProgramCharacteristics.ProgramCategory ) );
            AutoMapperSetup.CreateMapToAbstractDto<Program, ProgramSummaryDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<Program, ProgramDisplayNameDto> ();
        }

        private static void CreateStaffCredentialsDto ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Staff, StaffCredentialsDto> ();

            AutoMapperSetup.CreateMapToEditableDto<StaffCollegeDegree, StaffCollegeDegreeDto> ();
            AutoMapperSetup.CreateMapToEditableDto<StaffLicense, StaffLicenseDto> ()
                .ForMember (
                    dest => dest.StartDate,
                    opt =>
                    opt.MapFrom (
                        src => src.EffectiveDateRange != null ? src.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt =>
                    opt.MapFrom ( src => src.EffectiveDateRange != null ? src.EffectiveDateRange.EndDate : null ) );
            AutoMapperSetup.CreateMapToEditableDto<StaffTrainingCourse, StaffTrainingCourseDto> ();
            AutoMapperSetup.CreateMapToEditableDto<StaffCertification, StaffCertificationDto> ()
                .ForMember (
                    dest => dest.StartDate,
                    opt =>
                    opt.MapFrom (
                        src => src.EffectiveDateRange != null ? src.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt =>
                    opt.MapFrom ( src => src.EffectiveDateRange != null ? src.EffectiveDateRange.EndDate : null ) );
        }

        private static void CreateStaffDtoConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<StaffLanguage, StaffLanguageDto> ();
            AutoMapperSetup.CreateMapToEditableDto<StaffPhoto, StaffPhotoDto> ();

            AutoMapperSetup.CreateMapToEditableDto<StaffEvent, StaffEventDto> ();
            AutoMapperSetup.CreateMapToEditableDto<StaffChecklistItem, StaffChecklistItemDto> ();
            AutoMapperSetup.CreateMapToEditableDto<Staff, StaffHRDto> ()
                .ForMember ( dest => dest.EmploymentType, opt => opt.MapFrom ( src => src.StaffHr.EmploymentType ) )
                .ForMember ( dest => dest.TitleName, opt => opt.MapFrom ( src => src.StaffHr.TitleName ) )
                .ForMember ( dest => dest.SupervisorStaff, opt => opt.MapFrom ( src => src.StaffHr.SupervisorStaff ) )
                .ForMember ( dest => dest.ConfidentialNote, opt => opt.MapFrom ( src => src.StaffHr.ConfidentialNote ) )
                .ForMember ( dest => dest.SupervisorStaff, opt => opt.MapFrom ( src => src.StaffHr.SupervisorStaff ) );

            AutoMapperSetup.CreateMapToEditableDto<StaffLocationAssignment, StaffApprovedLocationDto> ();

            AutoMapperSetup.CreateMapToEditableDto<Staff, StaffLocationAssignmentDto> ()
                .ForMember ( dest => dest.PrimaryLocation, opt => opt.Ignore () )
                .ForMember ( dest => dest.Locations, opt => opt.MapFrom ( src => src.StaffLocationAssignments ) );

            AutoMapperSetup.CreateMapToEditableDto<Staff, StaffProfileDto> ()
                .ForMember ( dest => dest.PrefixName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Prefix ) )
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.First ) )
                .ForMember ( dest => dest.MiddleName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Middle ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Last ) )
                .ForMember ( dest => dest.SuffixName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Suffix ) )
                .ForMember ( dest => dest.Gender, opt => opt.MapFrom ( src => src.StaffProfile.Gender ) )
                .ForMember ( dest => dest.BirthDate, opt => opt.MapFrom ( src => src.StaffProfile.BirthDate ) )
                .ForMember ( dest => dest.SocialSecurityNumber, opt => opt.MapFrom ( src => src.StaffProfile.SocialSecurityNumber ) )
                .ForMember ( dest => dest.StaffType, opt => opt.MapFrom ( src => src.StaffProfile.StaffType ) )
                .ForMember ( dest => dest.ProfessionalCredentialNote, opt => opt.MapFrom ( src => src.StaffProfile.ProfessionalCredentialNote ) )
                .ForMember (
                    dest => dest.EmailAddress,
                    opt => opt.MapFrom ( src => src.StaffProfile.EmailAddress == null ? null : src.StaffProfile.EmailAddress.Address ) )
                .ForMember (
                    dest => dest.StartDate,
                    opt => opt.MapFrom ( src => src.StaffProfile.EmploymentDateRange != null ? src.StaffProfile.EmploymentDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.EndDate,
                    opt => opt.MapFrom ( src => src.StaffProfile.EmploymentDateRange != null ? src.StaffProfile.EmploymentDateRange.EndDate : null ) )
                .ForMember ( dest => dest.Note, opt => opt.MapFrom ( src => src.StaffProfile.Note ) );

            AutoMapperSetup.CreateMapToAbstractDto<SystemRolePermission, SystemPermissionDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.SystemPermission.Key ) )
                .ForMember ( dest => dest.DisplayName, opt => opt.MapFrom ( src => src.SystemPermission.DisplayName ) )
                .ForMember ( dest => dest.WellKnownName, opt => opt.MapFrom ( src => src.SystemPermission.WellKnownName ) )
                .ForMember ( dest => dest.Description, opt => opt.MapFrom ( src => src.SystemPermission.Description ) );

            AutoMapperSetup.CreateMapToAbstractDto<SystemPermission, SystemPermissionDto> ();

            AutoMapperSetup.CreateMapToAbstractDto<SystemRole, SystemRoleDto> ()
                .ForMember ( dest => dest.GrantedSystemRoles, opt => opt.MapFrom ( src => src.GrantedSystemRoleRelationships ) );

            AutoMapperSetup.CreateMapToAbstractDto<SystemRoleRelationship, SystemRoleDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.GrantedSystemRole.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.GrantedSystemRole.Name ) )
                .ForMember ( dest => dest.Description, opt => opt.MapFrom ( src => src.GrantedSystemRole.Description ) )
                .ForMember ( dest => dest.SystemRoleType, opt => opt.MapFrom ( src => src.GrantedSystemRole.SystemRoleType ) )
                .ForMember ( dest => dest.GrantedSystemPermissions, opt => opt.MapFrom ( src => src.GrantedSystemRole.GrantedSystemPermissions ) )
                .ForMember ( dest => dest.GrantedSystemRoles, opt => opt.Ignore () );

            AutoMapperSetup.CreateMapToAbstractDto<StaffSystemRole, StaffSystemRoleDto> ();

            AutoMapperSetup.CreateMapToAbstractDto<SystemAccount, SystemAccountDto> ()
                .ForMember ( dest => dest.EmailAddress, opt => opt.MapFrom ( src => src.EmailAddress.Address ) )
                .ForMember ( dest => dest.Username, opt => opt.Ignore () );

            AutoMapperSetup.CreateMapToAbstractDto<Staff, StaffDto> ()
                .ForMember ( dest => dest.StaffProfile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.Addresses, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.Identifiers, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PhoneNumbers, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.HumanResource, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.LocationAssignment, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.Credentials, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.SystemAccount, opt => opt.MapFrom ( src => src.SystemAccount ) )
                .ForMember ( dest => dest.SystemRoles, opt => opt.Ignore () );
        }

        private static void CreateStaffSearchResultDtoConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Staff, StaffSearchResultDto> ()
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.First ) )
                .ForMember ( dest => dest.MiddleName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Middle ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Last ) )
                .ForMember ( dest => dest.TitleName, opt => opt.MapFrom ( src => src.StaffHr.TitleName ) )
                .ForMember ( dest => dest.ProfessionalCredentialNote, opt => opt.MapFrom ( src => src.StaffProfile.ProfessionalCredentialNote ) )
                .ForMember (
                    dest => dest.SupervisorStaffFirstName,
                    opt =>
                    opt.MapFrom (
                        src => src.StaffHr.SupervisorStaff != null ? src.StaffHr.SupervisorStaff.StaffProfile.StaffName.First : string.Empty ) )
                .ForMember (
                    dest => dest.SupervisorStaffLastName,
                    opt =>
                    opt.MapFrom (
                        src => src.StaffHr.SupervisorStaff != null ? src.StaffHr.SupervisorStaff.StaffProfile.StaffName.Last : string.Empty ) )
                .ForMember ( dest => dest.StaffType, opt => opt.MapFrom ( src => src.StaffProfile.StaffType.Name ) );
        }

        private static void CreateStaffSummaryAddressDtoConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<StaffIdentifier, StaffIdentifierSummaryDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<StaffAddress, StaffAddressSummaryDto> ()
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.Address.FirstStreetAddress ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.Address.SecondStreetAddress ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.Address.CityName ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.Address.StateProvince ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.Address.PostalCode.Code ) );
        }

        private static void CreateStaffSummaryDtoConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Staff, StaffNameDto> ()
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.First ) )
                .ForMember (
                    dest => dest.MiddleInitial,
                    opt =>
                    opt.MapFrom (
                        src =>
                        string.IsNullOrEmpty ( src.StaffProfile.StaffName.Middle )
                            ? string.Empty
                            : src.StaffProfile.StaffName.Middle.Substring ( 0, 1 ) ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Last ) )
                .ForMember ( dest => dest.ProfessionalCredentialNote, opt => opt.MapFrom ( src => src.StaffProfile.ProfessionalCredentialNote ) );

            AutoMapperSetup.CreateMapToAbstractDto<Staff, StaffSummaryDto> ()
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.First ) )
                .ForMember ( dest => dest.MiddleName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Middle ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Last ) )
                .ForMember ( dest => dest.SuffixName, opt => opt.MapFrom ( src => src.StaffProfile.StaffName.Suffix ) )
                .ForMember (
                    dest => dest.EmailAddress,
                    opt => opt.MapFrom ( src => src.StaffProfile.EmailAddress == null ? null : src.StaffProfile.EmailAddress.Address ) )
                .ForMember ( dest => dest.TitleName, opt => opt.MapFrom ( src => src.StaffHr.TitleName ) )
                .ForMember ( dest => dest.ProfessionalCredentialNote, opt => opt.MapFrom ( src => src.StaffProfile.ProfessionalCredentialNote ) )
                .ForMember (
                    dest => dest.SupervisorStaffFirstName,
                    opt =>
                    opt.MapFrom (
                        src => src.StaffHr.SupervisorStaff != null ? src.StaffHr.SupervisorStaff.StaffProfile.StaffName.First : string.Empty ) )
                .ForMember (
                    dest => dest.SupervisorStaffLastName,
                    opt =>
                    opt.MapFrom (
                        src => src.StaffHr.SupervisorStaff != null ? src.StaffHr.SupervisorStaff.StaffProfile.StaffName.Last : string.Empty ) )
                .ForMember ( dest => dest.StaffType, opt => opt.MapFrom ( src => src.StaffProfile.StaffType.Name ) );
        }

        private static void CreateStaffSummaryPhoneDtoConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<StaffPhone, StaffPhoneSummaryDto> ()
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.Phone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.Phone.PhoneExtensionNumber ) );
        }

        #endregion
    }
}
