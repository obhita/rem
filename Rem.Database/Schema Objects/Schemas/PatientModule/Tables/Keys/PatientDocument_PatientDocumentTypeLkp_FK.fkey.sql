ALTER TABLE [PatientModule].[PatientDocument]
    ADD CONSTRAINT [PatientDocument_PatientDocumentTypeLkp_FK] FOREIGN KEY ([PatientDocumentTypeLkpKey]) REFERENCES [PatientModule].[PatientDocumentTypeLkp] ([PatientDocumentTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

