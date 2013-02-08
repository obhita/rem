ALTER TABLE [BillingOfficeModule].[BillingOffice]
    ADD CONSTRAINT [BillingOffice_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

