ALTER TABLE [AgencyModule].[AgencyCharacteristic]
    ADD CONSTRAINT [AgencyCharacteristic_OrganizationCharacteristicLkp_FK] FOREIGN KEY ([OrganizationCharacteristicLkpKey]) REFERENCES [AgencyModule].[OrganizationCharacteristicLkp] ([OrganizationCharacteristicLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

