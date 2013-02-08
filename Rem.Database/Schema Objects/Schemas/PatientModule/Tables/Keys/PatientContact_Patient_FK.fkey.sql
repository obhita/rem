ALTER TABLE [PatientModule].[PatientContact]
    ADD CONSTRAINT [PatientContact_Patient_FK] FOREIGN KEY ([PatientKey]) REFERENCES [PatientModule].[Patient] ([PatientKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

