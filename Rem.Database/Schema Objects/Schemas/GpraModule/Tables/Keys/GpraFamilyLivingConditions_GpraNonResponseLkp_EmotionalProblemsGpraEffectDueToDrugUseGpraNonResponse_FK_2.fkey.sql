ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraNonResponseLkp_EmotionalProblemsGpraEffectDueToDrugUseGpraNonResponse_FK] FOREIGN KEY ([EmotionalProblemsGpraEffectDueToDrugUseGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

