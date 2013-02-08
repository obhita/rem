ALTER TABLE [AgencyModule].[AgencyPhone]
    ADD CONSTRAINT [AgencyPhone_AgencyAddressAndPhone_FK] FOREIGN KEY ([AgencyAddressAndPhoneKey]) REFERENCES [AgencyModule].[AgencyAddressAndPhone] ([AgencyAddressAndPhoneKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

