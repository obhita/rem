CREATE TABLE [VisitModule].[CodingContext] (
    [CodingContextKey]          BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [CodedDate]                 DATE               NOT NULL,
    [CodingStatusEnum]          NVARCHAR (50)      NOT NULL,
    [ErrorNote]                 NVARCHAR (MAX)     NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [CodedByStaffKey]           BIGINT             NULL,
    [VisitKey]                  BIGINT             NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([CodingContextKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



