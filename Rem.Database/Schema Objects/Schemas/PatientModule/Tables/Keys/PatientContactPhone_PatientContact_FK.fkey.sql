ALTER TABLE [PatientModule].[PatientContactPhone]
    ADD CONSTRAINT [PatientContactPhone_PatientContact_FK] FOREIGN KEY ([PatientContactKey]) REFERENCES [PatientModule].[PatientContact] ([PatientContactKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

