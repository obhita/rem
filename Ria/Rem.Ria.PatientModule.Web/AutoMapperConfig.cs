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
using AutoMapper;
using Pillar.Common.Bootstrapper;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.TedsModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.DensAsiModule;
using Rem.Domain.Clinical.GpraModule;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Domain.Clinical.RadiologyModule;
using Rem.Domain.Clinical.SbirtModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Infrastructure.Service.DataTransferObject.Mapping;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.PatientModule.Web.CdsRuleService;
using Rem.Ria.PatientModule.Web.ClinicalCaseEditor;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.DensAsiInterview;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;
using Rem.Ria.PatientModule.Web.GainShortScreener;
using Rem.Ria.PatientModule.Web.GpraInterview;
using Rem.Ria.PatientModule.Web.ImportC32;
using Rem.Ria.PatientModule.Web.Mapping;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile;
using Rem.Ria.PatientModule.Web.PatientEditor;
using Rem.Ria.PatientModule.Web.PatientReminder;
using Rem.Ria.PatientModule.Web.TedsInterview;
using ContactPreference = Rem.WellKnownNames.PatientModule.ContactPreference;
using PatientPhoneType = Rem.WellKnownNames.PatientModule.PatientPhoneType;

namespace Rem.Ria.PatientModule.Web
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
            CreateFrontDeskConfig ();

            CreateAllergyConfig ();

            CreatePatientConfig ();

            // Patient Editor mappings 
            CreatePatientDtoConfig ();
            CreatePatientAliasesConfig ();
            CreatePatientPhoneNumbersConfig ();
            CreatePatientAddressesConfig ();
            CreateIdentifiersConfig ();
            CreatePatientDemographicDetailsConfig ();
            CreatePatientOtherConsiderationsConfig ();
            CreatePatientAllergiesConfig ();
            CreatePatientConfidentialInformationConfig ();
            CreatePatientRaceAndEthnicityConfig ();

            CreateMedicationConfig ();

            CreateProblemConfig ();

            CreateActivitiesConfig ();

            CreateSpecialInitiativeConfig ();

            CreatePriorityPopulationConfig ();

            CreateSignedCommentConfig ();

            CreateClinicalCaseConfig ();

            CreateVisitConfig ();

            CreatePatientDocumentConfig ();

            CreatePatientAccessEventConfig ();

            AutoMapperSetup.CreateMapToAbstractDto<CodedConceptLookupBase, CodedConceptLookupValueDto> ();

            CreatePatientReminderConfig ();

            CreateAppointmentDetailsConfig ();

            CreatePatientAlertsConfig ();

            CreateCdsRuleConfig ();

            CreateGpraInterviewSectionConfig ();

            CreateDensAsiInterviewSectionConfig ();

            CreateGainShortScreenerConfig ();

            CreateProblemEnrollmentConfig ();

            CreateSelfPaymentConfig ();

            CreatePayorConfig ();

            CreateTedsAdmissionInterviewConfig ();

            CreateTedsDischargeInterviewConfig ();

            CreateProvenanceConfig ();
        }

        #endregion

        #region Methods

        private static void CreateActivitiesConfig ()
        {
            MapActivityTypeToActivityTypeDto ();
            MapActivityToActivityDto ();
            CreateVitalSignConfig ();
            CreateLabSpecimenConfig ();

            ActivityAutoMapperSetup.CreateMapToActivityDto<RadiologyOrder, RadiologyOrderDto> ();

            ActivityAutoMapperSetup.CreateMapToActivityDto<Immunization, ImmunizationDto>()
                .ForMember ( dest => dest.VaccineCodedConcept, opt => opt.MapFrom ( i => i.ImmunizationVaccineInfo.VaccineCodedConcept ) )
                .ForMember ( dest => dest.VaccineLotNumber, opt => opt.MapFrom ( i => i.ImmunizationVaccineInfo.VaccineLotNumber ) )
                .ForMember (
                    dest => dest.VaccineManufacturerCode,
                    opt => opt.MapFrom ( i => i.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerCode ) )
                .ForMember (
                    dest => dest.VaccineManufacturerName,
                    opt => opt.MapFrom ( i => i.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerName ) )
                .ForMember ( dest => dest.AdministeredAmount, opt => opt.MapFrom ( i => i.ImmunizationAdministration.AdministeredAmount ) )
                .ForMember (
                    dest => dest.ImmunizationUnitOfMeasure, opt => opt.MapFrom ( i => i.ImmunizationAdministration.ImmunizationUnitOfMeasure ) );

            ActivityAutoMapperSetup.CreateMapToActivityDto<BriefIntervention, BriefInterventionDto>();

            ActivityAutoMapperSetup.CreateMapToActivityDto<Phq9, Phq9Dto>();

            ActivityAutoMapperSetup.CreateMapToActivityDto<AuditC, AuditCDto> ()
                .ForMember (
                    dest => dest.PatientGender,
                    opt => opt.MapFrom ( auditC => auditC.Visit.ClinicalCase.Patient.Profile.PatientGender.AdministrativeGender ) );

            ActivityAutoMapperSetup.CreateMapToActivityDto<IndividualCounseling, IndividualCounselingDto> ();

            ActivityAutoMapperSetup.CreateMapToActivityDto<Dast10, Dast10Dto>()
                .ForMember ( dest => dest.IsNidaDrugQuestionnaireCreated, opt => opt.Ignore () );

            ActivityAutoMapperSetup.CreateMapToActivityDto<NidaDrugQuestionnaire, NidaDrugQuestionnaireDto>()
                .ForMember (
                    dest => dest.CannabisUseAnswerNumber,
                    opt => opt.MapFrom ( src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.CannabisUseAnswerNumber ) )
                .ForMember (
                    dest => dest.CocaineUseAnswerNumber,
                    opt => opt.MapFrom ( src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.CannabisUseAnswerNumber ) )
                .ForMember (
                    dest => dest.OpioidsUseAnswerNumber,
                    opt => opt.MapFrom ( src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.CannabisUseAnswerNumber ) )
                .ForMember (
                    dest => dest.MethamphetamineUseAnswerNumber,
                    opt => opt.MapFrom ( src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.CannabisUseAnswerNumber ) )
                .ForMember (
                    dest => dest.SedativesUseAnswerNumber,
                    opt => opt.MapFrom ( src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.CannabisUseAnswerNumber ) )
                .ForMember (
                    dest => dest.OtherDrug1TypeName,
                    opt =>
                    opt.MapFrom (
                        src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.OtherDrug1NidaDrugQuestionnaireOtherDrugInfo.DrugTypeName ) )
                .ForMember (
                    dest => dest.OtherDrug1AnswerNumber,
                    opt =>
                    opt.MapFrom (
                        src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.OtherDrug1NidaDrugQuestionnaireOtherDrugInfo.AnswerNumber ) )
                .ForMember (
                    dest => dest.OtherDrug2TypeName,
                    opt =>
                    opt.MapFrom (
                        src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.OtherDrug2NidaDrugQuestionnaireOtherDrugInfo.DrugTypeName ) )
                .ForMember (
                    dest => dest.OtherDrug2AnswerNumber,
                    opt =>
                    opt.MapFrom (
                        src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.OtherDrug2NidaDrugQuestionnaireOtherDrugInfo.AnswerNumber ) )
                .ForMember (
                    dest => dest.OtherDrug3TypeName,
                    opt =>
                    opt.MapFrom (
                        src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.OtherDrug3NidaDrugQuestionnaireOtherDrugInfo.DrugTypeName ) )
                .ForMember (
                    dest => dest.OtherDrug3AnswerNumber,
                    opt =>
                    opt.MapFrom (
                        src => src.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.OtherDrug3NidaDrugQuestionnaireOtherDrugInfo.AnswerNumber ) )
                .ForMember (
                    dest => dest.DrugUseByInjectionIndicator,
                    opt => opt.MapFrom ( src => src.NidaDrugQuestionnaireInjectionDrugUse.DrugUseByInjectionIndicator ) )
                .ForMember (
                    dest => dest.LastDrugUseByInjectionAnswerNumber,
                    opt => opt.MapFrom ( src => src.NidaDrugQuestionnaireInjectionDrugUse.LastDrugUseByInjectionAnswerNumber ) )
                .ForMember ( dest => dest.IsDast10ResultChanged, opt => opt.Ignore () );

            ActivityAutoMapperSetup.CreateMapToActivityDto<SocialHistory, SocialHistoryDto>()
                .ForMember ( dest => dest.SmokingStatus, opt => opt.MapFrom ( src => src.SocialHistorySmoking.SmokingStatus ) )
                .ForMember (
                    dest => dest.SmokingStatusAreYouWillingToQuitDate,
                    opt => opt.MapFrom ( src => src.SocialHistorySmoking.SmokingStatusAreYouWillingToQuitDate ) )
                .ForMember (
                    dest => dest.SmokingStatusAreYouWillingToQuitIndicator,
                    opt => opt.MapFrom ( src => src.SocialHistorySmoking.SmokingStatusAreYouWillingToQuitIndicator ) )
                .ForMember (
                    dest => dest.Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber,
                    opt =>
                    opt.MapFrom (
                        src => src.SocialHistoryDast10.Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber ) )
                .ForMember (
                    dest => dest.AuditCDrinkBeerWineOrOtherAlcoholicBeveragesIndicator,
                    opt => opt.MapFrom ( src => src.SocialHistoryAuditC.AuditCDrinkBeerWineOrOtherAlcoholicBeveragesIndicator ) )
                .ForMember (
                    dest => dest.Phq2LittleInterestInDoingThingsAnswerNumber,
                    opt => opt.MapFrom ( src => src.SocialHistoryPhq2.Phq2LittleInterestInDoingThingsAnswerNumber ) )
                .ForMember (
                    dest => dest.Phq2FeelingDownAnswerNumber, opt => opt.MapFrom ( src => src.SocialHistoryPhq2.Phq2FeelingDownAnswerNumber ) )
                .ForMember ( dest => dest.Phq2Score, opt => opt.MapFrom ( src => src.SocialHistoryPhq2.Phq2Score ) )
                .ForMember (
                    dest => dest.IsPhq2ScoreAbovePhq9ThresholdIndicator,
                    opt => opt.MapFrom ( src => src.SocialHistoryPhq2.IsPhq2ScoreAbovePhq9ThresholdIndicator ) )
                .ForMember ( dest => dest.IsAuditCCreated, opt => opt.Ignore () )
                .ForMember ( dest => dest.IsDast10Created, opt => opt.Ignore () )
                .ForMember ( dest => dest.IsPhq9Created, opt => opt.Ignore () );

            ActivityAutoMapperSetup.CreateMapToActivityDto<Audit, AuditDto> ();
        }

        private static void CreateAllergyConfig ()
        {
            // Creating these maps must be in order
            AutoMapperSetup.CreateMapToAbstractDto<AllergyType, LookupValueDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<AllergyStatus, LookupValueDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<AllergySeverityType, LookupValueDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<Reaction, LookupValueDto> ();
            AutoMapperSetup.CreateMapToEditableDto<Allergy, AllergyDto> ()
                .ForMember ( dest => dest.PatientKey, opt => opt.MapFrom ( src => src.Patient.Key ) )
                .ForMember (
                    dest => dest.OnsetStartDate,
                    opt => opt.MapFrom ( src => src.OnsetDateRange == null ? null : src.OnsetDateRange.StartDate ) )
                .ForMember (
                    dest => dest.OnsetEndDate,
                    opt => opt.MapFrom ( src => src.OnsetDateRange == null ? null : src.OnsetDateRange.EndDate ) )
                .ForMember (
                    dest => dest.AllergyReactions,
                    opt => opt.MapFrom ( src => src.AllergyReactions.Select ( ar => ar.Reaction ) ) )
                .ForMember ( dest => dest.ProvenanceKey, opt => opt.MapFrom ( src => src.Provenance.Key ) );
        }

        private static void CreateAppointmentDetailsConfig ()
        {
            //todo: refactor the mapping for patient address!
            AutoMapperSetup.CreateMapToEditableDto<Visit, AppointmentDetailsDto> ()
                .ForMember ( dest => dest.AppointmentKey, opt => opt.MapFrom ( src => src.Key ) )
                .ForMember ( dest => dest.ClinicianKey, opt => opt.MapFrom ( src => src.Staff.Key ) )
                .ForMember ( dest => dest.Location, opt => opt.MapFrom ( src => src.ServiceLocation ) )
                .ForMember ( dest => dest.PatientKey, opt => opt.MapFrom ( src => src.ClinicalCase.Patient.Key ) )
                .ForMember ( dest => dest.PatientFirstName, opt => opt.MapFrom ( src => src.ClinicalCase.Patient.Name.First ) )
                .ForMember ( dest => dest.PatientLastName, opt => opt.MapFrom ( src => src.ClinicalCase.Patient.Name.Last ) )
                .ForMember ( dest => dest.PatientPrefix, opt => opt.MapFrom ( src => src.ClinicalCase.Patient.Name.Prefix ) )
                .ForMember ( dest => dest.PatientDateOfBirth, opt => opt.MapFrom ( src => src.ClinicalCase.Patient.Profile.BirthDate ) )
                .ForMember ( dest => dest.PatientGender, opt => opt.MapFrom ( src => src.ClinicalCase.Patient.Profile.PatientGender ) )
                .ForMember ( dest => dest.PatientUniqueIdentifier, opt => opt.MapFrom ( src => src.ClinicalCase.Patient.UniqueIdentifier ) )
                .ForMember (
                    dest => dest.PatientPhoneNumber,
                    opt => opt.MapFrom (
                        src => src.ClinicalCase.Patient.PhoneNumbers.FirstOrDefault ( p => p.PatientPhoneType.WellKnownName == PatientPhoneType.Home ) ) )
                .ForMember (
                    dest => dest.PatientCity,
                    opt => opt.MapFrom (
                        src =>
                        src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home )
                        == null
                            ? string.Empty
                            : src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home ).Address
                                  .CityName ) )
                .ForMember (
                    dest => dest.PatientAddressLine1,
                    opt => opt.MapFrom (
                        src =>
                        src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home )
                        == null
                            ? string.Empty
                            : src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home ).Address
                                  .FirstStreetAddress ) )
                .ForMember (
                    dest => dest.PatientState,
                    opt => opt.MapFrom (
                        src =>
                        src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home )
                        == null
                            ? string.Empty
                            : src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home ).Address
                                  .StateProvince.ShortName ) )
                .ForMember (
                    dest => dest.PatientPostalCode,
                    opt => opt.MapFrom (
                        src =>
                        src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home )
                        == null
                            ? string.Empty
                            : src.ClinicalCase.Patient.Addresses.FirstOrDefault ( a => a.PatientAddressType.WellKnownName == PatientAddressType.Home ).Address
                                  .PostalCode.Code ) )
                .ForMember ( dest => dest.AppointmentStartDateTime, opt => opt.MapFrom ( src => src.AppointmentDateTimeRange.StartDateTime ) )
                .ForMember ( dest => dest.AppointmentEndDateTime, opt => opt.MapFrom ( src => src.AppointmentDateTimeRange.EndDateTime ) )
                .ForMember ( dest => dest.VisitStatus, opt => opt.MapFrom ( src => src.VisitStatus ) )
                .ForMember ( dest => dest.ActivityNames, opt => opt.MapFrom ( src => src.Activities.Select ( a => a.ActivityType.Name ).ToList () ) )
                .ForMember ( dest => dest.VisitTemplateKey, opt => opt.Ignore () );
        }

        private static void CreateCdsRuleConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<CdsRule, CdsRuleDto> ()
                .ForMember (
                    dest => dest.ProblemCodedConcept,
                    opt =>
                    opt.MapFrom (
                        src => new ProblemDto
                            {
                                ProblemCodeCodedConcept = Mapper.Map<CodedConcept, CodedConceptDto> ( src.ProblemCodedConcept )
                            } ) );
        }

        private static void CreateClinicalCaseConfig ()
        {
            //AutoMapperSetup.CreateMapToAbstractDto<ClinicalCase, CaseSummaryDto> ()
            //    .ForMember ( dest => dest.AdmittedByStaffFirstName,
            //                 opt => opt.MapFrom ( src => src.AdmittedByStaff == null ? string.Empty : src.AdmittedByStaff.StaffProfile.StaffName.FirstName ) )
            //    .ForMember ( dest => dest.AdmittedByStaffLastName,
            //                 opt => opt.MapFrom(src => src.AdmittedByStaff == null ? string.Empty : src.AdmittedByStaff.StaffProfile.StaffName.LastName));

            AutoMapperSetup.CreateMapToAbstractDto<ClinicalCase, CaseSummaryDto> ()
                .ForMember ( dest => dest.ClinicalCaseStartDate, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.ClinicalCaseStartDate ) )
                .ForMember (
                    dest => dest.PatientPresentingProblemNote, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.PatientPresentingProblemNote ) )
                .ForMember ( dest => dest.InitialLocation, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.InitialLocation ) )
                .ForMember ( dest => dest.ClinicalCaseCloseDate, opt => opt.Ignore () )
                .ForMember ( dest => dest.AdmittedByStaffFirstName, opt => opt.Ignore () )
                .ForMember ( dest => dest.AdmittedByStaffLastName, opt => opt.Ignore () );

            AutoMapperSetup.CreateMapToAbstractDto<ClinicalCase, ClinicalCaseSummaryDto> ()
                .ForMember ( dest => dest.ClinicalCaseStartDate, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.ClinicalCaseStartDate ) )
                .ForMember ( dest => dest.ClinicalCaseCloseDate, opt => opt.Ignore () );

            AutoMapperSetup.CreateMapToAbstractDto<ClinicalCase, ClinicalCaseDto> ()
                .ForMember ( dest => dest.ClinicalCaseProfile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.ClinicalCaseStatus, opt => opt.MapFrom ( src => src ) );

            AutoMapperSetup.CreateMapToEditableDto<ClinicalCaseAdmission, ClinicalCaseAdmissionDto> ()
                .ForMember ( dest => dest.Key, opt => opt.Ignore () );
            AutoMapperSetup.CreateMapToEditableDto<ClinicalCaseDischarge, ClinicalCaseDischargeDto> ()
                .ForMember ( dest => dest.Key, opt => opt.Ignore () );

            AutoMapperSetup.CreateMapToEditableDto<ClinicalCase, ClinicalCaseProfileDto> ()
                .ForMember ( dest => dest.InitialLocation, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.InitialLocation ) )
                .ForMember ( dest => dest.ClinicalCaseStartDate, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.ClinicalCaseStartDate ) )
                .ForMember ( dest => dest.PerformedByStaff, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.PerformedByStaff ) )
                .ForMember (
                    dest => dest.PatientPresentingProblemNote, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.PatientPresentingProblemNote ) )
                .ForMember ( dest => dest.ReferralType, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.ReferralType ) )
                .ForMember ( dest => dest.InitialContactMethod, opt => opt.MapFrom ( src => src.ClinicalCaseProfile.InitialContactMethod ) )
                .ForMember ( dest => dest.PatientFullName, opt => opt.Ignore () );

            AutoMapperSetup.CreateMapToEditableDto<ClinicalCase, ClinicalCaseStatusDto> ()
                .ForMember (
                    dest => dest.ClinicalCaseCloseDate,
                    opt => opt.MapFrom ( src => src.ClinicalCaseCloseInfo == null ? null : src.ClinicalCaseCloseInfo.ClinicalCaseCloseDate ) )
                .ForMember (
                    dest => dest.ClinicalCaseClosingNote,
                    opt => opt.MapFrom ( src => src.ClinicalCaseCloseInfo == null ? null : src.ClinicalCaseCloseInfo.ClinicalCaseClosingNote ) );

            AutoMapperSetup.CreateMapToAbstractDto<ClinicalCaseStatus, LookupValueDto> ();
        }

        private static void CreateDensAsiInterviewSectionConfig ()
        {
            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiPatientProfileSection, DensAsiPatientProfileDto> ()
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.PostalCode == null ? null : src.PostalCode.Code ) );

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiMedicalStatusSection, DensAsiMedicalStatusDto> ();

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiEmploymentStatusSection, DensAsiEmploymentStatusDto> ();

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiDrugAlcoholUseSection, DensAsiDrugAlcoholUseDto> ();

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiLegalStatusSection, DensAsiLegalStatusDto> ();

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiFamilySocialRelationshipsSection, DensAsiFamilySocialRelationshipsDto> ();

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiPsychiatricStatusSection, DensAsiPsychiatricStatusDto> ();

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiDsmIvSection, DensAsiDsmIvDto> ();

            DensAsiSectionAutoMapperSetup.CreateMapToDensAsiDto<DensAsiClosureSection, DensAsiClosureDto> ();
        }

        private static void CreateFrontDeskConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<VisitTemplate, VisitTemplateDto> ();
        }

        private static void CreateGainShortScreenerConfig ()
        {
            ActivityAutoMapperSetup.CreateMapToActivityDto<Domain.Clinical.GainShortScreenerModule.GainShortScreener, GainShortScreenerDto>()
                .ForMember (
                    dest => dest.ProblemFeelingDepressedNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.ProblemFeelingDepressedNumber ) )
                .ForMember (
                    dest => dest.ProblemSleepingNumber, opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.ProblemSleepingNumber ) )
                .ForMember (
                    dest => dest.ProblemFeelingAnxiousNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.ProblemFeelingAnxiousNumber ) )
                .ForMember (
                    dest => dest.ProblemBecomingDistressedNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.ProblemBecomingDistressedNumber ) )
                .ForMember (
                    dest => dest.ProblemCommittingSuicideNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.ProblemCommittingSuicideNumber ) )
                .ForMember (
                    dest => dest.InternalizingDisorderScreenerPastMonthScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.InternalizingDisorderScreenerPastMonthScore ) )
                .ForMember (
                    dest => dest.InternalizingDisorderScreenerPastYearScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.InternalizingDisorderScreenerPastYearScore ) )
                .ForMember (
                    dest => dest.InternalizingDisorderScreenerLifetimeScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerInternalizingDisorder.InternalizingDisorderScreenerLifetimeScore ) )
                .ForMember (
                    dest => dest.TwoOrMoreLiedNumber, opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.TwoOrMoreLiedNumber ) )
                .ForMember (
                    dest => dest.TwoOrMoreHardTimePayingAttentionNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.TwoOrMoreHardTimePayingAttentionNumber ) )
                .ForMember (
                    dest => dest.TwoOrMoreHardTimeListeningNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.TwoOrMoreHardTimeListeningNumber ) )
                .ForMember (
                    dest => dest.TwoOrMoreThreatenedOthersNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.TwoOrMoreThreatenedOthersNumber ) )
                .ForMember (
                    dest => dest.TwoOrMoreStartedFightNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.TwoOrMoreStartedFightNumber ) )
                .ForMember (
                    dest => dest.ExternalizingDisorderScreenerPastMonthScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.ExternalizingDisorderScreenerPastMonthScore ) )
                .ForMember (
                    dest => dest.ExternalizingDisorderScreenerPastYearScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.ExternalizingDisorderScreenerPastYearScore ) )
                .ForMember (
                    dest => dest.ExternalizingDisorderScreenerLifetimeScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerExternalizingDisorder.ExternalizingDisorderScreenerLifetimeScore ) )
                .ForMember (
                    dest => dest.LastTimeUsedAlcoholDrugsNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.LastTimeUsedAlcoholDrugsNumber ) )
                .ForMember (
                    dest => dest.LastTimeSpentALotOfTimeGettingAlcoholNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.LastTimeSpentALotOfTimeGettingAlcoholNumber ) )
                .ForMember (
                    dest => dest.LastTimeKeptUsingAlcoholNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.LastTimeKeptUsingAlcoholNumber ) )
                .ForMember (
                    dest => dest.LastTimeUseAlcoholCauseYouToGiveUpNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.LastTimeUseAlcoholCauseYouToGiveUpNumber ) )
                .ForMember (
                    dest => dest.LastTimeHadWithdrawProblemsNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.LastTimeHadWithdrawProblemsNumber ) )
                .ForMember (
                    dest => dest.SubstanceDisorderScreenerPastMonthScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.SubstanceDisorderScreenerPastMonthScore ) )
                .ForMember (
                    dest => dest.SubstanceDisorderScreenerPastYearScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.SubstanceDisorderScreenerPastYearScore ) )
                .ForMember (
                    dest => dest.SubstanceDisorderScreenerLifetimeScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerSubstanceDisorder.SubstanceDisorderScreenerLifetimeScore ) )
                .ForMember (
                    dest => dest.LastTimeYouHadDisagreementNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.LastTimeYouHadDisagreementNumber ) )
                .ForMember (
                    dest => dest.LastTimeYouTookSomethingNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.LastTimeYouTookSomethingNumber ) )
                .ForMember (
                    dest => dest.LastTimeYouSoldIllegalDrugsNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.LastTimeYouSoldIllegalDrugsNumber ) )
                .ForMember (
                    dest => dest.LastTimeYouDroveUnderTheInfluenceNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.LastTimeYouDroveUnderTheInfluenceNumber ) )
                .ForMember (
                    dest => dest.LastTimeYouPurposelyDamagedPropertyNumber,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.LastTimeYouPurposelyDamagedPropertyNumber ) )
                .ForMember (
                    dest => dest.SignificantProblemsSeekingTreatmentIndicator,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.SignificantProblemsSeekingTreatmentIndicator ) )
                .ForMember (
                    dest => dest.SignificantProblemsSeekingTreatmentNote,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.SignificantProblemsSeekingTreatmentNote ) )
                .ForMember (
                    dest => dest.CrimeViolenceScreenerPastMonthScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.CrimeViolenceScreenerPastMonthScore ) )
                .ForMember (
                    dest => dest.CrimeViolenceScreenerPastYearScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.CrimeViolenceScreenerPastYearScore ) )
                .ForMember (
                    dest => dest.CrimeViolenceScreenerLifetimeScore,
                    opt => opt.MapFrom ( src => src.GainShortScreenerCrimeViolence.CrimeViolenceScreenerLifetimeScore ) );
        }

        private static void CreateGpraInterviewSectionConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<GpraInterviewInformation, GpraInterviewInformationDto> ()
                .ForMember ( dest => dest.Key, opt => opt.Ignore () )
                .ForMember ( dest => dest.AppointmentStartDateTime, opt => opt.Ignore () )
                .ForMember ( dest => dest.PatientUniqueIdentifier, opt => opt.Ignore () );

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraProblemsTreatmentRecoverySection, GpraProblemsTreatmentRecoveryDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraCrimeCriminalJusticeSection, GpraCrimeCriminalJusticeDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraProfessionalInformationSection, GpraProfessionalInformationDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraSocialConnectednessSection, GpraSocialConnectednessDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraDemographicsSection, GpraDemographicsDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraDrugAlcoholUseSection, GpraDrugAlcoholUseDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraPlannedServicesSection, GpraPlannedServicesDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraFamilyLivingConditionsSection, GpraFamilyLivingConditionsDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraFollowUpSection, GpraFollowUpDto> ();

            GpraNodeAutoMapperSetup.CreateMapToGpraDto<GpraDischargeSection, GpraDischargeDto> ();
        }

        private static void CreateIdentifiersConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientIdentifiersDto> ();
            AutoMapperSetup.CreateMapToEditableDto<PatientIdentifier, PatientIdentifierDto> ();
        }

        private static void CreateLabSpecimenConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<LabFacility, LabFacilityDto> ();
            AutoMapperSetup.CreateMapToEditableDto<LabResult, LabResultDto> ()
                .ForMember ( dest => dest.ReferenceRange, opt => opt.MapFrom ( src => src.LabTest.LabTestInfo.NormalRangeDescription ) );

            ActivityAutoMapperSetup.CreateMapToActivityDto<LabSpecimen, LabSpecimenDto> ()
                .ForMember (
                    dest => dest.LabResults,
                    opt => opt.MapFrom ( src => src.LabTests == null || src.LabTests.Count == 0 ? null : src.LabTests[0].LabResults ) )
                .ForMember (
                    dest => dest.LabTestName,
                    opt => opt.MapFrom ( src => src.LabTests == null || src.LabTests.Count == 0 ? null : src.LabTests[0].LabTestInfo.LabTestName ) )
                .ForMember (
                    dest => dest.LabTestDate,
                    opt => opt.MapFrom ( src => src.LabTests == null || src.LabTests.Count == 0 ? null : src.LabTests[0].LabTestInfo.TestReportDate ) )
                .ForMember (
                    dest => dest.LabTestNote,
                    opt =>
                    opt.MapFrom ( src => src.LabTests == null || src.LabTests.Count == 0 ? string.Empty : src.LabTests[0].LabTestInfo.LabTestNote ) );
        }

        private static void CreateMedicationConfig ()
        {
            MapPatientMedications ();
        }

        private static void CreatePatientAccessEventConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientAccessEvent, PatientAccessEventDto> ()
                .ForMember (
                    dest => dest.PatientName,
                    opt => opt.MapFrom ( src => src.Patient.Name.Last + ", " + src.Patient.Name.First ) )
                .ForMember (
                    dest => dest.PatientAccessEventTypeName,
                    opt => opt.MapFrom ( src => src.PatientAccessEventType.Name ) )
                .ForMember (
                    dest => dest.UserName,
                    opt =>
                    opt.MapFrom (
                        src =>
                        ( src.CreatedBySystemAccount == null )
                            ? string.Empty
                            : src.CreatedBySystemAccount.Identifier ) );
        }

        private static void CreatePatientAddressesConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientAddressesDto> ();
            AutoMapperSetup.CreateMapToEditableDto<PatientAddress, PatientAddressDto> ()
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.Address.FirstStreetAddress) )
                .ForMember(dest => dest.SecondStreetAddress, opt => opt.MapFrom(src => src.Address.SecondStreetAddress))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Address.CityName))
                .ForMember(dest => dest.CountyArea, opt => opt.MapFrom(src => src.Address.CountyArea))
                .ForMember(dest => dest.StateProvince, opt => opt.MapFrom(src => src.Address.StateProvince))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.Address.PostalCode == null ? null : src.Address.PostalCode.Code ) );
        }

        private static void CreatePatientAlertsConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientAlert, PatientAlertDto> ();
        }

        private static void CreatePatientAliasesConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientAliasesDto> ();
            AutoMapperSetup.CreateMapToEditableDto<PatientAlias, PatientAliasDto> ();
        }

        private static void CreatePatientAllergiesConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientAllergiesDto> ()
                .ForMember ( dest => dest.Allergies, opt => opt.MapFrom ( src => src.Allergies.Where ( a => a.Provenance == null ) ) );
            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientImportedAllergiesDto> ()
                .ForMember ( dest => dest.Allergies, opt => opt.MapFrom ( src => src.Allergies.Where ( a => a.Provenance != null ) ) );
        }

        private static void CreatePatientConfidentialInformationConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientConfidentialInformationDto> ()
                .ForMember (
                    dest => dest.ConfidentialFamilyInformationDescription,
                    opt => opt.MapFrom ( src => src.ConfidentialInfo.ConfidentialFamilyInformationDescription ) )
                .ForMember ( dest => dest.ConvictedOfArsonDate, opt => opt.MapFrom ( src => src.ConfidentialInfo.ConvictedOfArsonDate ) )
                .ForMember ( dest => dest.ConvictedOfArsonIndicator, opt => opt.MapFrom ( src => src.ConfidentialInfo.ConvictedOfArsonIndicator ) )
                .ForMember (
                    dest => dest.DomesticAbuseVictimIndicator, opt => opt.MapFrom ( src => src.ConfidentialInfo.DomesticAbuseVictimIndicator ) )
                .ForMember (
                    dest => dest.PhysicalAbuseVictimIndicator, opt => opt.MapFrom ( src => src.ConfidentialInfo.PhysicalAbuseVictimIndicator ) )
                .ForMember ( dest => dest.RegisteredSexOffenderDate, opt => opt.MapFrom ( src => src.ConfidentialInfo.RegisteredSexOffenderDate ) )
                .ForMember (
                    dest => dest.RegisteredSexOffenderIndicator, opt => opt.MapFrom ( src => src.ConfidentialInfo.RegisteredSexOffenderIndicator ) )
                .ForMember ( dest => dest.SexualAbuseVictimIndicator, opt => opt.MapFrom ( src => src.ConfidentialInfo.SexualAbuseVictimIndicator ) );
        }

        private static void CreatePatientConfig ()
        {
            MapRaceToRaceDto ();

            MapPatientRaceToPatientRaceDto ();

            MapPatientContactContactTypeToLookupValueDto ();

            MapPatientContactToPatientContactDto ();

            MapPatientAliasToPatientAliasDto ();

            MapPatientSpecialNeedToLookupValueDto ();

            MapPatientDisabilityToLookupValueDto ();

            MapPatientIdentifierToPatientIdentifierDto ();

            MapPatientToPatientQuickSearchResultsDto ();
        }

        private static void CreatePatientDemographicDetailsConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientDemographicDetailsDto> ()
                .ForMember ( dest => dest.BirthCityName, opt => opt.MapFrom ( src => src.BirthInfo.BirthCityName ) )
                .ForMember ( dest => dest.BirthCountyArea, opt => opt.MapFrom ( src => src.BirthInfo.BirthCountyArea ) )
                .ForMember ( dest => dest.BirthFirstName, opt => opt.MapFrom ( src => src.BirthInfo.BirthFirstName ) )
                .ForMember ( dest => dest.BirthLastName, opt => opt.MapFrom ( src => src.BirthInfo.BirthLastName ) )
                .ForMember ( dest => dest.BirthStateProvince, opt => opt.MapFrom ( src => src.BirthInfo.BirthStateProvince ) )
                .ForMember ( dest => dest.ZipCode, opt => opt.MapFrom ( src => src.AssignedArea.PostalCode.Code ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.AssignedArea.CountyArea ) )
                .ForMember ( dest => dest.GeographicalRegion, opt => opt.MapFrom ( src => src.AssignedArea.GeographicalRegion ) )
                .ForMember ( dest => dest.MotherFirstName, opt => opt.MapFrom ( src => src.MotherName.First ) )
                .ForMember ( dest => dest.MotherMaidenName, opt => opt.MapFrom ( src => src.MotherName.Maiden ) );
        }

        private static void CreatePatientDocumentConfig ()
        {
            CreatePatientDocumentConfig<PatientDocumentDto> ();
            CreatePatientDocumentConfig<MailAttachmentPatientDocumentDto> ()
                .ForMember(
                    destination => destination.MailId,
                    opt => opt.Ignore())
                .ForMember(
                destination => destination.MailFolderName,
                opt => opt.Ignore());
        }

        private static IMappingExpression<PatientDocument, TDto> CreatePatientDocumentConfig<TDto>() where TDto : PatientDocumentDto
        {
            return AutoMapperSetup.CreateMapToAbstractDto<PatientDocument, TDto>()
                .ForMember(
                    destination => destination.ClinicalStartDate,
                    opt => opt.MapFrom(src => src.ClinicalDateRange.StartDate))
                .ForMember(
                    destination => destination.ClinicalEndDate,
                    opt => opt.MapFrom(src => src.ClinicalDateRange.EndDate))
                .ForMember(
                    destination => destination.CreatedDate,
                    opt => opt.MapFrom(src => src.CreatedTimestamp.DateTime))
                .ForMember(
                    destination => destination.IsEncrypted,
                    opt => opt.Ignore());
        }

        private static void CreatePatientDtoConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientProfileDto> ()
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.Name.First ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.Name.Last ) )
                .ForMember ( dest => dest.MiddleName, opt => opt.MapFrom ( src => src.Name.Middle ) )
                .ForMember ( dest => dest.PrefixName, opt => opt.MapFrom ( src => src.Name.Prefix ) )
                .ForMember ( dest => dest.SuffixName, opt => opt.MapFrom ( src => src.Name.Suffix ) )
                .ForMember ( dest => dest.PatientGender, opt => opt.MapFrom ( src => src.Profile.PatientGender ) )
                .ForMember ( dest => dest.BirthDate, opt => opt.MapFrom ( src => src.Profile.BirthDate ) )
                .ForMember ( dest => dest.DeathDate, opt => opt.MapFrom ( src => src.Profile.DeathDate ) )
                .ForMember (
                    dest => dest.EmailAddress,
                    opt => opt.MapFrom ( src => src.Profile.EmailAddress == null ? null : src.Profile.EmailAddress.Address ) )
                .ForMember ( dest => dest.ContactPreference, opt => opt.MapFrom ( src => src.Profile.ContactPreference ) );

            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientLegalStatusDto> ()
                .ForMember ( dest => dest.CitizenshipCountry, opt => opt.MapFrom ( src => src.LegalInfo.CitizenshipCountry ) )
                .ForMember ( dest => dest.CustodialStatus, opt => opt.MapFrom ( src => src.LegalInfo.CustodialStatus ) )
                .ForMember ( dest => dest.ImmigrationStatus, opt => opt.MapFrom ( src => src.LegalInfo.ImmigrationStatus ) );

            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientVeteranInformationDto> ()
                .ForMember ( dest => dest.DisabilityDescription, opt => opt.MapFrom ( src => src.VeteranInformation.DisabilityDescription ) )
                .ForMember ( dest => dest.DisabilityPercentageValue, opt => opt.MapFrom ( src => src.VeteranInformation.DisabilityPercentageValue ) )
                .ForMember (
                    dest => dest.HaveCombatHistoryIndicator, opt => opt.MapFrom ( src => src.VeteranInformation.HaveCombatHistoryIndicator ) )
                .ForMember (
                    dest => dest.HaveServedInMilitaryIndicator, opt => opt.MapFrom ( src => src.VeteranInformation.HaveServedInMilitaryIndicator ) )
                .ForMember ( dest => dest.RegisteredVaHospitalName, opt => opt.MapFrom ( src => src.VeteranInformation.RegisteredVaHospitalName ) )
                .ForMember (
                    dest => dest.ServiceStartDate,
                    opt =>
                    opt.MapFrom ( src => src.VeteranInformation.ServiceDateRange == null ? null : src.VeteranInformation.ServiceDateRange.StartDate ) )
                .ForMember (
                    dest => dest.ServiceEndDate,
                    opt =>
                    opt.MapFrom ( src => src.VeteranInformation.ServiceDateRange == null ? null : src.VeteranInformation.ServiceDateRange.EndDate ) )
                .ForMember ( dest => dest.VaCaseNumber, opt => opt.MapFrom ( src => src.VeteranInformation.VaCaseNumber ) )
                .ForMember ( dest => dest.VeteranDischargeStatus, opt => opt.MapFrom ( src => src.VeteranInformation.VeteranDischargeStatus ) )
                .ForMember ( dest => dest.VeteranServiceBranch, opt => opt.MapFrom ( src => src.VeteranInformation.VeteranServiceBranch ) )
                .ForMember ( dest => dest.VeteranStatus, opt => opt.MapFrom ( src => src.VeteranInformation.VeteranStatus ) );

            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientContactsDto> ();

            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientDto> ()
                .ForMember ( dest => dest.PatientProfile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientAliases, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientPhoneNumbers, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientAddresses, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientIdentifiers, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientDemographicDetails, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientLegalStatus, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientOtherConsiderations, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientAllergies, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientImportedAllergies, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientConfidentialInformation, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientRaceAndEthnicity, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PatientContacts, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.VeteranInformation, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.ZipCode, opt => opt.MapFrom ( src => src.AssignedArea.PostalCode.Code ) );

            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientSummaryDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.First))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Name.Middle))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Last))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Profile.PatientGender.AdministrativeGender))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Profile.BirthDate))
                .ForMember ( dest => dest.PatientPhoneNumbers, opt => opt.MapFrom ( src => src ) )
                .ForMember (
                    dest => dest.PatientHomeAddress,
                    opt =>
                    opt.MapFrom (
                        src =>
                        src.Addresses.FirstOrDefault (
                            a => a.PatientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.Home ) ) );
        }

        private static void CreatePatientOtherConsiderationsConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientOtherConsiderationsDto> ()
                .ForMember ( dest => dest.Language, opt => opt.MapFrom ( src => src.Language.Language ) )
                .ForMember ( dest => dest.InterpreterNeededIndicator, opt => opt.MapFrom ( src => src.Language.InterpreterNeededIndicator ) );
        }

        private static void CreatePatientPhoneNumbersConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientPhoneNumbersDto> ();
            AutoMapperSetup.CreateMapToEditableDto<PatientPhone, PatientPhoneDto> ();
        }

        private static void CreatePatientRaceAndEthnicityConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<Patient, PatientRaceAndEthnicityDto> ()
                .ForMember ( dest => dest.DetailedEthnicity, opt => opt.MapFrom ( src => src.Ethnicity.DetailedEthnicity ) )
                .ForMember ( dest => dest.Ethnicity, opt => opt.MapFrom ( src => src.Ethnicity.Ethnicity ) )
                .ForMember ( dest => dest.PrimaryRace, opt => opt.MapFrom ( src => src.PrimaryPatientRace.Race ) );
        }

        private static void CreatePatientReminderConfig ()
        {
            Func<Patient, List<string>> contactPreferenceProvider = src =>
                {
                    if ( src.Profile.ContactPreference != null )
                    {
                        if ( src.Profile.ContactPreference.WellKnownName == ContactPreference.Letter )
                        {
                            return
                                src.Addresses.Select (
                                    add =>
                                    string.Format (
                                        "({0}) {1} {2}, {3} {4}",
                                        add.PatientAddressType.ShortName,
                                        add.Address.FirstStreetAddress,
                                        add.Address.CityName,
                                        add.Address.StateProvince.ShortName,
                                        add.Address.PostalCode ) ).ToList ();
                        }
                        if ( src.Profile.ContactPreference.WellKnownName == ContactPreference.Phone )
                        {
                            return
                                src.PhoneNumbers.Select (
                                    ph =>
                                    string.Format (
                                        "({0}) {1} ext. {2}",
                                        ph.PatientPhoneType.ShortName,
                                        ph.PhoneNumber,
                                        ph.PhoneExtensionNumber ) ).ToList ();
                        }
                        if ( src.Profile.ContactPreference.WellKnownName == ContactPreference.Email )
                        {
                            return new List<string> { src.Profile.EmailAddress.Address };
                        }
                    }
                    return null;
                };

            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientReminderResultDto> ()
                .ForMember ( dest => dest.PatientContactInfos, opt => opt.MapFrom ( src => contactPreferenceProvider ( src ) ) )
                .ForMember ( dest => dest.PatientIdentifier, opt => opt.MapFrom ( src => src.UniqueIdentifier ) )
                .ForMember ( dest => dest.PatientPrefixName, opt => opt.MapFrom ( src => src.Name.Prefix ) )
                .ForMember ( dest => dest.PatientName, opt => opt.MapFrom ( src => src.Name.Last + ", " + src.Name.First ) )
                .ForMember ( dest => dest.PatientContactPreference, opt => opt.MapFrom ( src => src.Profile.ContactPreference ) );
        }

        private static void CreatePriorityPopulationConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<ClinicalCasePriorityPopulation, LookupValueDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.PriorityPopulation.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.PriorityPopulation.Name ) )
                .ForMember (
                    dest => dest.WellKnownName,
                    opt => opt.MapFrom ( src => src.PriorityPopulation.WellKnownName ) )
                .ForMember (
                    dest => dest.ShortName,
                    opt => opt.MapFrom ( src => src.PriorityPopulation.ShortName ) )
                .ForMember (
                    dest => dest.SortOrderNumber,
                    opt => opt.MapFrom ( src => src.PriorityPopulation.SortOrderNumber ) );
        }

        private static void CreateProblemConfig ()
        {
            MapPatientProblem ();
        }

        private static void CreateProblemEnrollmentConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<ProgramEnrollment, ProgramEnrollmentDto> ()
                .ForMember ( dest => dest.ProgramOfferingKey, opt => opt.MapFrom ( src => src.ProgramOffering.Key ) )
                .ForMember ( dest => dest.ProgramName, opt => opt.MapFrom ( src => src.ProgramOffering.Program.Name ) )
                .ForMember ( dest => dest.Location, opt => opt.MapFrom ( src => src.ProgramOffering.Location ) )
                .ForMember ( dest => dest.ClinicalCaseKey, opt => opt.MapFrom ( src => src.ClinicalCase.Key ) );

            AutoMapperSetup.CreateMapToAbstractDto<DisenrollReason, LookupValueDto> ();

            AutoMapperSetup.CreateMapToAbstractDto<ProgramOffering, ProgramOfferingLocationDto> ()
                .ForMember ( dest => dest.LocationDisplayName, opt => opt.MapFrom ( src => src.Location.LocationProfile.LocationName.DisplayName ) );
        }

        private static void CreateSignedCommentConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<ClinicalCaseSignedComment, ClinicalCaseSignedCommentDto> ();
        }

        private static void CreateSpecialInitiativeConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<ClinicalCaseSpecialInitiative, LookupValueDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.SpecialInitiative.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.SpecialInitiative.Name ) )
                .ForMember ( dest => dest.WellKnownName, opt => opt.MapFrom ( src => src.SpecialInitiative.WellKnownName ) )
                .ForMember ( dest => dest.ShortName, opt => opt.MapFrom ( src => src.SpecialInitiative.ShortName ) )
                .ForMember (
                    dest => dest.SortOrderNumber,
                    opt => opt.MapFrom ( src => src.SpecialInitiative.SortOrderNumber ) );
        }

        private static void CreateVisitConfig ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<ProblemType, LookupValueDto> ();
            AutoMapperSetup.CreateMapToAbstractDto<VisitProblem, ProblemDto> ()
                .ForMember ( dest => dest.ProblemCodeCodedConcept, opt => opt.MapFrom ( src => src.Problem.ProblemCodeCodedConcept ) )
                .ForMember ( dest => dest.ProblemStatus, opt => opt.MapFrom ( src => src.Problem.ProblemStatus ) )
                .ForMember ( dest => dest.ProblemType, opt => opt.MapFrom ( src => src.Problem.ProblemType ) )
                .ForMember ( dest => dest.ObservedDate, opt => opt.MapFrom ( src => src.Problem.ObservedDate ) )
                .ForMember ( dest => dest.StatusChangedDate, opt => opt.MapFrom ( src => src.Problem.StatusChangedDate ) )
                .ForMember ( dest => dest.ObservedByStaff, opt => opt.MapFrom ( src => src.Problem.ObservedByStaff ) )
                .ForMember ( dest => dest.ClinicalCaseKey, opt => opt.MapFrom ( src => src.Problem.ClinicalCase.Key ) )
                .ForMember ( dest => dest.CauseOfDeathIndicator, opt => opt.MapFrom ( src => src.Problem.CauseOfDeathIndicator ) )
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.Problem.Key ) )
                .ForMember ( destination => destination.AssociatedIndicator, options => options.Ignore () )
                .ForMember (
                    dest => dest.OnsetStartDate,
                    opt => opt.MapFrom ( src => src.Problem.OnsetDateRange == null ? null : src.Problem.OnsetDateRange.StartDate ) )
                .ForMember (
                    dest => dest.OnsetEndDate,
                    opt => opt.MapFrom ( src => src.Problem.OnsetDateRange == null ? null : src.Problem.OnsetDateRange.EndDate ) )
                .ForMember ( dest => dest.ProvenanceKey, opt => opt.MapFrom ( src => src.Problem.Provenance.Key ) )
                .ForMember (
                    dest => dest.ProvenanceAssigningAuthorityName,
                    opt =>
                    opt.MapFrom (
                        src =>
                        src.Problem.Provenance == null || src.Problem.Provenance.TaggedDataElement == null
                            ? null
                            : src.Problem.Provenance.TaggedDataElement.AssigningAuthorityName ) )
                .ForMember (
                    dest => dest.ProvenanceDate,
                    opt =>
                    opt.MapFrom (
                        src => src.Problem.Provenance.CreatedTimestamp == null ? null : ( DateTime? )src.Problem.Provenance.CreatedTimestamp.DateTime ) );


            AutoMapperSetup.CreateMapToAbstractDto<VisitStatus, LookupValueDto> ();

            MapVisitToVisitDto ();
        }

        private static void CreateVitalSignConfig ()
        {
            AutoMapperSetup.CreateMapToEditableDto<BloodPressure, BloodPressureDto> ();
            AutoMapperSetup.CreateMapToEditableDto<HeartRate, HeartRateDto> ();
            ActivityAutoMapperSetup.CreateMapToActivityDtoWithoutResults<VitalSign, VitalSignDto>()
                .ForMember(dest => dest.Results, opt => opt.ResolveUsing<VitalSignToActivityResolver>());
        }

        private static void MapActivityToActivityDto ()
        {
            ActivityAutoMapperSetup.CreateMapToActivityDto<Activity, ActivityDto>();
        }

        private static void MapActivityTypeToActivityTypeDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<ActivityType, ActivityTypeDto> ();
        }

        private static void MapPatientAliasToPatientAliasDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientAlias, PatientAliasDto> ();
        }

        private static void MapPatientContactContactTypeToLookupValueDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientContactContactType, LookupValueDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.PatientContactType.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.PatientContactType.Name ) )
                .ForMember (
                    dest => dest.WellKnownName,
                    opt => opt.MapFrom ( src => src.PatientContactType.WellKnownName ) )
                .ForMember (
                    dest => dest.ShortName,
                    opt => opt.MapFrom ( src => src.PatientContactType.ShortName ) )
                .ForMember (
                    dest => dest.SortOrderNumber,
                    opt => opt.MapFrom ( src => src.PatientContactType.SortOrderNumber ) );
        }

        private static void MapPatientContactToPatientContactDto ()
        {
            AutoMapperSetup.CreateMapToEditableDto<PatientContact, PatientContactDto> ()
                .ForMember ( dest => dest.Profile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.ContactInformation, opt => opt.MapFrom ( src => src ) );
            AutoMapperSetup.CreateMapToEditableDto<PatientContact, PatientContactProfileDto> ()
                .ForMember ( dest => dest.PatientKey, opt => opt.MapFrom ( src => src.Patient.Key ) );
            AutoMapperSetup.CreateMapToEditableDto<PatientContact, PatientContactContactInformationDto> ();
            AutoMapperSetup.CreateMapToEditableDto<PatientContactPhone, PatientContactPhoneDto> ();
        }

        private static void MapPatientDisabilityToLookupValueDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientDisability, LookupValueDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.Disability.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.Disability.Name ) )
                .ForMember ( dest => dest.WellKnownName, opt => opt.MapFrom ( src => src.Disability.WellKnownName ) )
                .ForMember ( dest => dest.ShortName, opt => opt.MapFrom ( src => src.Disability.ShortName ) )
                .ForMember ( dest => dest.SortOrderNumber, opt => opt.MapFrom ( src => src.Disability.SortOrderNumber ) );
        }

        private static void MapPatientIdentifierToPatientIdentifierDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientIdentifier, PatientIdentifierDto> ()
                .ForMember (
                    dest => dest.EffectiveDate,
                    opt =>
                    opt.MapFrom (
                        src => src.EffectiveDateRange != null ? src.EffectiveDateRange.StartDate : null ) )
                .ForMember (
                    dest => dest.ExpirationDate,
                    opt =>
                    opt.MapFrom ( src => src.EffectiveDateRange != null ? src.EffectiveDateRange.EndDate : null ) );
        }

        private static void MapPatientMedications ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Medication, MedicationDto> ()
                .ForMember ( dest => dest.StartDate, opt => opt.MapFrom ( src => src.UsageDateRange == null ? null : src.UsageDateRange.StartDate ) )
                .ForMember ( dest => dest.EndDate, opt => opt.MapFrom ( src => src.UsageDateRange == null ? null : src.UsageDateRange.EndDate ) )
                .ForMember ( dest => dest.ProvenanceKey, opt => opt.MapFrom ( src => src.Provenance.Key ) )
                .ForMember (
                    dest => dest.ProvenanceAssigningAuthorityName,
                    opt =>
                    opt.MapFrom (
                        src =>
                        src.Provenance == null || src.Provenance.TaggedDataElement == null
                            ? null
                            : src.Provenance.TaggedDataElement.AssigningAuthorityName ) )
                .ForMember (
                    dest => dest.ProvenanceDate,
                    opt =>
                    opt.MapFrom ( src => src.Provenance.CreatedTimestamp == null ? null : ( DateTime? )src.Provenance.CreatedTimestamp.DateTime ) );
        }

        private static void MapPatientProblem ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Problem, ProblemDto> ()
                .ForMember ( destination => destination.AssociatedIndicator, options => options.Ignore () )
                .ForMember (
                    dest => dest.OnsetStartDate,
                    opt => opt.MapFrom ( src => src.OnsetDateRange == null ? null : src.OnsetDateRange.StartDate ) )
                .ForMember (
                    dest => dest.OnsetEndDate,
                    opt => opt.MapFrom ( src => src.OnsetDateRange == null ? null : src.OnsetDateRange.EndDate ) )
                .ForMember ( dest => dest.ProvenanceKey, opt => opt.MapFrom ( src => src.Provenance.Key ) )
                .ForMember (
                    dest => dest.ProvenanceAssigningAuthorityName,
                    opt =>
                    opt.MapFrom (
                        src =>
                        src.Provenance == null || src.Provenance.TaggedDataElement == null
                            ? null
                            : src.Provenance.TaggedDataElement.AssigningAuthorityName ) )
                .ForMember (
                    dest => dest.ProvenanceDate,
                    opt =>
                    opt.MapFrom ( src => src.Provenance.CreatedTimestamp == null ? null : ( DateTime? )src.Provenance.CreatedTimestamp.DateTime ) );
        }

        private static void MapPatientRaceToPatientRaceDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientRace, LookupValueDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.Race.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( p => p.Race.Name ) )
                .ForMember ( dest => dest.WellKnownName, opt => opt.MapFrom ( p => p.Race.WellKnownName ) )
                .ForMember (
                    dest => dest.ShortName,
                    opt => opt.MapFrom ( src => src.Race.ShortName ) )
                .ForMember ( dest => dest.SortOrderNumber, opt => opt.MapFrom ( p => p.Race.SortOrderNumber ) );
        }

        private static void MapPatientSpecialNeedToLookupValueDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PatientSpecialNeed, LookupValueDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.SpecialNeed.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.SpecialNeed.Name ) )
                .ForMember ( dest => dest.WellKnownName, opt => opt.MapFrom ( src => src.SpecialNeed.WellKnownName ) )
                .ForMember ( dest => dest.ShortName, opt => opt.MapFrom ( src => src.SpecialNeed.ShortName ) )
                .ForMember ( dest => dest.SortOrderNumber, opt => opt.MapFrom ( src => src.SpecialNeed.SortOrderNumber ) );
        }

        private static void MapPatientToPatientQuickSearchResultsDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Patient, PatientSearchResultDto> ()
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.Name.First ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.Name.Last ) )
                .ForMember ( dest => dest.MiddleName, opt => opt.MapFrom ( src => src.Name.Middle ) )
                .ForMember ( dest => dest.PrefixName, opt => opt.MapFrom ( src => src.Name.Prefix ) )
                .ForMember ( dest => dest.SuffixName, opt => opt.MapFrom ( src => src.Name.Suffix ) )
                .ForMember ( dest => dest.PatientGenderName, opt => opt.MapFrom ( src => src.Profile.PatientGender.Name ) )
                .ForMember( dest => dest.PatientGender, opt => opt.MapFrom(src => src.Profile.PatientGender))
                .ForMember ( dest => dest.BirthDate, opt => opt.MapFrom ( src => src.Profile.BirthDate ) );
        }

        private static void MapRaceToRaceDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<RaceDetailedEthnicity, LookupValueDto> ()
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.DetailedEthnicity.Key ) )
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.DetailedEthnicity.Name ) )
                .ForMember ( dest => dest.WellKnownName, opt => opt.MapFrom ( src => src.DetailedEthnicity.WellKnownName ) )
                .ForMember (
                    dest => dest.ShortName,
                    opt => opt.MapFrom ( src => src.DetailedEthnicity.ShortName ) )
                .ForMember (
                    dest => dest.SortOrderNumber,
                    opt => opt.MapFrom ( src => src.DetailedEthnicity.SortOrderNumber ) );

            AutoMapperSetup.CreateMapToAbstractDto<Race, LookupValueDto> ();
        }

        private static void MapVisitToVisitDto ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Visit, VisitDto> ()
                .ForMember ( opt => opt.AppointmentStartDateTime,
                    dest => dest.MapFrom ( visit => visit.AppointmentDateTimeRange.StartDateTime ) )
                .ForMember ( opt => opt.AppointmentEndDateTime,
                    dest => dest.MapFrom ( visit => visit.AppointmentDateTimeRange.EndDateTime ) )
                .ForMember ( opt => opt.Staff, dest => dest.MapFrom ( visit => visit.Staff ) )
                .ForMember ( opt => opt.Location, dest => dest.MapFrom ( visit => visit.ServiceLocation ) )
                .ForMember ( opt => opt.EndTimestampTime, dest => dest.Ignore () )
                .ForMember ( opt => opt.StartTimestampTime, dest => dest.Ignore () );
        }

        private static void CreateSelfPaymentConfig()
        {
            AutoMapperSetup.CreateMapToEditableDto<SelfPayment, SelfPaymentDto> ()
                .ForMember ( dest => dest.Amount, opt => opt.MapFrom ( src => src.Money.Amount ) )
                .ForMember ( dest => dest.CollectedByStaffKey, opt => opt.MapFrom ( src => src.CollectedByStaff.Key ) )
                .ForMember ( dest => dest.CultureName, opt => opt.MapFrom ( src => src.Money.Currency.CultureName ) )
                .ForMember ( dest => dest.CurrencyWellKnownName, opt => opt.MapFrom ( src => src.Money.Currency.WellKnownName ) );
        }

        private static void CreatePayorConfig()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Address, AddressDto> ()
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.PostalCode == null ? string.Empty : src.PostalCode.Code ) );

            AutoMapperSetup.CreateMapToAbstractDto<PayorCache, PayorCacheSummaryDto> ();

            AutoMapperSetup.CreateMapToEditableDto<PayorCoverageCache, PayorCoverageCacheDto> ()
                .ForMember ( dest => dest.StartDate, opt => opt.MapFrom ( src => src.EffectiveDateRange.StartDate ) )
                .ForMember ( dest => dest.EndDate, opt => opt.MapFrom ( src => src.EffectiveDateRange.EndDate ) );

            AutoMapperSetup.CreateMapToEditableDto<PayorSubscriberCache, PayorSubscriberCacheDto> ()
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.Name.First ) )
                .ForMember ( dest => dest.MiddleName, opt => opt.MapFrom ( src => src.Name.Middle ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.Name.Last ) )
                .ForMember ( dest => dest.Key, opt => opt.Ignore () );
        }

        private static void CreateTedsAdmissionInterviewConfig()
        {
            Mapper.CreateMap<DsmDiagnosisResponse, string> ().ConvertUsing<DsmDiagnosisResponseToStringTypeConverter>();

            ActivityAutoMapperSetup.CreateMapToActivityDto<TedsAdmissionInterview, TedsAdmissionInterviewDto> ()
                .ForMember (
                    dest => dest.PrimarySubstanceProblemType,
                    opt => opt.MapFrom (
                        src =>
                        src.PrimaryTedsAdmissionInterviewSubstanceUsage == null? null : src.PrimaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType ) )
                .ForMember (
                    dest => dest.PrimaryUseFrequencyType,
                    opt => opt.MapFrom (
                        src =>
                        src.PrimaryTedsAdmissionInterviewSubstanceUsage == null? null : src.PrimaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.UseFrequencyType ) )
                .ForMember (
                    dest => dest.PrimaryUsualAdministrationRouteType,
                    opt => opt.MapFrom (
                        src =>
                        src.PrimaryTedsAdmissionInterviewSubstanceUsage == null? null : src.PrimaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType ) )
                .ForMember (
                    dest => dest.PrimaryFirstUseAge,
                    opt => opt.MapFrom (
                        src =>
                        src.PrimaryTedsAdmissionInterviewSubstanceUsage == null
                            ? null
                            : src.PrimaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge ) )
                .ForMember (
                    dest => dest.PrimaryDetailedDrugCode,
                    opt => opt.MapFrom (
                        src =>
                        src.PrimaryTedsAdmissionInterviewSubstanceUsage == null
                            ? null
                            : src.PrimaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode ) )
                .ForMember (
                    dest => dest.SecondarySubstanceProblemType,
                    opt => opt.MapFrom (
                        src =>
                        src.SecondaryTedsAdmissionInterviewSubstanceUsage == null? null : src.SecondaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType ) )
                .ForMember (
                    dest => dest.SecondaryUseFrequencyType,
                    opt => opt.MapFrom (
                        src =>
                        src.SecondaryTedsAdmissionInterviewSubstanceUsage == null? null : src.SecondaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.UseFrequencyType ) )
                .ForMember (
                    dest => dest.SecondaryUsualAdministrationRouteType,
                    opt => opt.MapFrom (
                        src =>
                        src.SecondaryTedsAdmissionInterviewSubstanceUsage == null
                            ? null
                            : src.SecondaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType ) )
                .ForMember (
                    dest => dest.SecondaryFirstUseAge,
                    opt => opt.MapFrom (
                        src => src.SecondaryTedsAdmissionInterviewSubstanceUsage == null
                                   ? null
                                   : src.SecondaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge ) )
                .ForMember (
                    dest => dest.SecondaryDetailedDrugCode,
                    opt => opt.MapFrom (
                        src => src.SecondaryTedsAdmissionInterviewSubstanceUsage == null
                                   ? null
                                   : src.SecondaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode ) )
                .ForMember (
                    dest => dest.TertiarySubstanceProblemType,
                    opt => opt.MapFrom (
                        src =>
                        src.TertiaryTedsAdmissionInterviewSubstanceUsage == null? null: src.TertiaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType ) )
                .ForMember (
                    dest => dest.TertiaryUseFrequencyType,
                    opt => opt.MapFrom (
                        src =>
                        src.TertiaryTedsAdmissionInterviewSubstanceUsage == null
                            ? null
                            : src.TertiaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.UseFrequencyType ) )
                .ForMember (
                    dest => dest.TertiaryUsualAdministrationRouteType,
                    opt => opt.MapFrom (
                        src => src.TertiaryTedsAdmissionInterviewSubstanceUsage == null
                                   ? null
                                   : src.TertiaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType ) )
                .ForMember (
                    dest => dest.TertiaryFirstUseAge,
                    opt => opt.MapFrom (
                        src => src.TertiaryTedsAdmissionInterviewSubstanceUsage == null
                                   ? null
                                   : src.TertiaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge ) )
                .ForMember (
                    dest => dest.TertiaryDetailedDrugCode,
                    opt => opt.MapFrom (
                        src => src.TertiaryTedsAdmissionInterviewSubstanceUsage == null
                                   ? null
                                   : src.TertiaryTedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode ) )
                .ForMember (
                    dest => dest.DefaultNonResponseLookupWellKnownNames, opt => opt.MapFrom(src => TedsAdmissionInterview.DefaultNonResponseLookupWellKnownNames));
        }

        private static void CreateTedsDischargeInterviewConfig()
        {
            AutoMapperSetup.CreateMapToAbstractDto<TedsLookupBase, TedsLookupBaseDto>();
            AutoMapperSetup.CreateMapToAbstractDto<SubstanceProblemType, SubstanceProblemTypeDto>();

            ActivityAutoMapperSetup.CreateMapToActivityDto<TedsDischargeInterview, TedsDischargeInterviewDto> ();
            AutoMapperSetup.CreateMapToEditableDto<TedsDischargeInterview, TedsDischargeInterviewDto>()
               .ForMember(
                    dest => dest.TedsAdmissionInterviewKey,
                    opt => opt.MapFrom(src => src.TedsAdmissionInterview.Key))
                .ForMember(
                    dest => dest.PrimarySubstanceProblemType,
                    opt => opt.MapFrom(src => src.PrimaryTedsDischargeInterviewSubstanceUsage == null ? null : src.PrimaryTedsDischargeInterviewSubstanceUsage.SubstanceProblemAndFrequency.SubstanceProblemType))
                .ForMember(
                    dest => dest.PrimaryUseFrequencyType,
                    opt => opt.MapFrom(src => src.PrimaryTedsDischargeInterviewSubstanceUsage == null ? null : src.PrimaryTedsDischargeInterviewSubstanceUsage.SubstanceProblemAndFrequency.UseFrequencyType))
                .ForMember(
                    dest => dest.SecondarySubstanceProblemType,
                    opt => opt.MapFrom(src => src.SecondaryTedsDischargeInterviewSubstanceUsage == null ? null : src.SecondaryTedsDischargeInterviewSubstanceUsage.SubstanceProblemAndFrequency.SubstanceProblemType))
                .ForMember(
                    dest => dest.SecondaryUseFrequencyType,
                    opt => opt.MapFrom(src => src.SecondaryTedsDischargeInterviewSubstanceUsage == null ? null : src.SecondaryTedsDischargeInterviewSubstanceUsage.SubstanceProblemAndFrequency.UseFrequencyType))
                .ForMember(
                    dest => dest.TertiarySubstanceProblemType,
                    opt => opt.MapFrom(src => src.TertiaryTedsDischargeInterviewSubstanceUsage == null ? null : src.TertiaryTedsDischargeInterviewSubstanceUsage.SubstanceProblemAndFrequency.SubstanceProblemType))
                .ForMember(
                    dest => dest.TertiaryUseFrequencyType,
                    opt => opt.MapFrom(src => src.TertiaryTedsDischargeInterviewSubstanceUsage == null ? null : src.TertiaryTedsDischargeInterviewSubstanceUsage.SubstanceProblemAndFrequency.UseFrequencyType))
                    .ForMember(
                    dest => dest.TedsEmploymentStatus,
                    opt => opt.MapFrom(src => src.TedsEmploymentStatusInformation == null ? null : src.TedsEmploymentStatusInformation.TedsEmploymentStatus))
                .ForMember(
                    dest => dest.DetailedNotInLaborForce,
                    opt => opt.MapFrom(src => src.TedsEmploymentStatusInformation == null ? null : src.TedsEmploymentStatusInformation.DetailedNotInLaborForce))
                .ForMember(
                    dest => dest.DefaultNonResponseLookupWellKnownNames,
                    opt => opt.MapFrom(src => TedsDischargeInterview.DefaultNonResponseLookupWellKnownNames));
        }


        private static void CreateProvenanceConfig()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Provenance, ProvenanceDto> ()
                .ForMember ( dest => dest.Extension, opt => opt.MapFrom ( src => src.TaggedDataElement.ExtensionValue ) )
                .ForMember ( dest => dest.AssigningAuthority, opt => opt.MapFrom ( src => src.TaggedDataElement.AssigningAuthorityName ) )
                .ForMember ( dest => dest.ProviderDirectoryEntry, opt => opt.MapFrom ( src => src.AssignedAuthor.ProviderDirectoryEntryAddress ) )
                .ForMember ( dest => dest.PrefixName, opt => opt.MapFrom ( src => src.AssignedAuthor.Name.Prefix ) )
                .ForMember ( dest => dest.FirstName, opt => opt.MapFrom ( src => src.AssignedAuthor.Name.First ) )
                .ForMember ( dest => dest.LastName, opt => opt.MapFrom ( src => src.AssignedAuthor.Name.Last ) )
                .ForMember (
                    dest => dest.OrganizationExtension,
                    opt => opt.MapFrom ( src => src.RepresentedOrganization.OrganizationTaggedDataElement.ExtensionValue ) )
                .ForMember (
                    dest => dest.OrganizationAssigningAuthority,
                    opt => opt.MapFrom ( src => src.RepresentedOrganization.OrganizationTaggedDataElement.AssigningAuthorityName ) )
                .ForMember ( dest => dest.OrganizationName, opt => opt.MapFrom ( src => src.RepresentedOrganization.OrganizationName ) )
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.RepresentedOrganization.Phone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.RepresentedOrganization.Phone.PhoneExtensionNumber ) );
        }
        #endregion
    }
}
