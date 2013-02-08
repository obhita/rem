CREATE TABLE [ImmunizationModule].[Immunization] (
    [ActivityKey]                      BIGINT         NOT NULL,
    [ImmunizationNotGivenReasonLkpKey] BIGINT         NULL,
    [VaccineLotNumber]                 NVARCHAR (20)  NULL,
    [VaccineCodedConceptCode]          NVARCHAR (10)  NULL,
    [VaccineDisplayName]               NVARCHAR (100) NULL,
    [VaccineCodeSystemIdentifier]      NVARCHAR (50)  NULL,
    [VaccineCodeSystemVersionNumber]   NVARCHAR (20)  NULL,
    [VaccineCodeSystemName]            NVARCHAR (100) NULL,
    [VaccineOriginalDescription]       NVARCHAR (500) NULL,
    [VaccineNullFlavorIndicator]       BIT            NULL,
    [VaccineManufacturerCode]          NVARCHAR (10)  NULL,
    [VaccineManufacturerName]          NVARCHAR (100) NULL,
    [AdministeredAmount]               FLOAT          NULL,
    [ImmunizationUnitOfMeasureLkpKey]  BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



















