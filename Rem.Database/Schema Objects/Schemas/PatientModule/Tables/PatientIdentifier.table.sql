CREATE TABLE [PatientModule].[PatientIdentifier] (
    [PatientIdentifierKey]        BIGINT             NOT NULL,
    [Version]                     INT                NOT NULL,
    [Identifier]                  NVARCHAR (20)      NOT NULL,
    [Description]                 NVARCHAR (500)     NULL,
    [ActiveIndicator]             BIT                NULL,
    [CreatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [PatientIdentifierTypeLkpKey] BIGINT             NOT NULL,
    [PatientContactKey]           BIGINT             NULL,
    [PatientKey]                  BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]   BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]   BIGINT             NOT NULL,
    [EffectiveStartDate]          DATE               NULL,
    [EffectiveEndDate]            DATE               NULL,
    PRIMARY KEY CLUSTERED ([PatientIdentifierKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











