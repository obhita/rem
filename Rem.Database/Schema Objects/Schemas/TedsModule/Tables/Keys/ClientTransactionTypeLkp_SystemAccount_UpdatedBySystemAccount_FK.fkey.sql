ALTER TABLE [TedsModule].[ClientTransactionTypeLkp]
    ADD CONSTRAINT [ClientTransactionTypeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

