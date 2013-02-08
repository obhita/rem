CREATE TABLE [AgencyModule].[StaffAddress] (
    [StaffAddressKey]           BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [ConfidentialIndicator]     BIT                NULL,
    [YearsOfStayNumber]         INT                NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [StaffAddressTypeLkpKey]    BIGINT             NOT NULL,
    [StaffKey]                  BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [FirstStreetAddress]        NVARCHAR (255)     NOT NULL,
    [SecondStreetAddress]       NVARCHAR (255)     NULL,
    [CityName]                  NVARCHAR (100)     NOT NULL,
    [CountyAreaLkpKey]          BIGINT             NULL,
    [StateProvinceLkpKey]       BIGINT             NOT NULL,
    [CountryLkpKey]             BIGINT             NULL,
    [PostalCode]                NVARCHAR (10)      NULL,
    PRIMARY KEY CLUSTERED ([StaffAddressKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











