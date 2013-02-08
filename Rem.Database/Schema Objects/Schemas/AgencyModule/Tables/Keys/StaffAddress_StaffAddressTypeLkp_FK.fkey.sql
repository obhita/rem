ALTER TABLE [AgencyModule].[StaffAddress]
    ADD CONSTRAINT [StaffAddress_StaffAddressTypeLkp_FK] FOREIGN KEY ([StaffAddressTypeLkpKey]) REFERENCES [AgencyModule].[StaffAddressTypeLkp] ([StaffAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

