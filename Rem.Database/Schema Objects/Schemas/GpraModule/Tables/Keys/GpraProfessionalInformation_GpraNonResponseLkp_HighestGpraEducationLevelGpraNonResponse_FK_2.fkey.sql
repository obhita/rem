ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraNonResponseLkp_HighestGpraEducationLevelGpraNonResponse_FK] FOREIGN KEY ([HighestGpraEducationLevelGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

