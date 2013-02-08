CREATE TABLE [TedsModule].[TedsAdmissionInterview] (
    [ActivityKey]                                                        BIGINT        NOT NULL,
    [CoDependentIndicator]                                               BIT           NULL,
    [PrimaryTedsAdmissionInterviewSubstanceUsageKey]                     BIGINT        NULL,
    [SecondaryTedsAdmissionInterviewSubstanceUsageKey]                   BIGINT        NULL,
    [TertiaryTedsAdmissionInterviewSubstanceUsageKey]                    BIGINT        NULL,
    [PriorTreatmentEpisodesCount]                                        INT           NULL,
    [PriorTreatmentEpisodesCountTedsNonResponseLkpKey]                   BIGINT        NULL,
    [TedsGenderLkpKey]                                                   BIGINT        NULL,
    [TedsGenderTedsNonResponseLkpKey]                                    BIGINT        NULL,
    [PregnantIndicator]                                                  BIT           NULL,
    [PregnantIndicatorTedsNonResponseLkpKey]                             BIGINT        NULL,
    [TedsRaceLkpKey]                                                     BIGINT        NULL,
    [TedsRaceTedsNonResponseLkpKey]                                      BIGINT        NULL,
    [TedsEthnicityLkpKey]                                                BIGINT        NULL,
    [TedsEthnicityTedsNonResponseLkpKey]                                 BIGINT        NULL,
    [TedsEducationYearCount]                                             INT           NULL,
    [TedsEducationYearCountTedsNonResponseLkpKey]                        BIGINT        NULL,
    [TedsEmploymentStatusLkpKey]                                         BIGINT        NULL,
    [TedsEmploymentStatusTedsNonResponseLkpKey]                          BIGINT        NULL,
    [DetailedNotInLaborForceLkpKey]                                      BIGINT        NULL,
    [DetailedNotInLaborForceTedsNonResponseLkpKey]                       BIGINT        NULL,
    [MedicationAssistedOpioidTherapyIndicator]                           BIT           NULL,
    [MedicationAssistedOpioidTherapyIndicatorTedsNonResponseLkpKey]      BIGINT        NULL,
    [DsmDiagnosisTedsNonResponseLkpKey]                                  BIGINT        NULL,
    [DsmDiagnosisResponseCode]                                           NVARCHAR (10) NULL,
    [OtherPsychiatricProblemIndicator]                                   BIT           NULL,
    [OtherPsychiatricProblemIndicatorTedsNonResponseLkpKey]              BIGINT        NULL,
    [VeteranStatusIndicator]                                             BIT           NULL,
    [VeteranStatusIndicatorTedsNonResponseLkpKey]                        BIGINT        NULL,
    [LivingArrangementsTypeLkpKey]                                       BIGINT        NULL,
    [LivingArrangementsTypeTedsNonResponseLkpKey]                        BIGINT        NULL,
    [IncomeSourceTypeLkpKey]                                             BIGINT        NULL,
    [IncomeSourceTypeTedsNonResponseLkpKey]                              BIGINT        NULL,
    [PrimaryPaymentSourceTypeLkpKey]                                     BIGINT        NULL,
    [PrimaryPaymentSourceTypeTedsNonResponseLkpKey]                      BIGINT        NULL,
    [MaritalStatusLkpKey]                                                BIGINT        NULL,
    [MaritalStatusTedsNonResponseLkpKey]                                 BIGINT        NULL,
    [ArrestsInPastThirtyDaysCount]                                       INT           NULL,
    [ArrestsInPastThirtyDaysCountTedsNonResponseLkpKey]                  BIGINT        NULL,
    [ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkpKey]                BIGINT        NULL,
    [ParticipatedSelfHelpGroupInPastThirtyDaysTypeTedsNonResponseLkpKey] BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);































