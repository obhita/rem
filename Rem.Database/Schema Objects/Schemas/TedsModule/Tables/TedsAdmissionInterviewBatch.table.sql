CREATE TABLE [TedsModule].[TedsAdmissionInterviewBatch] (
    [TedsAdmissionInterviewBatchKey] BIGINT             NOT NULL,
    [Version]                        INT                NOT NULL,
    [SubmissionData]                 VARBINARY (MAX)    NULL,
    [CreatedTimestamp]               DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]               DATETIMEOFFSET (7) NOT NULL,
    [AgencyKey]                      BIGINT             NOT NULL,
    [TedsInterviewBatchStatusLkpKey] BIGINT             NULL,
    [CreatedBySystemAccountKey]      BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]      BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([TedsAdmissionInterviewBatchKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

