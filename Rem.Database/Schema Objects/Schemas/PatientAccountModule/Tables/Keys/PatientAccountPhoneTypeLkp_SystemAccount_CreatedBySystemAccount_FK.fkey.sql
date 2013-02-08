ALTER TABLE [PatientAccountModule].[PatientAccountPhoneTypeLkp]
    ADD CONSTRAINT [PatientAccountPhoneTypeLkp_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

