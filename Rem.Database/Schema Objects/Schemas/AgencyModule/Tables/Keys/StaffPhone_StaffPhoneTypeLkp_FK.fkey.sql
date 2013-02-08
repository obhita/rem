ALTER TABLE [AgencyModule].[StaffPhone]
    ADD CONSTRAINT [StaffPhone_StaffPhoneTypeLkp_FK] FOREIGN KEY ([StaffPhoneTypeLkpKey]) REFERENCES [AgencyModule].[StaffPhoneTypeLkp] ([StaffPhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

