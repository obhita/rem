ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraEducationLevelLkp_HighestGpraEducationLevel_FK] FOREIGN KEY ([HighestGpraEducationLevelLkpKey]) REFERENCES [GpraModule].[GpraEducationLevelLkp] ([GpraEducationLevelLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

