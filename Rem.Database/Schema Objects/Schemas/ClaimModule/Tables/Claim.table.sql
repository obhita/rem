CREATE TABLE [ClaimModule].[Claim] (
    [ClaimKey]                   BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [ServiceDate]                DATE               NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [EncounterKey]               BIGINT             NOT NULL,
    [PayorKey]                   BIGINT             NOT NULL,
    [ClaimBatchKey]              BIGINT             NULL,
    [PatientAccountKey]          BIGINT             NOT NULL,
    [ServiceLocationKey]         BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [ChargeAmountAmount]         DECIMAL (19, 5)    NOT NULL,
    [ChargeAmountCurrencyLkpKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([ClaimKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









