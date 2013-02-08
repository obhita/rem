CREATE TABLE [SecurityModule].[SystemAccount] (
    [SystemAccountKey]              BIGINT         NOT NULL,
    [Version]                       INT            NOT NULL,
    [Identifier]                    NVARCHAR (100) NOT NULL,
    [DisplayName]                   NVARCHAR (100) NOT NULL,
    [IdentityProviderUriIdentifier] NVARCHAR (100) NOT NULL,
    [IdentityProviderName]          NVARCHAR (100) NOT NULL,
    [EnabledIndicator]              BIT            NOT NULL,
    [EmailAddress]                  NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([SystemAccountKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
    UNIQUE NONCLUSTERED ([Identifier] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
);

























