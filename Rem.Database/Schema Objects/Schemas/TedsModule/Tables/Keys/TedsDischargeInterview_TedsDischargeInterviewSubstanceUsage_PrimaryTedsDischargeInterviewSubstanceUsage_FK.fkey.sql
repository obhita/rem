ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsDischargeInterviewSubstanceUsage_PrimaryTedsDischargeInterviewSubstanceUsage_FK] FOREIGN KEY ([PrimaryTedsDischargeInterviewSubstanceUsageKey]) REFERENCES [TedsModule].[TedsDischargeInterviewSubstanceUsage] ([TedsDischargeInterviewSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

