ALTER TABLE [GpraModule].[GpraCrimeCriminalJustice]
    ADD CONSTRAINT [GpraCrimeCriminalJustice_GpraNonResponseLkp_CrimeCountGpraNonResponse_FK] FOREIGN KEY ([CrimeCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

