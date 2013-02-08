ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiHasRelationshipOptionLkp_SexualPartnerDensAsiHasRelationshipOption_FK] FOREIGN KEY ([SexualPartnerDensAsiHasRelationshipOptionLkpKey]) REFERENCES [DensAsiModule].[DensAsiHasRelationshipOptionLkp] ([DensAsiHasRelationshipOptionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

