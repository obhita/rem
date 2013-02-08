ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiInterview_FK] FOREIGN KEY ([DensAsiInterviewKey]) REFERENCES [DensAsiModule].[DensAsiInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

