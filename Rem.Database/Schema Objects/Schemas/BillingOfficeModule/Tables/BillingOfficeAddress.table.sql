CREATE TABLE [BillingOfficeModule].[BillingOfficeAddress] (
    [BillingOfficeAddressKey]        BIGINT             NOT NULL,
    [Version]                        INT                NOT NULL,
    [CreatedTimestamp]               DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]               DATETIMEOFFSET (7) NOT NULL,
    [BillingOfficeAddressTypeLkpKey] BIGINT             NULL,
    [BillingOfficeKey]               BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]      BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]      BIGINT             NOT NULL,
    [FirstStreetAddress]             NVARCHAR (255)     NULL,
    [SecondStreetAddress]            NVARCHAR (255)     NULL,
    [CityName]                       NVARCHAR (100)     NULL,
    [CountyAreaLkpKey]               BIGINT             NULL,
    [StateProvinceLkpKey]            BIGINT             NULL,
    [CountryLkpKey]                  BIGINT             NULL,
    [PostalCode]                     NVARCHAR (10)      NULL,
    PRIMARY KEY CLUSTERED ([BillingOfficeAddressKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

