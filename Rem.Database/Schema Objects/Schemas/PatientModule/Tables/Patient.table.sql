CREATE TABLE [PatientModule].[Patient] (
    [PatientKey]                               BIGINT             NOT NULL,
    [Version]                                  INT                NOT NULL,
    [PaperFileIndicator]                       BIT                NULL,
    [Note]                                     NVARCHAR (MAX)     NULL,
    [UniqueIdentifier]                         NVARCHAR (20)      NOT NULL,
    [RevisedTimestamp]                         DATETIMEOFFSET (7) NULL,
    [RevisedAccountKey]                        BIGINT             NOT NULL,
    [CreatedTimestamp]                         DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                         DATETIMEOFFSET (7) NOT NULL,
    [AgencyKey]                                BIGINT             NOT NULL,
    [PrimaryPatientRaceKey]                    BIGINT             NULL,
    [PrimaryPatientPhotoKey]                   BIGINT             NULL,
    [SmokingStatusLkpKey]                      BIGINT             NULL,
    [RecordStatusLkpKey]                       BIGINT             NULL,
    [ReligiousAffiliationLkpKey]               BIGINT             NULL,
    [MaritalStatusLkpKey]                      BIGINT             NULL,
    [EducationStatusLkpKey]                    BIGINT             NULL,
    [CreatedBySystemAccountKey]                BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                BIGINT             NOT NULL,
    [PrefixName]                               NVARCHAR (100)     NULL,
    [FirstName]                                NVARCHAR (100)     NOT NULL,
    [MiddleName]                               NVARCHAR (100)     NULL,
    [LastName]                                 NVARCHAR (100)     NOT NULL,
    [SuffixName]                               NVARCHAR (100)     NULL,
    [BirthDate]                                DATE               NULL,
    [DeathDate]                                DATE               NULL,
    [PatientGenderLkpKey]                      BIGINT             NULL,
    [ContactPreferenceLkpKey]                  BIGINT             NULL,
    [EmailAddress]                             NVARCHAR (255)     NULL,
    [BirthFirstName]                           NVARCHAR (100)     NULL,
    [BirthLastName]                            NVARCHAR (100)     NULL,
    [BirthCityName]                            NVARCHAR (100)     NULL,
    [BirthCountyAreaLkpKey]                    BIGINT             NULL,
    [BirthStateProvinceLkpKey]                 BIGINT             NULL,
    [MotherFirstName]                          NVARCHAR (100)     NULL,
    [MotherMaidenName]                         NVARCHAR (100)     NULL,
    [AssignedCountyAreaLkpKey]                 BIGINT             NULL,
    [AssignedGeographicalRegionLkpKey]         BIGINT             NULL,
    [AssignedPostalCode]                       NVARCHAR (10)      NULL,
    [InterpreterNeededIndicator]               BIT                NULL,
    [LanguageLkpKey]                           BIGINT             NULL,
    [CitizenshipCountryLkpKey]                 BIGINT             NULL,
    [ImmigrationStatusLkpKey]                  BIGINT             NULL,
    [CustodialStatusLkpKey]                    BIGINT             NULL,
    [ConfidentialFamilyInformationDescription] NVARCHAR (500)     NULL,
    [SexualAbuseVictimIndicator]               BIT                NULL,
    [PhysicalAbuseVictimIndicator]             BIT                NULL,
    [DomesticAbuseVictimIndicator]             BIT                NULL,
    [RegisteredSexOffenderDate]                DATE               NULL,
    [RegisteredSexOffenderIndicator]           BIT                NULL,
    [ConvictedOfArsonDate]                     DATE               NULL,
    [ConvictedOfArsonIndicator]                BIT                NULL,
    [EthnicityLkpKey]                          BIGINT             NULL,
    [DetailedEthnicityLkpKey]                  BIGINT             NULL,
    [DisabilityDescription]                    NVARCHAR (500)     NULL,
    [DisabilityPercentageValue]                NVARCHAR (255)     NULL,
    [HaveCombatHistoryIndicator]               BIT                NULL,
    [HaveServedInMilitaryIndicator]            BIT                NULL,
    [RegisteredVaHospitalName]                 NVARCHAR (100)     NULL,
    [VaCaseNumber]                             NVARCHAR (20)      NULL,
    [VeteranDischargeStatusLkpKey]             BIGINT             NULL,
    [VeteranServiceBranchLkpKey]               BIGINT             NULL,
    [VeteranStatusLkpKey]                      BIGINT             NULL,
    [ServiceStartDate]                         DATE               NULL,
    [ServiceEndDate]                           DATE               NULL,
    PRIMARY KEY CLUSTERED ([PatientKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
    UNIQUE NONCLUSTERED ([UniqueIdentifier] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
);









































