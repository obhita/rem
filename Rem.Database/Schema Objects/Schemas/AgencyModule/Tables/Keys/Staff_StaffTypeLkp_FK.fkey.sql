ALTER TABLE [AgencyModule].[Staff]
    ADD CONSTRAINT [Staff_StaffTypeLkp_FK] FOREIGN KEY ([StaffTypeLkpKey]) REFERENCES [AgencyModule].[StaffTypeLkp] ([StaffTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

