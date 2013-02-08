ALTER TABLE [PatientModule].[Medication]
    ADD CONSTRAINT [Medication_MedicationDoseUnitLkp_FK] FOREIGN KEY ([MedicationDoseUnitLkpKey]) REFERENCES [PatientModule].[MedicationDoseUnitLkp] ([MedicationDoseUnitLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

