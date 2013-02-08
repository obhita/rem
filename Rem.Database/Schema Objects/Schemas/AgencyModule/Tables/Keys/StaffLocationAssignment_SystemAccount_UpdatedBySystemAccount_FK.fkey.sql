ALTER TABLE [AgencyModule].[StaffLocationAssignment]
    ADD CONSTRAINT [StaffLocationAssignment_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

