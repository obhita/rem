ALTER TABLE [AgencyModule].[StaffChecklistItem]
    ADD CONSTRAINT [StaffChecklistItem_StaffChecklistItemTypeLkp_FK] FOREIGN KEY ([StaffChecklistItemTypeLkpKey]) REFERENCES [AgencyModule].[StaffChecklistItemTypeLkp] ([StaffChecklistItemTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

