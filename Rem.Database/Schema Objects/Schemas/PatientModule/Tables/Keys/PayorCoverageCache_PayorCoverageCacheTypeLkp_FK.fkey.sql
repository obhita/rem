ALTER TABLE [PatientModule].[PayorCoverageCache]
    ADD CONSTRAINT [PayorCoverageCache_PayorCoverageCacheTypeLkp_FK] FOREIGN KEY ([PayorCoverageCacheTypeLkpKey]) REFERENCES [PatientModule].[PayorCoverageCacheTypeLkp] ([PayorCoverageCacheTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

