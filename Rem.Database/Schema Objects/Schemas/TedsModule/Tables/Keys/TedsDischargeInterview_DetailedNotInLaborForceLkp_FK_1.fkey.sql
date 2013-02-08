ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_DetailedNotInLaborForceLkp_FK] FOREIGN KEY ([DetailedNotInLaborForceLkpKey]) REFERENCES [TedsModule].[DetailedNotInLaborForceLkp] ([DetailedNotInLaborForceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

