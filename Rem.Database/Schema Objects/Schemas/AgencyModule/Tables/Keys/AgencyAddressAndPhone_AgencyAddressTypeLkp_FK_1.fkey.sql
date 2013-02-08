ALTER TABLE [AgencyModule].[AgencyAddressAndPhone]
    ADD CONSTRAINT [AgencyAddressAndPhone_AgencyAddressTypeLkp_FK] FOREIGN KEY ([AgencyAddressTypeLkpKey]) REFERENCES [AgencyModule].[AgencyAddressTypeLkp] ([AgencyAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

