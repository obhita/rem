CREATE TABLE [GpraModule].[GpraInterview] (
    [ActivityKey]                             BIGINT         NOT NULL,
    [SbirtSbiPositiveIndicator]               BIT            NULL,
    [SbirtWillingIndicator]                   BIT            NULL,
    [AuditCScore]                             INT            NULL,
    [CageScore]                               INT            NULL,
    [DastScore]                               INT            NULL,
    [Dast10Score]                             INT            NULL,
    [NiaaaGuideScore]                         INT            NULL,
    [AssistAlcoholSubScore]                   INT            NULL,
    [OtherScore]                              INT            NULL,
    [OtherSpecificationDescription]           NVARCHAR (500) NULL,
    [ContractGrantIdentifier]                 NVARCHAR (20)  NULL,
    [ConductedInterviewIndicator]             BIT            NULL,
    [CooccuringMhSaScreenerIndicator]         BIT            NULL,
    [PositiveCooccuringMhSaScreenerIndicator] BIT            NULL,
    [GpraPatientTypeLkpKey]                   BIGINT         NULL,
    [GpraInterviewTypeLkpKey]                 BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







