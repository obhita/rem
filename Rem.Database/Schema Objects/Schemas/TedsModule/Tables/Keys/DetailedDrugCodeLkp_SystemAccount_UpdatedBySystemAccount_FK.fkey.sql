ALTER TABLE [TedsModule].[DetailedDrugCodeLkp]
    ADD CONSTRAINT [DetailedDrugCodeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

