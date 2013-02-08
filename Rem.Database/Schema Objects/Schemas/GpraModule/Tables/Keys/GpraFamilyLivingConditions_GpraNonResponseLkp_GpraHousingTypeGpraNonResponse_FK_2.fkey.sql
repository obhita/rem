ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraNonResponseLkp_GpraHousingTypeGpraNonResponse_FK] FOREIGN KEY ([GpraHousingTypeGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

