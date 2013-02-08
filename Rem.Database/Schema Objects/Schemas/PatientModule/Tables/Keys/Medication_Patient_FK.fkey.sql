ALTER TABLE [PatientModule].[Medication]
    ADD CONSTRAINT [Medication_Patient_FK] FOREIGN KEY ([PatientKey]) REFERENCES [PatientModule].[Patient] ([PatientKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

