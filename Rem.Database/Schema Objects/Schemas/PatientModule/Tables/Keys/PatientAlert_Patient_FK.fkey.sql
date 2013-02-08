ALTER TABLE [PatientModule].[PatientAlert]
    ADD CONSTRAINT [PatientAlert_Patient_FK] FOREIGN KEY ([PatientKey]) REFERENCES [PatientModule].[Patient] ([PatientKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

