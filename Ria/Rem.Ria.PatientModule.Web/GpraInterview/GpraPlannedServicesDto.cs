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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraPlannedServices class.
    /// </summary>
    public class GpraPlannedServicesDto : GpraDtoBase
    {
        #region Constants and Fields

        private bool? _afterCareContinuingCareIndicator;
        private bool? _afterCareOtherIndicator;
        private bool? _afterCareRecoveryCoachingIndicator;
        private bool? _afterCareReplasePreventionIndicator;
        private bool? _afterCareSelfHelpSupportGroupsIndicator;
        private string _afterCareSpecificationNote;
        private bool? _afterCareSpiritualSupportIndicator;
        private bool? _caseMgmtChildCareIndicator;
        private bool? _caseMgmtEmploymentCoachingIndicator;
        private bool? _caseMgmtFamilyServicesIndicator;
        private bool? _caseMgmtHivAidsServiceIndicator;
        private bool? _caseMgmtIndividualServicesCoordinationIndicator;
        private bool? _caseMgmtOtherIndicator;
        private bool? _caseMgmtPreemploymentIndicator;
        private string _caseMgmtSpecificationNote;
        private bool? _caseMgmtTransitionalDrugFreeHousingIndicator;
        private bool? _caseMgmtTransportationIndicator;
        private bool? _educationHivAidsIndicator;
        private bool? _educationOtherIndicator;
        private bool? _educationSaIndicator;
        private string _educationSpecificationNote;
        private bool? _medicalAlcoholDrugTestIndicator;
        private bool? _medicalCareIndicator;
        private bool? _medicalHivAidsSupportAndTestingIndicator;
        private bool? _medicalOtherIndicator;
        private string _medicalSpecificationNote;
        private bool? _modalityAfterCareIndicator;
        private bool? _modalityCaseMgmtIndicator;
        private bool? _modalityDayTreatmentIndicator;
        private LookupValueDto _modalityGpraDetoxificationLocation;
        private bool? _modalityInpatientHospitalIndicator;
        private bool? _modalityIntensiveOutpatientIndicator;
        private bool? _modalityMethadoneIndicator;
        private bool? _modalityOtherSpecificationIndicator;
        private bool? _modalityOutpatientIndicator;
        private bool? _modalityOutreachIndicator;
        private bool? _modalityRecoverySupportIndicator;
        private bool? _modalityResidentialRehabilitationIndicator;
        private string _modalitySpecificationNote;
        private bool? _peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator;
        private bool? _peerToPeerRecoverySupportCoachingIndicator;
        private bool? _peerToPeerRecoverySupportHousingIndicator;
        private bool? _peerToPeerRecoverySupportInformationReferralIndicator;
        private bool? _peerToPeerRecoverySupportOtherIndicator;
        private string _peerToPeerRecoverySupportSpecificationNote;
        private bool? _treatmentAssessmentIndicator;
        private bool? _treatmentBriefInterventionIndicator;
        private bool? _treatmentBriefTreatmentIndicator;
        private bool? _treatmentCooccuringTreatmentIndicator;
        private bool? _treatmentFamilyCounselingIndicator;
        private bool? _treatmentGroupCounselingIndicator;
        private bool? _treatmentHivAidsCounselingIndicator;
        private bool? _treatmentIndividualCounselingIndicator;
        private bool? _treatmentOtherSpecificationIndicator;
        private bool? _treatmentPharmacologicalInterventionsIndicator;
        private bool? _treatmentRecoveryPlanningIndicator;
        private bool? _treatmentReferralToTreatmentIndicator;
        private bool? _treatmentScreeningIndicator;
        private string _treatmentSpecificationNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// After Care Services Question 1: Continuing Care
        /// </summary>
        /// <value>The after care continuing care indicator.</value>
        public bool? AfterCareContinuingCareIndicator
        {
            get { return _afterCareContinuingCareIndicator; }
            set { ApplyPropertyChange ( ref _afterCareContinuingCareIndicator, () => AfterCareContinuingCareIndicator, value ); }
        }

        /// <summary>
        /// After Care Services Question 6: Other After Care Services (Specify)
        /// </summary>
        /// <value>The after care other indicator.</value>
        public bool? AfterCareOtherIndicator
        {
            get { return _afterCareOtherIndicator; }
            set { ApplyPropertyChange ( ref _afterCareOtherIndicator, () => AfterCareOtherIndicator, value ); }
        }

        /// <summary>
        /// After Care Services Question 3: Recovery Coaching
        /// </summary>
        /// <value>The after care recovery coaching indicator.</value>
        public bool? AfterCareRecoveryCoachingIndicator
        {
            get { return _afterCareRecoveryCoachingIndicator; }
            set { ApplyPropertyChange ( ref _afterCareRecoveryCoachingIndicator, () => AfterCareRecoveryCoachingIndicator, value ); }
        }

        /// <summary>
        /// After Care Services Question 2: Relapse Prevention
        /// </summary>
        /// <value>The after care replase prevention indicator.</value>
        public bool? AfterCareReplasePreventionIndicator
        {
            get { return _afterCareReplasePreventionIndicator; }
            set { ApplyPropertyChange ( ref _afterCareReplasePreventionIndicator, () => AfterCareReplasePreventionIndicator, value ); }
        }

        /// <summary>
        /// After Care Services Question 4: Self-Help and Support Groups
        /// </summary>
        /// <value>The after care self help support groups indicator.</value>
        public bool? AfterCareSelfHelpSupportGroupsIndicator
        {
            get { return _afterCareSelfHelpSupportGroupsIndicator; }
            set { ApplyPropertyChange ( ref _afterCareSelfHelpSupportGroupsIndicator, () => AfterCareSelfHelpSupportGroupsIndicator, value ); }
        }

        /// <summary>
        /// After Care Services Question 6 specification
        /// </summary>
        /// <value>The after care specification note.</value>
        public string AfterCareSpecificationNote
        {
            get { return _afterCareSpecificationNote; }
            set { ApplyPropertyChange ( ref _afterCareSpecificationNote, () => AfterCareSpecificationNote, value ); }
        }

        /// <summary>
        /// After Care Services Question 5: Spiritual Support
        /// </summary>
        /// <value>The after care spiritual support indicator.</value>
        public bool? AfterCareSpiritualSupportIndicator
        {
            get { return _afterCareSpiritualSupportIndicator; }
            set { ApplyPropertyChange ( ref _afterCareSpiritualSupportIndicator, () => AfterCareSpiritualSupportIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 2: Child Care
        /// </summary>
        /// <value>The case MGMT child care indicator.</value>
        public bool? CaseMgmtChildCareIndicator
        {
            get { return _caseMgmtChildCareIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtChildCareIndicator, () => CaseMgmtChildCareIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 3: Employment Service.  B. Employment Coaching
        /// </summary>
        /// <value>The case MGMT employment coaching indicator.</value>
        public bool? CaseMgmtEmploymentCoachingIndicator
        {
            get { return _caseMgmtEmploymentCoachingIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtEmploymentCoachingIndicator, () => CaseMgmtEmploymentCoachingIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 1: Family Services (Including Marriage Education, Parenting, Child Development Services)
        /// </summary>
        /// <value>The case MGMT family services indicator.</value>
        public bool? CaseMgmtFamilyServicesIndicator
        {
            get { return _caseMgmtFamilyServicesIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtFamilyServicesIndicator, () => CaseMgmtFamilyServicesIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 6: HIV/AIDS Service
        /// </summary>
        /// <value>The case MGMT hiv aids service indicator.</value>
        public bool? CaseMgmtHivAidsServiceIndicator
        {
            get { return _caseMgmtHivAidsServiceIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtHivAidsServiceIndicator, () => CaseMgmtHivAidsServiceIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 4: Individual Services Coordination
        /// </summary>
        /// <value>The case MGMT individual services coordination indicator.</value>
        public bool? CaseMgmtIndividualServicesCoordinationIndicator
        {
            get { return _caseMgmtIndividualServicesCoordinationIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _caseMgmtIndividualServicesCoordinationIndicator, () => CaseMgmtIndividualServicesCoordinationIndicator, value );
            }
        }

        /// <summary>
        /// Case Management Services Question 8: Other Case Management Services
        /// </summary>
        /// <value>The case MGMT other indicator.</value>
        public bool? CaseMgmtOtherIndicator
        {
            get { return _caseMgmtOtherIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtOtherIndicator, () => CaseMgmtOtherIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 3: Employment Service.  A. Pre-employement
        /// </summary>
        /// <value>The case MGMT preemployment indicator.</value>
        public bool? CaseMgmtPreemploymentIndicator
        {
            get { return _caseMgmtPreemploymentIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtPreemploymentIndicator, () => CaseMgmtPreemploymentIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 8 specification
        /// </summary>
        /// <value>The case MGMT specification note.</value>
        public string CaseMgmtSpecificationNote
        {
            get { return _caseMgmtSpecificationNote; }
            set { ApplyPropertyChange ( ref _caseMgmtSpecificationNote, () => CaseMgmtSpecificationNote, value ); }
        }

        /// <summary>
        /// Case Management Services Question 7: Supportive Transitional Drug-Free Housing Services
        /// </summary>
        /// <value>The case MGMT transitional drug free housing indicator.</value>
        public bool? CaseMgmtTransitionalDrugFreeHousingIndicator
        {
            get { return _caseMgmtTransitionalDrugFreeHousingIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtTransitionalDrugFreeHousingIndicator, () => CaseMgmtTransitionalDrugFreeHousingIndicator, value ); }
        }

        /// <summary>
        /// Case Management Services Question 5: Transportation
        /// </summary>
        /// <value>The case MGMT transportation indicator.</value>
        public bool? CaseMgmtTransportationIndicator
        {
            get { return _caseMgmtTransportationIndicator; }
            set { ApplyPropertyChange ( ref _caseMgmtTransportationIndicator, () => CaseMgmtTransportationIndicator, value ); }
        }

        /// <summary>
        /// Education Services Question 2: HIV/AIDS Education
        /// </summary>
        /// <value>The education hiv aids indicator.</value>
        public bool? EducationHivAidsIndicator
        {
            get { return _educationHivAidsIndicator; }
            set { ApplyPropertyChange ( ref _educationHivAidsIndicator, () => EducationHivAidsIndicator, value ); }
        }

        /// <summary>
        /// Education Services Question 3: Other Education Services (Specify)
        /// </summary>
        /// <value>The education other indicator.</value>
        public bool? EducationOtherIndicator
        {
            get { return _educationOtherIndicator; }
            set { ApplyPropertyChange ( ref _educationOtherIndicator, () => EducationOtherIndicator, value ); }
        }

        /// <summary>
        /// Education Services Question 1: Substance Abuse Education
        /// </summary>
        /// <value>The education sa indicator.</value>
        public bool? EducationSaIndicator
        {
            get { return _educationSaIndicator; }
            set { ApplyPropertyChange ( ref _educationSaIndicator, () => EducationSaIndicator, value ); }
        }

        /// <summary>
        /// Education Services Question 3 specification
        /// </summary>
        /// <value>The education specification note.</value>
        public string EducationSpecificationNote
        {
            get { return _educationSpecificationNote; }
            set { ApplyPropertyChange ( ref _educationSpecificationNote, () => EducationSpecificationNote, value ); }
        }

        /// <summary>
        /// Medical Services Question 2: Alcohol/Drug Testing
        /// </summary>
        /// <value>The medical alcohol drug test indicator.</value>
        public bool? MedicalAlcoholDrugTestIndicator
        {
            get { return _medicalAlcoholDrugTestIndicator; }
            set { ApplyPropertyChange ( ref _medicalAlcoholDrugTestIndicator, () => MedicalAlcoholDrugTestIndicator, value ); }
        }

        /// <summary>
        /// Medical Services Question 1: Medical Care
        /// </summary>
        /// <value>The medical care indicator.</value>
        public bool? MedicalCareIndicator
        {
            get { return _medicalCareIndicator; }
            set { ApplyPropertyChange ( ref _medicalCareIndicator, () => MedicalCareIndicator, value ); }
        }

        /// <summary>
        /// Medical Services Question 3: HIV/ AIDS Medical Support and Testing
        /// </summary>
        /// <value>The medical hiv aids support and testing indicator.</value>
        public bool? MedicalHivAidsSupportAndTestingIndicator
        {
            get { return _medicalHivAidsSupportAndTestingIndicator; }
            set { ApplyPropertyChange ( ref _medicalHivAidsSupportAndTestingIndicator, () => MedicalHivAidsSupportAndTestingIndicator, value ); }
        }

        /// <summary>
        /// Medical Services Question 4: Other Medical Services (Specify)
        /// </summary>
        /// <value>The medical other indicator.</value>
        public bool? MedicalOtherIndicator
        {
            get { return _medicalOtherIndicator; }
            set { ApplyPropertyChange ( ref _medicalOtherIndicator, () => MedicalOtherIndicator, value ); }
        }

        /// <summary>
        /// Medical Services Question 4 specification
        /// </summary>
        /// <value>The medical specification note.</value>
        public string MedicalSpecificationNote
        {
            get { return _medicalSpecificationNote; }
            set { ApplyPropertyChange ( ref _medicalSpecificationNote, () => MedicalSpecificationNote, value ); }
        }

        /// <summary>
        /// Modality Question 10: After Care
        /// </summary>
        /// <value>The modality after care indicator.</value>
        public bool? ModalityAfterCareIndicator
        {
            get { return _modalityAfterCareIndicator; }
            set { ApplyPropertyChange ( ref _modalityAfterCareIndicator, () => ModalityAfterCareIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 1: Case Management
        /// </summary>
        /// <value>The modality case MGMT indicator.</value>
        public bool? ModalityCaseMgmtIndicator
        {
            get { return _modalityCaseMgmtIndicator; }
            set { ApplyPropertyChange ( ref _modalityCaseMgmtIndicator, () => ModalityCaseMgmtIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 2: Day Treatment
        /// </summary>
        /// <value>The modality day treatment indicator.</value>
        public bool? ModalityDayTreatmentIndicator
        {
            get { return _modalityDayTreatmentIndicator; }
            set { ApplyPropertyChange ( ref _modalityDayTreatmentIndicator, () => ModalityDayTreatmentIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 9: Detoxification (Select Only One). A. Hospital Inpatient, B. Free Standing Residential, C. Ambulatory Detoxification
        /// </summary>
        /// <value>The modality gpra detoxification location.</value>
        public LookupValueDto ModalityGpraDetoxificationLocation
        {
            get { return _modalityGpraDetoxificationLocation; }
            set { ApplyPropertyChange ( ref _modalityGpraDetoxificationLocation, () => ModalityGpraDetoxificationLocation, value ); }
        }

        /// <summary>
        /// Modality Question 3: Inpatient/Hospital (Other Than Detox)
        /// </summary>
        /// <value>The modality inpatient hospital indicator.</value>
        public bool? ModalityInpatientHospitalIndicator
        {
            get { return _modalityInpatientHospitalIndicator; }
            set { ApplyPropertyChange ( ref _modalityInpatientHospitalIndicator, () => ModalityInpatientHospitalIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 6: Intensive Outpatient
        /// </summary>
        /// <value>The modality intensive outpatient indicator.</value>
        public bool? ModalityIntensiveOutpatientIndicator
        {
            get { return _modalityIntensiveOutpatientIndicator; }
            set { ApplyPropertyChange ( ref _modalityIntensiveOutpatientIndicator, () => ModalityIntensiveOutpatientIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 7: Methadone
        /// </summary>
        /// <value>The modality methadone indicator.</value>
        public bool? ModalityMethadoneIndicator
        {
            get { return _modalityMethadoneIndicator; }
            set { ApplyPropertyChange ( ref _modalityMethadoneIndicator, () => ModalityMethadoneIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 12: Other (Specify)
        /// </summary>
        /// <value>The modality other specification indicator.</value>
        public bool? ModalityOtherSpecificationIndicator
        {
            get { return _modalityOtherSpecificationIndicator; }
            set { ApplyPropertyChange ( ref _modalityOtherSpecificationIndicator, () => ModalityOtherSpecificationIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 4: Outpatient
        /// </summary>
        /// <value>The modality outpatient indicator.</value>
        public bool? ModalityOutpatientIndicator
        {
            get { return _modalityOutpatientIndicator; }
            set { ApplyPropertyChange ( ref _modalityOutpatientIndicator, () => ModalityOutpatientIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 5: Outreach
        /// </summary>
        /// <value>The modality outreach indicator.</value>
        public bool? ModalityOutreachIndicator
        {
            get { return _modalityOutreachIndicator; }
            set { ApplyPropertyChange ( ref _modalityOutreachIndicator, () => ModalityOutreachIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 11: Recovery Support
        /// </summary>
        /// <value>The modality recovery support indicator.</value>
        public bool? ModalityRecoverySupportIndicator
        {
            get { return _modalityRecoverySupportIndicator; }
            set { ApplyPropertyChange ( ref _modalityRecoverySupportIndicator, () => ModalityRecoverySupportIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 8: Residential/Rehabilitation
        /// </summary>
        /// <value>The modality residential rehabilitation indicator.</value>
        public bool? ModalityResidentialRehabilitationIndicator
        {
            get { return _modalityResidentialRehabilitationIndicator; }
            set { ApplyPropertyChange ( ref _modalityResidentialRehabilitationIndicator, () => ModalityResidentialRehabilitationIndicator, value ); }
        }

        /// <summary>
        /// Modality Question 12 specification
        /// </summary>
        /// <value>The modality specification note.</value>
        public string ModalitySpecificationNote
        {
            get { return _modalitySpecificationNote; }
            set { ApplyPropertyChange ( ref _modalitySpecificationNote, () => ModalitySpecificationNote, value ); }
        }

        /// <summary>
        /// Peer-To-Peer Recovery Support Services Question 3: Alcohol-and Drug-Free Social Activities
        /// </summary>
        /// <value>The peer to peer recovery support alchol drug free activities indicator.</value>
        public bool? PeerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator
        {
            get { return _peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator,
                    () => PeerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator,
                    value );
            }
        }

        /// <summary>
        /// Peer-To-Peer Recovery Support Services Question 1: Peer Coaching or Mentoring
        /// </summary>
        /// <value>The peer to peer recovery support coaching indicator.</value>
        public bool? PeerToPeerRecoverySupportCoachingIndicator
        {
            get { return _peerToPeerRecoverySupportCoachingIndicator; }
            set { ApplyPropertyChange ( ref _peerToPeerRecoverySupportCoachingIndicator, () => PeerToPeerRecoverySupportCoachingIndicator, value ); }
        }

        /// <summary>
        /// Peer-To-Peer Recovery Support Services Question 2: Housing Support
        /// </summary>
        /// <value>The peer to peer recovery support housing indicator.</value>
        public bool? PeerToPeerRecoverySupportHousingIndicator
        {
            get { return _peerToPeerRecoverySupportHousingIndicator; }
            set { ApplyPropertyChange ( ref _peerToPeerRecoverySupportHousingIndicator, () => PeerToPeerRecoverySupportHousingIndicator, value ); }
        }

        /// <summary>
        /// Peer-To-Peer Recovery Support Services Question 4: Information and Referral
        /// </summary>
        /// <value>The peer to peer recovery support information referral indicator.</value>
        public bool? PeerToPeerRecoverySupportInformationReferralIndicator
        {
            get { return _peerToPeerRecoverySupportInformationReferralIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _peerToPeerRecoverySupportInformationReferralIndicator, () => PeerToPeerRecoverySupportInformationReferralIndicator, value );
            }
        }

        /// <summary>
        /// Peer-To-Peer Recovery Support Services Question 5: Other Peer-to-Peer Recovery Support Services (Specify)
        /// </summary>
        /// <value>The peer to peer recovery support other indicator.</value>
        public bool? PeerToPeerRecoverySupportOtherIndicator
        {
            get { return _peerToPeerRecoverySupportOtherIndicator; }
            set { ApplyPropertyChange ( ref _peerToPeerRecoverySupportOtherIndicator, () => PeerToPeerRecoverySupportOtherIndicator, value ); }
        }

        /// <summary>
        /// Peer-To-Peer Recovery Support Services Question 5 specification
        /// </summary>
        /// <value>The peer to peer recovery support specification note.</value>
        public string PeerToPeerRecoverySupportSpecificationNote
        {
            get { return _peerToPeerRecoverySupportSpecificationNote; }
            set { ApplyPropertyChange ( ref _peerToPeerRecoverySupportSpecificationNote, () => PeerToPeerRecoverySupportSpecificationNote, value ); }
        }

        /// <summary>
        /// Treatment Services Question 5: Assessment
        /// </summary>
        /// <value>The treatment assessment indicator.</value>
        public bool? TreatmentAssessmentIndicator
        {
            get { return _treatmentAssessmentIndicator; }
            set { ApplyPropertyChange ( ref _treatmentAssessmentIndicator, () => TreatmentAssessmentIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 2: Brief Intervention
        /// </summary>
        /// <value>The treatment brief intervention indicator.</value>
        public bool? TreatmentBriefInterventionIndicator
        {
            get { return _treatmentBriefInterventionIndicator; }
            set { ApplyPropertyChange ( ref _treatmentBriefInterventionIndicator, () => TreatmentBriefInterventionIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 3: Brief Treatment
        /// </summary>
        /// <value>The treatment brief treatment indicator.</value>
        public bool? TreatmentBriefTreatmentIndicator
        {
            get { return _treatmentBriefTreatmentIndicator; }
            set { ApplyPropertyChange ( ref _treatmentBriefTreatmentIndicator, () => TreatmentBriefTreatmentIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 10: Co-Occurring Treatment/Recovery Services
        /// </summary>
        /// <value>The treatment cooccuring treatment indicator.</value>
        public bool? TreatmentCooccuringTreatmentIndicator
        {
            get { return _treatmentCooccuringTreatmentIndicator; }
            set { ApplyPropertyChange ( ref _treatmentCooccuringTreatmentIndicator, () => TreatmentCooccuringTreatmentIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 9: Family/Marriage Counseling
        /// </summary>
        /// <value>The treatment family counseling indicator.</value>
        public bool? TreatmentFamilyCounselingIndicator
        {
            get { return _treatmentFamilyCounselingIndicator; }
            set { ApplyPropertyChange ( ref _treatmentFamilyCounselingIndicator, () => TreatmentFamilyCounselingIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 8: Group Counseling
        /// </summary>
        /// <value>The treatment group counseling indicator.</value>
        public bool? TreatmentGroupCounselingIndicator
        {
            get { return _treatmentGroupCounselingIndicator; }
            set { ApplyPropertyChange ( ref _treatmentGroupCounselingIndicator, () => TreatmentGroupCounselingIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 12: HIV/AIDS Counseling
        /// </summary>
        /// <value>The treatment hiv aids counseling indicator.</value>
        public bool? TreatmentHivAidsCounselingIndicator
        {
            get { return _treatmentHivAidsCounselingIndicator; }
            set { ApplyPropertyChange ( ref _treatmentHivAidsCounselingIndicator, () => TreatmentHivAidsCounselingIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 7: Individual Counseling
        /// </summary>
        /// <value>The treatment individual counseling indicator.</value>
        public bool? TreatmentIndividualCounselingIndicator
        {
            get { return _treatmentIndividualCounselingIndicator; }
            set { ApplyPropertyChange ( ref _treatmentIndividualCounselingIndicator, () => TreatmentIndividualCounselingIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 13:  Other (Specify)
        /// </summary>
        /// <value>The treatment other specification indicator.</value>
        public bool? TreatmentOtherSpecificationIndicator
        {
            get { return _treatmentOtherSpecificationIndicator; }
            set { ApplyPropertyChange ( ref _treatmentOtherSpecificationIndicator, () => TreatmentOtherSpecificationIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 11: Pharmacological Interventions
        /// </summary>
        /// <value>The treatment pharmacological interventions indicator.</value>
        public bool? TreatmentPharmacologicalInterventionsIndicator
        {
            get { return _treatmentPharmacologicalInterventionsIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _treatmentPharmacologicalInterventionsIndicator, () => TreatmentPharmacologicalInterventionsIndicator, value );
            }
        }

        /// <summary>
        /// Treatment Services Question 6: Treatment/Recovery Planning
        /// </summary>
        /// <value>The treatment recovery planning indicator.</value>
        public bool? TreatmentRecoveryPlanningIndicator
        {
            get { return _treatmentRecoveryPlanningIndicator; }
            set { ApplyPropertyChange ( ref _treatmentRecoveryPlanningIndicator, () => TreatmentRecoveryPlanningIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 4: Referral to Treatment
        /// </summary>
        /// <value>The treatment referral to treatment indicator.</value>
        public bool? TreatmentReferralToTreatmentIndicator
        {
            get { return _treatmentReferralToTreatmentIndicator; }
            set { ApplyPropertyChange ( ref _treatmentReferralToTreatmentIndicator, () => TreatmentReferralToTreatmentIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 1: Screening
        /// </summary>
        /// <value>The treatment screening indicator.</value>
        public bool? TreatmentScreeningIndicator
        {
            get { return _treatmentScreeningIndicator; }
            set { ApplyPropertyChange ( ref _treatmentScreeningIndicator, () => TreatmentScreeningIndicator, value ); }
        }

        /// <summary>
        /// Treatment Services Question 13: Other Clinical Services (Specify)
        /// </summary>
        /// <value>The treatment specification note.</value>
        public string TreatmentSpecificationNote
        {
            get { return _treatmentSpecificationNote; }
            set { ApplyPropertyChange ( ref _treatmentSpecificationNote, () => TreatmentSpecificationNote, value ); }
        }

        #endregion
    }
}
