ALTER TABLE [AgencyModule].[AgencyAlias]
    ADD CONSTRAINT [AgencyAlias_Agency_FK] FOREIGN KEY ([AgencyKey]) REFERENCES [AgencyModule].[Agency] ([AgencyKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

