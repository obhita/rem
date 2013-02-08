CREATE TABLE [PayorModule].[PayorType] (
    [PayorTypeKey]                BIGINT             NOT NULL,
    [Version]                     INT                NOT NULL,
    [Name]                        NVARCHAR (100)     NOT NULL,
    [BillingFormEnum]             NVARCHAR (50)      NOT NULL,
    [SubmitterIdentifier]         NVARCHAR (20)      NULL,
    [CreatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]            DATETIMEOFFSET (7) NOT NULL,
    [BillingOfficeKey]            BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]   BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]   BIGINT             NOT NULL,
    [InterchangeReceiverNumber]   NVARCHAR (20)      NULL,
    [InterchangeSenderNumber]     NVARCHAR (20)      NULL,
    [CompositeDelimiter]          NCHAR (1)          NULL,
    [ElementDelimiter]            NCHAR (1)          NULL,
    [SegmentDelimiter]            NCHAR (1)          NULL,
    [RepetitionDelimiter]         NCHAR (1)          NULL,
    [BillingFirstStreetAddress]   NVARCHAR (255)     NULL,
    [BillingSecondStreetAddress]  NVARCHAR (255)     NULL,
    [BillingCityName]             NVARCHAR (100)     NULL,
    [BillingCountyAreaLkpKey]     BIGINT             NULL,
    [BillingStateProvinceLkpKey]  BIGINT             NULL,
    [BillingCountryLkpKey]        BIGINT             NULL,
    [PostalCode]                  NVARCHAR (10)      NULL,
    [BillingPhoneNumber]          NVARCHAR (20)      NULL,
    [BillingPhoneExtensionNumber] NVARCHAR (20)      NULL,
    [FtpHostValue]                NVARCHAR (255)     NULL,
    [FtpUserName]                 NVARCHAR (100)     NULL,
    [FtpPassCode]                 NVARCHAR (10)      NULL,
    PRIMARY KEY CLUSTERED ([PayorTypeKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









