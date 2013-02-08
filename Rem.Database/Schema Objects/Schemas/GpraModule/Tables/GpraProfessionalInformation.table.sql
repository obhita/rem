CREATE TABLE [GpraModule].[GpraProfessionalInformation] (
    [GpraInterviewKey]                                        BIGINT             NOT NULL,
    [CreatedTimestamp]                                        DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                        DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                               BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                               BIGINT             NOT NULL,
    [OtherJobTrainingProgramSpecificationNote]                NVARCHAR (MAX)     NULL,
    [OtherEmploymentTypeSpecificationNote]                    NVARCHAR (MAX)     NULL,
    [OtherPretaxIncomeSpecificationNote]                      NVARCHAR (MAX)     NULL,
    [GpraJobTrainingProgramLkpKey]                            BIGINT             NULL,
    [GpraJobTrainingProgramGpraNonResponseLkpKey]             BIGINT             NULL,
    [HighestGpraEducationLevelLkpKey]                         BIGINT             NULL,
    [HighestGpraEducationLevelGpraNonResponseLkpKey]          BIGINT             NULL,
    [GpraEmploymentStatusLkpKey]                              BIGINT             NULL,
    [GpraEmploymentStatusGpraNonResponseLkpKey]               BIGINT             NULL,
    [WagesPretaxIncomeAmount]                                 INT                NULL,
    [WagesPretaxIncomeAmountGpraNonResponseLkpKey]            BIGINT             NULL,
    [PublicAssistancePretaxIncomeAmount]                      INT                NULL,
    [PublicAssistancePretaxIncomeAmountGpraNonResponseLkpKey] BIGINT             NULL,
    [RetirementPretaxIncomeAmount]                            INT                NULL,
    [RetirementPretaxIncomeAmountGpraNonResponseLkpKey]       BIGINT             NULL,
    [DisabilityPretaxIncomeAmount]                            INT                NULL,
    [DisabilityPretaxIncomeAmountGpraNonResponseLkpKey]       BIGINT             NULL,
    [NonLegalPretaxIncomeAmount]                              INT                NULL,
    [NonLegalPretaxIncomeAmountGpraNonResponseLkpKey]         BIGINT             NULL,
    [FamilyFriendsPretaxIncomeAmount]                         INT                NULL,
    [FamilyFriendsPretaxIncomeAmountGpraNonResponseLkpKey]    BIGINT             NULL,
    [OtherPretaxIncomeAmount]                                 INT                NULL,
    [OtherPretaxIncomeAmountGpraNonResponseLkpKey]            BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([GpraInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







