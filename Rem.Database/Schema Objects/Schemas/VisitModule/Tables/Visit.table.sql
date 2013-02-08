CREATE TABLE [VisitModule].[Visit] (
    [AppointmentKey]     BIGINT         NOT NULL,
    [Name]               NVARCHAR (100) NOT NULL,
    [CptCode]            NVARCHAR (10)  NOT NULL,
    [CheckedInDateTime]  DATETIME       NULL,
    [ClinicalCaseKey]    BIGINT         NOT NULL,
    [VisitStatusLkpKey]  BIGINT         NOT NULL,
    [ServiceLocationKey] BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([AppointmentKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

























