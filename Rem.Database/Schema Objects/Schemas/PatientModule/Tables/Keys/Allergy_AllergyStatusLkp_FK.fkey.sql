ALTER TABLE [PatientModule].[Allergy]
    ADD CONSTRAINT [Allergy_AllergyStatusLkp_FK] FOREIGN KEY ([AllergyStatusLkpKey]) REFERENCES [PatientModule].[AllergyStatusLkp] ([AllergyStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

