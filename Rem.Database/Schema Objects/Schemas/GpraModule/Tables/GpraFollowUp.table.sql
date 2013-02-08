CREATE TABLE [GpraModule].[GpraFollowUp] (
    [GpraInterviewKey]                   BIGINT             NOT NULL,
    [CreatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]          BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]          BIGINT             NOT NULL,
    [GpraFollowUpStatusOtherDescription] NVARCHAR (500)     NULL,
    [PatientReceivingServicesIndicator]  BIT                NULL,
    [GpraFollowUpStatusLkpKey]           BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([GpraInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





