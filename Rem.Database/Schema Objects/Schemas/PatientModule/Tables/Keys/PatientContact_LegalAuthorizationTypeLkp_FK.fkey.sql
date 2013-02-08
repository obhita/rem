ALTER TABLE [PatientModule].[PatientContact]
    ADD CONSTRAINT [PatientContact_LegalAuthorizationTypeLkp_FK] FOREIGN KEY ([LegalAuthorizationTypeLkpKey]) REFERENCES [PatientModule].[LegalAuthorizationTypeLkp] ([LegalAuthorizationTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

