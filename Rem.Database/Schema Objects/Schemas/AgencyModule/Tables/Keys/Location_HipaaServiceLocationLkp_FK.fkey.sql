ALTER TABLE [AgencyModule].[Location]
    ADD CONSTRAINT [Location_HipaaServiceLocationLkp_FK] FOREIGN KEY ([HipaaServiceLocationLkpKey]) REFERENCES [AgencyModule].[HipaaServiceLocationLkp] ([HipaaServiceLocationLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

