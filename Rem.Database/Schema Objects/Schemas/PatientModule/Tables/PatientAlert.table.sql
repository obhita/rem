CREATE TABLE [PatientModule].[PatientAlert] (
    [PatientAlertKey]           BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [Name]                      NVARCHAR (100)     NOT NULL,
    [Note]                      NVARCHAR (MAX)     NOT NULL,
    [CdsIdentifier]             NVARCHAR (20)      NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientAlertKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



