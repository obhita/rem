ALTER TABLE [EncounterModule].[Encounter]
    ADD CONSTRAINT [Encounter_Location_ServiceLocation_FK] FOREIGN KEY ([ServiceLocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

