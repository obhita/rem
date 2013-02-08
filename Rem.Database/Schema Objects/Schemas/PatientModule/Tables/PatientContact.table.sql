CREATE TABLE [PatientModule].[PatientContact] (
    [PatientContactKey]                    BIGINT             NOT NULL,
    [Version]                              INT                NOT NULL,
    [FirstName]                            NVARCHAR (100)     NOT NULL,
    [MiddleName]                           NVARCHAR (100)     NULL,
    [LastName]                             NVARCHAR (100)     NOT NULL,
    [FirstStreetAddress]                   NVARCHAR (255)     NULL,
    [SecondStreetAddress]                  NVARCHAR (255)     NULL,
    [CityName]                             NVARCHAR (100)     NULL,
    [PostalCode]                           NVARCHAR (10)      NULL,
    [PrimaryIndicator]                     BIT                NULL,
    [Note]                                 NVARCHAR (MAX)     NULL,
    [ConsentExpirationDate]                DATE               NULL,
    [CanContactIndicator]                  BIT                NULL,
    [ConsentOnFileIndicator]               BIT                NULL,
    [SocialSecurityNumber]                 NVARCHAR (20)      NULL,
    [EmailAddress]                         NVARCHAR (255)     NULL,
    [EmergencyIndicator]                   BIT                NULL,
    [DesignatedFollowUpIndicator]          BIT                NULL,
    [BirthDate]                            DATE               NULL,
    [CreatedTimestamp]                     DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                     DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                           BIGINT             NOT NULL,
    [CountyAreaLkpKey]                     BIGINT             NULL,
    [StateProvinceLkpKey]                  BIGINT             NULL,
    [CountryLkpKey]                        BIGINT             NULL,
    [PatientContactRelationshipTypeLkpKey] BIGINT             NULL,
    [LegalAuthorizationTypeLkpKey]         BIGINT             NULL,
    [GenderLkpKey]                         BIGINT             NULL,
    [CreatedBySystemAccountKey]            BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]            BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientContactKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);























