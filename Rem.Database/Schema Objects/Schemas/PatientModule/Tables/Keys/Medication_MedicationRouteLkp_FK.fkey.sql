ALTER TABLE [PatientModule].[Medication]
    ADD CONSTRAINT [Medication_MedicationRouteLkp_FK] FOREIGN KEY ([MedicationRouteLkpKey]) REFERENCES [PatientModule].[MedicationRouteLkp] ([MedicationRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

