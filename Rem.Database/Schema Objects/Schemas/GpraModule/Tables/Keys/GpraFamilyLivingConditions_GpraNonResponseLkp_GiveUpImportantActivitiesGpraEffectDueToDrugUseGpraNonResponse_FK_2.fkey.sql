ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraNonResponseLkp_GiveUpImportantActivitiesGpraEffectDueToDrugUseGpraNonResponse_FK] FOREIGN KEY ([GiveUpImportantActivitiesGpraEffectDueToDrugUseGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

