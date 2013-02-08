ALTER TABLE [GpraModule].[GpraDemographics]
    ADD CONSTRAINT [GpraDemographics_GpraNonResponseLkp_RaceAsianIndicatorGpraNonResponse_FK] FOREIGN KEY ([RaceAsianIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

