CREATE TABLE [AgencyModule].[LocationEmailAddress] (
    [LocationEmailAddressKey]        BIGINT             NOT NULL,
    [Version]                        INT                NOT NULL,
    [CreatedTimestamp]               DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]               DATETIMEOFFSET (7) NOT NULL,
    [LocationEmailAddressTypeLkpKey] BIGINT             NOT NULL,
    [LocationKey]                    BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]      BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]      BIGINT             NOT NULL,
    [EmailAddress]                   NVARCHAR (255)     NULL,
    PRIMARY KEY CLUSTERED ([LocationEmailAddressKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











