CREATE TABLE [AgencyModule].[AgencyIdentifier] (
    [AgencyIdentifierKey]        BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [IdentifierNumber]           NVARCHAR (20)      NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [AgencyIdentifierTypeLkpKey] BIGINT             NOT NULL,
    [AgencyKey]                  BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [EffectiveStartDate]         DATE               NULL,
    [EffectiveEndDate]           DATE               NULL,
    PRIMARY KEY CLUSTERED ([AgencyIdentifierKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





