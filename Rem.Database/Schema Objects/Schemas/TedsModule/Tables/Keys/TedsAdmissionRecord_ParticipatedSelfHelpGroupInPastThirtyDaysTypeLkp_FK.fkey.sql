ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkp_FK] FOREIGN KEY ([ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkpKey]) REFERENCES [TedsModule].[ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkp] ([ParticipatedSelfHelpGroupInPastThirtyDaysTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

