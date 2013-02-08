ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_EducationStatusLkp_FK] FOREIGN KEY ([EducationStatusLkpKey]) REFERENCES [PatientModule].[EducationStatusLkp] ([EducationStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



