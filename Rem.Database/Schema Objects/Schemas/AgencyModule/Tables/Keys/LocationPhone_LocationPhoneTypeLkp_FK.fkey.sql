ALTER TABLE [AgencyModule].[LocationPhone]
    ADD CONSTRAINT [LocationPhone_LocationPhoneTypeLkp_FK] FOREIGN KEY ([LocationPhoneTypeLkpKey]) REFERENCES [AgencyModule].[LocationPhoneTypeLkp] ([LocationPhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

