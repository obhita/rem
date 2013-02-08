ALTER TABLE [ClaimModule].[ClaimBatch]
    ADD CONSTRAINT [ClaimBatch_ClaimBatchStatusLkp_FK] FOREIGN KEY ([ClaimBatchStatusLkpKey]) REFERENCES [ClaimModule].[ClaimBatchStatusLkp] ([ClaimBatchStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

