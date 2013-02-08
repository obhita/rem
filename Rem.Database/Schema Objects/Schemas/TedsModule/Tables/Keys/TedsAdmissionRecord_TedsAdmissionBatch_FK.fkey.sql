ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsAdmissionBatch_FK] FOREIGN KEY ([TedsAdmissionBatchKey]) REFERENCES [TedsModule].[TedsAdmissionBatch] ([TedsAdmissionBatchKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

