ALTER TABLE [PatientModule].[PatientContactContactType]
    ADD CONSTRAINT [PatientContactContactType_PatientContactTypeLkp_FK] FOREIGN KEY ([PatientContactTypeLkpKey]) REFERENCES [PatientModule].[PatientContactTypeLkp] ([PatientContactTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

