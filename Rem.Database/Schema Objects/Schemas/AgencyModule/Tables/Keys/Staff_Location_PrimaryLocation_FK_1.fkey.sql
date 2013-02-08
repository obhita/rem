ALTER TABLE [AgencyModule].[Staff]
    ADD CONSTRAINT [Staff_Location_PrimaryLocation_FK] FOREIGN KEY ([PrimaryLocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

