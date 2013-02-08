ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_CountryLkp_CitizenshipCountry_FK] FOREIGN KEY ([CitizenshipCountryLkpKey]) REFERENCES [CommonModule].[CountryLkp] ([CountryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

