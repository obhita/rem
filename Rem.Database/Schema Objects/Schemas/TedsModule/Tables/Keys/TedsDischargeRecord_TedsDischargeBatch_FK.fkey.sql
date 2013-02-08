ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsDischargeBatch_FK] FOREIGN KEY ([TedsDischargeBatchKey]) REFERENCES [TedsModule].[TedsDischargeBatch] ([TedsDischargeBatchKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

