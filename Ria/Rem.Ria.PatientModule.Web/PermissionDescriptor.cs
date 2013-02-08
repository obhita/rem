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
using Rem.Ria.Infrastructure.Web.Permissions;
using Rem.Ria.PatientModule.Web.CdsRuleService;
using Rem.Ria.PatientModule.Web.ClinicalCaseEditor;
using Rem.Ria.PatientModule.Web.ClinicianDashboard;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.DensAsiInterview;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;
using Rem.Ria.PatientModule.Web.DroolsTest;
using Rem.Ria.PatientModule.Web.ImportC32;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;
using Rem.Ria.PatientModule.Web.GainShortScreener;
using Rem.Ria.PatientModule.Web.GpraInterview;
using Rem.Ria.PatientModule.Web.PatientAccessHistory;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile;
using Rem.Ria.PatientModule.Web.PatientEditor;
using Rem.Ria.PatientModule.Web.PatientList;
using Rem.Ria.PatientModule.Web.PatientReminder;
using Rem.Ria.PatientModule.Web.PatientSearch;
using Rem.Ria.PatientModule.Web.Service;
using Rem.Ria.PatientModule.Web.SystemAccountSearch;
using Rem.Ria.PatientModule.Web.TedsInterview;

namespace Rem.Ria.PatientModule.Web
{
    /// <summary>
    /// Class for descripting permission.
    /// </summary>
    public class PermissionDescriptor : IPermissionDescriptor
    {
        #region Constants and Fields

        private readonly ResourceList _resources = new ResourceListBuilder ()

            // Cds Rules Permissions
            .AddResource<GetDtoRequest<CdsRulesDto>> ( PatientPermission.CdsRulesViewPermission )
            .AddResource<SaveDtoRequest<CdsRulesDto>> ( PatientPermission.CdsRulesEditPermission )
            .AddResource<CheckCdsRulesRequest> ( PatientPermission.CdsRulesEditPermission )

            // Clinical Case Permissions
            .AddResource<GetDtoRequest<ClinicalCaseAdmissionDto>> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<GetDtoRequest<ClinicalCaseDischargeDto>> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<GetDtoRequest<ClinicalCaseDto>> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<GetDtoRequest<ClinicalCaseProfileDto>> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<GetDtoRequest<ClinicalCaseSignedCommentDto>> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<GetDtoRequest<ClinicalCaseStatusDto>> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<GetDtoRequest<ClinicalCaseSummaryDto>> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<SaveDtoRequest<ClinicalCaseAdmissionDto>> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<SaveDtoRequest<ClinicalCaseDischargeDto>> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<SaveDtoRequest<ClinicalCaseDto>> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<SaveDtoRequest<ClinicalCaseProfileDto>> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<SaveDtoRequest<ClinicalCaseSignedCommentDto>> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<SaveDtoRequest<ClinicalCaseStatusDto>> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<SaveDtoRequest<ClinicalCaseSummaryDto>> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<GetRecentVisitActivitiesByClinicalCaseRequest> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<GetScheduledVisitActivitiesByClinicalCaseRequest> ( PatientPermission.ClinicalCaseEditPermission )
            .AddResource<CreateNewClinicalCaseRequest> ( PatientPermission.ClinicalCaseEditPermission )

            // Clinician Dashboard Permissions
            .AddResource<GetClinicianPatientsRequest> ( PatientPermission.ClinicianDashboardViewPermission )

            // Patient Permissions
            .AddResource<GetDtoRequest<PatientDemographicDetailsDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientIdentifiersDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientLegalStatusDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientOtherConsiderationsDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientPhoneNumbersDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientProfileDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientRaceAndEthnicityDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientAddressesDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientAliasesDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientAllergiesDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientConfidentialInformationDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientContactsDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientContactProfileDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientContactContactInformationDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientContactDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<GetDtoRequest<PatientVeteranInformationDto>> ( PatientPermission.PatientViewPermission )
            .AddResource<SaveDtoRequest<PatientDemographicDetailsDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientIdentifiersDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientLegalStatusDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientOtherConsiderationsDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientPhoneNumbersDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientProfileDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientRaceAndEthnicityDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientAddressesDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientAliasesDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientAllergiesDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientConfidentialInformationDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientContactsDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientContactProfileDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientContactContactInformationDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientContactDto>> ( PatientPermission.PatientEditPermission )
            .AddResource<RemovePatientContactRequest> ( PatientPermission.PatientEditPermission )
            .AddResource<CreateNewPatientRequest> ( PatientPermission.PatientEditPermission )
            .AddResource<SaveDtoRequest<PatientVeteranInformationDto>> ( PatientPermission.PatientEditPermission )

            // Patient External History Permissions
            .AddResource<GetDtoRequest<PatientDocumentDto>> ( PatientPermission.PatientExternalHistoryViewPermission )
            .AddResource<SaveDtoRequest<PatientDocumentDto>> ( PatientPermission.PatientExternalHistoryEditPermission )
            .AddResource<DeletePatientDocumentRequest> ( PatientPermission.PatientExternalHistoryEditPermission )
            .AddResource<GetPatientDocumentsByPatientRequest> ( PatientPermission.PatientExternalHistoryViewPermission )
            .AddResource<GetDtoRequest<MailAttachmentPatientDocumentDto>>(PatientPermission.PatientExternalHistoryViewPermission)
            .AddResource<SaveDtoRequest<MailAttachmentPatientDocumentDto>>(PatientPermission.PatientExternalHistoryEditPermission)
            .AddResource<QueryPatientByDocumentRequest>(PatientPermission.PatientExternalHistoryViewPermission)
            .AddResource<CreatePatientImportDocumentRequest>(PatientPermission.PatientExternalHistoryViewPermission)

            // Problem Permissions
            .AddResource<GetDtoRequest<ProblemDto>> ( PatientPermission.ProblemViewPermission )
            .AddResource<SaveDtoRequest<ProblemDto>> ( PatientPermission.ProblemEditPermission )
            .AddResource<GetProblemByKeyRequest> ( PatientPermission.ProblemEditPermission )
            .AddResource<DeleteProblemRequest> ( PatientPermission.ProblemEditPermission )
            .AddResource<GetAllProblemsByClinicalCaseRequest> ( PatientPermission.ProblemViewPermission )

            // Visit Permissions
            .AddResource<GetDtoRequest<VisitDto>> ( PatientPermission.VisitViewPermission )
            .AddResource<SaveDtoRequest<VisitDto>> ( PatientPermission.VisitEditPermission )
            .AddResource<AssociateProblemsWithVisitRequest> ( PatientPermission.VisitEditPermission )
            .AddResource<DetachProblemFromVisitRequest> ( PatientPermission.VisitEditPermission )
            .AddResource<GetGrowthInformationByPatientKeyRequest> ( PatientPermission.VisitViewPermission )
            .AddResource<UpdateVisitStatusRequest> ( PatientPermission.VisitEditPermission )
            .AddResource<GetAllSchedulableActivityTypesRequest> ( PatientPermission.VisitViewPermission )
            .AddResource<ScheduleActivityRequest> ( PatientPermission.VisitEditPermission )
            .AddResource<DeleteActivityRequest> ( PatientPermission.VisitEditPermission )

            // Dens Asi Permissions
            .AddResource<GetDtoRequest<DensAsiClosureDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiDrugAlcoholUseDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiDsmIvDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiEmploymentStatusDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiFamilySocialRelationshipsDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiInterviewDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiLegalStatusDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiMedicalStatusDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiPatientProfileDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<GetDtoRequest<DensAsiPsychiatricStatusDto>> ( PatientPermission.DensAsiViewPermission )
            .AddResource<SaveDtoRequest<DensAsiClosureDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiDrugAlcoholUseDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiDsmIvDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiEmploymentStatusDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiFamilySocialRelationshipsDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiInterviewDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiLegalStatusDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiMedicalStatusDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiPatientProfileDto>> ( PatientPermission.DensAsiEditPermission )
            .AddResource<SaveDtoRequest<DensAsiPsychiatricStatusDto>> ( PatientPermission.DensAsiEditPermission )

            // TEDS Permissions
            .AddResource<GetDtoRequest<TedsAdmissionInterviewDto>>(PatientPermission.TedsViewPermission)
            .AddResource<SaveDtoRequest<TedsAdmissionInterviewDto>>(PatientPermission.TedsEditPermission)
            .AddResource<GetDtoRequest<TedsDischargeInterviewDto>>(PatientPermission.TedsViewPermission)
            .AddResource<SaveDtoRequest<TedsDischargeInterviewDto>>(PatientPermission.TedsEditPermission)
            .AddResource<GetDetailedDrugCodeListRequest>(PatientPermission.TedsViewPermission)

            // Front Desk Dashboard Permissions
            .AddResource<GetDtoRequest<VisitTemplateDto>> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetDtoRequest<AppointmentDetailsDto>> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetDtoRequest<ClinicianAppointmentDto>> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetDtoRequest<ClinicianScheduleDto>> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<SaveDtoRequest<AppointmentDetailsDto>> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<SaveDtoRequest<ClinicianAppointmentDto>> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<SaveDtoRequest<ClinicianScheduleDto>> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<DeleteClinicianAppointmentRequest> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<GetAvailableTimeSlotsRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetClinicianScheduleByClinicianKeyAndDateRangeRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetCliniciansByLocationKeyRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetVisitTemplatesByAgencyKeyRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<UpdateClinicianAppointmentRequest> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<GetDtoRequest<PatientSummaryDto>> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetSelfPaymentsByPatientKeyRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<SaveDtoRequest<SelfPaymentDto>> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<SaveDtoRequest<PayorCoverageCacheDto>> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<GetPayorCacheByKeywordRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetPayorCoverageCachesByPatientKeyRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetDtoRequest<PayorCoverageCacheDto>> ( PatientPermission.FrontDeskDashboardEditPermission )

            // Gain Short Screener Permissions
            .AddResource<GetDtoRequest<GainShortScreenerDto>> ( PatientPermission.GainShortScreenerViewPermission )
            .AddResource<SaveDtoRequest<GainShortScreenerDto>> ( PatientPermission.GainShortScreenerEditPermission )

            // Gpra Interview Permissions
            .AddResource<GetDtoRequest<GpraCrimeCriminalJusticeDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraDemographicsDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraDischargeDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraDrugAlcoholUseDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraFamilyLivingConditionsDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraFollowUpDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraInterviewDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraInterviewInformationDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraPlannedServicesDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraProblemsTreatmentRecoveryDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraProfessionalInformationDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GetDtoRequest<GpraSocialConnectednessDto>> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<SaveDtoRequest<GpraCrimeCriminalJusticeDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraDemographicsDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraDischargeDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraDrugAlcoholUseDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraFamilyLivingConditionsDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraFollowUpDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraInterviewDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraInterviewInformationDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraPlannedServicesDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraProblemsTreatmentRecoveryDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraProfessionalInformationDto>> ( PatientPermission.GpraInterviewEditPermission )
            .AddResource<SaveDtoRequest<GpraSocialConnectednessDto>> ( PatientPermission.GpraInterviewEditPermission )

            // Patient Access History Permissions
            .AddResource<GetPatientAccessHistoryBySearchCriteriaRequest> ( PatientPermission.PatientAccessHistoryViewPermission )

            // Audit C Permissions
            .AddResource<GetDtoRequest<AuditCDto>> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<SaveDtoRequest<AuditCDto>> ( PatientPermission.PatientDashboardEditPermission )


            // Audit Permissions
            .AddResource<GetDtoRequest<AuditDto>> ( PatientPermission.AuditViewPermission )
            .AddResource<SaveDtoRequest<AuditDto>> ( PatientPermission.AuditEditPermission )

            // Brief Intervention Permissions
            .AddResource<GetDtoRequest<BriefInterventionDto>> ( PatientPermission.BriefInterventionViewPermission )
            .AddResource<SaveDtoRequest<BriefInterventionDto>> ( PatientPermission.BriefInterventionEditPermission )

            // Case Summary Permissions
            .AddResource<GetDtoRequest<CaseSummaryDto>> ( PatientPermission.CaseSummaryViewPermission )
            .AddResource<SaveDtoRequest<CaseSummaryDto>> ( PatientPermission.CaseSummaryEditPermission )

            // Case Alerts Permissions


            // Dast 10 Permissions
            .AddResource<GetDtoRequest<Dast10Dto>> ( PatientPermission.Dast10ViewPermission )
            .AddResource<SaveDtoRequest<Dast10Dto>> ( PatientPermission.Dast10EditPermission )

            // Immunization Permissions
            .AddResource<GetDtoRequest<ImmunizationDto>> ( PatientPermission.ImmunizationViewPermission )
            .AddResource<SaveDtoRequest<ImmunizationDto>> ( PatientPermission.ImmunizationEditPermission )

            // Individual Counseling Permissions
            .AddResource<GetDtoRequest<IndividualCounselingDto>> ( PatientPermission.IndividualCounselingViewPermission )
            .AddResource<SaveDtoRequest<IndividualCounselingDto>> ( PatientPermission.IndividualCounselingEditPermission )

            // Lab Results Permissions
            .AddResource<GetDtoRequest<LabResultDto>> ( PatientPermission.LabResultsViewPermission )
            .AddResource<GetDtoRequest<LabSpecimenDto>> ( PatientPermission.LabResultsViewPermission )
            .AddResource<SaveDtoRequest<LabResultDto>> ( PatientPermission.LabResultsEditPermission )
            .AddResource<SaveDtoRequest<LabSpecimenDto>> ( PatientPermission.LabResultsEditPermission )
            .AddResource<GetLabResultsRequest> ( PatientPermission.LabResultsViewPermission )

            // Medication Permissions
            .AddResource<GetDtoRequest<MedicationDto>> ( PatientPermission.MedicationViewPermission )
            .AddResource<SaveDtoRequest<MedicationDto>> ( PatientPermission.MedicationEditPermission )
            .AddResource<GetAllMedicationsByPatientRequest> ( PatientPermission.MedicationViewPermission )
            .AddResource<GetMedicationByKeyRequest> ( PatientPermission.MedicationViewPermission )
            .AddResource<MedicationFormStrengthRequest> ( PatientPermission.MedicationViewPermission )

            // Nida Permissions
            .AddResource<GetDtoRequest<NidaDrugQuestionnaireDto>> ( PatientPermission.NidaViewPermission )
            .AddResource<SaveDtoRequest<NidaDrugQuestionnaireDto>> ( PatientPermission.NidaEditPermission )

            // Patient Dashboard Permissions


            // Phq 9 Permissions
            .AddResource<GetDtoRequest<Phq9Dto>> ( PatientPermission.Phq9ViewPermission )
            .AddResource<SaveDtoRequest<Phq9Dto>> ( PatientPermission.Phq9EditPermission )

            // Radiology Orders Permissions
            .AddResource<GetDtoRequest<RadiologyOrderDto>> ( PatientPermission.RadiologyOrdersViewPermission )
            .AddResource<SaveDtoRequest<RadiologyOrderDto>> ( PatientPermission.RadiologyOrdersEditPermission )
            .AddResource<CancelRadiologyOrderRequest> ( PatientPermission.RadiologyOrdersEditPermission )

            // ScheduledAndRecentActivitiesPermissions
            .AddResource<GetDtoRequest<ActivityDto>> ( PatientPermission.ScheduledAndRecentActivitiesViewPermission )
            .AddResource<SaveDtoRequest<ActivityDto>> ( PatientPermission.ScheduledAndRecentActivitiesEditPermission )
            .AddResource<GetAllActivitiesByClinicalCaseRequest> ( PatientPermission.ScheduledAndRecentActivitiesViewPermission )

            // Social History Permissions
            .AddResource<GetDtoRequest<SocialHistoryDto>> ( PatientPermission.SocialHistoryViewPermission )
            .AddResource<SaveDtoRequest<SocialHistoryDto>> ( PatientPermission.SocialHistoryEditPermission )

            // Vital Signs Permissions
            .AddResource<GetDtoRequest<VitalSignDto>> ( PatientPermission.VitalSignsViewPermission )
            .AddResource<SaveDtoRequest<VitalSignDto>> ( PatientPermission.VitalSignsEditPermission )

            // Patient List Permissions
            .AddResource<PatientListSearchRequest> ( PatientPermission.PatientListViewPermission )

            // Patient Reminders Permissions
            .AddResource<PatientReminderSearchRequest> ( PatientPermission.PatientRemindersViewPermission )

            // Patient Search Permissions
            .AddResource<GetPatientByKeyRequest> ( PatientPermission.PatientSearchViewPermission )
            .AddResource<GetPatientsByAdvancedSearchRequest> ( PatientPermission.PatientSearchViewPermission )
            .AddResource<GetPatientsByKeywordsRequest> ( PatientPermission.PatientSearchViewPermission )

            // Patient Workspace Permissions
            .AddResource<GetAllClinicalCasesByPatientRequest> ( PatientPermission.PatientWorkspaceViewPermission )
            .AddResource<GetDefaultClinicalCaseByPatientRequest> ( PatientPermission.PatientWorkspaceViewPermission )

            // C32 Permissions
            .AddResource<PostC32ToPopHealtheRequest> ( PatientPermission.C32ViewPermission )
            .AddResource<SendC32Request> ( PatientPermission.C32ViewPermission )
            .AddResource<GetDataFromC32Request>(PatientPermission.C32ImportPermission)
            .AddResource<ImportC32Request> (PatientPermission.C32ImportPermission  )

            // System Account Permissions
            .AddResource<GetSystemAccountsByAdvancedSearchRequest> ( PatientPermission.SystemAccountViewPermission )
            .AddResource<GetSystemAccountsByKeywordRequest> ( PatientPermission.SystemAccountViewPermission )

            // Interoperability Workspace Permissions

            // TODO: this is temporary code, need define permissions for program enrollment later when there is detail requirement
            // Program Enrollment Permissions
            .AddResource<GetProgramEnrollmentByKeyRequest> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<GetAllProgramEnrollmentsByClinicalCaseRequest> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<GetAvailableEnrollingStaffsRequest> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<GetAvailableProgramOfferingLocationsRequest> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<GetAvailableProgramsRequest> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<CreateProgramEnrollmentRequest> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<DeleteProgramEnrollmentRequest> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<DisenrollProgramEnrollmentRequest> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<ReviseProgramEnrollmentRequest> ( PatientPermission.PatientDashboardEditPermission )

            // Global Permissions
            .AddResource<LogPatientEventAccessRequest> ( InfrastructurePermission.AccessUserInterfacePermission )

            //DroolsTest
            .AddResource<SendDroolsTestRequest> (InfrastructurePermission.AccessUserInterfacePermission)

            // TODO: Add Permissions for Requests  
            // Direct Message Center Permissions
            .AddResource<GetImapFolderItemsRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<GetImapMailRequest>(PatientPermission.FrontDeskDashboardViewPermission)
            .AddResource<GetHealthcareProviderDirectoryEntriesRequest> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<SendDirectMessageRequest> ( PatientPermission.C32ViewPermission );

        #endregion

        // IPermissionDescriptor Members

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
