ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiNonResponseLkp_ArrestedChargedArsonCount_FK] FOREIGN KEY ([ArrestedChargedArsonCountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

