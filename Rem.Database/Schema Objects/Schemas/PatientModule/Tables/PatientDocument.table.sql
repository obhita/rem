CREATE TABLE [PatientModule].[PatientDocument] (
    [PatientDocumentKey]        BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [FileName]                  NVARCHAR (100)     NOT NULL,
    [Document]                  VARBINARY (MAX)    NOT NULL,
    [DocumentProviderName]      NVARCHAR (100)     NULL,
    [Description]               NVARCHAR (500)     NULL,
    [OtherDocumentTypeName]     NVARCHAR (100)     NULL,
    [DocumentHashValue]         NVARCHAR (255)     NOT NULL,
    [C32ImportedIndicator]      BIT                NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                BIGINT             NOT NULL,
    [PatientDocumentTypeLkpKey] BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [ClinicalEndDate]           DATE               NULL,
    [ClinicalStartDate]         DATE               NULL,
    PRIMARY KEY CLUSTERED ([PatientDocumentKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

















