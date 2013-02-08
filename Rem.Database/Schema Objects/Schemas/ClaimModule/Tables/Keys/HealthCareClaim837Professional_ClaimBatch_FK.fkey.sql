ALTER TABLE [ClaimModule].[HealthCareClaim837Professional]
    ADD CONSTRAINT [HealthCareClaim837Professional_ClaimBatch_FK] FOREIGN KEY ([ClaimBatchKey]) REFERENCES [ClaimModule].[ClaimBatch] ([ClaimBatchKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

