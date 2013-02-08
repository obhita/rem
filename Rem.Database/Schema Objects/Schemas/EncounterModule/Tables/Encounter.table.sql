CREATE TABLE [EncounterModule].[Encounter] (
    [EncounterKey]               BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [ServiceDate]                DATE               NOT NULL,
    [TrackingNumber]             BIGINT             NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [PatientAccountKey]          BIGINT             NOT NULL,
    [ServiceLocationKey]         BIGINT             NOT NULL,
    [ServiceProviderStaffKey]    BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [ChargeAmountAmount]         DECIMAL (19, 5)    NULL,
    [ChargeAmountCurrencyLkpKey] BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([EncounterKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







