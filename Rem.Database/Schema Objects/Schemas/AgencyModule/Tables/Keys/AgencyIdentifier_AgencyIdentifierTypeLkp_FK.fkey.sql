ALTER TABLE [AgencyModule].[AgencyIdentifier]
    ADD CONSTRAINT [AgencyIdentifier_AgencyIdentifierTypeLkp_FK] FOREIGN KEY ([AgencyIdentifierTypeLkpKey]) REFERENCES [AgencyModule].[AgencyIdentifierTypeLkp] ([AgencyIdentifierTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

