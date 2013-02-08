CREATE TABLE [GpraModule].[GpraCrimeCriminalJustice] (
    [GpraInterviewKey]                              BIGINT             NOT NULL,
    [CreatedTimestamp]                              DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                              DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                     BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                     BIGINT             NOT NULL,
    [ArrestedCount]                                 INT                NULL,
    [ArrestedCountGpraNonResponseLkpKey]            BIGINT             NULL,
    [ArrestedDrugCount]                             INT                NULL,
    [ArrestedDrugCountGpraNonResponseLkpKey]        BIGINT             NULL,
    [NightsConfinedCount]                           INT                NULL,
    [NightsConfinedCountGpraNonResponseLkpKey]      BIGINT             NULL,
    [CrimeCount]                                    INT                NULL,
    [CrimeCountGpraNonResponseLkpKey]               BIGINT             NULL,
    [AwaitingTrialIndicator]                        BIT                NULL,
    [AwaitingTrialIndicatorGpraNonResponseLkpKey]   BIGINT             NULL,
    [ParoleProbationIndicator]                      BIT                NULL,
    [ParoleProbationIndicatorGpraNonResponseLkpKey] BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([GpraInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







