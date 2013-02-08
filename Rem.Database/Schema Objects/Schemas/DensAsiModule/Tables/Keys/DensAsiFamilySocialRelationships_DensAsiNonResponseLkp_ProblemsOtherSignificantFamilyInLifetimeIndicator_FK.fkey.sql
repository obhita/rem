ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiNonResponseLkp_ProblemsOtherSignificantFamilyInLifetimeIndicator_FK] FOREIGN KEY ([ProblemsOtherSignificantFamilyInLifetimeIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

