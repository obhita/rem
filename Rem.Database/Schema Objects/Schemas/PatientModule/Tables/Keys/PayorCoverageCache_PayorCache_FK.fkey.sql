ALTER TABLE [PatientModule].[PayorCoverageCache]
    ADD CONSTRAINT [PayorCoverageCache_PayorCache_FK] FOREIGN KEY ([PayorCacheKey]) REFERENCES [PatientModule].[PayorCache] ([PayorCacheKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

