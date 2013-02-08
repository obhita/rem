ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkp_FK] FOREIGN KEY ([ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkpKey]) REFERENCES [TedsModule].[ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkp] ([ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

