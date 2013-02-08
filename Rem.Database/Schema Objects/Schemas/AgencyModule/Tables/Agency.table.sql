CREATE TABLE [AgencyModule].[Agency] (
    [AgencyKey]                 BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [LegalName]                 NVARCHAR (100)     NOT NULL,
    [DoingBusinessAsName]       NVARCHAR (100)     NULL,
    [DisplayName]               NVARCHAR (100)     NULL,
    [WebsiteUrlName]            NVARCHAR (100)     NULL,
    [Note]                      NVARCHAR (MAX)     NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [AgencyTypeLkpKey]          BIGINT             NOT NULL,
    [GeographicalRegionLkpKey]  BIGINT             NULL,
    [ParentAgencyKey]           BIGINT             NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [EffectiveStartDate]        DATE               NULL,
    [EffectiveEndDate]          DATE               NULL,
    PRIMARY KEY CLUSTERED ([AgencyKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











