CREATE TABLE [GpraModule].[GpraDischarge] (
    [GpraInterviewKey]                               BIGINT             NOT NULL,
    [CreatedTimestamp]                               DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                               DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                      BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                      BIGINT             NOT NULL,
    [GpraDischargeTerminationReasonOtherDescription] NVARCHAR (500)     NULL,
    [GpraDischargeDate]                              DATE               NULL,
    [GpraDischargeStatusOtherDescription]            NVARCHAR (500)     NULL,
    [GpraHivTestIndicator]                           BIT                NULL,
    [GpraReferToHivTestIndicator]                    BIT                NULL,
    [GpraDischargeTerminationReasonLkpKey]           BIGINT             NULL,
    [GpraDischargeStatusLkpKey]                      BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([GpraInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







