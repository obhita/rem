CREATE TABLE [PatientModule].[PatientPhoto] (
    [PatientPhotoKey]           BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [Picture]                   VARBINARY (MAX)    NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [PatientPhotoTypeLkpKey]    BIGINT             NOT NULL,
    [PatientKey]                BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientPhotoKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

















