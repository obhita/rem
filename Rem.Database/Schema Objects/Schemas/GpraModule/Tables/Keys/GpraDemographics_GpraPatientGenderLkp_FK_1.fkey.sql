ALTER TABLE [GpraModule].[GpraDemographics]
    ADD CONSTRAINT [GpraDemographics_GpraPatientGenderLkp_FK] FOREIGN KEY ([GpraPatientGenderLkpKey]) REFERENCES [GpraModule].[GpraPatientGenderLkp] ([GpraPatientGenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

