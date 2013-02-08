ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiHasParentalRelationshipOptionLkp_MotherDensAsiHasParentalRelationshipOption_FK] FOREIGN KEY ([MotherDensAsiHasParentalRelationshipOptionLkpKey]) REFERENCES [DensAsiModule].[DensAsiHasParentalRelationshipOptionLkp] ([DensAsiHasParentalRelationshipOptionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

