ALTER TABLE [GpraModule].[GpraProblemsTreatmentRecovery]
    ADD CONSTRAINT [GpraProblemsTreatmentRecovery_GpraNonResponseLkp_OutpatientAlcoholSubstanceAbuseTimeCountGpraNonResponse_FK] FOREIGN KEY ([OutpatientAlcoholSubstanceAbuseTimeCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

