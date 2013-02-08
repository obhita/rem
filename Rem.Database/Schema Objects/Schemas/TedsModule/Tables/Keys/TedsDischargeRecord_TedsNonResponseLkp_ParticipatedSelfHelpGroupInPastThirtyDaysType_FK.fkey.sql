ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsNonResponseLkp_ParticipatedSelfHelpGroupInPastThirtyDaysType_FK] FOREIGN KEY ([ParticipatedSelfHelpGroupInPastThirtyDaysTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

