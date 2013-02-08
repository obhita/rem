ALTER TABLE [AgencyModule].[Location]
    ADD CONSTRAINT [Location_Agency_FK] FOREIGN KEY ([AgencyKey]) REFERENCES [AgencyModule].[Agency] ([AgencyKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

