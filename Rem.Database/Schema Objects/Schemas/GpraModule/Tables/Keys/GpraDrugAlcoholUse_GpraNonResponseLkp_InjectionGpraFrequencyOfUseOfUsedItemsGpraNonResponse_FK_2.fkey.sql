ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraNonResponseLkp_InjectionGpraFrequencyOfUseOfUsedItemsGpraNonResponse_FK] FOREIGN KEY ([InjectionGpraFrequencyOfUseOfUsedItemsGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

