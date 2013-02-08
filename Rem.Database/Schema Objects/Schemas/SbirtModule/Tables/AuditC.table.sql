CREATE TABLE [SbirtModule].[AuditC] (
    [ActivityKey]                          BIGINT NOT NULL,
    [HowOftenYouDrinkNumber]               INT    NULL,
    [AlcoholicDrinksPerDayNumber]          INT    NULL,
    [HowOftenYouHaveSixOrMoreDrinksNumber] INT    NULL,
    [AuditCScore]                          INT    NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





