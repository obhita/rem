CREATE TABLE [PatientModule].[PayorCache] (
    [PayorCacheKey]             BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [Name]                      NVARCHAR (100)     NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [BillingOfficeCacheKey]     BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [FirstStreetAddress]        NVARCHAR (255)     NULL,
    [SecondStreetAddress]       NVARCHAR (255)     NULL,
    [CityName]                  NVARCHAR (100)     NULL,
    [CountyAreaLkpKey]          BIGINT             NULL,
    [StateProvinceLkpKey]       BIGINT             NULL,
    [CountryLkpKey]             BIGINT             NULL,
    [PostalCode]                NVARCHAR (10)      NULL,
    PRIMARY KEY CLUSTERED ([PayorCacheKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





