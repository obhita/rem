ALTER TABLE [PatientModule].[PatientAlias]
    ADD CONSTRAINT [PatientAlias_PatientAliasTypeLkp_FK] FOREIGN KEY ([PatientAliasTypeLkpKey]) REFERENCES [PatientModule].[PatientAliasTypeLkp] ([PatientAliasTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

