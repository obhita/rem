ALTER TABLE [PatientModule].[PatientDocumentTypeLkp]
    ADD CONSTRAINT [PatientDocumentTypeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

