ALTER TABLE [AgencyModule].[StaffIdentifier]
    ADD CONSTRAINT [StaffIdentifier_StaffIdentifierTypeLkp_FK] FOREIGN KEY ([StaffIdentifierTypeLkpKey]) REFERENCES [AgencyModule].[StaffIdentifierTypeLkp] ([StaffIdentifierTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

