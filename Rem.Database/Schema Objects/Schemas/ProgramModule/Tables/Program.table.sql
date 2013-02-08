CREATE TABLE [ProgramModule].[Program] (
    [ProgramKey]                                      BIGINT             NOT NULL,
    [Version]                                         INT                NOT NULL,
    [Name]                                            NVARCHAR (100)     NOT NULL,
    [DisplayName]                                     NVARCHAR (100)     NULL,
    [StartDate]                                       DATE               NOT NULL,
    [EndDate]                                         DATE               NULL,
    [CreatedTimestamp]                                DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                DATETIMEOFFSET (7) NOT NULL,
    [AgencyKey]                                       BIGINT             NOT NULL,
    [CapacityTypeLkpKey]                              BIGINT             NULL,
    [CreatedBySystemAccountKey]                       BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                       BIGINT             NOT NULL,
    [ProgramCharacteristicsAgeGroupLkpKey]            BIGINT             NULL,
    [ProgramCharacteristicsGenderSpecificationLkpKey] BIGINT             NULL,
    [ProgramCharacteristicsTreatmentApproachLkpKey]   BIGINT             NULL,
    [ProgramCharacteristicsProgramCategoryLkpKey]     BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([ProgramKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









