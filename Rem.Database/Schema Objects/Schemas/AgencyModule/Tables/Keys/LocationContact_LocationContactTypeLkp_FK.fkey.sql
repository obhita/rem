ALTER TABLE [AgencyModule].[LocationContact]
    ADD CONSTRAINT [LocationContact_LocationContactTypeLkp_FK] FOREIGN KEY ([LocationContactTypeLkpKey]) REFERENCES [AgencyModule].[LocationContactTypeLkp] ([LocationContactTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

