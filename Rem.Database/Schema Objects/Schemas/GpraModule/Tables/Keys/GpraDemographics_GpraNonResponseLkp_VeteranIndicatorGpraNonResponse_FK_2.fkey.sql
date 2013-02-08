ALTER TABLE [GpraModule].[GpraDemographics]
    ADD CONSTRAINT [GpraDemographics_GpraNonResponseLkp_VeteranIndicatorGpraNonResponse_FK] FOREIGN KEY ([VeteranIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

