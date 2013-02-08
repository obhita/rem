ALTER TABLE [AgencyModule].[LocationEmailAddress]
    ADD CONSTRAINT [LocationEmailAddress_Location_FK] FOREIGN KEY ([LocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

