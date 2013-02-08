CREATE TABLE [ClinicalCaseModule].[Problem] (
    [ProblemKey]                         BIGINT             NOT NULL,
    [Version]                            INT                NOT NULL,
    [StatusChangedDate]                  DATE               NULL,
    [ObservedDate]                       DATE               NULL,
    [CauseOfDeathIndicator]              BIT                NULL,
    [CreatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [ClinicalCaseKey]                    BIGINT             NOT NULL,
    [ProblemStatusLkpKey]                BIGINT             NULL,
    [ProblemTypeLkpKey]                  BIGINT             NULL,
    [ObservedByStaffKey]                 BIGINT             NULL,
    [ProvenanceKey]                      BIGINT             NULL,
    [CreatedBySystemAccountKey]          BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]          BIGINT             NOT NULL,
    [ProblemCodeCodedConceptCode]        NVARCHAR (10)      NOT NULL,
    [ProblemCodeDisplayName]             NVARCHAR (100)     NULL,
    [ProblemCodeCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [ProblemCodeCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [ProblemCodeCodeSystemName]          NVARCHAR (100)     NULL,
    [ProblemCodeOriginalDescription]     NVARCHAR (500)     NULL,
    [ProblemCodeNullFlavorIndicator]     BIT                NOT NULL,
    [OnsetEndDate]                       DATE               NULL,
    [OnsetStartDate]                     DATE               NULL,
    PRIMARY KEY CLUSTERED ([ProblemKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);













