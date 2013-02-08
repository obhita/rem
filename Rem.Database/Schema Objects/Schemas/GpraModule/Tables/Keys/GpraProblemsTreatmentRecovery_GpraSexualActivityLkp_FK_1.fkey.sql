ALTER TABLE [GpraModule].[GpraProblemsTreatmentRecovery]
    ADD CONSTRAINT [GpraProblemsTreatmentRecovery_GpraSexualActivityLkp_FK] FOREIGN KEY ([GpraSexualActivityLkpKey]) REFERENCES [GpraModule].[GpraSexualActivityLkp] ([GpraSexualActivityLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

