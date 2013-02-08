ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraEffectDueToDrugUseLkp_GiveUpImportantActivitiesGpraEffectDueToDrugUse_FK] FOREIGN KEY ([GiveUpImportantActivitiesGpraEffectDueToDrugUseLkpKey]) REFERENCES [GpraModule].[GpraEffectDueToDrugUseLkp] ([GpraEffectDueToDrugUseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

