CREATE TABLE [AgencyModule].[LocationAddressAndPhone] (
    [LocationAddressAndPhoneKey] BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [LocationKey]                BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [ConfidentialIndicator]      BIT                NOT NULL,
    [LocationAddressTypeLkpKey]  BIGINT             NOT NULL,
    [FirstStreetAddress]         NVARCHAR (255)     NOT NULL,
    [SecondStreetAddress]        NVARCHAR (255)     NULL,
    [CityName]                   NVARCHAR (100)     NOT NULL,
    [CountyAreaLkpKey]           BIGINT             NULL,
    [StateProvinceLkpKey]        BIGINT             NOT NULL,
    [CountryLkpKey]              BIGINT             NULL,
    [PostalCode]                 NVARCHAR (10)      NULL,
    PRIMARY KEY CLUSTERED ([LocationAddressAndPhoneKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



