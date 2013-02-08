CREATE TABLE [TedsModule].[TedsDischargeRecordSubstanceUsage] (
    [TedsDischargeRecordSubstanceUsageKey]      BIGINT             NOT NULL,
    [Version]                                   INT                NOT NULL,
    [CreatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [TedsDischargeRecordKey]                    BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [SubstanceProblemTypeLkpKey]                BIGINT             NULL,
    [SubstanceProblemTypeTedsNonResponseLkpKey] BIGINT             NULL,
    [UseFrequencyTypeLkpKey]                    BIGINT             NULL,
    [UseFrequencyTypeTedsNonResponseLkpKey]     BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([TedsDischargeRecordSubstanceUsageKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

