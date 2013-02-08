/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
** Begin Persona Data
*/

/*
** Initialize the HiValue table.  Currently we are setting this to a very 
** large number (12,000,000).  This should really be seeded to 1.
*/

SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

:r .\HiValue.sql

/* Security Module */
:r .\SecurityModule.sql

/* Reports Module */
:r .\ReportsModule.sql

/*
** Initialize Lookup Tables
*/
									 
/* ClinicalCaseModule */
:r .\CodeTableInitialization\ClinicalCaseModule_ClinicalCaseStatusLkp.sql
:r .\CodeTableInitialization\ClinicalCaseModule_DischargeReasonLkp.sql
:r .\CodeTableInitialization\ClinicalCaseModule_InitialContactMethodLkp.sql
:r .\CodeTableInitialization\ClinicalCaseModule_PriorityPopulationLkp.sql
:r .\CodeTableInitialization\ClinicalCaseModule_ReferralTypeLkp.sql
:r .\CodeTableInitialization\ClinicalCaseModule_SpecialInitiativeLkp.sql
:r .\CodeTableInitialization\ClinicalCaseModule_ProblemTypeLkp.sql
:r .\CodeTableInitialization\ClinicalCaseModule_ProblemStatusLkp.sql

/* CommonModule */
:r .\CodeTableInitialization\CommonModule_CountryLkp.sql
:r .\CodeTableInitialization\CommonModule_GeographicalRegionLkp.sql
:r .\CodeTableInitialization\CommonModule_RecordStatusLkp.sql
:r .\CodeTableInitialization\CommonModule_StateProvinceLkp.sql
:r .\CodeTableInitialization\CommonModule_CountyAreaLkp.sql
:r .\CodeTableInitialization\CommonModule_GenderLkp.sql
:r .\CodeTableInitialization\CommonModule_AdministrativeGenderLkp.sql
:r .\CodeTableInitialization\CommonModule_LanguageLkp.sql
:r .\CodeTableInitialization\CommonModule_PrefixLkp.sql
:r .\CodeTableInitialization\CommonModule_SuffixLkp.sql
:r .\CodeTableInitialization\CommonModule_CurrencyLkp.sql
:r .\CodeTableInitialization\CommonModule_PaymentMethodLkp.sql

/* AgencyModule */
:r .\CodeTableInitialization\AgencyModule_AgencyPhoneTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_AgencyAddressTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_AgencyEmailAddressTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_AgencyIdentifierTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_AgencyContactTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_AgencyTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_LocationPhoneTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_LocationAddressTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_LocationEmailAddressTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_LocationContactTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_LocationIdentifierTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_StaffTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_StaffAddressTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_StaffPhoneTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_StaffEventTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_StaffChecklistItemTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_EmploymentTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_CollegeDegreeLkp.sql
:r .\CodeTableInitialization\AgencyModule_LicenseLkp.sql
:r .\CodeTableInitialization\AgencyModule_CertificationLkp.sql
:r .\CodeTableInitialization\AgencyModule_TrainingCourseLkp.sql
:r .\CodeTableInitialization\AgencyModule_StaffIdentifierTypeLkp.sql
:r .\CodeTableInitialization\AgencyModule_LanguageFluencyLkp.sql

/* ProgramModule */
:r .\CodeTableInitialization\ProgramModule_AgeGroupLkp.sql
:r .\CodeTableInitialization\ProgramModule_GenderSpecificationLkp.sql
:r .\CodeTableInitialization\ProgramModule_ProgramCategoryLkp.sql
:r .\CodeTableInitialization\ProgramModule_TreatmentApproachLkp.sql
:r .\CodeTableInitialization\ProgramModule_CapacityTypeLkp.sql
:r .\CodeTableInitialization\ProgramModule_DisenrollReasonLkp.sql

/* PatientModule */
:r .\CodeTableInitialization\PatientModule_CustodialStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_DisabilityLkp.sql
:r .\CodeTableInitialization\PatientModule_LegalAuthorizationTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientAliasTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientContactTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientIdentifierTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientPhotoTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientContactRelationshipTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_SmokingStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_SpecialNeedLkp.sql
:r .\CodeTableInitialization\PatientModule_DetailedEthnicityLkp.sql
:r .\CodeTableInitialization\PatientModule_EducationStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_EthnicityLkp.sql
:r .\CodeTableInitialization\PatientModule_ImmigrationStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_MaritalStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientAddressTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientAccessEventTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientPhoneTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_RaceLkp.sql
:r .\CodeTableInitialization\PatientModule_RaceDetailedEthnicity.sql
:r .\CodeTableInitialization\PatientModule_ReligiousAffiliationLkp.sql
:r .\CodeTableInitialization\PatientModule_ReactionLkp.sql
:r .\CodeTableInitialization\PatientModule_VeteranStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_VeteranDischargeStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_VeteranServiceBranchLkp.sql
:r .\CodeTableInitialization\PatientModule_AllergySeverityTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_AllergyStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_AllergyTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientDocumentTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_MedicationDoseUnitLkp.sql
:r .\CodeTableInitialization\PatientModule_MedicationRouteLkp.sql
:r .\CodeTableInitialization\PatientModule_MedicationStatusLkp.sql
:r .\CodeTableInitialization\PatientModule_DiscontinuedReasonLkp.sql
:r .\CodeTableInitialization\PatientModule_ContactPreferenceLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientGenderLkp.sql
:r .\CodeTableInitialization\PatientModule_PatientContactPhoneTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PayorCoverageCacheTypeLkp.sql
:r .\CodeTableInitialization\PatientModule_PayorSubscriberRelationshipCacheTypeLkp.sql

/* VisitModule */
:r .\CodeTableInitialization\VisitModule_ActivityTypeLkp.sql
:r .\CodeTableInitialization\VisitModule_VisitStatusLkp.sql
:r .\CodeTableInitialization\VisitModule_VitalSignPhysicalExamNotDoneReasonLkp.sql

/* LabModule */
:r .\CodeTableInitialization\LabModule_LabSpecimenTypeLkp.sql
:r .\CodeTableInitialization\LabModule_LabTestNameLkp.sql


/* RadiologyModule */
:r .\CodeTableInitialization\RadiologyModule_RadiologyTestTypeLkp.sql

/* ImmunizationModule */
:r .\CodeTableInitialization\ImmunizationModule_ImmunizationUnitOfMeasureLkp.sql
:r .\CodeTableInitialization\ImmunizationModule_ImmunizationNotGivenReasonLkp.sql

/* GpraModule */
:r .\CodeTableInitialization\GpraModule_GpraNonResponseLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraDetoxificationLocationLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraDrugRouteLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraEducationLevelLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraEffectDueToDrugUseLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraEmploymentStatusLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraFrequencyOfUseOfUsedItemsLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraHousingTypeLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraInterviewTypeLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraJobTrainingProgramLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraOverallHealthLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraPatientGenderLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraPatientTypeLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraPlaceToLiveLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraPsychologicalImpactLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraSexualActivityLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraTroubleContactLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraFollowUpStatusLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraDischargeTerminationReasonLkp.sql
:r .\CodeTableInitialization\GpraModule_GpraDischargeStatusLkp.sql

/* DensAsiModule */
:r .\CodeTableInitialization\DensAsiModule_DensAsiControlledEnvironmentLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiDrugAlcoholAdministrationRouteLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiEmploymentPatternLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiFreeTimeSpentTypeLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiInterviewClassLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiInterviewContactTypeLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiInterviewerRatingLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiLivingArrangementTypeLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiMaritalStatusLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiNonResponseLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiOccupationTypeLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiPatientRatingLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiProblematicSubstanceLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiReligionLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiViolationTypeLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiTreatmentModalityLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiIncompleteInterviewReasonLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiSatisfactionLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiHasRelationshipOptionLkp.sql
:r .\CodeTableInitialization\DensAsiModule_DensAsiHasParentalRelationshipOptionLkp.sql


/* Teds Module*/
:r .\CodeTableInitialization\TedsModule_ClientTransactionTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_DetailedNotInLaborForceLkp.sql
:r .\CodeTableInitialization\TedsModule_ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_UseFrequencyTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_LivingArrangementsTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_PriorTreatmentEpisodesCountTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_PrincipalReferralSourceTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_SubstanceProblemTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_SystemTransactionTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsDischargeReasonLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsEmploymentStatusLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsEthnicityLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsNonResponseLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsRaceLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsServiceTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsGenderLkp.sql
:r .\CodeTableInitialization\TedsModule_UsualAdministrationRouteTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_DetailedCriminalJusticeReferralLkp.sql
:r .\CodeTableInitialization\TedsModule_DetailedDrugCodeLkp.sql
:r .\CodeTableInitialization\TedsModule_HealthInsuranceTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_PrimaryPaymentSourceTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_IncomeSourceTypeLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsMaritalStatusLkp.sql
:r .\CodeTableInitialization\TedsModule_TedsBatchStatusLkp.sql

/*Billing Office Module*/
:r .\CodeTableInitialization\BillingOfficeModule_BillingOfficeAddressTypeLkp.sql
:r .\CodeTableInitialization\BillingOfficeModule_BillingOfficePhoneTypeLkp.sql

/*
 * Billing Module
*/
:r .\CodeTableInitialization\ClaimModule_ClaimBatchStatusLkp.sql

/*Payor Module*/
:r .\CodeTableInitialization\PayorModule_PayorAddressTypeLkp.sql
:r .\CodeTableInitialization\PayorModule_PayorPhoneTypeLkp.sql

/*Patient Account Module*/
:r .\CodeTableInitialization\PatientAccountModule_PatientAccountPhoneTypeLkp.sql
:r .\CodeTableInitialization\PatientAccountModule_PayorCoverageTypeLkp.sql
:r .\CodeTableInitialization\PatientAccountModule_PayorSubscriberRelationshipTypeLkp.sql

/*
** Initialize all persona data.
*/
:r .\PersonaData.sql
:r .\C32QualityOfMeasure.sql


/* Health Care claim 837 professional test data*/
:r .\Hcc837pInitialization.sql

/*
** End Persona Data
*/
