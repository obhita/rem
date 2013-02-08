ALTER TABLE [PatientModule].[PayorCache]
    ADD CONSTRAINT [PayorCache_BillingOfficeCache_FK] FOREIGN KEY ([BillingOfficeCacheKey]) REFERENCES [PatientModule].[BillingOfficeCache] ([BillingOfficeCacheKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

