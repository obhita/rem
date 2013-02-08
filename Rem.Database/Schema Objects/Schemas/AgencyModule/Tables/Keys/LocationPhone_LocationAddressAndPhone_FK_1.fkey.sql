ALTER TABLE [AgencyModule].[LocationPhone]
    ADD CONSTRAINT [LocationPhone_LocationAddressAndPhone_FK] FOREIGN KEY ([LocationAddressAndPhoneKey]) REFERENCES [AgencyModule].[LocationAddressAndPhone] ([LocationAddressAndPhoneKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

