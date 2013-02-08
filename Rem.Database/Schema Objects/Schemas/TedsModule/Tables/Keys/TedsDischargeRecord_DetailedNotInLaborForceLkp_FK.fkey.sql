ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_DetailedNotInLaborForceLkp_FK] FOREIGN KEY ([DetailedNotInLaborForceLkpKey]) REFERENCES [TedsModule].[DetailedNotInLaborForceLkp] ([DetailedNotInLaborForceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

