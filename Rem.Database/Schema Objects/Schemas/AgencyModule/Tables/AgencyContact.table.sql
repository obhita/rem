CREATE TABLE [AgencyModule].[AgencyContact] (
    [AgencyContactKey]            BIGINT             NOT NULL,
    [Version]                     INT                NOT NULL,
    [EffectiveStartDate]          DATE               NULL,
    [StatusIndicator]             BIT                NOT NULL,
    [AlternativeContactIndicator] BIT                NOT NULL,
    [CreatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [AgencyContactTypeLkpKey]     BIGINT             NOT NULL,
    [ContactStaffKey]             BIGINT             NOT NULL,
    [AgencyKey]                   BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]   BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]   BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([AgencyContactKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







