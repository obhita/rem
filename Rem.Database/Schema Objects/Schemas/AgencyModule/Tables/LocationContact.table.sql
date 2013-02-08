CREATE TABLE [AgencyModule].[LocationContact] (
    [LocationContactKey]          BIGINT             NOT NULL,
    [Version]                     INT                NOT NULL,
    [AlternativeContactIndicator] BIT                NOT NULL,
    [StatusIndicator]             BIT                NOT NULL,
    [CreatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [LocationContactTypeLkpKey]   BIGINT             NOT NULL,
    [ContactStaffKey]             BIGINT             NOT NULL,
    [LocationKey]                 BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]   BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]   BIGINT             NOT NULL,
    [EffectiveStartDate]          DATE               NULL,
    [EffectiveEndDate]            DATE               NULL,
    PRIMARY KEY CLUSTERED ([LocationContactKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









