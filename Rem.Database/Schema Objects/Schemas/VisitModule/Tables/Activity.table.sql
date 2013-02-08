CREATE TABLE [VisitModule].[Activity] (
    [ActivityKey]               BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [VisitKey]                  BIGINT             NULL,
    [ClinicalCaseKey]           BIGINT             NOT NULL,
    [ActivityTypeLkpKey]        BIGINT             NOT NULL,
    [ProvenanceKey]             BIGINT             NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [ActivityEndDateTime]       DATETIME           NOT NULL,
    [ActivityStartDateTime]     DATETIME           NOT NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

















