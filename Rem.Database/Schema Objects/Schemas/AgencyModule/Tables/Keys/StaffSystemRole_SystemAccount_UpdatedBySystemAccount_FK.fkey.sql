ALTER TABLE [AgencyModule].[StaffSystemRole]
    ADD CONSTRAINT [StaffSystemRole_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

