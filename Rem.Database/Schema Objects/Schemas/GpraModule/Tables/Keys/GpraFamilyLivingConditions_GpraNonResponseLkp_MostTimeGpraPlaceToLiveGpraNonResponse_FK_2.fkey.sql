ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraNonResponseLkp_MostTimeGpraPlaceToLiveGpraNonResponse_FK] FOREIGN KEY ([MostTimeGpraPlaceToLiveGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

