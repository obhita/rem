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

namespace Rem.Ria.Infrastructure.Web.Permissions
{
    /// <summary>
    /// PatientPermission class.
    /// </summary>
    public static class PatientPermission
    {
        #region Constants and Fields

        /// <summary>
        /// AuditC EditPermission
        /// </summary>
        public static readonly Permission AuditCEditPermission = new Permission { Name = "patientmodule/auditcedit" };

        /// <summary>
        /// AuditC ViewPermission
        /// </summary>
        public static readonly Permission AuditCViewPermission = new Permission { Name = "patientmodule/auditcview" };

        /// <summary>
        /// Audit EditPermission
        /// </summary>
        public static readonly Permission AuditEditPermission = new Permission { Name = "patientmodule/auditedit" };

        /// <summary>
        /// Audit ViewPermission
        /// </summary>
        public static readonly Permission AuditViewPermission = new Permission { Name = "patientmodule/auditview" };

        /// <summary>
        /// BriefIntervention EditPermission
        /// </summary>
        public static readonly Permission BriefInterventionEditPermission = new Permission { Name = "patientmodule/briefinterventionedit" };

        /// <summary>
        /// BriefIntervention ViewPermission
        /// </summary>
        public static readonly Permission BriefInterventionViewPermission = new Permission { Name = "patientmodule/briefinterventionview" };

        /// <summary>
        /// C32 ViewPermission
        /// </summary>
        public static readonly Permission C32ViewPermission = new Permission { Name = "patientmodule/c32view" };

        /// <summary>
        /// C32 ImportPermission
        /// </summary>
        public static readonly Permission C32ImportPermission = new Permission { Name = "patientmodule/c32import" };

        /// <summary>
        /// CaseAlerts EditPermission
        /// </summary>
        public static readonly Permission CaseAlertsEditPermission = new Permission { Name = "patientmodule/casealertsedit" };

        /// <summary>
        /// CaseAlerts ViewPermission
        /// </summary>
        public static readonly Permission CaseAlertsViewPermission = new Permission { Name = "patientmodule/casealertsview" };

        /// <summary>
        /// CaseSummary EditPermission
        /// </summary>
        public static readonly Permission CaseSummaryEditPermission = new Permission { Name = "patientmodule/casesummaryedit" };

        /// <summary>
        /// CaseSummary ViewPermission
        /// </summary>
        public static readonly Permission CaseSummaryViewPermission = new Permission { Name = "patientmodule/casesummaryview" };

        /// <summary>
        /// CdsRules EditPermission
        /// </summary>
        public static readonly Permission CdsRulesEditPermission = new Permission { Name = "patientmodule/cdsrulesedit" };

        /// <summary>
        /// CdsRules ViewPermission
        /// </summary>
        public static readonly Permission CdsRulesViewPermission = new Permission { Name = "patientmodule/cdsrulesview" };

        /// <summary>
        /// ClinicalCase EditPermission
        /// </summary>
        public static readonly Permission ClinicalCaseEditPermission = new Permission { Name = "patientmodule/clinicalcaseedit" };

        /// <summary>
        /// ClinicalCase ViewPermission
        /// </summary>
        public static readonly Permission ClinicalCaseViewPermission = new Permission { Name = "patientmodule/clinicalcaseview" };

        /// <summary>
        /// ClinicianDashboard AlertsViewPermission
        /// </summary>
        public static readonly Permission ClinicianDashboardAlertsViewPermission = new Permission
            { Name = "patientmodule/cliniciandashboardalertsview" };

        /// <summary>
        /// ClinicianDashboard EditPermission
        /// </summary>
        public static readonly Permission ClinicianDashboardEditPermission = new Permission { Name = "patientmodule/cliniciandashboardedit" };

        /// <summary>
        /// ClinicianDashboard LabResultsViewPermission
        /// </summary>
        public static readonly Permission ClinicianDashboardLabResultsViewPermission = new Permission
            { Name = "patientmodule/cliniciandashboardlabresultsview" };

        /// <summary>
        /// ClinicianDashboard MedicationOrdersViewPermission
        /// </summary>
        public static readonly Permission ClinicianDashboardMedicationOrdersViewPermission = new Permission
            { Name = "patientmodule/cliniciandashboardmedicationordersview" };

        /// <summary>
        /// ClinicianDashboard ViewPermission
        /// </summary>
        public static readonly Permission ClinicianDashboardViewPermission = new Permission { Name = "patientmodule/cliniciandashboardview" };

        /// <summary>
        /// Dast10Edit Permission
        /// </summary>
        public static readonly Permission Dast10EditPermission = new Permission { Name = "patientmodule/dast10edit" };

        /// <summary>
        /// Dast10 ViewPermission
        /// </summary>
        public static readonly Permission Dast10ViewPermission = new Permission { Name = "patientmodule/dast10view" };

        /// <summary>
        /// DensAsi EditPermission
        /// </summary>
        public static readonly Permission DensAsiEditPermission = new Permission { Name = "patientmodule/densasiedit" };

        /// <summary>
        /// DensAsi ViewPermission
        /// </summary>
        public static readonly Permission DensAsiViewPermission = new Permission { Name = "patientmodule/densasiview" };

        /// <summary>
        /// TEDS EditPermission
        /// </summary>
        public static readonly Permission TedsEditPermission = new Permission { Name = "patientmodule/tedsedit" };

        /// <summary>
        /// TEDS ViewPermission
        /// </summary>
        public static readonly Permission TedsViewPermission = new Permission { Name = "patientmodule/tedsview" };

        /// <summary>
        /// FrontDeskDashboard EditPermission
        /// </summary>
        public static readonly Permission FrontDeskDashboardEditPermission = new Permission { Name = "patientmodule/frontdeskdashboardedit" };

        /// <summary>
        /// FrontDeskDashboard ViewPermission
        /// </summary>
        public static readonly Permission FrontDeskDashboardViewPermission = new Permission { Name = "patientmodule/frontdeskdashboardview" };

        /// <summary>
        /// GainShortScreener EditPermission
        /// </summary>
        public static readonly Permission GainShortScreenerEditPermission = new Permission { Name = "patientmodule/gainshortscreeneredit" };

        /// <summary>
        /// GainShortScreener ViewPermission
        /// </summary>
        public static readonly Permission GainShortScreenerViewPermission = new Permission { Name = "patientmodule/gainshortscreenerview" };

        /// <summary>
        /// GpraInterview EditPermission
        /// </summary>
        public static readonly Permission GpraInterviewEditPermission = new Permission { Name = "patientmodule/gprainterviewedit" };

        /// <summary>
        /// GpraInterview ViewPermission
        /// </summary>
        public static readonly Permission GpraInterviewViewPermission = new Permission { Name = "patientmodule/gprainterviewview" };

        /// <summary>
        /// Immunization EditPermission
        /// </summary>
        public static readonly Permission ImmunizationEditPermission = new Permission { Name = "patientmodule/immunizationedit" };

        /// <summary>
        /// Immunization ViewPermission
        /// </summary>
        public static readonly Permission ImmunizationViewPermission = new Permission { Name = "patientmodule/immunizationview" };

        /// <summary>
        /// IndividualCounseling EditPermission
        /// </summary>
        public static readonly Permission IndividualCounselingEditPermission = new Permission { Name = "patientmodule/individualcounselingedit" };

        /// <summary>
        /// IndividualCounseling ViewPermission
        /// </summary>
        public static readonly Permission IndividualCounselingViewPermission = new Permission { Name = "patientmodule/individualcounselingview" };

        /// <summary>
        /// Interoperability WorkspaceViewPermission
        /// </summary>
        public static readonly Permission InteroperabilityWorkspaceViewPermission = new Permission
            { Name = "patientmodule/interoperabilityworkspaceview" };

        /// <summary>
        /// LabResults EditPermission
        /// </summary>
        public static readonly Permission LabResultsEditPermission = new Permission { Name = "patientmodule/labresultsedit" };

        /// <summary>
        /// LabResults ViewPermission
        /// </summary>
        public static readonly Permission LabResultsViewPermission = new Permission { Name = "patientmodule/labresultsview" };

        /// <summary>
        /// Medication EditPermission
        /// </summary>
        public static readonly Permission MedicationEditPermission = new Permission { Name = "patientmodule/medicationedit" };

        /// <summary>
        /// Medication ViewPermission
        /// </summary>
        public static readonly Permission MedicationViewPermission = new Permission { Name = "patientmodule/medicationview" };

        /// <summary>
        /// NidaEdit Permission
        /// </summary>
        public static readonly Permission NidaEditPermission = new Permission { Name = "patientmodule/nidaedit" };

        /// <summary>
        /// NidaView Permission
        /// </summary>
        public static readonly Permission NidaViewPermission = new Permission { Name = "patientmodule/nidaview" };

        /// <summary>
        /// PatientAccessHistory ViewPermission
        /// </summary>
        public static readonly Permission PatientAccessHistoryViewPermission = new Permission { Name = "patientmodule/patientaccesshistoryview" };

        /// <summary>
        /// PatientDashboard EditPermission
        /// </summary>
        public static readonly Permission PatientDashboardEditPermission = new Permission { Name = "patientmodule/patientdashboardedit" };

        /// <summary>
        /// PatientDashboard ViewPermission
        /// </summary>
        public static readonly Permission PatientDashboardViewPermission = new Permission { Name = "patientmodule/patientdashboardview" };

        /// <summary>
        /// PatientEdit Permission
        /// </summary>
        public static readonly Permission PatientEditPermission = new Permission { Name = "patientmodule/patientedit" };

        /// <summary>
        /// PatientExternalHistory EditPermission
        /// </summary>
        public static readonly Permission PatientExternalHistoryEditPermission = new Permission { Name = "patientmodule/patientexternalhistoryedit" };

        /// <summary>
        /// PatientExternalHistory ViewPermission
        /// </summary>
        public static readonly Permission PatientExternalHistoryViewPermission = new Permission { Name = "patientmodule/patientexternalhistoryview" };

        /// <summary>
        /// PatientList ViewPermission
        /// </summary>
        public static readonly Permission PatientListViewPermission = new Permission { Name = "patientmodule/patientlistview" };

        /// <summary>
        /// PatientReminders ViewPermission
        /// </summary>
        public static readonly Permission PatientRemindersViewPermission = new Permission { Name = "patientmodule/patientremindersview" };

        /// <summary>
        /// PatientSearch ViewPermission
        /// </summary>
        public static readonly Permission PatientSearchViewPermission = new Permission { Name = "patientmodule/patientsearchview" };

        /// <summary>
        /// Patient ViewPermission
        /// </summary>
        public static readonly Permission PatientViewPermission = new Permission { Name = "patientmodule/patientview" };

        /// <summary>
        /// PatientWorkspace ViewPermission
        /// </summary>
        public static readonly Permission PatientWorkspaceViewPermission = new Permission { Name = "patientmodule/patientworkspaceview" };

        /// <summary>
        /// Phq9 EditPermission
        /// </summary>
        public static readonly Permission Phq9EditPermission = new Permission { Name = "patientmodule/phq9edit" };

        /// <summary>
        /// Phq9 ViewPermission
        /// </summary>
        public static readonly Permission Phq9ViewPermission = new Permission { Name = "patientmodule/phq9view" };

        /// <summary>
        /// Problem EditPermission
        /// </summary>
        public static readonly Permission ProblemEditPermission = new Permission { Name = "patientmodule/problemedit" };

        /// <summary>
        /// Problem ViewPermission
        /// </summary>
        public static readonly Permission ProblemViewPermission = new Permission { Name = "patientmodule/problemview" };

        /// <summary>
        /// RadiologyOrders EditPermission
        /// </summary>
        public static readonly Permission RadiologyOrdersEditPermission = new Permission { Name = "patientmodule/radiologyordersedit" };

        /// <summary>
        /// RadiologyOrders ViewPermission
        /// </summary>
        public static readonly Permission RadiologyOrdersViewPermission = new Permission { Name = "patientmodule/radiologyordersview" };

        /// <summary>
        /// ScheduledAndRecent ActivitiesEditPermission
        /// </summary>
        public static readonly Permission ScheduledAndRecentActivitiesEditPermission = new Permission
            { Name = "patientmodule/scheduled&recentactivitiesedit" };

        /// <summary>
        /// ScheduledAndRecent ActivitiesViewPermission
        /// </summary>
        public static readonly Permission ScheduledAndRecentActivitiesViewPermission = new Permission
            { Name = "patientmodule/scheduled&recentactivitiesview" };

        /// <summary>
        /// SocialHistory EditPermission
        /// </summary>
        public static readonly Permission SocialHistoryEditPermission = new Permission { Name = "patientmodule/socialhistoryedit" };

        /// <summary>
        /// SocialHistory ViewPermission
        /// </summary>
        public static readonly Permission SocialHistoryViewPermission = new Permission { Name = "patientmodule/socialhistoryview" };

        /// <summary>
        /// SystemAccount ViewPermission
        /// </summary>
        public static readonly Permission SystemAccountViewPermission = new Permission { Name = "patientmodule/systemaccountview" };

        /// <summary>
        /// Visit EditPermission
        /// </summary>
        public static readonly Permission VisitEditPermission = new Permission { Name = "patientmodule/visitedit" };

        /// <summary>
        /// Visit ViewPermission
        /// </summary>
        public static readonly Permission VisitViewPermission = new Permission { Name = "patientmodule/visitview" };

        /// <summary>
        /// VitalSigns EditPermission
        /// </summary>
        public static readonly Permission VitalSignsEditPermission = new Permission { Name = "patientmodule/vitalsignsedit" };

        /// <summary>
        /// VitalSigns ViewPermission
        /// </summary>
        public static readonly Permission VitalSignsViewPermission = new Permission { Name = "patientmodule/vitalsignsview" };

        #endregion
    }
}
