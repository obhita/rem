CREATE TABLE [SbirtModule].[Dast10] (
    [ActivityKey]                                       BIGINT NOT NULL,
    [HaveYouUsedDrugsIndicator]                         BIT    NULL,
    [DoYouAbuseMoreThanOneDrugIndicator]                BIT    NULL,
    [AreYouAbleToStopUsingDrugsIndicator]               BIT    NULL,
    [HaveYouHadBlackoutsOrFlashbacksIndicator]          BIT    NULL,
    [DoYouFeelBadOrGuiltyIndicator]                     BIT    NULL,
    [DoesYourSpouseOrParentComplainIndicator]           BIT    NULL,
    [HaveYouNeglectedYourFamilyIndicator]               BIT    NULL,
    [HaveYouEngagedInIllegalActivitiesIndicator]        BIT    NULL,
    [HaveYouEverExperiencedWithdrawalSymptomsIndicator] BIT    NULL,
    [HaveYouHadMedicalProblemsIndicator]                BIT    NULL,
    [SeverityScore]                                     INT    NULL,
    [Dast10Result]                                      INT    NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





