ALTER TABLE [PayorModule].[PayorType]
    ADD CONSTRAINT [PayorType_CountryLkp_BillingCountry_FK] FOREIGN KEY ([BillingCountryLkpKey]) REFERENCES [CommonModule].[CountryLkp] ([CountryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

