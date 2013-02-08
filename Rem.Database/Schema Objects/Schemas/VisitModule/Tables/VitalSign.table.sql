CREATE TABLE [VisitModule].[VitalSign] (
    [ActivityKey]                              BIGINT NOT NULL,
    [WeightLbsMeasure]                         FLOAT  NULL,
    [DietaryConsultationOrderIndicator]        BIT    NULL,
    [BmiFollowUpPlanIndicator]                 BIT    NULL,
    [VitalSignPhysicalExamNotDoneReasonLkpKey] BIGINT NULL,
    [HeightFeetMeasure]                        INT    NULL,
    [HeightInchesMeasure]                      FLOAT  NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);















