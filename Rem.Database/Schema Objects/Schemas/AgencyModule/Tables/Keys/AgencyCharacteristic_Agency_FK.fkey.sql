ALTER TABLE [AgencyModule].[AgencyCharacteristic]
    ADD CONSTRAINT [AgencyCharacteristic_Agency_FK] FOREIGN KEY ([AgencyKey]) REFERENCES [AgencyModule].[Agency] ([AgencyKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

