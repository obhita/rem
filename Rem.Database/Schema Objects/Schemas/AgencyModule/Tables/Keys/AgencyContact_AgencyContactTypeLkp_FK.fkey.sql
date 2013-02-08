ALTER TABLE [AgencyModule].[AgencyContact]
    ADD CONSTRAINT [AgencyContact_AgencyContactTypeLkp_FK] FOREIGN KEY ([AgencyContactTypeLkpKey]) REFERENCES [AgencyModule].[AgencyContactTypeLkp] ([AgencyContactTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

