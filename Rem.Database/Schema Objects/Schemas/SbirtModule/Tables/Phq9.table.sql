CREATE TABLE [SbirtModule].[Phq9] (
    [ActivityKey]                              BIGINT NOT NULL,
    [LittleInterestInDoingThingsAnswerNumber]  INT    NULL,
    [FeelingDownAnswerNumber]                  INT    NULL,
    [TroubleSleepingAnswerNumber]              INT    NULL,
    [FeelingTiredAnswerNumber]                 INT    NULL,
    [PoorAppetiteAnswerNumber]                 INT    NULL,
    [FeelingBadAboutSelfAnswerNumber]          INT    NULL,
    [TroubleConcentratingAnswerNumber]         INT    NULL,
    [ActingSluggishOrFidgityAnswerNumber]      INT    NULL,
    [ThoughtsOfHurtingSelfAnswerNumber]        INT    NULL,
    [HaveTheseProblemsAffectedYouAnswerNumber] INT    NULL,
    [SeverityScore]                            INT    NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



