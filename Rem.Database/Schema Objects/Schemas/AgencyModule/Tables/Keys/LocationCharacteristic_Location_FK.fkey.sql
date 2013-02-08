ALTER TABLE [AgencyModule].[LocationCharacteristic]
    ADD CONSTRAINT [LocationCharacteristic_Location_FK] FOREIGN KEY ([LocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

