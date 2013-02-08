ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiNonResponseLkp_ProblemsMotherInLastThirtyDaysIndicator_FK] FOREIGN KEY ([ProblemsMotherInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

