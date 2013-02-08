ALTER TABLE [DensAsiModule].[DensAsiClosure]
    ADD CONSTRAINT [DensAsiClosure_DensAsiInterview_FK] FOREIGN KEY ([DensAsiInterviewKey]) REFERENCES [DensAsiModule].[DensAsiInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

