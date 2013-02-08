ALTER TABLE [AgencyModule].[AgencyIdentifierTypeLkp]
    ADD CONSTRAINT [AgencyIdentifierTypeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

