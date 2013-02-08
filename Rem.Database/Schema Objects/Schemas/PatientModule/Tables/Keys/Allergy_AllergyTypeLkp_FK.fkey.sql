ALTER TABLE [PatientModule].[Allergy]
    ADD CONSTRAINT [Allergy_AllergyTypeLkp_FK] FOREIGN KEY ([AllergyTypeLkpKey]) REFERENCES [PatientModule].[AllergyTypeLkp] ([AllergyTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

