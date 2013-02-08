ALTER TABLE [AgencyModule].[Agency]
    ADD CONSTRAINT [Agency_AgencyTypeLkp_FK] FOREIGN KEY ([AgencyTypeLkpKey]) REFERENCES [AgencyModule].[AgencyTypeLkp] ([AgencyTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

