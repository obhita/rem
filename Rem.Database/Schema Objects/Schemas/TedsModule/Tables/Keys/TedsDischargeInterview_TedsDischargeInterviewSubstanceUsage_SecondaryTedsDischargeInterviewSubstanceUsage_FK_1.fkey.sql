ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsDischargeInterviewSubstanceUsage_SecondaryTedsDischargeInterviewSubstanceUsage_FK] FOREIGN KEY ([SecondaryTedsDischargeInterviewSubstanceUsageKey]) REFERENCES [TedsModule].[TedsDischargeInterviewSubstanceUsage] ([TedsDischargeInterviewSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

