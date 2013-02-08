ALTER TABLE [PatientModule].[PatientPhotoTypeLkp]
    ADD CONSTRAINT [PatientPhotoTypeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

