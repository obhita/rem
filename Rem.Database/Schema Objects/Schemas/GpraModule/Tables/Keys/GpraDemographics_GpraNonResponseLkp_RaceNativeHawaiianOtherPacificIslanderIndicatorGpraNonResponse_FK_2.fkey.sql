ALTER TABLE [GpraModule].[GpraDemographics]
    ADD CONSTRAINT [GpraDemographics_GpraNonResponseLkp_RaceNativeHawaiianOtherPacificIslanderIndicatorGpraNonResponse_FK] FOREIGN KEY ([RaceNativeHawaiianOtherPacificIslanderIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

