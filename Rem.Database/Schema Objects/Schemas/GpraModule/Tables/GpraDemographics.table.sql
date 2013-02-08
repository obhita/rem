CREATE TABLE [GpraModule].[GpraDemographics] (
    [GpraInterviewKey]                                                     BIGINT             NOT NULL,
    [CreatedTimestamp]                                                     DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                                     DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                                            BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                                            BIGINT             NOT NULL,
    [GpraPatientGenderSpecificationNote]                                   NVARCHAR (MAX)     NULL,
    [EthnicGroupSpecificationNote]                                         NVARCHAR (MAX)     NULL,
    [GpraPatientGenderLkpKey]                                              BIGINT             NULL,
    [GpraPatientGenderGpraNonResponseLkpKey]                               BIGINT             NULL,
    [HispanicLatinoIndicator]                                              BIT                NULL,
    [HispanicLatinoIndicatorGpraNonResponseLkpKey]                         BIGINT             NULL,
    [EthnicGroupCentralAmericanIndicator]                                  BIT                NULL,
    [EthnicGroupCentralAmericanIndicatorGpraNonResponseLkpKey]             BIGINT             NULL,
    [EthnicGroupCubanIndicator]                                            BIT                NULL,
    [EthnicGroupCubanIndicatorGpraNonResponseLkpKey]                       BIGINT             NULL,
    [EthnicGroupDominicanIndicator]                                        BIT                NULL,
    [EthnicGroupDominicanIndicatorGpraNonResponseLkpKey]                   BIGINT             NULL,
    [EthnicGroupMexicanIndicator]                                          BIT                NULL,
    [EthnicGroupMexicanIndicatorGpraNonResponseLkpKey]                     BIGINT             NULL,
    [EthnicGroupPuertoRicanIndicator]                                      BIT                NULL,
    [EthnicGroupPuertoRicanIndicatorGpraNonResponseLkpKey]                 BIGINT             NULL,
    [EthnicGroupSouthAmericanIndicator]                                    BIT                NULL,
    [EthnicGroupSouthAmericanIndicatorGpraNonResponseLkpKey]               BIGINT             NULL,
    [EthnicGroupOtherIndicator]                                            BIT                NULL,
    [EthnicGroupOtherIndicatorGpraNonResponseLkpKey]                       BIGINT             NULL,
    [RaceBlackAfricanAmericanIndicator]                                    BIT                NULL,
    [RaceBlackAfricanAmericanIndicatorGpraNonResponseLkpKey]               BIGINT             NULL,
    [RaceAsianIndicator]                                                   BIT                NULL,
    [RaceAsianIndicatorGpraNonResponseLkpKey]                              BIGINT             NULL,
    [RaceNativeHawaiianOtherPacificIslanderIndicator]                      BIT                NULL,
    [RaceNativeHawaiianOtherPacificIslanderIndicatorGpraNonResponseLkpKey] BIGINT             NULL,
    [RaceAlaskaNativeIndicator]                                            BIT                NULL,
    [RaceAlaskaNativeIndicatorGpraNonResponseLkpKey]                       BIGINT             NULL,
    [RaceWhiteIndicator]                                                   BIT                NULL,
    [RaceWhiteIndicatorGpraNonResponseLkpKey]                              BIGINT             NULL,
    [RaceAmericanIndianIndicator]                                          BIT                NULL,
    [RaceAmericanIndianIndicatorGpraNonResponseLkpKey]                     BIGINT             NULL,
    [BirthDate]                                                            DATE               NULL,
    [BirthDateGpraNonResponseLkpKey]                                       BIGINT             NULL,
    [VeteranIndicator]                                                     BIT                NULL,
    [VeteranIndicatorGpraNonResponseLkpKey]                                BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([GpraInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);















