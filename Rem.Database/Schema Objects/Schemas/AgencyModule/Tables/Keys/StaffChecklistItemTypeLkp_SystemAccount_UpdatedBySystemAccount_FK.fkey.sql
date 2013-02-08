ALTER TABLE [AgencyModule].[StaffCheckListItemTypeLkp]
    ADD CONSTRAINT [StaffChecklistItemTypeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

