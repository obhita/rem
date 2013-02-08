﻿CREATE TABLE [AgencyModule].[LocationPhone] (
    [LocationPhoneKey]           BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [LocationAddressAndPhoneKey] BIGINT             NOT NULL,
    [LocationPhoneTypeLkpKey]    BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [PhoneNumber]                NVARCHAR (20)      NOT NULL,
    [PhoneExtensionNumber]       NVARCHAR (20)      NULL,
    PRIMARY KEY CLUSTERED ([LocationPhoneKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



