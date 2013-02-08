CREATE TABLE [PayorModule].[Payor] (
    [PayorKey]                                  BIGINT             NOT NULL,
    [Version]                                   INT                NOT NULL,
    [ElectronicTransmitterIdentificationNumber] NVARCHAR (20)      NOT NULL,
    [Name]                                      NVARCHAR (100)     NOT NULL,
    [PayorIdentifier]                           NVARCHAR (20)      NULL,
    [WebsiteAddress]                            NVARCHAR (255)     NULL,
    [CreatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [BillingOfficeKey]                          BIGINT             NOT NULL,
    [PrimaryPayorTypeMemberKey]                 BIGINT             NULL,
    [CreatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [EffectiveEndDate]                          DATE               NULL,
    [EffectiveStartDate]                        DATE               NULL,
    [EmailAddress]                              NVARCHAR (255)     NULL,
    PRIMARY KEY CLUSTERED ([PayorKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







