ALTER TABLE [GpraModule].[GpraPlannedServices]
    ADD CONSTRAINT [GpraPlannedServices_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

