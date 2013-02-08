ALTER TABLE [PatientAccountModule].[PatientAccount]
    ADD CONSTRAINT [PatientAccount_CountryLkp_HomeCountry_FK] FOREIGN KEY ([HomeCountryLkpKey]) REFERENCES [CommonModule].[CountryLkp] ([CountryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

