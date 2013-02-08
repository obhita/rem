ALTER TABLE [PatientModule].[PatientAddress]
    ADD CONSTRAINT [PatientAddress_CountryLkp_FK] FOREIGN KEY ([CountryLkpKey]) REFERENCES [CommonModule].[CountryLkp] ([CountryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

