ALTER TABLE [DensAsiModule].[DensAsiClosure]
    ADD CONSTRAINT [DensAsiClosure_DensAsiNonResponseLkp_DensAsiIncompleteInterviewReason_FK] FOREIGN KEY ([DensAsiIncompleteInterviewReasonDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

