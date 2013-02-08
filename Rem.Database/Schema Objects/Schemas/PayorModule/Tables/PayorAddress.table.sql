CREATE TABLE [PayorModule].[PayorAddress] (
    [PayorAddressKey]           BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [PayorKey]                  BIGINT             NOT NULL,
    [PayorAddressTypeLkpKey]    BIGINT             NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [FirstStreetAddress]        NVARCHAR (255)     NULL,
    [SecondStreetAddress]       NVARCHAR (255)     NULL,
    [CityName]                  NVARCHAR (100)     NULL,
    [CountyAreaLkpKey]          BIGINT             NULL,
    [StateProvinceLkpKey]       BIGINT             NULL,
    [CountryLkpKey]             BIGINT             NULL,
    [PostalCode]                NVARCHAR (10)      NULL,
    PRIMARY KEY CLUSTERED ([PayorAddressKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



