CREATE TABLE [AgencyModule].[LocationFrequentlyAskedQuestion] (
    [LocationFrequentlyAskedQuestionKey] BIGINT             NOT NULL,
    [Version]                            INT                NOT NULL,
    [QuestionNote]                       NVARCHAR (MAX)     NOT NULL,
    [AnswerNote]                         NVARCHAR (MAX)     NOT NULL,
    [CreatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                   DATETIMEOFFSET (7) NOT NULL,
    [LocationKey]                        BIGINT             NOT NULL,
    [CreatedBySystemAccountKey]          BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]          BIGINT             NOT NULL,
    PRIMARY KEY CLUSTERED ([LocationFrequentlyAskedQuestionKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);







