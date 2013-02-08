ALTER TABLE [PatientModule].[PatientDisability]
    ADD CONSTRAINT [PatientDisability_Patient_FK] FOREIGN KEY ([PatientKey]) REFERENCES [PatientModule].[Patient] ([PatientKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

