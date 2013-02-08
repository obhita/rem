CREATE TABLE [AgencyModule].[Location] (
    [LocationKey]                BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [AgencyKey]                  BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [WebsiteUrlName]             NVARCHAR (100)     NULL,
    [CountyAreaLkpKey]           BIGINT             NULL,
    [GeographicalRegionLkpKey]   BIGINT             NULL,
    [HipaaServiceLocationLkpKey] BIGINT             NULL,
    [Name]                       NVARCHAR (100)     NOT NULL,
    [DisplayName]                NVARCHAR (100)     NULL,
    [EffectiveStartDate]         DATE               NULL,
    [EffectiveEndDate]           DATE               NULL,
    PRIMARY KEY CLUSTERED ([LocationKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











