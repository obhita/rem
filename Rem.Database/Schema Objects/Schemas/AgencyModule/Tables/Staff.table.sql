CREATE TABLE [AgencyModule].[Staff] (
    [StaffKey]                   BIGINT             NOT NULL,
    [Version]                    INT                NOT NULL,
    [CreatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]           DATETIMEOFFSET (7) NOT NULL,
    [AgencyKey]                  BIGINT             NOT NULL,
    [PrimaryLocationKey]         BIGINT             NULL,
    [StaffPhotoKey]              BIGINT             NULL,
    [SystemAccountKey]           BIGINT             NULL,
    [CreatedBySystemAccountKey]  BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]  BIGINT             NOT NULL,
    [BirthDate]                  DATE               NULL,
    [SocialSecurityNumber]       NVARCHAR (20)      NULL,
    [ProfessionalCredentialNote] NVARCHAR (MAX)     NULL,
    [Note]                       NVARCHAR (MAX)     NULL,
    [GenderLkpKey]               BIGINT             NULL,
    [StaffTypeLkpKey]            BIGINT             NULL,
    [PrefixName]                 NVARCHAR (100)     NULL,
    [FirstName]                  NVARCHAR (100)     NOT NULL,
    [MiddleName]                 NVARCHAR (100)     NULL,
    [LastName]                   NVARCHAR (100)     NOT NULL,
    [SuffixName]                 NVARCHAR (100)     NULL,
    [EmailAddress]               NVARCHAR (255)     NULL,
    [EmploymentEndDate]          DATE               NULL,
    [EmploymentStartDate]        DATE               NULL,
    [TitleName]                  NVARCHAR (100)     NULL,
    [ConfidentialNote]           NVARCHAR (MAX)     NULL,
    [EmploymentTypeLkpKey]       BIGINT             NULL,
    [SupervisorStaffKey]         BIGINT             NULL,
    [EmergencyContactName]       NVARCHAR (100)     NULL,
    [EmergencyPhoneNumber]       NVARCHAR (20)      NULL,
    [EmergencyWorkPhoneNumber]   NVARCHAR (20)      NULL,
    [DirectAddressPassword]      NVARCHAR (255)     NULL,
    [DirectAddress]              NVARCHAR (255)     NULL,
    PRIMARY KEY CLUSTERED ([StaffKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



































