ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiNonResponseLkp_LivingWithAnyoneWhoHasAlcoholProblemIndicator_FK] FOREIGN KEY ([LivingWithAnyoneWhoHasAlcoholProblemIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

