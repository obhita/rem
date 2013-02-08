CREATE TABLE [PatientModule].[PatientPhone] (
    [PatientPhoneKey]           BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [PhoneNumber]               NVARCHAR (20)      NOT NULL,
    [PhoneExtensionNumber]      NVARCHAR (20)      NULL,
    [ConfidentialIndicator]     BIT                NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [PatientPhoneTypeLkpKey]    BIGINT             NOT NULL,
    [PatientKey]                BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientPhoneKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







