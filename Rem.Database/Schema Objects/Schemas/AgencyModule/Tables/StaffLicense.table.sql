CREATE TABLE [AgencyModule].[StaffLicense] (
    [StaffLicenseKey]           BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [LicenseLkpKey]             BIGINT             NOT NULL,
    [StaffKey]                  BIGINT             NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [EffectiveStartDate]        DATE               NULL,
    [EffectiveEndDate]          DATE               NULL,
    PRIMARY KEY CLUSTERED ([StaffLicenseKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





