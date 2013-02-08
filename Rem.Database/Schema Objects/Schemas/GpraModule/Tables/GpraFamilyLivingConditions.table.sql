CREATE TABLE [GpraModule].[GpraFamilyLivingConditions] (
    [GpraInterviewKey]                                                     BIGINT             NOT NULL,
    [CreatedTimestamp]                                                     DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                                     DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                                            BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                                            BIGINT             NOT NULL,
    [OtherHousingTypeSpecificationNote]                                    NVARCHAR (MAX)     NULL,
    [MostTimeGpraPlaceToLiveLkpKey]                                        BIGINT             NULL,
    [MostTimeGpraPlaceToLiveGpraNonResponseLkpKey]                         BIGINT             NULL,
    [GpraHousingTypeLkpKey]                                                BIGINT             NULL,
    [GpraHousingTypeGpraNonResponseLkpKey]                                 BIGINT             NULL,
    [StressGpraEffectDueToDrugUseLkpKey]                                   BIGINT             NULL,
    [StressGpraEffectDueToDrugUseGpraNonResponseLkpKey]                    BIGINT             NULL,
    [GiveUpImportantActivitiesGpraEffectDueToDrugUseLkpKey]                BIGINT             NULL,
    [GiveUpImportantActivitiesGpraEffectDueToDrugUseGpraNonResponseLkpKey] BIGINT             NULL,
    [EmotionalProblemsGpraEffectDueToDrugUseLkpKey]                        BIGINT             NULL,
    [EmotionalProblemsGpraEffectDueToDrugUseGpraNonResponseLkpKey]         BIGINT             NULL,
    [PregnancyIndicator]                                                   BIT                NULL,
    [PregnancyIndicatorGpraNonResponseLkpKey]                              BIGINT             NULL,
    [ChildrenIndicator]                                                    BIT                NULL,
    [ChildrenIndicatorGpraNonResponseLkpKey]                               BIGINT             NULL,
    [ChildCount]                                                           INT                NULL,
    [ChildCountGpraNonResponseLkpKey]                                      BIGINT             NULL,
    [ChildrenInChildProtectionIndicator]                                   BIT                NULL,
    [ChildrenInChildProtectionIndicatorGpraNonResponseLkpKey]              BIGINT             NULL,
    [ChildrenInChildProtectionCount]                                       INT                NULL,
    [ChildrenInChildProtectionCountGpraNonResponseLkpKey]                  BIGINT             NULL,
    [PatientLostParentalRightsChildCount]                                  INT                NULL,
    [PatientLostParentalRightsChildCountGpraNonResponseLkpKey]             BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([GpraInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







