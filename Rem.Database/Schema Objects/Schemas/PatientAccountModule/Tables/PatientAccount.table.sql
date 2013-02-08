CREATE TABLE [PatientAccountModule].[PatientAccount] (
    [PatientAccountKey]          BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [MedicalRecordNumber]        BIGINT             NOT NULL,
    [BirthDate]                  DATE               NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [BillingOfficeKey]           BIGINT             NOT NULL,
    [AdministrativeGenderLkpKey] BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [HomeFirstStreetAddress]     NVARCHAR (255)     NULL,
    [HomeSecondStreetAddress]    NVARCHAR (255)     NULL,
    [HomeCityName]               NVARCHAR (100)     NULL,
    [HomeCountyAreaLkpKey]       BIGINT             NULL,
    [HomeStateProvinceLkpKey]    BIGINT             NULL,
    [HomeCountryLkpKey]          BIGINT             NULL,
    [PostalCode]                 NVARCHAR (10)      NULL,
    [PrefixName]                 NVARCHAR (100)     NULL,
    [FirstName]                  NVARCHAR (100)     NOT NULL,
    [MiddleName]                 NVARCHAR (100)     NULL,
    [LastName]                   NVARCHAR (100)     NOT NULL,
    [SuffixName]                 NVARCHAR (100)     NULL,
    PRIMARY KEY CLUSTERED ([PatientAccountKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
    UNIQUE NONCLUSTERED ([MedicalRecordNumber] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
);











