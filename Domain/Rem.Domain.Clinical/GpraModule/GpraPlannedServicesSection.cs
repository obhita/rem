#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraPlannedServicesSection contains patient planned services information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraPlannedServicesSection : GpraInterviewSectionBase
    {
        private readonly bool? _afterCareContinuingCareIndicator;
        private readonly bool? _afterCareOtherIndicator;
        private readonly bool? _afterCareRecoveryCoachingIndicator;
        private readonly bool? _afterCareReplasePreventionIndicator;
        private readonly bool? _afterCareSelfHelpSupportGroupsIndicator;
        private readonly string _afterCareSpecificationNote;
        private readonly bool? _afterCareSpiritualSupportIndicator;
        private readonly bool? _caseMgmtChildCareIndicator;
        private readonly bool? _caseMgmtEmploymentCoachingIndicator;
        private readonly bool? _caseMgmtFamilyServicesIndicator;
        private readonly bool? _caseMgmtHivAidsServiceIndicator;
        private readonly bool? _caseMgmtIndividualServicesCoordinationIndicator;
        private readonly bool? _caseMgmtOtherIndicator;
        private readonly bool? _caseMgmtPreemploymentIndicator;
        private readonly string _caseMgmtSpecificationNote;
        private readonly bool? _caseMgmtTransitionalDrugFreeHousingIndicator;
        private readonly bool? _caseMgmtTransportationIndicator;
        private readonly bool? _educationHivAidsIndicator;
        private readonly bool? _educationOtherIndicator;
        private readonly bool? _educationSaIndicator;
        private readonly string _educationSpecificationNote;
        private readonly bool? _medicalAlcoholDrugTestIndicator;
        private readonly bool? _medicalCareIndicator;
        private readonly bool? _medicalHivAidsSupportAndTestingIndicator;
        private readonly bool? _medicalOtherIndicator;
        private readonly string _medicalSpecificationNote;
        private readonly bool? _modalityAfterCareIndicator;
        private readonly bool? _modalityCaseMgmtIndicator;
        private readonly bool? _modalityDayTreatmentIndicator;
        private readonly GpraDetoxificationLocation _modalityGpraDetoxificationLocation;
        private readonly bool? _modalityInpatientHospitalIndicator;
        private readonly bool? _modalityIntensiveOutpatientIndicator;
        private readonly bool? _modalityMethadoneIndicator;
        private readonly bool? _modalityOtherSpecificationIndicator;
        private readonly bool? _modalityOutpatientIndicator;
        private readonly bool? _modalityOutreachIndicator;
        private readonly bool? _modalityRecoverySupportIndicator;
        private readonly bool? _modalityResidentialRehabilitationIndicator;
        private readonly string _modalitySpecificationNote;
        private readonly bool? _peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator;
        private readonly bool? _peerToPeerRecoverySupportCoachingIndicator;
        private readonly bool? _peerToPeerRecoverySupportHousingIndicator;
        private readonly bool? _peerToPeerRecoverySupportInformationReferralIndicator;
        private readonly bool? _peerToPeerRecoverySupportOtherIndicator;
        private readonly string _peerToPeerRecoverySupportSpecificationNote;
        private readonly bool? _treatmentAssessmentIndicator;
        private readonly bool? _treatmentBriefInterventionIndicator;
        private readonly bool? _treatmentBriefTreatmentIndicator;
        private readonly bool? _treatmentCooccuringTreatmentIndicator;
        private readonly bool? _treatmentFamilyCounselingIndicator;
        private readonly bool? _treatmentGroupCounselingIndicator;
        private readonly bool? _treatmentHivAidsCounselingIndicator;
        private readonly bool? _treatmentIndividualCounselingIndicator;
        private readonly bool? _treatmentOtherSpecificationIndicator;
        private readonly bool? _treatmentPharmacologicalInterventionsIndicator;
        private readonly bool? _treatmentRecoveryPlanningIndicator;
        private readonly bool? _treatmentReferralToTreatmentIndicator;
        private readonly bool? _treatmentScreeningIndicator;
        private readonly string _treatmentSpecificationNote;

        private GpraPlannedServicesSection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraPlannedServicesSection"/> class.
        /// </summary>
        /// <param name="afterCareContinuingCareIndicator">The after care continuing care indicator.</param>
        /// <param name="afterCareOtherIndicator">The after care other indicator.</param>
        /// <param name="afterCareRecoveryCoachingIndicator">The after care recovery coaching indicator.</param>
        /// <param name="afterCareReplasePreventionIndicator">The after care replase prevention indicator.</param>
        /// <param name="afterCareSelfHelpSupportGroupsIndicator">The after care self help support groups indicator.</param>
        /// <param name="afterCareSpecificationNote">The after care specification note.</param>
        /// <param name="afterCareSpiritualSupportIndicator">The after care spiritual support indicator.</param>
        /// <param name="caseMgmtChildCareIndicator">The case MGMT child care indicator.</param>
        /// <param name="caseMgmtEmploymentCoachingIndicator">The case MGMT employment coaching indicator.</param>
        /// <param name="caseMgmtFamilyServicesIndicator">The case MGMT family services indicator.</param>
        /// <param name="caseMgmtHivAidsServiceIndicator">The case MGMT hiv aids service indicator.</param>
        /// <param name="caseMgmtIndividualServicesCoordinationIndicator">The case MGMT individual services coordination indicator.</param>
        /// <param name="caseMgmtOtherIndicator">The case MGMT other indicator.</param>
        /// <param name="caseMgmtPreemploymentIndicator">The case MGMT preemployment indicator.</param>
        /// <param name="caseMgmtSpecificationNote">The case MGMT specification note.</param>
        /// <param name="caseMgmtTransitionalDrugFreeHousingIndicator">The case MGMT transitional drug free housing indicator.</param>
        /// <param name="caseMgmtTransportationIndicator">The case MGMT transportation indicator.</param>
        /// <param name="educationHivAidsIndicator">The education hiv aids indicator.</param>
        /// <param name="educationOtherIndicator">The education other indicator.</param>
        /// <param name="educationSaIndicator">The education sa indicator.</param>
        /// <param name="educationSpecificationNote">The education specification note.</param>
        /// <param name="medicalAlcoholDrugTestIndicator">The medical alcohol drug test indicator.</param>
        /// <param name="medicalCareIndicator">The medical care indicator.</param>
        /// <param name="medicalHivAidsSupportAndTestingIndicator">The medical hiv aids support and testing indicator.</param>
        /// <param name="medicalOtherIndicator">The medical other indicator.</param>
        /// <param name="medicalSpecificationNote">The medical specification note.</param>
        /// <param name="modalityAfterCareIndicator">The modality after care indicator.</param>
        /// <param name="modalityCaseMgmtIndicator">The modality case MGMT indicator.</param>
        /// <param name="modalityDayTreatmentIndicator">The modality day treatment indicator.</param>
        /// <param name="modalityGpraDetoxificationLocation">The modality gpra detoxification location.</param>
        /// <param name="modalityInpatientHospitalIndicator">The modality inpatient hospital indicator.</param>
        /// <param name="modalityIntensiveOutpatientIndicator">The modality intensive outpatient indicator.</param>
        /// <param name="modalityMethadoneIndicator">The modality methadone indicator.</param>
        /// <param name="modalityOtherSpecificationIndicator">The modality other specification indicator.</param>
        /// <param name="modalityOutpatientIndicator">The modality outpatient indicator.</param>
        /// <param name="modalityOutreachIndicator">The modality outreach indicator.</param>
        /// <param name="modalityRecoverySupportIndicator">The modality recovery support indicator.</param>
        /// <param name="modalityResidentialRehabilitationIndicator">The modality residential rehabilitation indicator.</param>
        /// <param name="modalitySpecificationNote">The modality specification note.</param>
        /// <param name="peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator">The peer to peer recovery support alchol drug free activities indicator.</param>
        /// <param name="peerToPeerRecoverySupportCoachingIndicator">The peer to peer recovery support coaching indicator.</param>
        /// <param name="peerToPeerRecoverySupportHousingIndicator">The peer to peer recovery support housing indicator.</param>
        /// <param name="peerToPeerRecoverySupportInformationReferralIndicator">The peer to peer recovery support information referral indicator.</param>
        /// <param name="peerToPeerRecoverySupportOtherIndicator">The peer to peer recovery support other indicator.</param>
        /// <param name="peerToPeerRecoverySupportSpecificationNote">The peer to peer recovery support specification note.</param>
        /// <param name="treatmentAssessmentIndicator">The treatment assessment indicator.</param>
        /// <param name="treatmentBriefInterventionIndicator">The treatment brief intervention indicator.</param>
        /// <param name="treatmentBriefTreatmentIndicator">The treatment brief treatment indicator.</param>
        /// <param name="treatmentCooccuringTreatmentIndicator">The treatment cooccuring treatment indicator.</param>
        /// <param name="treatmentFamilyCounselingIndicator">The treatment family counseling indicator.</param>
        /// <param name="treatmentGroupCounselingIndicator">The treatment group counseling indicator.</param>
        /// <param name="treatmentHivAidsCounselingIndicator">The treatment hiv aids counseling indicator.</param>
        /// <param name="treatmentIndividualCounselingIndicator">The treatment individual counseling indicator.</param>
        /// <param name="treatmentOtherSpecificationIndicator">The treatment other specification indicator.</param>
        /// <param name="treatmentPharmacologicalInterventionsIndicator">The treatment pharmacological interventions indicator.</param>
        /// <param name="treatmentRecoveryPlanningIndicator">The treatment recovery planning indicator.</param>
        /// <param name="treatmentReferralToTreatmentIndicator">The treatment referral to treatment indicator.</param>
        /// <param name="treatmentScreeningIndicator">The treatment screening indicator.</param>
        /// <param name="treatmentSpecificationNote">The treatment specification note.</param>
        public GpraPlannedServicesSection(bool? afterCareContinuingCareIndicator,
                                                      bool? afterCareOtherIndicator,
                                                      bool? afterCareRecoveryCoachingIndicator,
                                                      bool? afterCareReplasePreventionIndicator,
                                                      bool? afterCareSelfHelpSupportGroupsIndicator,
                                                      string afterCareSpecificationNote,
                                                      bool? afterCareSpiritualSupportIndicator,
                                                      bool? caseMgmtChildCareIndicator,
                                                      bool? caseMgmtEmploymentCoachingIndicator,
                                                      bool? caseMgmtFamilyServicesIndicator,
                                                      bool? caseMgmtHivAidsServiceIndicator,
                                                      bool? caseMgmtIndividualServicesCoordinationIndicator,
                                                      bool? caseMgmtOtherIndicator,
                                                      bool? caseMgmtPreemploymentIndicator,
                                                      string caseMgmtSpecificationNote,
                                                      bool? caseMgmtTransitionalDrugFreeHousingIndicator,
                                                      bool? caseMgmtTransportationIndicator,
                                                      bool? educationHivAidsIndicator,
                                                      bool? educationOtherIndicator,
                                                      bool? educationSaIndicator,
                                                      string educationSpecificationNote,
                                                      bool? medicalAlcoholDrugTestIndicator,
                                                      bool? medicalCareIndicator,
                                                      bool? medicalHivAidsSupportAndTestingIndicator,
                                                      bool? medicalOtherIndicator,
                                                      string medicalSpecificationNote,
                                                      bool? modalityAfterCareIndicator,
                                                      bool? modalityCaseMgmtIndicator,
                                                      bool? modalityDayTreatmentIndicator,
                                                      GpraDetoxificationLocation modalityGpraDetoxificationLocation,
                                                      bool? modalityInpatientHospitalIndicator,
                                                      bool? modalityIntensiveOutpatientIndicator,
                                                      bool? modalityMethadoneIndicator,
                                                      bool? modalityOtherSpecificationIndicator,
                                                      bool? modalityOutpatientIndicator,
                                                      bool? modalityOutreachIndicator,
                                                      bool? modalityRecoverySupportIndicator,
                                                      bool? modalityResidentialRehabilitationIndicator,
                                                      string modalitySpecificationNote,
                                                      bool? peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator,
                                                      bool? peerToPeerRecoverySupportCoachingIndicator,
                                                      bool? peerToPeerRecoverySupportHousingIndicator,
                                                      bool? peerToPeerRecoverySupportInformationReferralIndicator,
                                                      bool? peerToPeerRecoverySupportOtherIndicator,
                                                      string peerToPeerRecoverySupportSpecificationNote,
                                                      bool? treatmentAssessmentIndicator,
                                                      bool? treatmentBriefInterventionIndicator,
                                                      bool? treatmentBriefTreatmentIndicator,
                                                      bool? treatmentCooccuringTreatmentIndicator,
                                                      bool? treatmentFamilyCounselingIndicator,
                                                      bool? treatmentGroupCounselingIndicator,
                                                      bool? treatmentHivAidsCounselingIndicator,
                                                      bool? treatmentIndividualCounselingIndicator,
                                                      bool? treatmentOtherSpecificationIndicator,
                                                      bool? treatmentPharmacologicalInterventionsIndicator,
                                                      bool? treatmentRecoveryPlanningIndicator,
                                                      bool? treatmentReferralToTreatmentIndicator,
                                                      bool? treatmentScreeningIndicator,
                                                      string treatmentSpecificationNote)
        {
            _afterCareContinuingCareIndicator = afterCareContinuingCareIndicator;
            _afterCareOtherIndicator = afterCareOtherIndicator;
            _afterCareRecoveryCoachingIndicator = afterCareRecoveryCoachingIndicator;
            _afterCareReplasePreventionIndicator = afterCareReplasePreventionIndicator;
            _afterCareSelfHelpSupportGroupsIndicator = afterCareSelfHelpSupportGroupsIndicator;
            _afterCareSpecificationNote = afterCareSpecificationNote;
            _afterCareSpiritualSupportIndicator = afterCareSpiritualSupportIndicator;
            _caseMgmtChildCareIndicator = caseMgmtChildCareIndicator;
            _caseMgmtEmploymentCoachingIndicator = caseMgmtEmploymentCoachingIndicator;
            _caseMgmtFamilyServicesIndicator = caseMgmtFamilyServicesIndicator;
            _caseMgmtHivAidsServiceIndicator = caseMgmtHivAidsServiceIndicator;
            _caseMgmtIndividualServicesCoordinationIndicator = caseMgmtIndividualServicesCoordinationIndicator;
            _caseMgmtOtherIndicator = caseMgmtOtherIndicator;
            _caseMgmtPreemploymentIndicator = caseMgmtPreemploymentIndicator;
            _caseMgmtSpecificationNote = caseMgmtSpecificationNote;
            _caseMgmtTransitionalDrugFreeHousingIndicator = caseMgmtTransitionalDrugFreeHousingIndicator;
            _caseMgmtTransportationIndicator = caseMgmtTransportationIndicator;
            _educationHivAidsIndicator = educationHivAidsIndicator;
            _educationOtherIndicator = educationOtherIndicator;
            _educationSaIndicator = educationSaIndicator;
            _educationSpecificationNote = educationSpecificationNote;
            _medicalAlcoholDrugTestIndicator = medicalAlcoholDrugTestIndicator;
            _medicalCareIndicator = medicalCareIndicator;
            _medicalHivAidsSupportAndTestingIndicator = medicalHivAidsSupportAndTestingIndicator;
            _medicalOtherIndicator = medicalOtherIndicator;
            _medicalSpecificationNote = medicalSpecificationNote;
            _modalityAfterCareIndicator = modalityAfterCareIndicator;
            _modalityCaseMgmtIndicator = modalityCaseMgmtIndicator;
            _modalityDayTreatmentIndicator = modalityDayTreatmentIndicator;
            _modalityGpraDetoxificationLocation = modalityGpraDetoxificationLocation;
            _modalityInpatientHospitalIndicator = modalityInpatientHospitalIndicator;
            _modalityIntensiveOutpatientIndicator = modalityIntensiveOutpatientIndicator;
            _modalityMethadoneIndicator = modalityMethadoneIndicator;
            _modalityOtherSpecificationIndicator = modalityOtherSpecificationIndicator;
            _modalityOutpatientIndicator = modalityOutpatientIndicator;
            _modalityOutreachIndicator = modalityOutreachIndicator;
            _modalityRecoverySupportIndicator = modalityRecoverySupportIndicator;
            _modalityResidentialRehabilitationIndicator = modalityResidentialRehabilitationIndicator;
            _modalitySpecificationNote = modalitySpecificationNote;
            _peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator = peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator;
            _peerToPeerRecoverySupportCoachingIndicator = peerToPeerRecoverySupportCoachingIndicator;
            _peerToPeerRecoverySupportHousingIndicator = peerToPeerRecoverySupportHousingIndicator;
            _peerToPeerRecoverySupportInformationReferralIndicator = peerToPeerRecoverySupportInformationReferralIndicator;
            _peerToPeerRecoverySupportOtherIndicator = peerToPeerRecoverySupportOtherIndicator;
            _peerToPeerRecoverySupportSpecificationNote = peerToPeerRecoverySupportSpecificationNote;
            _treatmentAssessmentIndicator = treatmentAssessmentIndicator;
            _treatmentBriefInterventionIndicator = treatmentBriefInterventionIndicator;
            _treatmentBriefTreatmentIndicator = treatmentBriefTreatmentIndicator;
            _treatmentCooccuringTreatmentIndicator = treatmentCooccuringTreatmentIndicator;
            _treatmentFamilyCounselingIndicator = treatmentFamilyCounselingIndicator;
            _treatmentGroupCounselingIndicator = treatmentGroupCounselingIndicator;
            _treatmentHivAidsCounselingIndicator = treatmentHivAidsCounselingIndicator;
            _treatmentIndividualCounselingIndicator = treatmentIndividualCounselingIndicator;
            _treatmentOtherSpecificationIndicator = treatmentOtherSpecificationIndicator;
            _treatmentPharmacologicalInterventionsIndicator = treatmentPharmacologicalInterventionsIndicator;
            _treatmentRecoveryPlanningIndicator = treatmentRecoveryPlanningIndicator;
            _treatmentReferralToTreatmentIndicator = treatmentReferralToTreatmentIndicator;
            _treatmentScreeningIndicator = treatmentScreeningIndicator;
            _treatmentSpecificationNote = treatmentSpecificationNote;
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of case management.
        /// Modality Question 1: Case Management
        /// </summary>
        public virtual bool? ModalityCaseMgmtIndicator
        {
            get { return _modalityCaseMgmtIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of day treatment.
        /// Modality Question 2: Day Treatment 
        /// </summary>
        public virtual bool? ModalityDayTreatmentIndicator
        {
            get { return _modalityDayTreatmentIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of inpatient hospital.
        /// Modality Question 3: Inpatient/Hospital (Other Than Detox)
        /// </summary>
        public virtual bool? ModalityInpatientHospitalIndicator
        {
            get { return _modalityInpatientHospitalIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of outpatient.
        /// Modality Question 4: Outpatient 
        /// </summary>
        public virtual bool? ModalityOutpatientIndicator
        {
            get { return _modalityOutpatientIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of outreach.
        /// Modality Question 5: Outreach
        /// </summary>
        public virtual bool? ModalityOutreachIndicator
        {
            get { return _modalityOutreachIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of intensive outpatient.
        /// Modality Question 6: Intensive Outpatient
        /// </summary>
        public virtual bool? ModalityIntensiveOutpatientIndicator
        {
            get { return _modalityIntensiveOutpatientIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of methadone.
        /// Modality Question 7: Methadone
        /// </summary>
        public virtual bool? ModalityMethadoneIndicator
        {
            get { return _modalityMethadoneIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of residential rehabilitation.
        /// Modality Question 8: Residential/Rehabilitation
        /// </summary>
        public virtual bool? ModalityResidentialRehabilitationIndicator
        {
            get { return _modalityResidentialRehabilitationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the modality Gpra detoxification location.
        /// Modality Question 9: Detoxification (Select Only One). A. Hospital Inpatient, B. Free Standing Residential, C. Ambulatory Detoxification 
        /// </summary>
        public virtual GpraDetoxificationLocation ModalityGpraDetoxificationLocation
        {
            get { return _modalityGpraDetoxificationLocation; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of after care.
        /// Modality Question 10: After Care
        /// </summary>
        public virtual bool? ModalityAfterCareIndicator
        {
            get { return _modalityAfterCareIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of recovery support.
        /// Modality Question 11: Recovery Support
        /// </summary>
        public virtual bool? ModalityRecoverySupportIndicator
        {
            get { return _modalityRecoverySupportIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the modality of other specification.
        /// Modality Question 12: Other (Specify)
        /// </summary>
        public virtual bool? ModalityOtherSpecificationIndicator
        {
            get { return _modalityOtherSpecificationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the modality specification note.
        /// Modality Question 12 specification
        /// </summary>
        public virtual string ModalitySpecificationNote
        {
            get { return _modalitySpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment screening.
        /// Treatment Services Question 1: Screening
        /// </summary>
        public virtual bool? TreatmentScreeningIndicator
        {
            get { return _treatmentScreeningIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment brief intervention.
        /// Treatment Services Question 2: Brief Intervention
        /// </summary>
        public virtual bool? TreatmentBriefInterventionIndicator
        {
            get { return _treatmentBriefInterventionIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment brief treatment.
        /// Treatment Services Question 3: Brief Treatment
        /// </summary>
        public virtual bool? TreatmentBriefTreatmentIndicator
        {
            get { return _treatmentBriefTreatmentIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment referral to treatment.
        /// Treatment Services Question 4: Referral to Treatment
        /// </summary>
        public virtual bool? TreatmentReferralToTreatmentIndicator
        {
            get { return _treatmentReferralToTreatmentIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment assessment.
        /// Treatment Services Question 5: Assessment
        /// </summary>
        public virtual bool? TreatmentAssessmentIndicator
        {
            get { return _treatmentAssessmentIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment recovery planning.
        /// Treatment Services Question 6: Treatment/Recovery Planning
        /// </summary>
        public virtual bool? TreatmentRecoveryPlanningIndicator
        {
            get { return _treatmentRecoveryPlanningIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment individual counseling.
        /// Treatment Services Question 7: Individual Counseling
        /// </summary>
        public virtual bool? TreatmentIndividualCounselingIndicator
        {
            get { return _treatmentIndividualCounselingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment group counseling.
        /// Treatment Services Question 8: Group Counseling
        /// </summary>
        public virtual bool? TreatmentGroupCounselingIndicator
        {
            get { return _treatmentGroupCounselingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment family counseling.
        /// Treatment Services Question 9: Family/Marriage Counseling
        /// </summary>
        public virtual bool? TreatmentFamilyCounselingIndicator
        {
            get { return _treatmentFamilyCounselingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment cooccuring treatment.
        /// Treatment Services Question 10: Co-Occurring Treatment/Recovery Services
        /// </summary>
        public virtual bool? TreatmentCooccuringTreatmentIndicator
        {
            get { return _treatmentCooccuringTreatmentIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment pharmacological interventions.
        /// Treatment Services Question 11: Pharmacological Interventions
        /// </summary>
        public virtual bool? TreatmentPharmacologicalInterventionsIndicator
        {
            get { return _treatmentPharmacologicalInterventionsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment hiv aids counseling.
        /// Treatment Services Question 12: HIV/AIDS Counseling
        /// </summary>
        public virtual bool? TreatmentHivAidsCounselingIndicator
        {
            get { return _treatmentHivAidsCounselingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating treatment other specification.
        /// </summary>
        public virtual bool? TreatmentOtherSpecificationIndicator
        {
            get { return _treatmentOtherSpecificationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the treatment specification note.
        /// Treatment Services Question 13: Other Clinical Services (Specify)
        /// </summary>
        public virtual string TreatmentSpecificationNote
        {
            get { return _treatmentSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management family services.
        /// Case Management Services Question 1: Family Services (Including Marriage Education, Parenting, Child Development Services)
        /// </summary>
        public virtual bool? CaseMgmtFamilyServicesIndicator
        {
            get { return _caseMgmtFamilyServicesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management child care.
        /// Case Management Services Question 2: Child Care
        /// </summary>
        public virtual bool? CaseMgmtChildCareIndicator
        {
            get { return _caseMgmtChildCareIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management pre-employment.
        /// Case Management Services Question 3: Employment Service.  A. Pre-employement
        /// </summary>
        public virtual bool? CaseMgmtPreemploymentIndicator
        {
            get { return _caseMgmtPreemploymentIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management employment coaching.
        /// Case Management Services Question 3: Employment Service.  B. Employment Coaching
        /// </summary>
        public virtual bool? CaseMgmtEmploymentCoachingIndicator
        {
            get { return _caseMgmtEmploymentCoachingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management individual services coordination.
        /// Case Management Services Question 4: Individual Services Coordination
        /// </summary>
        public virtual bool? CaseMgmtIndividualServicesCoordinationIndicator
        {
            get { return _caseMgmtIndividualServicesCoordinationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management transportation.
        /// Case Management Services Question 5: Transportation
        /// </summary>
        public virtual bool? CaseMgmtTransportationIndicator
        {
            get { return _caseMgmtTransportationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management HIV/AIDS service.
        /// Case Management Services Question 6: HIV/AIDS Service
        /// </summary>
        public virtual bool? CaseMgmtHivAidsServiceIndicator
        {
            get { return _caseMgmtHivAidsServiceIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management transitional drug free housing.
        /// Case Management Services Question 7: Supportive Transitional Drug-Free Housing Services
        /// </summary>
        public virtual bool? CaseMgmtTransitionalDrugFreeHousingIndicator
        {
            get { return _caseMgmtTransitionalDrugFreeHousingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating case management other.
        /// Case Management Services Question 8: Other Case Management Services
        /// </summary>
        public virtual bool? CaseMgmtOtherIndicator
        {
            get { return _caseMgmtOtherIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the case management specification note.
        /// Case Management Services Question 8 specification
        /// </summary>
        public virtual string CaseMgmtSpecificationNote
        {
            get { return _caseMgmtSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating medical care.
        /// Medical Services Question 1: Medical Care
        /// </summary>
        public virtual bool? MedicalCareIndicator
        {
            get { return _medicalCareIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating medical alcohol drug test.
        /// Medical Services Question 2: Alcohol/Drug Testing
        /// </summary>
        public virtual bool? MedicalAlcoholDrugTestIndicator
        {
            get { return _medicalAlcoholDrugTestIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating medical HIV/AIDS support and testing.
        /// Medical Services Question 3: HIV/ AIDS Medical Support and Testing
        /// </summary>
        public virtual bool? MedicalHivAidsSupportAndTestingIndicator
        {
            get { return _medicalHivAidsSupportAndTestingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating medical other.
        /// Medical Services Question 4: Other Medical Services (Specify)
        /// </summary>
        public virtual bool? MedicalOtherIndicator
        {
            get { return _medicalOtherIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the medical specification note.
        /// Medical Services Question 4 specification
        /// </summary>
        public virtual string MedicalSpecificationNote
        {
            get { return _medicalSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating after care continuing care.
        /// After Care Services Question 1: Continuing Care
        /// </summary>
        public virtual bool? AfterCareContinuingCareIndicator
        {
            get { return _afterCareContinuingCareIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating after care replase prevention.
        /// After Care Services Question 2: Relapse Prevention
        /// </summary>
        public virtual bool? AfterCareReplasePreventionIndicator
        {
            get { return _afterCareReplasePreventionIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating after care recovery coaching.
        /// After Care Services Question 3: Recovery Coaching
        /// </summary>
        public virtual bool? AfterCareRecoveryCoachingIndicator
        {
            get { return _afterCareRecoveryCoachingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating after care self help support groups.
        /// After Care Services Question 4: Self-Help and Support Groups
        /// </summary>
        public virtual bool? AfterCareSelfHelpSupportGroupsIndicator
        {
            get { return _afterCareSelfHelpSupportGroupsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating after care spiritual support.
        /// After Care Services Question 5: Spiritual Support
        /// </summary>
        public virtual bool? AfterCareSpiritualSupportIndicator
        {
            get { return _afterCareSpiritualSupportIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating after care other.
        /// After Care Services Question 6: Other After Care Services (Specify)
        /// </summary>
        public virtual bool? AfterCareOtherIndicator
        {
            get { return _afterCareOtherIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the after care specification note.
        /// After Care Services Question 6 specification
        /// </summary>
        public virtual string AfterCareSpecificationNote
        {
            get { return _afterCareSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating education SA.
        /// Education Services Question 1: Substance Abuse Education
        /// </summary>
        public virtual bool? EducationSaIndicator
        {
            get { return _educationSaIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating education HIV/AIDS aids.
        /// Education Services Question 2: HIV/AIDS Education
        /// </summary>
        public virtual bool? EducationHivAidsIndicator
        {
            get { return _educationHivAidsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating education other.
        /// Education Services Question 3: Other Education Services (Specify)
        /// </summary>
        public virtual bool? EducationOtherIndicator
        {
            get { return _educationOtherIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the education specification note.
        /// Education Services Question 3 specification
        /// </summary>
        public virtual string EducationSpecificationNote
        {
            get { return _educationSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating peer to peer recovery support coaching.
        /// Peer-To-Peer Recovery Support Services Question 1: Peer Coaching or Mentoring
        /// </summary>
        public virtual bool? PeerToPeerRecoverySupportCoachingIndicator
        {
            get { return _peerToPeerRecoverySupportCoachingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating peer to peer recovery support housing.
        /// Peer-To-Peer Recovery Support Services Question 2: Housing Support
        /// </summary>
        public virtual bool? PeerToPeerRecoverySupportHousingIndicator
        {
            get { return _peerToPeerRecoverySupportHousingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating peer to peer recovery support alchol drug free activities.
        /// Peer-To-Peer Recovery Support Services Question 3: Alcohol-and Drug-Free Social Activities
        /// </summary>
        public virtual bool? PeerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator
        {
            get { return _peerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating peer to peer recovery support information referral.
        /// Peer-To-Peer Recovery Support Services Question 4: Information and Referral
        /// </summary>
        public virtual bool? PeerToPeerRecoverySupportInformationReferralIndicator
        {
            get { return _peerToPeerRecoverySupportInformationReferralIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating peer to peer recovery support other.
        /// Peer-To-Peer Recovery Support Services Question 5: Other Peer-to-Peer Recovery Support Services (Specify)
        /// </summary>
        public virtual bool? PeerToPeerRecoverySupportOtherIndicator
        {
            get { return _peerToPeerRecoverySupportOtherIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the peer to peer recovery support specification note.
        /// Peer-To-Peer Recovery Support Services Question 5 specification
        /// </summary>
        public virtual string PeerToPeerRecoverySupportSpecificationNote
        {
            get { return _peerToPeerRecoverySupportSpecificationNote; }
            private set { }
        }
    }
}