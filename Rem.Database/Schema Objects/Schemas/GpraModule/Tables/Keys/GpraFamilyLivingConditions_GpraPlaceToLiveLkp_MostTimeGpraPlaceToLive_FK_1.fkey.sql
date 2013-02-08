ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraPlaceToLiveLkp_MostTimeGpraPlaceToLive_FK] FOREIGN KEY ([MostTimeGpraPlaceToLiveLkpKey]) REFERENCES [GpraModule].[GpraPlaceToLiveLkp] ([GpraPlaceToLiveLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

