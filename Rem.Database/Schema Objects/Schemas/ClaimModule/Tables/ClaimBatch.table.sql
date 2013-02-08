CREATE TABLE [ClaimModule].[ClaimBatch] (
    [ClaimBatchKey]              BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [PayorTypeKey]               BIGINT             NOT NULL,
    [ClaimBatchStatusLkpKey]     BIGINT             NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [ChargeAmountAmount]         DECIMAL (19, 5)    NOT NULL,
    [ChargeAmountCurrencyLkpKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([ClaimBatchKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









