CREATE TABLE [PatientModule].[CdsRule] (
    [CdsRuleKey]                        BIGINT             NOT NULL,
    [Version]                           INT                NOT NULL,
    [Name]                              NVARCHAR (100)     NULL,
    [RecommendationNote]                NVARCHAR (MAX)     NULL,
    [Age]                               INT                NULL,
    [ValidLabOrderMonthCount]           INT                NULL,
    [CreatedTimestamp]                  DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                  DATETIMEOFFSET (7) NOT NULL,
    [LabTestNameLkpKey]                 BIGINT             NULL,
    [CreatedBySystemAccountKey]         BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]         BIGINT             NOT NULL,
    [MedicationCodedConceptCode]        NVARCHAR (10)      NULL,
    [MedicationDisplayName]             NVARCHAR (100)     NULL,
    [MedicationCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [MedicationCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [MedicationCodeSystemName]          NVARCHAR (100)     NULL,
    [MedicationOriginalDescription]     NVARCHAR (500)     NULL,
    [MedicationNullFlavorIndicator]     BIT                NULL,
    [ProblemCodedConceptCode]           NVARCHAR (10)      NULL,
    [ProblemDisplayName]                NVARCHAR (100)     NULL,
    [ProblemCodeSystemIdentifier]       NVARCHAR (50)      NULL,
    [ProblemCodeSystemVersionNumber]    NVARCHAR (20)      NULL,
    [ProblemCodeSystemName]             NVARCHAR (100)     NULL,
    [ProblemOriginalDescription]        NVARCHAR (500)     NULL,
    [ProblemNullFlavorIndicator]        BIT                NULL,
    PRIMARY KEY CLUSTERED ([CdsRuleKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











