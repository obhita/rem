ALTER TABLE [GpraModule].[GpraProblemsTreatmentRecovery]
    ADD CONSTRAINT [GpraProblemsTreatmentRecovery_GpraNonResponseLkp_GpraSexualActivityGpraNonResponse_FK] FOREIGN KEY ([GpraSexualActivityGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

