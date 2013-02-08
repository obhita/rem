ALTER TABLE [PatientAccountModule].[PatientAccountPhone]
    ADD CONSTRAINT [PatientAccountPhone_PatientAccountPhoneTypeLkp_FK] FOREIGN KEY ([PatientAccountPhoneTypeLkpKey]) REFERENCES [PatientAccountModule].[PatientAccountPhoneTypeLkp] ([PatientAccountPhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

