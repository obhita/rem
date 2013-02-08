ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraNonResponseLkp_PregnancyIndicatorGpraNonResponse_FK] FOREIGN KEY ([PregnancyIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

