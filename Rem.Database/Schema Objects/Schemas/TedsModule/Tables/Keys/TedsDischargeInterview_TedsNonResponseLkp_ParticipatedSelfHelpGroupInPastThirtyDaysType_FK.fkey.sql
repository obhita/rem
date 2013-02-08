ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsNonResponseLkp_ParticipatedSelfHelpGroupInPastThirtyDaysType_FK] FOREIGN KEY ([ParticipatedSelfHelpGroupInPastThirtyDaysTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

