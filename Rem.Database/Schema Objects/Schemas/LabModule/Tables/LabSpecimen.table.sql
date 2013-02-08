CREATE TABLE [LabModule].[LabSpecimen] (
    [ActivityKey]                       BIGINT         NOT NULL,
    [LabReceivedDate]                   DATE           NULL,
    [TestNotCompletedReasonDescription] NVARCHAR (500) NULL,
    [CollectedHereIndicator]            BIT            NULL,
    [LabSpecimenTypeLkpKey]             BIGINT         NULL,
    [LabFacilityName]                   NVARCHAR (100) NULL,
    [LabFacilityStreetAddress]          NVARCHAR (255) NULL,
    [LabFacilityCityName]               NVARCHAR (100) NULL,
    [LabFacilityStateName]              NVARCHAR (100) NULL,
    [LabFacilityPostalCode]             NVARCHAR (10)  NULL,
    PRIMARY KEY CLUSTERED ([ActivityKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);











