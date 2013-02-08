CREATE TABLE [SbirtModule].[Audit] (
    [ActivityKey]                                       BIGINT NOT NULL,
    [HowOftenYouDrinkNumber]                            INT    NULL,
    [AlcoholicDrinksPerDayNumber]                       INT    NULL,
    [HowOftenYouHaveSixOrMoreDrinksNumber]              INT    NULL,
    [PastYearHowOftenYouWereUnableToStopDrinkingNumber] INT    NULL,
    [PastYearHowOftenYouFailedNormalExpectationNumber]  INT    NULL,
    [PastYearHowOftenYouDrinkInMorningNumber]           INT    NULL,
    [PastYearHowOftenYouHadGuiltAfterDrinkingNumber]    INT    NULL,
    [PastYearHowOftenYouForgotNightBeforeNumber]        INT    NULL,
    [YouOrSomeoneInjuredDueToYourDrinkingNumber]        INT    NULL,
    [HealthWorkerSuggestedToCutDownDrinkingNumber]      INT    NULL,
    [AuditScore]                                        INT    NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

