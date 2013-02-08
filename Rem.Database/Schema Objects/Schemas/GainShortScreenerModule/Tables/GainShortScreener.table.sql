CREATE TABLE [GainShortScreenerModule].[GainShortScreener] (
    [ActivityKey]                                  BIGINT         NOT NULL,
    [InternalizingDisorderScreenerPastMonthScore]  INT            NULL,
    [InternalizingDisorderScreenerPastYearScore]   INT            NULL,
    [InternalizingDisorderScreenerLifetimeScore]   INT            NULL,
    [ExternalizingDisorderScreenerPastMonthScore]  INT            NULL,
    [ExternalizingDisorderScreenerPastYearScore]   INT            NULL,
    [ExternalizingDisorderScreenerLifetimeScore]   INT            NULL,
    [SubstanceDisorderScreenerPastMonthScore]      INT            NULL,
    [SubstanceDisorderScreenerPastYearScore]       INT            NULL,
    [SubstanceDisorderScreenerLifetimeScore]       INT            NULL,
    [CrimeViolenceScreenerPastMonthScore]          INT            NULL,
    [CrimeViolenceScreenerPastYearScore]           INT            NULL,
    [CrimeViolenceScreenerLifetimeScore]           INT            NULL,
    [TotalScreenerPastMonthScore]                  INT            NULL,
    [TotalScreenerPastYearScore]                   INT            NULL,
    [TotalScreenerLifetimeScore]                   INT            NULL,
    [ProblemFeelingDepressedNumber]                INT            NULL,
    [ProblemSleepingNumber]                        INT            NULL,
    [ProblemFeelingAnxiousNumber]                  INT            NULL,
    [ProblemBecomingDistressedNumber]              INT            NULL,
    [ProblemCommittingSuicideNumber]               INT            NULL,
    [TwoOrMoreLiedNumber]                          INT            NULL,
    [TwoOrMoreHardTimePayingAttentionNumber]       INT            NULL,
    [TwoOrMoreHardTimeListeningNumber]             INT            NULL,
    [TwoOrMoreThreatenedOthersNumber]              INT            NULL,
    [TwoOrMoreStartedFightNumber]                  INT            NULL,
    [LastTimeUsedAlcoholDrugsNumber]               INT            NULL,
    [LastTimeSpentALotOfTimeGettingAlcoholNumber]  INT            NULL,
    [LastTimeKeptUsingAlcoholNumber]               INT            NULL,
    [LastTimeUseAlcoholCauseYouToGiveUpNumber]     INT            NULL,
    [LastTimeHadWithdrawProblemsNumber]            INT            NULL,
    [LastTimeYouHadDisagreementNumber]             INT            NULL,
    [LastTimeYouTookSomethingNumber]               INT            NULL,
    [LastTimeYouSoldIllegalDrugsNumber]            INT            NULL,
    [LastTimeYouDroveUnderTheInfluenceNumber]      INT            NULL,
    [LastTimeYouPurposelyDamagedPropertyNumber]    INT            NULL,
    [SignificantProblemsSeekingTreatmentIndicator] BIT            NULL,
    [SignificantProblemsSeekingTreatmentNote]      NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











