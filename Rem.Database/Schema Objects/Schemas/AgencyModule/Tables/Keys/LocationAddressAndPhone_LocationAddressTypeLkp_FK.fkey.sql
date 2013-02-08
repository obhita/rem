ALTER TABLE [AgencyModule].[LocationAddressAndPhone]
    ADD CONSTRAINT [LocationAddressAndPhone_LocationAddressTypeLkp_FK] FOREIGN KEY ([LocationAddressTypeLkpKey]) REFERENCES [AgencyModule].[LocationAddressTypeLkp] ([LocationAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

