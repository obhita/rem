ALTER TABLE [GpraModule].[GpraCrimeCriminalJustice]
    ADD CONSTRAINT [GpraCrimeCriminalJustice_GpraNonResponseLkp_ArrestedCountGpraNonResponse_FK] FOREIGN KEY ([ArrestedCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

