ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_DetailedNotInLaborForceLkp_FK] FOREIGN KEY ([DetailedNotInLaborForceLkpKey]) REFERENCES [TedsModule].[DetailedNotInLaborForceLkp] ([DetailedNotInLaborForceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



