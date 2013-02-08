CREATE TABLE [DensAsiModule].[DensAsiClosure] (
    [DensAsiInterviewKey]                                      BIGINT             NOT NULL,
    [CreatedTimestamp]                                         DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                         DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                                BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                                BIGINT             NOT NULL,
    [MostAppropriateDensAsiTreatmentModalityNote]              NVARCHAR (MAX)     NULL,
    [DensAsiIncompleteInterviewReasonNote]                     NVARCHAR (MAX)     NULL,
    [MostAppropriateDensAsiTreatmentModalityLkpKey]            BIGINT             NULL,
    [DensAsiIncompleteInterviewReasonLkpKey]                   BIGINT             NULL,
    [DensAsiIncompleteInterviewReasonDensAsiNonResponseLkpKey] BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([DensAsiInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







