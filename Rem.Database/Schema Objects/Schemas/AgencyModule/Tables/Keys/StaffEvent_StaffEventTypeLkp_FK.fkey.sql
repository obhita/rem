ALTER TABLE [AgencyModule].[StaffEvent]
    ADD CONSTRAINT [StaffEvent_StaffEventTypeLkp_FK] FOREIGN KEY ([StaffEventTypeLkpKey]) REFERENCES [AgencyModule].[StaffEventTypeLkp] ([StaffEventTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

