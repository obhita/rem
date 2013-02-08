CREATE TABLE [TedsModule].[TedsAdmissionRecordSubstanceUsage] (
    [TedsAdmissionRecordSubstanceUsageKey]              BIGINT             NOT NULL,
    [Version]                                           INT                NOT NULL,
    [CreatedTimestamp]                                  DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                  DATETIMEOFFSET (7) NOT NULL,
    [TedsAdmissionRecordKey]                            BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]                         BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                         BIGINT             NOT NULL,
    [SubstanceProblemTypeLkpKey]                        BIGINT             NULL,
    [SubstanceProblemTypeTedsNonResponseLkpKey]         BIGINT             NULL,
    [UseFrequencyTypeLkpKey]                            BIGINT             NULL,
    [UseFrequencyTypeTedsNonResponseLkpKey]             BIGINT             NULL,
    [UsualAdministrationRouteTypeLkpKey]                BIGINT             NULL,
    [UsualAdministrationRouteTypeTedsNonResponseLkpKey] BIGINT             NULL,
    [FirstUseAge]                                       INT                NULL,
    [FirstUseAgeTedsNonResponseLkpKey]                  BIGINT             NULL,
    [DetailedDrugCodeLkpKey]                            BIGINT             NULL,
    [DetailedDrugCodeTedsNonResponseLkpKey]             BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([TedsAdmissionRecordSubstanceUsageKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



