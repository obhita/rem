CREATE TABLE [PatientModule].[Medication] (
    [MedicationKey]                         BIGINT             NOT NULL,
    [Version]                               INT                NOT NULL,
    [MedicationDoseValue]                   FLOAT              NULL,
    [OverTheCounterIndicator]               BIT                NULL,
    [InstructionsNote]                      NVARCHAR (MAX)     NULL,
    [PrescribingPhysicianName]              NVARCHAR (100)     NULL,
    [DiscontinuedByPhysicianName]           NVARCHAR (100)     NULL,
    [DiscontinuedReasonOtherDescription]    NVARCHAR (500)     NULL,
    [FrequencyDescription]                  NVARCHAR (500)     NULL,
    [CreatedTimestamp]                      DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                      DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                            BIGINT             NOT NULL,
    [MedicationRouteLkpKey]                 BIGINT             NULL,
    [MedicationDoseUnitLkpKey]              BIGINT             NULL,
    [MedicationStatusLkpKey]                BIGINT             NULL,
    [DiscontinuedReasonLkpKey]              BIGINT             NULL,
    [ProvenanceKey]                         BIGINT             NULL,
    [CreatedBySystemAccountKey]             BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]             BIGINT             NOT NULL,
    [RootMedicationCodedConceptCode]        NVARCHAR (10)      NULL,
    [RootMedicationDisplayName]             NVARCHAR (100)     NULL,
    [RootMedicationCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [RootMedicationCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [RootMedicationCodeSystemName]          NVARCHAR (100)     NULL,
    [RootMedicationOriginalDescription]     NVARCHAR (500)     NULL,
    [RootMedicationNullFlavorIndicator]     BIT                NULL,
    [MedicationCodeCodedConceptCode]        NVARCHAR (10)      NOT NULL,
    [MedicationCodeDisplayName]             NVARCHAR (100)     NULL,
    [MedicationCodeCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [MedicationCodeCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [MedicationCodeCodeSystemName]          NVARCHAR (100)     NULL,
    [MedicationCodeOriginalDescription]     NVARCHAR (500)     NULL,
    [MedicationCodeNullFlavorIndicator]     BIT                NOT NULL,
    [UsageEndDate]                          DATE               NULL,
    [UsageStartDate]                        DATE               NULL,
    PRIMARY KEY CLUSTERED ([MedicationKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







































