ALTER TABLE [ClaimModule].[Claim]
    ADD CONSTRAINT [Claim_ClaimBatch_FK] FOREIGN KEY ([ClaimBatchKey]) REFERENCES [ClaimModule].[ClaimBatch] ([ClaimBatchKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

