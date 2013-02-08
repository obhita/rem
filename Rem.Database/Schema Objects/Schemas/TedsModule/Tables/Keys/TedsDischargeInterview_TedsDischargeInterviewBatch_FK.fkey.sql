ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsDischargeInterviewBatch_FK] FOREIGN KEY ([TedsDischargeInterviewBatchKey]) REFERENCES [TedsModule].[TedsDischargeInterviewBatch] ([TedsDischargeInterviewBatchKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

