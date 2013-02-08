CREATE TABLE [AgencyModule].[AgencyEmailAddress] (
    [AgencyEmailAddressKey]        BIGINT             NOT NULL,
    [Version]                      INT                NOT NULL,
    [CreatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [AgencyEmailAddressTypeLkpKey] BIGINT             NOT NULL,
    [AgencyKey]                    BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]    BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]    BIGINT             NOT NULL,
    [EmailAddress]                 NVARCHAR (255)     NULL,
    PRIMARY KEY CLUSTERED ([AgencyEmailAddressKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











