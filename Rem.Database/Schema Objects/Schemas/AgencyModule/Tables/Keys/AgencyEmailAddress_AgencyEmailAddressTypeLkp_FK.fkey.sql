ALTER TABLE [AgencyModule].[AgencyEmailAddress]
    ADD CONSTRAINT [AgencyEmailAddress_AgencyEmailAddressTypeLkp_FK] FOREIGN KEY ([AgencyEmailAddressTypeLkpKey]) REFERENCES [AgencyModule].[AgencyEmailAddressTypeLkp] ([AgencyEmailAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

