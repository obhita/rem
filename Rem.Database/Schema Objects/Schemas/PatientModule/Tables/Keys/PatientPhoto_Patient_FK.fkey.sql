ALTER TABLE [PatientModule].[PatientPhoto]
    ADD CONSTRAINT [PatientPhoto_Patient_FK] FOREIGN KEY ([PatientKey]) REFERENCES [PatientModule].[Patient] ([PatientKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

