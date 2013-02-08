CREATE TABLE [SecurityModule].[SystemUsageEvent] (
    [SystemUsageEventKey]       BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [UsageTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [IpAddress]                 NVARCHAR (255)     NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [SystemAccountKey]          BIGINT             NOT NULL,
    [EventTypeLkpKey]           BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([SystemUsageEventKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







