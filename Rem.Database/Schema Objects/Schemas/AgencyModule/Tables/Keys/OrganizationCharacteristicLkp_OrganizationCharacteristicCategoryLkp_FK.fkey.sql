ALTER TABLE [AgencyModule].[OrganizationCharacteristicLkp]
    ADD CONSTRAINT [OrganizationCharacteristicLkp_OrganizationCharacteristicCategoryLkp_FK] FOREIGN KEY ([OrganizationCharacteristicCategoryLkpKey]) REFERENCES [AgencyModule].[OrganizationCharacteristicCategoryLkp] ([OrganizationCharacteristicCategoryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

