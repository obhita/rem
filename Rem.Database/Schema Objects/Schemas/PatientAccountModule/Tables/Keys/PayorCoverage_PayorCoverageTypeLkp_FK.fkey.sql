ALTER TABLE [PatientAccountModule].[PayorCoverage]
    ADD CONSTRAINT [PayorCoverage_PayorCoverageTypeLkp_FK] FOREIGN KEY ([PayorCoverageTypeLkpKey]) REFERENCES [PatientAccountModule].[PayorCoverageTypeLkp] ([PayorCoverageTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

