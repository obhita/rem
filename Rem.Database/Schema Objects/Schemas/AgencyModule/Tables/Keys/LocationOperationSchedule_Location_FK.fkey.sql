ALTER TABLE [AgencyModule].[LocationOperationSchedule]
    ADD CONSTRAINT [LocationOperationSchedule_Location_FK] FOREIGN KEY ([LocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

