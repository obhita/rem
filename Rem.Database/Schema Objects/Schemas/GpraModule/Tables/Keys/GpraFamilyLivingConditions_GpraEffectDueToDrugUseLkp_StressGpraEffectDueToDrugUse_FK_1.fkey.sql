ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraEffectDueToDrugUseLkp_StressGpraEffectDueToDrugUse_FK] FOREIGN KEY ([StressGpraEffectDueToDrugUseLkpKey]) REFERENCES [GpraModule].[GpraEffectDueToDrugUseLkp] ([GpraEffectDueToDrugUseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

