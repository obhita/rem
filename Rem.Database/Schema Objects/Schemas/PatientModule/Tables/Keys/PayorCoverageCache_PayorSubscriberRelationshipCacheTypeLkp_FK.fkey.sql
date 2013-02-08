ALTER TABLE [PatientModule].[PayorCoverageCache]
    ADD CONSTRAINT [PayorCoverageCache_PayorSubscriberRelationshipCacheTypeLkp_FK] FOREIGN KEY ([PayorSubscriberRelationshipCacheTypeLkpKey]) REFERENCES [PatientModule].[PayorSubscriberRelationshipCacheTypeLkp] ([PayorSubscriberRelationshipCacheTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

