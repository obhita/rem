ALTER TABLE [DensAsiModule].[DensAsiClosure]
    ADD CONSTRAINT [DensAsiClosure_DensAsiIncompleteInterviewReasonLkp_FK] FOREIGN KEY ([DensAsiIncompleteInterviewReasonLkpKey]) REFERENCES [DensAsiModule].[DensAsiIncompleteInterviewReasonLkp] ([DensAsiIncompleteInterviewReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

