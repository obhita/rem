ALTER TABLE [AgencyModule].[StaffChecklistItem]
    ADD CONSTRAINT [StaffChecklistItem_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

