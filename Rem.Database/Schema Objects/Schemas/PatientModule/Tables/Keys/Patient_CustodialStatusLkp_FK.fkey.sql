ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_CustodialStatusLkp_FK] FOREIGN KEY ([CustodialStatusLkpKey]) REFERENCES [PatientModule].[CustodialStatusLkp] ([CustodialStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

