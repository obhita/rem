ALTER TABLE [PatientModule].[Medication]
    ADD CONSTRAINT [Medication_MedicationStatusLkp_FK] FOREIGN KEY ([MedicationStatusLkpKey]) REFERENCES [PatientModule].[MedicationStatusLkp] ([MedicationStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

