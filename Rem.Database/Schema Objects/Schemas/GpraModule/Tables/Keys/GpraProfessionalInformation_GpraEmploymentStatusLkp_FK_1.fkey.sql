ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraEmploymentStatusLkp_FK] FOREIGN KEY ([GpraEmploymentStatusLkpKey]) REFERENCES [GpraModule].[GpraEmploymentStatusLkp] ([GpraEmploymentStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

