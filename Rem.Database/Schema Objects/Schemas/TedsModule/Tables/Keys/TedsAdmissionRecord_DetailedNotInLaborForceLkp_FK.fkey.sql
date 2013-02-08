ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_DetailedNotInLaborForceLkp_FK] FOREIGN KEY ([DetailedNotInLaborForceLkpKey]) REFERENCES [TedsModule].[DetailedNotInLaborForceLkp] ([DetailedNotInLaborForceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

