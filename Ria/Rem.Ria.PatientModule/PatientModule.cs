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

using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.Common.WCF;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Bootstrapper;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.PatientModule.CdsAlerts;
using Rem.Ria.PatientModule.CdsRuleEditor;
using Rem.Ria.PatientModule.ClinicalCaseEditor;
using Rem.Ria.PatientModule.ClinicianDashboard;
using Rem.Ria.PatientModule.Common;
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
using Rem.Ria.PatientModule.Service;
using Rem.Ria.PatientModule.StaffAlerts;
using Rem.Ria.PatientModule.SystemAccountSearch;
using Rem.Ria.PatientModule.TedsInterview;
using Rem.WellKnownNames.VisitModule;
using TerminologyService.Client.SL;

namespace Rem.Ria.PatientModule
{
    /// <summary>
    /// PatientModule class.
    /// </summary>
    public class PatientModule : IModule
    {
        #region Constants and Fields

        /// <summary>
        /// This is the region where all the Workspaces (Tabs) are inserted. E.g.:Patient Workspace
        /// </summary>
        public static readonly string WorkspacesRegion = "WorkspacesRegion";

        private readonly IAccessControlManager _accessControlManager;
        private readonly IUnityContainer _container;
        private readonly ICurrentUserPermissionService _currentUserPermissionService;
        private readonly IMetadataService _metadataService;
        private readonly INavigationService _navigationService;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="container">The container.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="currentUserPermissionService">The current user permission service.</param>
        /// <param name="navigationService">The navigation service.</param>
        public PatientModule (
            IRegionManager regionManager,
            IUnityContainer container,
            IAccessControlManager accessControlManager,
            IMetadataService metadataService,
            ICurrentUserPermissionService currentUserPermissionService,
            INavigationService navigationService )
        {
            _regionManager = regionManager;
            _container = container;
            _accessControlManager = accessControlManager;
            _metadataService = metadataService;
            _currentUserPermissionService = currentUserPermissionService;
            _navigationService = navigationService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize ()
        {
            RegisterServices ();

            RegisterQuickPickers();
            RegisterPatientWorkspaceScreens ();
            RegisterFrontDeskDashboardScreens ();
            RegisterClinicianDashboardScreens ();
            RegisterInteroperabilityScreens ();
            RegisterCdsRuleEditorScreens ();

            RegisterInteroperabilityWorkspaceScreens ();
            RegisterSearchScreens ();

            RegisterActivityViews ();

            RegisterRuleCollections ();

            RegisterDirectMessageCenterScreens ();

            _currentUserPermissionService.RegisterForPermissions ( UserInitialized );
        }

        private void RegisterDirectMessageCenterScreens ()
        {
            _container
                .SimplyRegisterType<MessageCenterWorkspaceView> ()
                .SimplyRegisterType<HealthProvidersDirectoryView> ()
                .SimplyRegisterType<SendC32View> ()
                .SimplyRegisterType<SendNewMailView>()
                .SimplyRegisterType<SaveMailAttachmentPatientDocumentView>();
        }

        private void RegisterQuickPickers ()
        {
            _regionManager.RegisterViewWithRegion (
                "PayorCacheQuickPicker",
                () => _container.Resolve<PayorCacheQuickPickerView> () );
        }

        /// <summary>
        /// Users the initialized.
        /// </summary>
        public void UserInitialized ()
        {
            _navigationService.Navigate ( WorkspacesRegion, "FrontDeskDashboardView" );

            // TODO: MessageCenterWorkspaceView should not always show. 
            _navigationService.Navigate(WorkspacesRegion, "MessageCenterWorkspaceView");

            _regionManager.Regions[WorkspacesRegion].Activate ( _regionManager.Regions[WorkspacesRegion].Views.First () );

            _navigationService.Navigate (
                "HomePageTilesRegion",
                "ClinicianDashboardScheduleTileView",
                null,
                new[] { new KeyValuePair<string, string> ( "SelectedDate", DateTime.Now.ToShortDateString () ) } );
            _navigationService.Navigate ( "HomePageTilesRegion", "StaffAlertsTileView" );
            _navigationService.Navigate ( "HomePageTilesRegion", "ClinicianMedicationOrdersTileView" );
            _navigationService.Navigate ( "HomePageTilesRegion", "ClinicianLabResultsTileView" );
            _navigationService.Navigate ( "HomePageTilesRegion", "ClinicianPatientListView" );
        }

        #endregion

        #region Methods

        private void RegisterActivityViews()
        {
            var activityTypeToViewMapperService = _container.Resolve<ActivityTypeToViewMapperService> ();
            _container.RegisterInstance (
                typeof( IActivityTypeToViewMapperService ), activityTypeToViewMapperService, new ContainerControlledLifetimeManager () );

            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( VitalsView ), ActivityType.VitalSign );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( LabResultsView ), ActivityType.LabSpecimen );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( RadiologyOrderView ), ActivityType.RadiologyOrder );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( ImmunizationView ), ActivityType.Immunization );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( SocialHistoryView ), ActivityType.SocialHistory );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( BriefInterventionView ), ActivityType.BriefIntervention );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( AuditCView ), ActivityType.AuditC );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( Phq9View ), ActivityType.Phq9 );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( IndividualCounselingView ), ActivityType.IndividualCounseling );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( Dast10View ), ActivityType.Dast10 );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( GpraInterviewView ), ActivityType.GpraInterview );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( DensAsiInterviewView ), ActivityType.DensAsiInterview );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( GainShortScreenerView ), ActivityType.GainShortScreener );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( AuditView ), ActivityType.Audit );
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( NidaDrugQuestionnaireView ), ActivityType.NidaDrugQuestionnaire );
            activityTypeToViewMapperService.RegisterViewForActivityType(typeof(TedsAdmissionInterviewView), ActivityType.TedsAdmissionInterview);
            activityTypeToViewMapperService.RegisterViewForActivityType ( typeof( TedsDischargeInterviewView ), ActivityType.TedsDischargeInterview );
        }

        private void RegisterCdsRuleEditorScreens ()
        {
            _container.RegisterType<object, CdsRuleEditorView> ( "CdsRuleEditorView" );
        }

        private void RegisterClinicianDashboardScreens ()
        {
            _container.RegisterType<object, ClinicianLabResultsTileView> ( "ClinicianLabResultsTileView" );
            _container.RegisterType<object, ClinicianMedicationOrdersTileView> ( "ClinicianMedicationOrdersTileView" );
            _container.RegisterType<object, CdsAlertView> ( "CdsAlertView" );
            _container.RegisterType<object, ClinicianPatientListView> ( "ClinicianPatientListView" );
            _container.RegisterType<object, ClinicianDashboardScheduleTileView> ( "ClinicianDashboardScheduleTileView" );
            _container.RegisterType<object, StaffAlertsTileView> ( "StaffAlertsTileView" );
        }

        private void RegisterFrontDeskDashboardScreens ()
        {
            _container.RegisterType<object, FrontDeskDashboardView> ( "FrontDeskDashboardView" );
            _container.RegisterType<object, PatientAccessHistoryView> ( "PatientAccessHistoryView" );
            _container.RegisterType<object, AppointmentSchedulerView> ( "AppointmentSchedulerView" );
            _container.RegisterType<object, ClinicianScheduleTileView> ( "ClinicianScheduleTileView" );
            _container.RegisterType<object, AppointmentDetailsView> ( "AppointmentDetailsView" );
            _container.RegisterType<object, BillingView>("BillingView");
            _container.RegisterType<object, PatientSummaryView>("PatientSummaryView");
            _container.RegisterType<object, SelfPaymentView>("SelfPaymentView");
            _container.RegisterType<object, PayorCoverageView>("PayorCoverageView");
            _container.RegisterType<object, AddSelfPaymentView>("AddSelfPaymentView");
            _container.RegisterType<object, EditPayorCoverageView> ( "EditPayorCoverageView" );
        }

        private void RegisterInteroperabilityScreens ()
        {
            _container.RegisterType<object, TerminologyVocabularyView> ( "TerminologyVocabularyView" );
            _container.RegisterType<object, SingleConceptView> ( "SingleConceptView" );
            _container.RegisterType<object, DroolsRestTestView> ( "DroolsRestTestView" );
        }

        private void RegisterInteroperabilityWorkspaceScreens ()
        {
            _container.RegisterType<object, InteroperabilityWorkspaceView> ( "InteroperabilityWorkspaceView" );
        }

        private void RegisterPatientWorkspaceScreens()
        {
            _container.RegisterType<object, PatientWorkspaceView> ( "PatientWorkspaceView" );
            _container.RegisterType<object, PatientDashboardView> ( "PatientDashboardView" );

            _container.RegisterType<object, PatientEditorView> ( "PatientEditorView" );

            _container.RegisterType<object, VisitListView> ( "VisitListView" );
            _container.RegisterType<object, MedicationListView> ( "MedicationListView" );
            _container.RegisterType<object, CaseProblemListView> ( "CaseProblemListView" );
            _container.RegisterType<object, EditCaseProblemView> ( "EditCaseProblemView" );
            _container.RegisterType<object, CaseSnapshotView> ( "CaseSnapshotView" );
            _container.RegisterType<object, VisitView> ( "VisitView" );
            _container.RegisterType<object, VitalsView> ( "VitalsView" );
            _container.RegisterType<object, RadiologyOrderView> ( "RadiologyOrderView" );
            _container.RegisterType<object, ImmunizationView> ( "ImmunizationView" );
            _container.RegisterType<object, BriefInterventionView> ( "BriefInterventionView" );
            _container.RegisterType<object, AuditCView> ( "AuditCView" );
            _container.RegisterType<object, AuditCResultView> ( "AuditCResultView" );
            _container.RegisterType<object, AuditView> ( "AuditView" );
            _container.RegisterType<object, AuditResultView> ( "AuditResultView" );
            _container.RegisterType<object, LabResultsView> ( "LabResultsView" );
            _container.RegisterType<object, ClinicalCaseEditorView> ( "ClinicalCaseEditorView" );
            _container.RegisterType<object, CaseActivitiesView> ( "CaseActivitiesView" );
            _container.RegisterType<object, CaseSummaryView> ( "CaseSummaryView" );
            _container.RegisterType<object, CaseAlertsView> ( "CaseAlertsView" );
            _container.RegisterType<object, ExternalPatientHistoryView> ( "ExternalPatientHistoryView" );
            _container.RegisterType<object, UploadDocumentView> ( "UploadDocumentView" );
            _container.RegisterType<object, PatientHistoryDocumentView> ( "PatientHistoryDocumentView" );
            _container.RegisterType<object, GrowthRateView> ( "GrowthRateView" );
            _container.RegisterType<object, EditMedicationView> ( "EditMedicationView" );
            _container.RegisterType<object, Phq9View> ( "Phq9View" );
            _container.RegisterType<object, Dast10View> ( "Dast10View" );
            _container.RegisterType<object, Dast10ResultView> ( "Dast10ResultView" );
            _container.RegisterType<object, NidaDrugQuestionnaireView> ( "NidaDrugQuestionnaireView" );
            _container.RegisterType<object, Phq9ResultView> ( "Phq9ResultView" );
            _container.RegisterType<object, PatientListView> ( "PatientListView" );
            _container.RegisterType<object, PatientReminderView> ( "PatientReminderView" );
            _container.RegisterType<object, SocialHistoryView> ( "SocialHistoryView" );
            _container.RegisterType<object, SocialHistorySmokingCessationAdvisoryView> ( "SocialHistorySmokingCessationAdvisoryView" );
            _container.RegisterType<object, IndividualCounselingView> ( "IndividualCounselingView" );
            _container.RegisterType<object, GpraInterviewView> ( "GpraInterviewView" );
            _container.RegisterType<object, DensAsiInterviewView> ( "DensAsiInterviewView" );
            _container.RegisterType<object, GainShortScreenerView> ( "GainShortScreenerView" );
            _container.RegisterType<object, PatientContactEditorView> ( "PatientContactEditorView" );
            _container.RegisterType<object, ProgramEnrollmentListView> ( "ProgramEnrollmentListView" );
            _container.RegisterType<object, CreateProgramEnrollmentView> ( "CreateProgramEnrollmentView" );
            _container.RegisterType<object, EditProgramEnrollmentView> ( "EditProgramEnrollmentView" );
            _container.RegisterType<object, DisenrollProgramEnrollmentView> ( "DisenrollProgramEnrollmentView" );
            _container.RegisterType<object, TedsAdmissionInterviewView>("TedsAdmissionInterviewView");
            _container.RegisterType<object, TedsDischargeInterviewView>("TedsDischargeInterviewView");
            _container.RegisterType<object, ImportC32View>("ImportC32View");
        }

        private void RegisterRuleCollections ()
        {
            _container.RegisterType<IRuleCollection<GpraInterviewViewModel>, GpraInterviewViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
            _container.RegisterType<IRuleCollection<NidaDrugQuestionnaireViewModel>, NidaDrugQuestionnaireViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
            _container.RegisterType<IRuleCollection<EditMedicationViewModel>, EditMedicationViewModelRuleCollection> (
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRuleCollection<PatientListViewModel>, PatientListViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
            _container.RegisterType<IRuleCollection<PatientReminderViewModel>, PatientReminderViewModelRuleCollection> (
                new ContainerControlledLifetimeManager () );
            _container.RegisterType<IRuleCollection<DensAsiInterviewViewModel>, DensAsiInterviewViewModelRuleCollection>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRuleCollection<AddSelfPaymentViewModel>, AddSelfPaymentViewModelRuleCollection>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRuleCollection<EditPayorCoverageViewModel>, EditPayorCoverageViewModelRuleCollection>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRuleCollection<TedsAdmissionInterviewViewModel>, TedsAdmissionInterviewViewModelRuleCollection>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRuleCollection<TedsDischargeInterviewViewModel>, TedsDischargeInterviewViewModelRuleCollection>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRuleCollection<SaveMailAttachmentPatientDocumentViewModel>, SaveMailAttachmentPatientDocumentViewModelRuleCollection>(
                new ContainerControlledLifetimeManager());
        }

        private void RegisterSearchScreens ()
        {
            _regionManager.RegisterViewWithRegion ( "PatientSearch", () => _container.Resolve<PatientSearchView> () );
            _regionManager.RegisterViewWithRegion ( "SystemAccountSearch", () => _container.Resolve<SystemAccountSearchView> () );

            _container.RegisterType<object, MainPatientSearchView> ( "MainPatientSearchView", new ContainerControlledLifetimeManager () );

            _regionManager.RegisterViewWithRegion ( "ProblemDtsSearch", () => new DTSSearchView ( _container.Resolve<ProblemDtsSearchViewModel> () ) );
            _regionManager.RegisterViewWithRegion (
                "MedicationDtsSearch", () => new DTSSearchView ( _container.Resolve<MedicationDtsSearchViewModel> () ) );
            _regionManager.RegisterViewWithRegion ( "AllergyDtsSearch", () => new DTSSearchView ( _container.Resolve<AllergyDtsSearchViewModel> () ) );
            _regionManager.RegisterViewWithRegion (
                "DrugAllergyDtsSearch", () => new DTSSearchView ( _container.Resolve<DrugAllergyDtsSearchViewModel> () ) );
            _regionManager.RegisterViewWithRegion (
                "LabTestNameDtsSearch", () => new DTSSearchView ( _container.Resolve<LabTestNameDtsSearchViewModel> () ) );
            _regionManager.RegisterViewWithRegion (
                "ImmunizationDtsSearch", () => new DTSSearchView ( _container.Resolve<VaccineNameDtsSearchViewModel> () ) );
        }

        private void RegisterServices ()
        {
            var genericsToApply = KnownTypeHelper.GetGenerics ( typeof( Const ).Assembly );
            KnownTypeHelper.RegisterNonGenericRequestsAndResponses ( typeof( PatientModule ).Assembly );
            KnownTypeHelper.RegisterGenerics ( genericsToApply, typeof( PatientModule ).Assembly );
            KnownTypeHelper.RegisterKnownTypesFromIKnownTypeProviders ( typeof( PatientModule ).Assembly );

            KnownTypeProvider.RegisterDerivedTypesOf<AbstractDataTransferObject>(typeof(PatientModule).Assembly.GetTypes().Where ( t => !t.IsGenericType ) );

            _container.RegisterType<ITerminologyProxy, TerminologyProxy> ();
            _container.RegisterType<ICdsAlertService, CdsAlertService> ();
            _container.RegisterType<IPatientAccessService, PatientAccessService> ();
            _accessControlManager.RegisterPermissionDescriptor (
                new ClientPermissionDescriptor () );
            _metadataService.LoadMetadataForModule ( this );
        }

        #endregion
    }
}
