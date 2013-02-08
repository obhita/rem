CREATE TABLE [PatientModule].[PayorCoverageCache] (
    [PayorCoverageCacheKey]                      BIGINT             NOT NULL,
    [Version]                                    INT                NOT NULL,
    [MemberNumber]                               NVARCHAR (20)      NOT NULL,
    [CreatedTimestamp]                           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                           DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                                 BIGINT             NULL,
    [PayorCacheKey]                              BIGINT             NULL,
    [PayorCoverageCacheTypeLkpKey]               BIGINT             NULL,
    [CreatedBySystemAccountKey]                  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                  BIGINT             NOT NULL,
    [EffectiveEndDate]                           DATE               NULL,
    [EffectiveStartDate]                         DATE               NULL,
    [BirthDate]                                  DATE               NULL,
    [AdministrativeGenderLkpKey]                 BIGINT             NULL,
    [PayorSubscriberRelationshipCacheTypeLkpKey] BIGINT             NULL,
    [FirstStreetAddress]                         NVARCHAR (255)     NOT NULL,
    [SecondStreetAddress]                        NVARCHAR (255)     NULL,
    [CityName]                                   NVARCHAR (100)     NOT NULL,
    [CountyAreaLkpKey]                           BIGINT             NULL,
    [StateProvinceLkpKey]                        BIGINT             NOT NULL,
    [CountryLkpKey]                              BIGINT             NULL,
    [PostalCode]                                 NVARCHAR (10)      NULL,
    [PrefixName]                                 NVARCHAR (100)     NULL,
    [FirstName]                                  NVARCHAR (100)     NOT NULL,
    [MiddleName]                                 NVARCHAR (100)     NULL,
    [LastName]                                   NVARCHAR (100)     NOT NULL,
    [SuffixName]                                 NVARCHAR (100)     NULL,
    PRIMARY KEY CLUSTERED ([PayorCoverageCacheKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



