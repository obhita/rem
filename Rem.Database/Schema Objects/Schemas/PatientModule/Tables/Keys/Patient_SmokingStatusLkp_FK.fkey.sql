ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_SmokingStatusLkp_FK] FOREIGN KEY ([SmokingStatusLkpKey]) REFERENCES [PatientModule].[SmokingStatusLkp] ([SmokingStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

