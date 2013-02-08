CREATE TABLE [TedsModule].[TedsDischargeInterview] (
    [ActivityKey]                                                        BIGINT NOT NULL,
    [LastFaceToFaceContactDate]                                          DATE   NULL,
    [TedsAdmissionInterviewKey]                                          BIGINT NULL,
    [PrimaryTedsDischargeInterviewSubstanceUsageKey]                     BIGINT NULL,
    [SecondaryTedsDischargeInterviewSubstanceUsageKey]                   BIGINT NULL,
    [TertiaryTedsDischargeInterviewSubstanceUsageKey]                    BIGINT NULL,
    [TedsDischargeReasonLkpKey]                                          BIGINT NULL,
    [TedsDischargeReasonTedsNonResponseLkpKey]                           BIGINT NULL,
    [LivingArrangementsTypeLkpKey]                                       BIGINT NULL,
    [LivingArrangementsTypeTedsNonResponseLkpKey]                        BIGINT NULL,
    [TedsEmploymentStatusLkpKey]                                         BIGINT NULL,
    [TedsEmploymentStatusTedsNonResponseLkpKey]                          BIGINT NULL,
    [DetailedNotInLaborForceLkpKey]                                      BIGINT NULL,
    [DetailedNotInLaborForceTedsNonResponseLkpKey]                       BIGINT NULL,
    [ArrestsInPastThirtyDaysCount]                                       INT    NULL,
    [ArrestsInPastThirtyDaysCountTedsNonResponseLkpKey]                  BIGINT NULL,
    [ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkpKey]                BIGINT NULL,
    [ParticipatedSelfHelpGroupInPastThirtyDaysTypeTedsNonResponseLkpKey] BIGINT NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





























