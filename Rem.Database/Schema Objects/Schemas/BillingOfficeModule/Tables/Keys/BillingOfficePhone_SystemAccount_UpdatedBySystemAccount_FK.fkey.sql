ALTER TABLE [BillingOfficeModule].[BillingOfficePhone]
    ADD CONSTRAINT [BillingOfficePhone_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

