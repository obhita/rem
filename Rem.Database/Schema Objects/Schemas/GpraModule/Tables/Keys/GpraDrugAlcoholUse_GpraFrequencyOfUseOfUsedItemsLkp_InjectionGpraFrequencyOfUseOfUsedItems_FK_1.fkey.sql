ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraFrequencyOfUseOfUsedItemsLkp_InjectionGpraFrequencyOfUseOfUsedItems_FK] FOREIGN KEY ([InjectionGpraFrequencyOfUseOfUsedItemsLkpKey]) REFERENCES [GpraModule].[GpraFrequencyOfUseOfUsedItemsLkp] ([GpraFrequencyOfUseOfUsedItemsLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

