CREATE TABLE [LabModule].[LabResult] (
    [LabResultKey]                             BIGINT             NOT NULL,
    [Version]                                  INT                NOT NULL,
    [UnitOfMeasureCode]                        NVARCHAR (10)      NULL,
    [Value]                                    FLOAT              NULL,
    [CreatedTimestamp]                         DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                         DATETIMEOFFSET (7) NOT NULL,
    [LabTestKey]                               BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]                BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                BIGINT             NOT NULL,
    [LabTestResultNameCodedConceptCode]        NVARCHAR (10)      NOT NULL,
    [LabTestResultNameDisplayName]             NVARCHAR (100)     NULL,
    [LabTestResultNameCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [LabTestResultNameCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [LabTestResultNameCodeSystemName]          NVARCHAR (100)     NULL,
    [LabTestResultNameOriginalDescription]     NVARCHAR (500)     NULL,
    [LabTestResultNameNullFlavorIndicator]     BIT                NOT NULL,
    PRIMARY KEY CLUSTERED ([LabResultKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









