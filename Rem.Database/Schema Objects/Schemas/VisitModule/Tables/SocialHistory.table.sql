CREATE TABLE [VisitModule].[SocialHistory] (
    [ActivityKey]                                                                          BIGINT NOT NULL,
    [SmokingStatusAreYouWillingToQuitIndicator]                                            BIT    NULL,
    [SmokingStatusAreYouWillingToQuitDate]                                                 DATE   NULL,
    [SmokingStatusLkpKey]                                                                  BIGINT NULL,
    [Phq2LittleInterestInDoingThingsAnswerNumber]                                          INT    NULL,
    [Phq2FeelingDownAnswerNumber]                                                          INT    NULL,
    [Phq2Score]                                                                            INT    NULL,
    [IsPhq2ScoreAbovePhq9ThresholdIndicator]                                               BIT    NULL,
    [Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber] INT    NULL,
    [AuditCDrinkBeerWineOrOtherAlcoholicBeveragesIndicator]                                BIT    NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











