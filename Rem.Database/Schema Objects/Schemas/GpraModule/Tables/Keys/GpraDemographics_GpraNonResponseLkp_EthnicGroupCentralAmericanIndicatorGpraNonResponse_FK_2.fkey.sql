ALTER TABLE [GpraModule].[GpraDemographics]
    ADD CONSTRAINT [GpraDemographics_GpraNonResponseLkp_EthnicGroupCentralAmericanIndicatorGpraNonResponse_FK] FOREIGN KEY ([EthnicGroupCentralAmericanIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

