ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiHasParentalRelationshipOptionLkp_FatherDensAsiHasParentalRelationshipOption_FK] FOREIGN KEY ([FatherDensAsiHasParentalRelationshipOptionLkpKey]) REFERENCES [DensAsiModule].[DensAsiHasParentalRelationshipOptionLkp] ([DensAsiHasParentalRelationshipOptionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

