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
using Rem.Ria.PatientModule.CdsAlerts;
using Rem.Ria.PatientModule.CdsRuleEditor;
using Rem.Ria.PatientModule.ClinicalCaseEditor;
using Rem.Ria.PatientModule.ClinicianDashboard;
using Rem.Ria.PatientModule.DensAsiInterview;
using Rem.Ria.PatientModule.DirectMessageCenter;
using Rem.Ria.PatientModule.DtsSearch;
using Rem.Ria.PatientModule.FrontDeskDashboard;
using Rem.Ria.PatientModule.GainShortScreener;
using Rem.Ria.PatientModule.GpraInterview;
using Rem.Ria.PatientModule.ImportC32;
using Rem.Ria.PatientModule.InteroperabilityWorkspace;
using Rem.Ria.PatientModule.PatientAccessHistory;
using Rem.Ria.PatientModule.PatientContactEditor;
using Rem.Ria.PatientModule.PatientDashboard;
using Rem.Ria.PatientModule.PatientDashboard.ProgramEnrollmentTile;
using Rem.Ria.PatientModule.PatientEditor;
using Rem.Ria.PatientModule.PatientList;
using Rem.Ria.PatientModule.PatientReminder;
using Rem.Ria.PatientModule.PatientSearch;
using Rem.Ria.PatientModule.PatientWorkspace;
using Rem.Ria.PatientModule.QuickPickers;
using Rem.Ria.PatientModule.SystemAccountSearch;
using Rem.Ria.PatientModule.TedsInterview;

namespace Rem.Ria.PatientModule
{
    /// <summary>
    /// Class for describing client permission.
    /// </summary>
    public class ClientPermissionDescriptor : IPermissionDescriptor
    {
        #region Constants and Fields

        private readonly ResourceList _resources = new ResourceListBuilder ()

            // Cds Rules Permissions
            .AddResource<CdsAlertView> ( PatientPermission.CdsRulesViewPermission )
            .AddResource<CdsRuleEditorView> ( PatientPermission.CdsRulesViewPermission )
            .AddResource<CdsRuleEditorViewModel> ( PatientPermission.CdsRulesEditPermission )
            .AddResource<CdsAlertViewModel> ( PatientPermission.CdsRulesEditPermission )

            // Clinical Case Permissions
            .AddResource<ClinicalCaseEditorView> ( PatientPermission.ClinicalCaseViewPermission )
            .AddResource<ClinicalCaseEditorViewModel> ( PatientPermission.ClinicalCaseEditPermission )

            // Clinician Dashboard Permissions
            .AddResource<ClinicianDashboardScheduleTileView> ( PatientPermission.ClinicianDashboardViewPermission )
            .AddResource<ClinicianLabResultsTileView> ( PatientPermission.ClinicianDashboardLabResultsViewPermission )
            .AddResource<ClinicianLabResultsTileViewModel> ( PatientPermission.ClinicianDashboardLabResultsViewPermission )
            .AddResource<ClinicianMedicationOrdersTileView> ( PatientPermission.ClinicianDashboardMedicationOrdersViewPermission )
            .AddResource<ClinicianMedicationOrdersTileViewModel> ( PatientPermission.ClinicianDashboardMedicationOrdersViewPermission )
            .AddResource<ClinicianPatientListView> ( PatientPermission.ClinicianDashboardViewPermission )
            .AddResource<ClinicianPatientListViewModel> ( PatientPermission.ClinicianDashboardEditPermission )

            // Patient Permissions
            .AddResource<AllergyDtsSearchViewModel> ( PatientPermission.PatientEditPermission )
            .AddResource<PatientContactEditorView> ( PatientPermission.PatientViewPermission )
            .AddResource<PatientContactEditorViewModel> ( PatientPermission.PatientEditPermission )
            .AddResource<PatientEditorView> ( PatientPermission.PatientViewPermission )
            .AddResource<PatientEditorViewModel> ( PatientPermission.PatientEditPermission )
            .AddResource<PayorCacheQuickPickerViewModel> ( PatientPermission.PatientViewPermission )

            // Patient External History Permissions
            .AddResource<ExternalPatientHistoryView> ( PatientPermission.PatientExternalHistoryViewPermission )
            .AddResource<ExternalPatientHistoryViewModel> ( PatientPermission.PatientExternalHistoryEditPermission )
            .AddResource<PatientHistoryDocumentView> ( PatientPermission.PatientExternalHistoryViewPermission )
            .AddResource<PatientHistoryDocumentViewModel> ( PatientPermission.PatientExternalHistoryEditPermission )
            .AddResource<UploadDocumentView> ( PatientPermission.PatientExternalHistoryViewPermission )
            .AddResource<UploadDocumentViewModel> ( PatientPermission.PatientExternalHistoryEditPermission )
            .AddResource<SaveMailAttachmentPatientDocumentView> ( PatientPermission.PatientExternalHistoryViewPermission )
            .AddResource<SaveMailAttachmentPatientDocumentViewModel> ( PatientPermission.PatientExternalHistoryEditPermission )

            // Problem Permissions
            .AddResource<ProblemDtsSearchViewModel> ( PatientPermission.ProblemEditPermission )
            .AddResource<CaseProblemListView> ( PatientPermission.ProblemViewPermission )
            .AddResource<EditCaseProblemView> ( PatientPermission.ProblemViewPermission )
            .AddResource<CaseProblemListViewModel> ( PatientPermission.ProblemEditPermission )
            .AddResource<EditCaseProblemViewModel> ( PatientPermission.ProblemEditPermission )

            // Visit Permissions
            .AddResource<GrowthRateView> ( PatientPermission.VisitViewPermission )
            .AddResource<GrowthRateViewModel> ( PatientPermission.VisitEditPermission )
            .AddResource<VisitView> ( PatientPermission.VisitViewPermission )
            .AddResource<VisitViewModel> ( PatientPermission.VisitEditPermission )

            // Dens Asi Permissions
            .AddResource<DensAsiInterviewView> ( PatientPermission.DensAsiViewPermission )
            .AddResource<DensAsiInterviewViewModel> ( PatientPermission.DensAsiEditPermission )

            // TEDS Permissions
            .AddResource<TedsAdmissionInterviewView> ( PatientPermission.TedsViewPermission )
            .AddResource<TedsAdmissionInterviewViewModel> ( PatientPermission.TedsEditPermission )
            .AddResource<TedsDischargeInterviewView> ( PatientPermission.TedsViewPermission )
            .AddResource<TedsDischargeInterviewViewModel> ( PatientPermission.TedsViewPermission )

            // Front Desk Dashboard Permissions
            .AddResource<AppointmentDetailsView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<AppointmentDetailsViewModel> (
                PatientPermission.FrontDeskDashboardEditPermission,
                rlb => rlb
                           .AddResource (
                               PropertyUtil.ExtractPropertyName<AppointmentDetailsViewModel, ICommand> ( p => p.ViewProfileCommand ),
                               PatientPermission.PatientViewPermission )
            )
            .AddResource<AppointmentSchedulerView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<AppointmentSchedulerViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<ClinicianScheduleTileView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<ClinicianScheduleTileViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<FrontDeskDashboardView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<FrontDeskDashboardViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<BillingView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<BillingViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<PatientSummaryView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<PatientSummaryViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<SelfPaymentView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<SelfPaymentViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<PayorCoverageView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<PayorCoverageViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<AddSelfPaymentView> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<AddSelfPaymentViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<EditPayorCoverageView> ( PatientPermission.FrontDeskDashboardEditPermission )
            .AddResource<EditPayorCoverageViewModel> ( PatientPermission.FrontDeskDashboardEditPermission )

            // Gain Short Screener Permissions
            .AddResource<GainShortScreenerView> ( PatientPermission.GainShortScreenerViewPermission )
            .AddResource<GainShortScreenerViewModel> ( PatientPermission.GainShortScreenerEditPermission )

            // Gpra Interview Permissions
            .AddResource<GpraInterviewView> ( PatientPermission.GpraInterviewViewPermission )
            .AddResource<GpraInterviewViewModel> ( PatientPermission.GpraInterviewEditPermission )

            // Patient Access History Permissions
            .AddResource<PatientAccessHistoryView> ( PatientPermission.PatientAccessHistoryViewPermission )
            .AddResource<PatientAccessHistoryViewModel> ( PatientPermission.PatientAccessHistoryViewPermission )

            // Audit C Permissions
            .AddResource<AuditCResultView> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<AuditCResultViewModel> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<AuditCView> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<AuditCViewModel> ( PatientPermission.PatientDashboardEditPermission )


            // Audit Permissions
            .AddResource<AuditResultView> ( PatientPermission.AuditViewPermission )
            .AddResource<AuditResultViewModel> ( PatientPermission.AuditEditPermission )
            .AddResource<AuditView> ( PatientPermission.AuditViewPermission )
            .AddResource<AuditViewModel> ( PatientPermission.AuditEditPermission )

            // Brief Intervention Permissions
            .AddResource<BriefInterventionView> ( PatientPermission.BriefInterventionViewPermission )
            .AddResource<BriefInterventionViewModel> ( PatientPermission.BriefInterventionEditPermission )

            // Case Summary Permissions
            .AddResource<CaseActivitiesView> ( PatientPermission.CaseSummaryViewPermission )
            .AddResource<CaseActivitiesViewModel> ( PatientPermission.CaseSummaryEditPermission )
            .AddResource<CaseSnapshotView> ( PatientPermission.CaseSummaryViewPermission )
            .AddResource<CaseSnapshotViewModel> ( PatientPermission.CaseSummaryEditPermission )
            .AddResource<CaseSummaryView> ( PatientPermission.CaseSummaryViewPermission )
            .AddResource<CaseSummaryViewModel> ( PatientPermission.CaseSummaryEditPermission )

            // Case Alerts Permissions
            .AddResource<CaseAlertsView> ( PatientPermission.CaseAlertsViewPermission )
            .AddResource<CaseAlertsViewModel> ( PatientPermission.CaseAlertsEditPermission )

            // Dast 10 Permissions
            .AddResource<Dast10ResultView> ( PatientPermission.Dast10ViewPermission )
            .AddResource<Dast10ResultViewModel> ( PatientPermission.Dast10EditPermission )
            .AddResource<Dast10View> ( PatientPermission.Dast10ViewPermission )
            .AddResource<Dast10ViewModel> ( PatientPermission.Dast10EditPermission )

            // Immunization Permissions
            .AddResource<VaccineNameDtsSearchViewModel> ( PatientPermission.ImmunizationViewPermission )
            .AddResource<ImmunizationView> ( PatientPermission.ImmunizationViewPermission )
            .AddResource<ImmunizationViewModel> ( PatientPermission.ImmunizationEditPermission )

            // Individual Counseling Permissions
            .AddResource<IndividualCounselingView> ( PatientPermission.IndividualCounselingViewPermission )
            .AddResource<IndividualCounselingViewModel> ( PatientPermission.IndividualCounselingEditPermission )

            // Lab Results Permissions
            .AddResource<LabTestNameDtsSearchViewModel> ( PatientPermission.LabResultsEditPermission )
            .AddResource<LabResultsView> ( PatientPermission.LabResultsViewPermission )
            .AddResource<LabResultsViewModel> ( PatientPermission.LabResultsEditPermission )

            // Medication Permissions
            .AddResource<MedicationDtsSearchViewModel> ( PatientPermission.MedicationEditPermission )
            .AddResource<EditMedicationView> ( PatientPermission.MedicationViewPermission )
            .AddResource<EditMedicationViewModel> ( PatientPermission.MedicationEditPermission )
            .AddResource<MedicationListView> ( PatientPermission.MedicationViewPermission )
            .AddResource<MedicationListViewModel> ( PatientPermission.MedicationEditPermission )

            // Nida Permissions
            .AddResource<NidaDrugQuestionnaireView> ( PatientPermission.NidaViewPermission )
            .AddResource<NidaDrugQuestionnaireViewModel> ( PatientPermission.NidaEditPermission )

            // Patient Dashboard Permissions
            .AddResource<PatientDashboardView> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<PatientDashboardViewModel> ( PatientPermission.PatientDashboardEditPermission )

            // Phq 9 Permissions
            .AddResource<Phq9ResultView> ( PatientPermission.Phq9ViewPermission )
            .AddResource<Phq9ResultViewModel> ( PatientPermission.Phq9EditPermission )
            .AddResource<Phq9View> ( PatientPermission.Phq9ViewPermission )
            .AddResource<Phq9ViewModel> ( PatientPermission.Phq9EditPermission )

            // Radiology Orders Permissions
            .AddResource<RadiologyOrderView> ( PatientPermission.RadiologyOrdersViewPermission )
            .AddResource<RadiologyOrderViewModel> ( PatientPermission.RadiologyOrdersEditPermission )

            // ScheduledAndRecentActivitiesPermissions
            .AddResource<VisitListView> ( PatientPermission.ScheduledAndRecentActivitiesViewPermission )
            .AddResource<VisitListViewModel> ( PatientPermission.ScheduledAndRecentActivitiesEditPermission )

            // Social History Permissions
            .AddResource<SocialHistorySmokingCessationAdvisoryView> ( PatientPermission.SocialHistoryViewPermission )
            .AddResource<SocialHistorySmokingCessationAdvisoryViewModel> ( PatientPermission.SocialHistoryEditPermission )
            .AddResource<SocialHistoryView> ( PatientPermission.SocialHistoryViewPermission )
            .AddResource<SocialHistoryViewModel> ( PatientPermission.SocialHistoryEditPermission )

            // Vital Signs Permissions
            .AddResource<VitalsView> ( PatientPermission.VitalSignsViewPermission )
            .AddResource<VitalsViewModel> ( PatientPermission.VitalSignsEditPermission )


            // Patient List Permissions
            .AddResource<PatientListView> ( PatientPermission.PatientListViewPermission )
            .AddResource<PatientListViewModel> ( PatientPermission.PatientListViewPermission )

            // Patient Reminders Permissions
            .AddResource<PatientReminderView> ( PatientPermission.PatientRemindersViewPermission )
            .AddResource<PatientReminderViewModel> ( PatientPermission.PatientRemindersViewPermission )

            // Patient Search Permissions
            .AddResource<MainPatientSearchView> ( PatientPermission.PatientSearchViewPermission )
            .AddResource<MainPatientSearchViewModel> (
                PatientPermission.PatientSearchViewPermission,
                rlb => rlb
                           .AddResource (
                               PropertyUtil.ExtractPropertyName<MainPatientSearchViewModel, ICommand> ( p => p.ViewProfileCommand ),
                               PatientPermission.PatientViewPermission )
                           .AddResource (
                               PropertyUtil.ExtractPropertyName<MainPatientSearchViewModel, ICommand> ( p => p.AddNewCommand ),
                               PatientPermission.PatientEditPermission )
            )
            .AddResource<PatientSearchView> ( PatientPermission.PatientSearchViewPermission )
            .AddResource<PatientSearchViewModel> ( PatientPermission.PatientSearchViewPermission )

            // Patient Workspace Permissions
            .AddResource<PatientWorkspaceViewModel> (
                PatientPermission.PatientWorkspaceViewPermission,
                rlb => rlb
                           .AddResource (
                               PropertyUtil.ExtractPropertyName<PatientWorkspaceViewModel, ICommand> ( p => p.GoToDashboardCommand ),
                               ( PatientPermission.PatientDashboardViewPermission ) ) )
            .AddResource<PatientWorkspaceView> ( PatientPermission.PatientWorkspaceViewPermission )



            // System Account Permissions
            .AddResource<SystemAccountSearchView> ( PatientPermission.SystemAccountViewPermission )
            .AddResource<SystemAccountSearchViewModel> ( PatientPermission.SystemAccountViewPermission )

            // Interoperability Workspace Permissions
            .AddResource<InteroperabilityWorkspaceView> ( PatientPermission.InteroperabilityWorkspaceViewPermission )
            .AddResource<InteroperabilityWorkspaceViewModel> ( PatientPermission.InteroperabilityWorkspaceViewPermission )
            .AddResource<SingleConceptView> ( PatientPermission.InteroperabilityWorkspaceViewPermission )
            .AddResource<SingleConceptViewModel> ( PatientPermission.InteroperabilityWorkspaceViewPermission )
            .AddResource<TerminologyVocabularyView> ( PatientPermission.InteroperabilityWorkspaceViewPermission )
            .AddResource<TerminologyVocabularyViewModel> ( PatientPermission.InteroperabilityWorkspaceViewPermission )
            .AddResource<DroolsRestTestView> ( PatientPermission.InteroperabilityWorkspaceViewPermission )
            .AddResource<DroolsRestTestViewModel> ( PatientPermission.InteroperabilityWorkspaceViewPermission )

            // TODO: this is temporary code, need define permissions for program enrollment later when there is detail requirement
            // Program Enrollment Permissions
            .AddResource<ProgramEnrollmentListView> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<ProgramEnrollmentListViewModel> ( PatientPermission.PatientDashboardViewPermission )
            .AddResource<CreateProgramEnrollmentView> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<CreateProgramEnrollmentViewModel> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<EditProgramEnrollmentView> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<EditProgramEnrollmentViewModel> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<DisenrollProgramEnrollmentView> ( PatientPermission.PatientDashboardEditPermission )
            .AddResource<DisenrollProgramEnrollmentViewModel> ( PatientPermission.PatientDashboardEditPermission )

            // Direct Message Center Permissions
            // TODO: Add Permissions for the Message Center Workspace
            .AddResource<MessageCenterWorkspaceView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<MessageCenterWorkspaceViewModel> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<SendNewMailView> ( PatientPermission.FrontDeskDashboardViewPermission )
            .AddResource<SendNewMailViewModel> ( PatientPermission.FrontDeskDashboardViewPermission )

            // TODO: Add Permissions for the Health Providers Directory
            .AddResource<HealthProvidersDirectoryView> ( PatientPermission.C32ViewPermission )
            .AddResource<HealthProvidersDirectoryViewModel> ( PatientPermission.C32ViewPermission )

            // C32 Permissions
            .AddResource<SendC32View> ( PatientPermission.C32ViewPermission )
            .AddResource<SendC32ViewModel> ( PatientPermission.C32ViewPermission )

            // C32 Import Permissions
            .AddResource<ImportC32View> ( PatientPermission.C32ImportPermission )
            .AddResource<ImportC32ViewModel> ( PatientPermission.C32ImportPermission );

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
