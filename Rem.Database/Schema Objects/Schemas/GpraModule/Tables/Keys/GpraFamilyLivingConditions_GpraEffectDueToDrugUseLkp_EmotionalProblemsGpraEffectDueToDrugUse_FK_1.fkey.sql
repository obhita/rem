ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraEffectDueToDrugUseLkp_EmotionalProblemsGpraEffectDueToDrugUse_FK] FOREIGN KEY ([EmotionalProblemsGpraEffectDueToDrugUseLkpKey]) REFERENCES [GpraModule].[GpraEffectDueToDrugUseLkp] ([GpraEffectDueToDrugUseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

