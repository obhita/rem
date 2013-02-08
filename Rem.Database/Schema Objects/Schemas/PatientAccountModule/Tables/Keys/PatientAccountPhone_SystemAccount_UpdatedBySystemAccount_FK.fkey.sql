ALTER TABLE [PatientAccountModule].[PatientAccountPhone]
    ADD CONSTRAINT [PatientAccountPhone_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

