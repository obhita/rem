ALTER TABLE [AgencyModule].[LocationAddressAndPhone]
    ADD CONSTRAINT [LocationAddressAndPhone_Location_FK] FOREIGN KEY ([LocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

