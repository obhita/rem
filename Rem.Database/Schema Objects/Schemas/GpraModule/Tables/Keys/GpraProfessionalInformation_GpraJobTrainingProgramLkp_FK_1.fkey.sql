ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraJobTrainingProgramLkp_FK] FOREIGN KEY ([GpraJobTrainingProgramLkpKey]) REFERENCES [GpraModule].[GpraJobTrainingProgramLkp] ([GpraJobTrainingProgramLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

