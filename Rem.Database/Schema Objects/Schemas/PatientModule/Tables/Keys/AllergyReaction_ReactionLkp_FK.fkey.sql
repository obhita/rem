ALTER TABLE [PatientModule].[AllergyReaction]
    ADD CONSTRAINT [AllergyReaction_ReactionLkp_FK] FOREIGN KEY ([ReactionLkpKey]) REFERENCES [PatientModule].[ReactionLkp] ([ReactionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

