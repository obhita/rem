﻿CREATE TABLE [PatientAccountModule].[PatientAccountPhone] (
    [PatientAccountPhoneKey]        BIGINT             NOT NULL,
    [Version]                       INT                NOT NULL,
    [CreatedTimestamp]              DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]              DATETIMEOFFSET (7) NOT NULL,
    [PatientAccountKey]             BIGINT             NOT NULL,
    [PatientAccountPhoneTypeLkpKey] BIGINT             NULL,
    [CreatedBySystemAccountKey]     BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]     BIGINT             NOT NULL,
    [PhoneNumber]                   NVARCHAR (20)      NULL,
    [PhoneExtensionNumber]          NVARCHAR (20)      NULL,
    PRIMARY KEY CLUSTERED ([PatientAccountPhoneKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

