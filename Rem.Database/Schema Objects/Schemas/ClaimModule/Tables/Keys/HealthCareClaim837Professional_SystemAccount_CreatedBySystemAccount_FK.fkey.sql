ALTER TABLE [ClaimModule].[HealthCareClaim837Professional]
    ADD CONSTRAINT [HealthCareClaim837Professional_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

