ALTER TABLE [GpraModule].[GpraProblemsTreatmentRecovery]
    ADD CONSTRAINT [GpraProblemsTreatmentRecovery_GpraOverallHealthLkp_FK] FOREIGN KEY ([GpraOverallHealthLkpKey]) REFERENCES [GpraModule].[GpraOverallHealthLkp] ([GpraOverallHealthLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

