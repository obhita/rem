CREATE TABLE [VisitModule].[BriefIntervention] (
    [ActivityKey]                         BIGINT NOT NULL,
    [NutritionCounselingIndicator]        BIT    NULL,
    [PhysicalActivityCounselingIndicator] BIT    NULL,
    [TobaccoCessationCounselingIndicator] BIT    NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



