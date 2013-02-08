ALTER TABLE [AgencyModule].[StaffCheckListItemTypeLkp]
    ADD CONSTRAINT [StaffChecklistItemTypeLkp_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

