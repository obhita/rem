ALTER TABLE [EncounterModule].[Service]
    ADD CONSTRAINT [Service_Encounter_FK] FOREIGN KEY ([EncounterKey]) REFERENCES [EncounterModule].[Encounter] ([EncounterKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

