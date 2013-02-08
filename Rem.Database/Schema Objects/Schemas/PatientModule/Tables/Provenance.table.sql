CREATE TABLE [PatientModule].[Provenance] (
    [ProvenanceKey]                      BIGINT             NOT NULL,
    [Version]                            INT                NOT NULL,
    [SignedTimestamp]                    DATETIMEOFFSET (7) NOT NULL,
    [CreatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]          BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]          BIGINT             NOT NULL,
    [ExtensionValue]                     NVARCHAR (255)     NULL,
    [AssigningAuthorityName]             NVARCHAR (100)     NOT NULL,
    [ProviderDirectoryEntryAddress]      NVARCHAR (255)     NULL,
    [PrefixName]                         NVARCHAR (100)     NULL,
    [FirstName]                          NVARCHAR (100)     NULL,
    [MiddleName]                         NVARCHAR (100)     NULL,
    [LastName]                           NVARCHAR (100)     NULL,
    [SuffixName]                         NVARCHAR (100)     NULL,
    [OrganizationName]                   NVARCHAR (100)     NULL,
    [OrganizationExtensionValue]         NVARCHAR (255)     NULL,
    [OrganizationAssigningAuthorityName] NVARCHAR (100)     NULL,
    [PhoneNumber]                        NVARCHAR (20)      NULL,
    [PhoneExtensionNumber]               NVARCHAR (20)      NULL,
    PRIMARY KEY CLUSTERED ([ProvenanceKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);





