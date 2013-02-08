CREATE TABLE [SbirtModule].[NidaDrugQuestionnaire] (
    [ActivityKey]                        BIGINT         NOT NULL,
    [NidaDrugQuestionnaireIndicator]     BIT            NULL,
    [CannabisUseAnswerNumber]            INT            NULL,
    [CocaineUseAnswerNumber]             INT            NULL,
    [OpioidsUseAnswerNumber]             INT            NULL,
    [MethamphetamineUseAnswerNumber]     INT            NULL,
    [SedativesUseAnswerNumber]           INT            NULL,
    [OtherDrug1DrugTypeName]             NVARCHAR (100) NULL,
    [OtherDrug1AnswerNumber]             INT            NULL,
    [OtherDrug2DrugTypeName]             NVARCHAR (100) NULL,
    [OtherDrug2AnswerNumber]             INT            NULL,
    [OtherDrug3DrugTypeName]             NVARCHAR (100) NULL,
    [OtherDrug3AnswerNumber]             INT            NULL,
    [DrugUseByInjectionIndicator]        BIT            NULL,
    [LastDrugUseByInjectionAnswerNumber] INT            NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





