CREATE TABLE [PatientModule].[Allergy] (
    [AllergyKey]                      BIGINT             NOT NULL,
    [Version]                         INT                NOT NULL,
    [CreatedTimestamp]                DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                      BIGINT             NOT NULL,
    [AllergyStatusLkpKey]             BIGINT             NOT NULL,
    [AllergyTypeLkpKey]               BIGINT             NULL,
    [AllergySeverityTypeLkpKey]       BIGINT             NULL,
    [ProvenanceKey]                   BIGINT             NULL,
    [CreatedBySystemAccountKey]       BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]       BIGINT             NOT NULL,
    [AllergenCodedConceptCode]        NVARCHAR (10)      NOT NULL,
    [AllergenDisplayName]             NVARCHAR (100)     NULL,
    [AllergenCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [AllergenCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [AllergenCodeSystemName]          NVARCHAR (100)     NULL,
    [AllergenOriginalDescription]     NVARCHAR (500)     NULL,
    [AllergenNullFlavorIndicator]     BIT                NOT NULL,
    [OnsetEndDate]                    DATE               NULL,
    [OnsetStartDate]                  DATE               NULL,
    PRIMARY KEY CLUSTERED ([AllergyKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



















