ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsNonResponseLkp_ArrestsInPastThirtyDaysCount_FK] FOREIGN KEY ([ArrestsInPastThirtyDaysCountTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

