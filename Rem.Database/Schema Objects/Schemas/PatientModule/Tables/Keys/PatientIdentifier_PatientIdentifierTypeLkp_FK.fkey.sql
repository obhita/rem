ALTER TABLE [PatientModule].[PatientIdentifier]
    ADD CONSTRAINT [PatientIdentifier_PatientIdentifierTypeLkp_FK] FOREIGN KEY ([PatientIdentifierTypeLkpKey]) REFERENCES [PatientModule].[PatientIdentifierTypeLkp] ([PatientIdentifierTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

