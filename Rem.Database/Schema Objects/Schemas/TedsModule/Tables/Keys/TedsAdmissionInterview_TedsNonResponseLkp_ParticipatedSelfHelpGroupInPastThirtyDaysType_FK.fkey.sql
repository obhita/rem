ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsNonResponseLkp_ParticipatedSelfHelpGroupInPastThirtyDaysType_FK] FOREIGN KEY ([ParticipatedSelfHelpGroupInPastThirtyDaysTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

