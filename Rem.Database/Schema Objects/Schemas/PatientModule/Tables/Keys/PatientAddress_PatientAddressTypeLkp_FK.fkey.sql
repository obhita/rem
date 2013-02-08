ALTER TABLE [PatientModule].[PatientAddress]
    ADD CONSTRAINT [PatientAddress_PatientAddressTypeLkp_FK] FOREIGN KEY ([PatientAddressTypeLkpKey]) REFERENCES [PatientModule].[PatientAddressTypeLkp] ([PatientAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

