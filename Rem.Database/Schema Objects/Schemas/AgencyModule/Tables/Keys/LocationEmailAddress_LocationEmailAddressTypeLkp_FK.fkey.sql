ALTER TABLE [AgencyModule].[LocationEmailAddress]
    ADD CONSTRAINT [LocationEmailAddress_LocationEmailAddressTypeLkp_FK] FOREIGN KEY ([LocationEmailAddressTypeLkpKey]) REFERENCES [AgencyModule].[LocationEmailAddressTypeLkp] ([LocationEmailAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

