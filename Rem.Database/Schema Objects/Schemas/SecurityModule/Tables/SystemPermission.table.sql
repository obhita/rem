CREATE TABLE [SecurityModule].[SystemPermission] (
    [SystemPermissionKey]       BIGINT             NOT NULL,
    [Version]                   INT                NOT NULL,
    [WellKnownName]             NVARCHAR (100)     NOT NULL,
    [DisplayName]               NVARCHAR (100)     NOT NULL,
    [Description]               NVARCHAR (500)     NOT NULL,
    [CreatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]          DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey] BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey] BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([SystemPermissionKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);













