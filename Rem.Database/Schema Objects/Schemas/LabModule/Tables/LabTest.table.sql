CREATE TABLE [LabModule].[LabTest] (
    [LabTestKey]                                BIGINT             NOT NULL,
    [Version]                                   INT                NOT NULL,
    [CreatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                          DATETIMEOFFSET (7) NOT NULL,
    [LabSpecimenKey]                            BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                 BIGINT             NOT NULL,
    [NormalRangeDescription]                    NVARCHAR (500)     NULL,
    [TestReportDate]                            DATE               NULL,
    [LabTestNote]                               NVARCHAR (MAX)     NULL,
    [LabTestNameLkpKey]                         BIGINT             NULL,
    [LabTestTypeCodedConceptCode]               NVARCHAR (10)      NULL,
    [LabTestTypeDisplayName]                    NVARCHAR (100)     NULL,
    [LabTestTypeCodeSystemIdentifier]           NVARCHAR (50)      NULL,
    [LabTestTypeCodeSystemVersionNumber]        NVARCHAR (20)      NULL,
    [LabTestTypeCodeSystemName]                 NVARCHAR (100)     NULL,
    [LabTestTypeOriginalDescription]            NVARCHAR (500)     NULL,
    [LabTestTypeNullFlavorIndicator]            BIT                NULL,
    [InterpretationCodeCodedConceptCode]        NVARCHAR (10)      NULL,
    [InterpretationCodeDisplayName]             NVARCHAR (100)     NULL,
    [InterpretationCodeCodeSystemIdentifier]    NVARCHAR (50)      NULL,
    [InterpretationCodeCodeSystemVersionNumber] NVARCHAR (20)      NULL,
    [InterpretationCodeCodeSystemName]          NVARCHAR (100)     NULL,
    [InterpretationCodeOriginalDescription]     NVARCHAR (500)     NULL,
    [InterpretationCodeNullFlavorIndicator]     BIT                NULL,
    [LabResultStatusCodedConceptCode]           NVARCHAR (10)      NULL,
    [LabResultStatusDisplayName]                NVARCHAR (100)     NULL,
    [LabResultStatusCodeSystemIdentifier]       NVARCHAR (50)      NULL,
    [LabResultStatusCodeSystemVersionNumber]    NVARCHAR (20)      NULL,
    [LabResultStatusCodeSystemName]             NVARCHAR (100)     NULL,
    [LabResultStatusOriginalDescription]        NVARCHAR (500)     NULL,
    [LabResultStatusNullFlavorIndicator]        BIT                NULL,
    PRIMARY KEY CLUSTERED ([LabTestKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

















