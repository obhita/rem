CREATE TABLE [TedsModule].[TedsBatch] (
    [TedsBatchKey]              BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [ReportDate]                DATE               NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [AgencyKey]                 BIGINT             NULL,
    [ExtractedStaffKey]         BIGINT             NULL,
    [TedsBatchStatusLkpKey]     BIGINT             NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [SubmissionEndDate]         DATE               NULL,
    [SubmissionStartDate]       DATE               NULL,
    PRIMARY KEY CLUSTERED ([TedsBatchKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

