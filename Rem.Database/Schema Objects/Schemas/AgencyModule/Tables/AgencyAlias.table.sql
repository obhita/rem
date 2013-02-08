CREATE TABLE [AgencyModule].[AgencyAlias] (
    [AgencyAliasKey]            BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [Name]                      NVARCHAR (100)     NOT NULL,
    [Note]                      NVARCHAR (MAX)     NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [AgencyKey]                 BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([AgencyAliasKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







