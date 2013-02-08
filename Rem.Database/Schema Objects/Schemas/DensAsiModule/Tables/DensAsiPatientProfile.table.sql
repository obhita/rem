CREATE TABLE [DensAsiModule].[DensAsiPatientProfile] (
    [DensAsiInterviewKey]                                                 BIGINT             NOT NULL,
    [CreatedTimestamp]                                                    DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                                    DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                                           BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                                           BIGINT             NOT NULL,
    [AdmissionDate]                                                       DATE               NULL,
    [BirthDate]                                                           DATE               NULL,
    [FirstName]                                                           NVARCHAR (100)     NULL,
    [LastName]                                                            NVARCHAR (100)     NULL,
    [FirstStreetAddress]                                                  NVARCHAR (255)     NULL,
    [SecondStreetAddress]                                                 NVARCHAR (255)     NULL,
    [CityName]                                                            NVARCHAR (100)     NULL,
    [HispanicOrLatinoIndicator]                                           BIT                NULL,
    [InterviewDate]                                                       DATE               NULL,
    [InterviewDateNote]                                                   NVARCHAR (MAX)     NULL,
    [DensAsiInterviewClassNote]                                           NVARCHAR (MAX)     NULL,
    [DensAsiInterviewContactTypeNote]                                     NVARCHAR (MAX)     NULL,
    [YearsAndMonthsAtCurrentAddressTimeSpanNote]                          NVARCHAR (MAX)     NULL,
    [ResidenceOwnedByYouOrFamilyIndicatorNote]                            NVARCHAR (MAX)     NULL,
    [PreferredDensAsiReligionNote]                                        NVARCHAR (MAX)     NULL,
    [LastThirtyDaysDensAsiControlledEnvironmentNote]                      NVARCHAR (MAX)     NULL,
    [LastThirtyDaysControlledEnvironmentDayCountNote]                     NVARCHAR (MAX)     NULL,
    [ChristianReligionIndicatorNote]                                      NVARCHAR (MAX)     NULL,
    [BuddhismReligionIndicatorNote]                                       NVARCHAR (MAX)     NULL,
    [NoParticularReligiousSectIndicatorNote]                              NVARCHAR (MAX)     NULL,
    [PatientAdministrativeGenderLkpKey]                                   BIGINT             NULL,
    [StateProvinceLkpKey]                                                 BIGINT             NULL,
    [RaceLkpKey]                                                          BIGINT             NULL,
    [DensAsiInterviewClassLkpKey]                                         BIGINT             NULL,
    [PostalCode]                                                          NVARCHAR (10)      NULL,
    [DensAsiInterviewContactTypeLkpKey]                                   BIGINT             NULL,
    [DensAsiInterviewContactTypeDensAsiNonResponseLkpKey]                 BIGINT             NULL,
    [YearsAndMonthsAtCurrentAddressTimeSpan]                              BIGINT             NULL,
    [YearsAndMonthsAtCurrentAddressTimeSpanDensAsiNonResponseLkpKey]      BIGINT             NULL,
    [ResidenceOwnedByYouOrFamilyIndicator]                                BIT                NULL,
    [ResidenceOwnedByYouOrFamilyIndicatorDensAsiNonResponseLkpKey]        BIGINT             NULL,
    [PreferredDensAsiReligionLkpKey]                                      BIGINT             NULL,
    [PreferredDensAsiReligionDensAsiNonResponseLkpKey]                    BIGINT             NULL,
    [LastThirtyDaysDensAsiControlledEnvironmentLkpKey]                    BIGINT             NULL,
    [LastThirtyDaysDensAsiControlledEnvironmentDensAsiNonResponseLkpKey]  BIGINT             NULL,
    [LastThirtyDaysControlledEnvironmentDayCount]                         INT                NULL,
    [LastThirtyDaysControlledEnvironmentDayCountDensAsiNonResponseLkpKey] BIGINT             NULL,
    [ChristianReligionIndicator]                                          BIT                NULL,
    [ChristianReligionIndicatorDensAsiNonResponseLkpKey]                  BIGINT             NULL,
    [BuddhismReligionIndicator]                                           BIT                NULL,
    [BuddhismReligionIndicatorDensAsiNonResponseLkpKey]                   BIGINT             NULL,
    [NoParticularReligiousSectIndicator]                                  BIT                NULL,
    [NoParticularReligiousSectIndicatorDensAsiNonResponseLkpKey]          BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([DensAsiInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);















