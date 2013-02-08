CREATE TABLE [AgencyModule].[StaffTrainingCourse] (
    [StaffTrainingCourseKey]    BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [OtherTrainingCourseName]   NVARCHAR (100)     NULL,
    [CompletedDate]             DATE               NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [TrainingCourseLkpKey]      BIGINT             NOT NULL,
    [StaffKey]                  BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([StaffTrainingCourseKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







