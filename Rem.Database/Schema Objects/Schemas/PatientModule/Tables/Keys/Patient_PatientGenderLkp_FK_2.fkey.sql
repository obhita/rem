ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_PatientGenderLkp_FK] FOREIGN KEY ([PatientGenderLkpKey]) REFERENCES [PatientModule].[PatientGenderLkp] ([PatientGenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

