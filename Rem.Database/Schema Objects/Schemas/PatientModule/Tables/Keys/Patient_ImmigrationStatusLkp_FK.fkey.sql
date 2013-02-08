ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_ImmigrationStatusLkp_FK] FOREIGN KEY ([ImmigrationStatusLkpKey]) REFERENCES [PatientModule].[ImmigrationStatusLkp] ([ImmigrationStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



