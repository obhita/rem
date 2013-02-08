ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiHasRelationshipOptionLkp_BrotherSisterDensAsiHasRelationshipOption_FK] FOREIGN KEY ([BrotherSisterDensAsiHasRelationshipOptionLkpKey]) REFERENCES [DensAsiModule].[DensAsiHasRelationshipOptionLkp] ([DensAsiHasRelationshipOptionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

