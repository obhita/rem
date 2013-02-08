ALTER TABLE [TedsModule].[TedsDischargeInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeInterviewSubstanceUsage_TedsDischargeInterview_FK] FOREIGN KEY ([TedsDischargeInterviewKey]) REFERENCES [TedsModule].[TedsDischargeInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

