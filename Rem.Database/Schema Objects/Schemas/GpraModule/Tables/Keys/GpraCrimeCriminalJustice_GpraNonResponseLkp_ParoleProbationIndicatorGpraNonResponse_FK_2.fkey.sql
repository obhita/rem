ALTER TABLE [GpraModule].[GpraCrimeCriminalJustice]
    ADD CONSTRAINT [GpraCrimeCriminalJustice_GpraNonResponseLkp_ParoleProbationIndicatorGpraNonResponse_FK] FOREIGN KEY ([ParoleProbationIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

