CREATE TABLE [TedsModule].[TedsDischargeInterviewSubstanceUsage] (
    [TedsDischargeInterviewSubstanceUsageKey]   BIGINT             NOT NULL,
    [Version]                                   INT                NOT NULL,
    [CreatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [TedsDischargeInterviewKey]                 BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [SubstanceProblemTypeLkpKey]                BIGINT             NULL,
    [SubstanceProblemTypeTedsNonResponseLkpKey] BIGINT             NULL,
    [UseFrequencyTypeLkpKey]                    BIGINT             NULL,
    [UseFrequencyTypeTedsNonResponseLkpKey]     BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([TedsDischargeInterviewSubstanceUsageKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







