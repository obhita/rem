ALTER TABLE [AgencyModule].[OrganizationCharacteristicCategoryLkp]
    ADD CONSTRAINT [OrganizationCharacteristicCategoryLkp_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

