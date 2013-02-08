ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiHasRelationshipOptionLkp_FriendsDensAsiHasRelationshipOption_FK] FOREIGN KEY ([FriendsDensAsiHasRelationshipOptionLkpKey]) REFERENCES [DensAsiModule].[DensAsiHasRelationshipOptionLkp] ([DensAsiHasRelationshipOptionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

