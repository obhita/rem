ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_VeteranStatusLkp_FK] FOREIGN KEY ([VeteranStatusLkpKey]) REFERENCES [PatientModule].[VeteranStatusLkp] ([VeteranStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

