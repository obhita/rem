CREATE TABLE [ProgramModule].[ProgramEnrollment] (
    [ProgramEnrollmentKey]      BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [EnrollmentDate]            DATE               NOT NULL,
    [DisenrollmentDate]         DATE               NULL,
    [CommentsNote]              NVARCHAR (MAX)     NULL,
    [DisenrollOtherReasonNote]  NVARCHAR (MAX)     NULL,
    [DaysOnWaitingListCount]    INT                NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [ProgramOfferingKey]        BIGINT             NOT NULL,
    [ClinicalCaseKey]           BIGINT             NOT NULL,
    [EnrollingStaffKey]         BIGINT             NOT NULL,
    [DisenrollReasonLkpKey]     BIGINT             NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([ProgramEnrollmentKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



