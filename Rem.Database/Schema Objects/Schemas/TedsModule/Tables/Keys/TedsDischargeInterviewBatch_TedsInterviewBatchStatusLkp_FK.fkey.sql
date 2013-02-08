ALTER TABLE [TedsModule].[TedsDischargeInterviewBatch]
    ADD CONSTRAINT [TedsDischargeInterviewBatch_TedsInterviewBatchStatusLkp_FK] FOREIGN KEY ([TedsInterviewBatchStatusLkpKey]) REFERENCES [TedsModule].[TedsInterviewBatchStatusLkp] ([TedsInterviewBatchStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

