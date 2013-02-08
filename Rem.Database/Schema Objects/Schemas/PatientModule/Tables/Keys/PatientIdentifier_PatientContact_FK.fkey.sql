ALTER TABLE [PatientModule].[PatientIdentifier]
    ADD CONSTRAINT [PatientIdentifier_PatientContact_FK] FOREIGN KEY ([PatientContactKey]) REFERENCES [PatientModule].[PatientContact] ([PatientContactKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

