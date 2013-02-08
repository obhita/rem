ALTER TABLE [GpraModule].[GpraDemographics]
    ADD CONSTRAINT [GpraDemographics_GpraNonResponseLkp_RaceAmericanIndianIndicatorGpraNonResponse_FK] FOREIGN KEY ([RaceAmericanIndianIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

