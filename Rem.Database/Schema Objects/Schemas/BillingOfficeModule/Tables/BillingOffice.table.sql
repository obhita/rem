CREATE TABLE [BillingOfficeModule].[BillingOffice] (
    [BillingOfficeKey]                          BIGINT             NOT NULL,
    [Version]                                   INT                NOT NULL,
    [ElectronicTransmitterIdentificationNumber] NVARCHAR (20)      NOT NULL,
    [CreatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [AdministratorStaffKey]                     BIGINT             NOT NULL,
    [AgencyKey]                                 BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [Name]                                      NVARCHAR (100)     NULL,
    [EffectiveEndDate]                          DATE               NULL,
    [EffectiveStartDate]                        DATE               NULL,
    [EmailAddress]                              NVARCHAR (255)     NULL,
    PRIMARY KEY CLUSTERED ([BillingOfficeKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
    UNIQUE NONCLUSTERED ([AgencyKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
);







