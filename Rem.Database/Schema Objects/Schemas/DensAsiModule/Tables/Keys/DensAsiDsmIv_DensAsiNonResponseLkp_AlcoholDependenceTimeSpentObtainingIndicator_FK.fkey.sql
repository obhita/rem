ALTER TABLE [DensAsiModule].[DensAsiDsmIv]
    ADD CONSTRAINT [DensAsiDsmIv_DensAsiNonResponseLkp_AlcoholDependenceTimeSpentObtainingIndicator_FK] FOREIGN KEY ([AlcoholDependenceTimeSpentObtainingIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

