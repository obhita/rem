ALTER TABLE [PatientModule].[AllergyReaction]
    ADD CONSTRAINT [AllergyReaction_Allergy_FK] FOREIGN KEY ([AllergyKey]) REFERENCES [PatientModule].[Allergy] ([AllergyKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

