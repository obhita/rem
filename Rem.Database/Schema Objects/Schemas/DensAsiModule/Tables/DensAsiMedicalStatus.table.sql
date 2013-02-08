CREATE TABLE [DensAsiModule].[DensAsiMedicalStatus] (
    [DensAsiInterviewKey]                                                                      BIGINT             NOT NULL,
    [CreatedTimestamp]                                                                         DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                                                         DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                                                                BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                                                                BIGINT             NOT NULL,
    [HopitalizedForMedicalProblemsCountNote]                                                   NVARCHAR (MAX)     NULL,
    [YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote]                     NVARCHAR (MAX)     NULL,
    [ChronicMedicalProblemThatInterferesWithLifeDescription]                                   NVARCHAR (500)     NULL,
    [ChronicMedicalProblemThatInterferesWithLifeNote]                                          NVARCHAR (MAX)     NULL,
    [TakingPrescribedMedicationsForPhysicalProblemDescription]                                 NVARCHAR (500)     NULL,
    [TakingPrescribedMedicationsForPhysicalProblemNote]                                        NVARCHAR (MAX)     NULL,
    [ReceivePensionForPhysicalDisabilityDescription]                                           NVARCHAR (500)     NULL,
    [ReceivePensionForPhysicalDisabilityNote]                                                  NVARCHAR (MAX)     NULL,
    [MedicalProblemsDayCountNote]                                                              NVARCHAR (MAX)     NULL,
    [TroubledByMedicalProblemsDensAsiPatientRatingNote]                                        NVARCHAR (MAX)     NULL,
    [ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingNote]                              NVARCHAR (MAX)     NULL,
    [PatientTreatmentDensAsiInterviewerRatingNote]                                             NVARCHAR (MAX)     NULL,
    [ConfidenceRateDistortedByPatientMisrepresentationIndicator]                               BIT                NULL,
    [ConfidenceRateDistortedByPatientMisrepresentationIndicatorNote]                           NVARCHAR (MAX)     NULL,
    [ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator]                           BIT                NULL,
    [ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote]                       NVARCHAR (MAX)     NULL,
    [SectionNote]                                                                              NVARCHAR (MAX)     NULL,
    [PatientTreatmentDensAsiInterviewerRatingLkpKey]                                           BIGINT             NULL,
    [HopitalizedForMedicalProblemsCount]                                                       INT                NULL,
    [HopitalizedForMedicalProblemsCountDensAsiNonResponseLkpKey]                               BIGINT             NULL,
    [YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan]                         BIGINT             NULL,
    [YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanDensAsiNonResponseLkpKey] BIGINT             NULL,
    [ChronicMedicalProblemThatInterferesWithLifeIndicator]                                     BIT                NULL,
    [ChronicMedicalProblemThatInterferesWithLifeIndicatorDensAsiNonResponseLkpKey]             BIGINT             NULL,
    [TakingPrescribedMedicationsForPhysicalProblemIndicator]                                   BIT                NULL,
    [TakingPrescribedMedicationsForPhysicalProblemIndicatorDensAsiNonResponseLkpKey]           BIGINT             NULL,
    [ReceivePensionForPhysicalDisabilityIndicator]                                             BIT                NULL,
    [ReceivePensionForPhysicalDisabilityIndicatorDensAsiNonResponseLkpKey]                     BIGINT             NULL,
    [MedicalProblemsDayCount]                                                                  INT                NULL,
    [MedicalProblemsDayCountDensAsiNonResponseLkpKey]                                          BIGINT             NULL,
    [TroubledByMedicalProblemsDensAsiPatientRatingLkpKey]                                      BIGINT             NULL,
    [TroubledByMedicalProblemsDensAsiPatientRatingDensAsiNonResponseLkpKey]                    BIGINT             NULL,
    [ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingLkpKey]                            BIGINT             NULL,
    [ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingDensAsiNonResponseLkpKey]          BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([DensAsiInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









