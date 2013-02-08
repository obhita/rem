ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiNonResponseLkp_ArrestedChargedProstitutionCount_FK] FOREIGN KEY ([ArrestedChargedProstitutionCountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

