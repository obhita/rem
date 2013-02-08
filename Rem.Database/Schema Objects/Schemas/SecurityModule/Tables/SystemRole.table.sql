CREATE TABLE [SecurityModule].[SystemRole] (
    [SystemRoleKey]             BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [Name]                      NVARCHAR (100)     NOT NULL,
    [WellKnownName]             NVARCHAR (100)     NULL,
    [Description]               NVARCHAR (500)     NULL,
    [SystemRoleTypeEnum]        NVARCHAR (50)      NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([SystemRoleKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

























