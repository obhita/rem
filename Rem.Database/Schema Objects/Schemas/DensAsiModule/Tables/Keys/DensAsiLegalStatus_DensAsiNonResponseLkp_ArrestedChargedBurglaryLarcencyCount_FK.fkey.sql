ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiNonResponseLkp_ArrestedChargedBurglaryLarcencyCount_FK] FOREIGN KEY ([ArrestedChargedBurglaryLarcencyCountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

