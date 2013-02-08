CREATE TABLE [ClinicalCaseModule].[ClinicalCase] (
    [ClinicalCaseKey]              BIGINT             NOT NULL,
    [Version]                      INT                NOT NULL,
    [ClinicalCaseNumber]           BIGINT             NOT NULL,
    [ClinicalCaseNote]             NVARCHAR (MAX)     NULL,
    [CreatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]             DATETIMEOFFSET (7) NOT NULL,
    [PatientKey]                   BIGINT             NOT NULL,
    [ClinicalCaseStatusLkpKey]     BIGINT             NULL,
    [CreatedBySystemAccountKey]    BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]    BIGINT             NOT NULL,
    [ClinicalCaseStartDate]        DATE               NULL,
    [PatientPresentingProblemNote] NVARCHAR (MAX)     NULL,
    [InitialLocationKey]           BIGINT             NULL,
    [PerformedByStaffKey]          BIGINT             NULL,
    [ReferralTypeLkpKey]           BIGINT             NULL,
    [InitialContactMethodLkpKey]   BIGINT             NULL,
    [ClinicalCaseCloseDate]        DATE               NULL,
    [ClinicalCaseClosingNote]      NVARCHAR (MAX)     NULL,
    [AdmissionDate]                DATE               NULL,
    [AdmissionNote]                NVARCHAR (MAX)     NULL,
    [AdmittedByStaffKey]           BIGINT             NULL,
    [DischargeDate]                DATE               NULL,
    [DischargeNote]                NVARCHAR (MAX)     NULL,
    [DischargeReasonLkpKey]        BIGINT             NULL,
    [DischargedByStaffKey]         BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([ClinicalCaseKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);



























