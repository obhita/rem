ALTER TABLE [AgencyModule].[AgencyPhone]
    ADD CONSTRAINT [AgencyPhone_AgencyPhoneTypeLkp_FK] FOREIGN KEY ([AgencyPhoneTypeLkpKey]) REFERENCES [AgencyModule].[AgencyPhoneTypeLkp] ([AgencyPhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

