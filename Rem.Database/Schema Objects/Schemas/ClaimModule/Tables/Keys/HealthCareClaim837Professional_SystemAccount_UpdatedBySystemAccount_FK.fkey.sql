ALTER TABLE [ClaimModule].[HealthCareClaim837Professional]
    ADD CONSTRAINT [HealthCareClaim837Professional_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

