CREATE TABLE [AgencyModule].[Appointment] (
    [AppointmentKey]            BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [SubjectDescription]        NVARCHAR (500)     NULL,
    [Note]                      NVARCHAR (MAX)     NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [StaffKey]                  BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [AppointmentStartDateTime]  DATETIME           NOT NULL,
    [AppointmentEndDateTime]    DATETIME           NOT NULL,
    PRIMARY KEY CLUSTERED ([AppointmentKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

















