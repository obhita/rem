ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiHasRelationshipOptionLkp_ChildrenDensAsiHasRelationshipOption_FK] FOREIGN KEY ([ChildrenDensAsiHasRelationshipOptionLkpKey]) REFERENCES [DensAsiModule].[DensAsiHasRelationshipOptionLkp] ([DensAsiHasRelationshipOptionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

