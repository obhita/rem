ALTER TABLE [AgencyModule].[LocationCharacteristic]
    ADD CONSTRAINT [LocationCharacteristic_OrganizationCharacteristicLkp_FK] FOREIGN KEY ([OrganizationCharacteristicLkpKey]) REFERENCES [AgencyModule].[OrganizationCharacteristicLkp] ([OrganizationCharacteristicLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

