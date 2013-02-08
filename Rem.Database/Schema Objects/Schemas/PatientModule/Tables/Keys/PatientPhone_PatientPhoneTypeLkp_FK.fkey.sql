ALTER TABLE [PatientModule].[PatientPhone]
    ADD CONSTRAINT [PatientPhone_PatientPhoneTypeLkp_FK] FOREIGN KEY ([PatientPhoneTypeLkpKey]) REFERENCES [PatientModule].[PatientPhoneTypeLkp] ([PatientPhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

