ALTER TABLE [PatientModule].[PatientContactPhone]
    ADD CONSTRAINT [PatientContactPhone_PatientContactPhoneTypeLkp_FK] FOREIGN KEY ([PatientContactPhoneTypeLkpKey]) REFERENCES [PatientModule].[PatientContactPhoneTypeLkp] ([PatientContactPhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

