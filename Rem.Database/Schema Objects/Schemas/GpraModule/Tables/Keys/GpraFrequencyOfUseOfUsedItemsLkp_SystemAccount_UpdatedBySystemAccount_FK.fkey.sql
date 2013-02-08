ALTER TABLE [GpraModule].[GpraFrequencyOfUseOfUsedItemsLkp]
    ADD CONSTRAINT [GpraFrequencyOfUseOfUsedItemsLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

