ALTER TABLE [AgencyModule].[LocationIdentifier]
    ADD CONSTRAINT [LocationIdentifier_LocationIdentifierTypeLkp_FK] FOREIGN KEY ([LocationIdentifierTypeLkpKey]) REFERENCES [AgencyModule].[LocationIdentifierTypeLkp] ([LocationIdentifierTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

