﻿CREATE TABLE [TedsModule].[TedsNonResponseLkp] (
    [TedsNonResponseLkpKey]     BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [WellKnownName]             NVARCHAR (100)     NULL,
    [Name]                      NVARCHAR (100)     NOT NULL,
    [ShortName]                 NVARCHAR (100)     NULL,
    [Description]               NVARCHAR (500)     NULL,
    [SortOrderNumber]           INT                NULL,
    [SystemOwnedIndicator]      BIT                NOT NULL,
    [DefaultIndicator]          BIT                NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [EffectiveEndDate]          DATE               NULL,
    [EffectiveStartDate]        DATE               NULL,
    PRIMARY KEY CLUSTERED ([TedsNonResponseLkpKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

