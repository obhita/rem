ALTER TABLE [PatientModule].[PatientContactContactType]
    ADD CONSTRAINT [PatientContactContactType_PatientContact_FK] FOREIGN KEY ([PatientContactKey]) REFERENCES [PatientModule].[PatientContact] ([PatientContactKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

