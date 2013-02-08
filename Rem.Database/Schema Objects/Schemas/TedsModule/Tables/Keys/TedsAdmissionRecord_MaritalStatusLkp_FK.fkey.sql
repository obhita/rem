ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_MaritalStatusLkp_FK] FOREIGN KEY ([MaritalStatusLkpKey]) REFERENCES [PatientModule].[MaritalStatusLkp] ([MaritalStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

