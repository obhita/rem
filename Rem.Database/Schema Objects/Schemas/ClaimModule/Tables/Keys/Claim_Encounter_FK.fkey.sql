ALTER TABLE [ClaimModule].[Claim]
    ADD CONSTRAINT [Claim_Encounter_FK] FOREIGN KEY ([EncounterKey]) REFERENCES [EncounterModule].[Encounter] ([EncounterKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

