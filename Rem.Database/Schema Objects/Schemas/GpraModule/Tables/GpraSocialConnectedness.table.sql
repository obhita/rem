CREATE TABLE [GpraModule].[GpraSocialConnectedness] (
    [GpraInterviewKey]                                    BIGINT             NOT NULL,
    [CreatedTimestamp]                                    DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                    DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                           BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                           BIGINT             NOT NULL,
    [GpraTroubleContactSpecificationNote]                 NVARCHAR (MAX)     NULL,
    [AttendVoluntaryGroupsIndicator]                      BIT                NULL,
    [AttendVoluntaryGroupsIndicatorGpraNonResponseLkpKey] BIGINT             NULL,
    [AttendVoluntaryGroupsCount]                          INT                NULL,
    [AttendVoluntaryGroupsCountGpraNonResponseLkpKey]     BIGINT             NULL,
    [AttendReligiousGroupsIndicator]                      BIT                NULL,
    [AttendReligiousGroupsIndicatorGpraNonResponseLkpKey] BIGINT             NULL,
    [AttendReligiousGroupsCount]                          INT                NULL,
    [AttendReligiousGroupsCountGpraNonResponseLkpKey]     BIGINT             NULL,
    [AttendOtherGroupsIndicator]                          BIT                NULL,
    [AttendOtherGroupsIndicatorGpraNonResponseLkpKey]     BIGINT             NULL,
    [AttendOtherGroupsCount]                              INT                NULL,
    [AttendOtherGroupsCountGpraNonResponseLkpKey]         BIGINT             NULL,
    [InteractFamilyFriendsIndicator]                      BIT                NULL,
    [InteractFamilyFriendsIndicatorGpraNonResponseLkpKey] BIGINT             NULL,
    [GpraTroubleContactLkpKey]                            BIGINT             NULL,
    [GpraTroubleContactGpraNonResponseLkpKey]             BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([GpraInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);









