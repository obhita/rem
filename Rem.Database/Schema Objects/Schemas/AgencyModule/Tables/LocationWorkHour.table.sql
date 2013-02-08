CREATE TABLE [AgencyModule].[LocationWorkHour] (
    [LocationWorkHourKey]          BIGINT             NOT NULL,
    [Version]                      INT                NOT NULL,
    [DayOfWeekEnum]                NVARCHAR (50)      NOT NULL,
    [CreatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [LocationOperationScheduleKey] BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]    BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]    BIGINT             NOT NULL,
    [WorkHourStartTime]            TIME (7)           NULL,
    [WorkHourEndTime]              TIME (7)           NULL,
    PRIMARY KEY CLUSTERED ([LocationWorkHourKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



















