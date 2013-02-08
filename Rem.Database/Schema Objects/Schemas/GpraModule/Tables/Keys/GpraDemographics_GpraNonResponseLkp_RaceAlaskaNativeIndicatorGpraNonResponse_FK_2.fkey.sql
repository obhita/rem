ALTER TABLE [GpraModule].[GpraDemographics]
    ADD CONSTRAINT [GpraDemographics_GpraNonResponseLkp_RaceAlaskaNativeIndicatorGpraNonResponse_FK] FOREIGN KEY ([RaceAlaskaNativeIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

