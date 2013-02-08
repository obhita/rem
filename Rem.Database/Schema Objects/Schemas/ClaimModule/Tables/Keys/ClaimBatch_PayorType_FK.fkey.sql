ALTER TABLE [ClaimModule].[ClaimBatch]
    ADD CONSTRAINT [ClaimBatch_PayorType_FK] FOREIGN KEY ([PayorTypeKey]) REFERENCES [PayorModule].[PayorType] ([PayorTypeKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

