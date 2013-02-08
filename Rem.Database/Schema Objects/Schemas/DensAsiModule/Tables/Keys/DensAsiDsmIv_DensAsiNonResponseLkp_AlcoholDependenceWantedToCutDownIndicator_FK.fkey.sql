ALTER TABLE [DensAsiModule].[DensAsiDsmIv]
    ADD CONSTRAINT [DensAsiDsmIv_DensAsiNonResponseLkp_AlcoholDependenceWantedToCutDownIndicator_FK] FOREIGN KEY ([AlcoholDependenceWantedToCutDownIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

