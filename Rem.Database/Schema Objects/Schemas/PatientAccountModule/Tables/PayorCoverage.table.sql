CREATE TABLE [PatientAccountModule].[PayorCoverage] (
    [PayorCoverageKey]                      BIGINT             NOT NULL,
    [Version]                               INT                NOT NULL,
    [MemberNumber]                          NVARCHAR (20)      NOT NULL,
    [CreatedTimestamp]                      DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                      DATETIMEOFFSET (7) NOT NULL,
    [PayorKey]                              BIGINT             NOT NULL,
    [PayorCoverageTypeLkpKey]               BIGINT             NOT NULL,
    [PatientAccountKey]                     BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]             BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]             BIGINT             NOT NULL,
    [EffectiveCoverageEndDate]              DATE               NULL,
    [EffectiveCoverageStartDate]            DATE               NULL,
    [BirthDate]                             DATE               NULL,
    [AdministrativeGenderLkpKey]            BIGINT             NULL,
    [PayorSubscriberRelationshipTypeLkpKey] BIGINT             NULL,
    [FirstStreetAddress]                    NVARCHAR (255)     NULL,
    [SecondStreetAddress]                   NVARCHAR (255)     NULL,
    [CityName]                              NVARCHAR (100)     NULL,
    [CountyAreaLkpKey]                      BIGINT             NULL,
    [StateProvinceLkpKey]                   BIGINT             NULL,
    [CountryLkpKey]                         BIGINT             NULL,
    [PostalCode]                            NVARCHAR (10)      NULL,
    [PrefixName]                            NVARCHAR (100)     NULL,
    [FirstName]                             NVARCHAR (100)     NULL,
    [MiddleName]                            NVARCHAR (100)     NULL,
    [LastName]                              NVARCHAR (100)     NULL,
    [SuffixName]                            NVARCHAR (100)     NULL,
    PRIMARY KEY CLUSTERED ([PayorCoverageKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







