ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_MaritalStatusLkp_FK] FOREIGN KEY ([MaritalStatusLkpKey]) REFERENCES [PatientModule].[MaritalStatusLkp] ([MaritalStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

