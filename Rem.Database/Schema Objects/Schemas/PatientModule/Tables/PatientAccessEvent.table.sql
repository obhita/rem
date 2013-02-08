CREATE TABLE [PatientModule].[PatientAccessEvent] (
    [PatientAccessEventKey]        BIGINT             NOT NULL,
    [Version]                      INT                NOT NULL,
    [AuditedContextDescription]    NVARCHAR (500)     NOT NULL,
    [Note]                         NVARCHAR (MAX)     NOT NULL,
    [AggregateRootTypeName]        NVARCHAR (100)     NULL,
    [AggregateRootKey]             BIGINT             NULL,
    [AggregateNodeTypeName]        NVARCHAR (100)     NULL,
    [AggregateNodeKey]             BIGINT             NULL,
    [CreatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                   BIGINT             NOT NULL,
    [PatientAccessEventTypeLkpKey] BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]    BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]    BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientAccessEventKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);















