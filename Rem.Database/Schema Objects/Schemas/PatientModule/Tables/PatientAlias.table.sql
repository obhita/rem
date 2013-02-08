CREATE TABLE [PatientModule].[PatientAlias] (
    [PatientAliasKey]           BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [FirstName]                 NVARCHAR (100)     NULL,
    [MiddleName]                NVARCHAR (100)     NULL,
    [LastName]                  NVARCHAR (100)     NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [PatientAliasTypeLkpKey]    BIGINT             NOT NULL,
    [PatientKey]                BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientAliasKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







