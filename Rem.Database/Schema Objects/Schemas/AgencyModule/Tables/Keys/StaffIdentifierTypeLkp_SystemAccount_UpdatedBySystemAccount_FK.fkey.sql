ALTER TABLE [AgencyModule].[StaffIdentifierTypeLkp]
    ADD CONSTRAINT [StaffIdentifierTypeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

