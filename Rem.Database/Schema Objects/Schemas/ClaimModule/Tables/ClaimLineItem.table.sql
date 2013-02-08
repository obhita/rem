CREATE TABLE [ClaimModule].[ClaimLineItem] (
    [ClaimLineItemKey]                 BIGINT             NOT NULL,
    [Version]                          INT                NOT NULL,
    [CreatedTimestamp]                 DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                 DATETIMEOFFSET (7) NOT NULL,
    [ClaimKey]                         BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]        BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]        BIGINT             NOT NULL,
    [BillingCount]                     SMALLINT           NOT NULL,
    [ChargeAmountAmount]               DECIMAL (19, 5)    NOT NULL,
    [ChargeAmountCurrencyLkpKey]       BIGINT             NOT NULL,
    [DiagnosisCodedConceptCode]        NVARCHAR (10)      NOT NULL,
    [DiagnosisDisplayName]             NVARCHAR (100)     NULL,
    [DiagnosisCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [DiagnosisCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [DiagnosisCodeSystemName]          NVARCHAR (100)     NULL,
    [DiagnosisOriginalDescription]     NVARCHAR (500)     NULL,
    [DiagnosisNullFlavorIndicator]     BIT                NOT NULL,
    [ProcedureCodedConceptCode]        NVARCHAR (10)      NOT NULL,
    [ProcedureDisplayName]             NVARCHAR (100)     NULL,
    [ProcedureCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [ProcedureCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [ProcedureCodeSystemName]          NVARCHAR (100)     NULL,
    [ProcedureOriginalDescription]     NVARCHAR (500)     NULL,
    [ProcedureNullFlavorIndicator]     BIT                NOT NULL,
    [RatePerBillingUnitAmount]         DECIMAL (19, 5)    NULL,
    [RatePerBillingUnitCurrencyLkpKey] BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([ClaimLineItemKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









