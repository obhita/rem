CREATE TABLE [PatientModule].[SelfPayment] (
    [SelfPaymentKey]            BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [CollectedDate]             DATE               NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                BIGINT             NOT NULL,
    [CollectedByStaffKey]       BIGINT             NOT NULL,
    [PaymentMethodLkpKey]       BIGINT             NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    [Amount]                    DECIMAL (19, 5)    NULL,
    [CurrencyLkpKey]            BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([SelfPaymentKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







