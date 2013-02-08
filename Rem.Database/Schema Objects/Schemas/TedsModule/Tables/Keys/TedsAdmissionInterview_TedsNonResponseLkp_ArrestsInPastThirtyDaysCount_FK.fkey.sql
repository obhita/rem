ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsNonResponseLkp_ArrestsInPastThirtyDaysCount_FK] FOREIGN KEY ([ArrestsInPastThirtyDaysCountTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

