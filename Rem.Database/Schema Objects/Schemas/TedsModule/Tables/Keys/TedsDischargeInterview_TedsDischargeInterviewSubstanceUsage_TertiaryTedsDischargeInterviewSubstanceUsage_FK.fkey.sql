ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsDischargeInterviewSubstanceUsage_TertiaryTedsDischargeInterviewSubstanceUsage_FK] FOREIGN KEY ([TertiaryTedsDischargeInterviewSubstanceUsageKey]) REFERENCES [TedsModule].[TedsDischargeInterviewSubstanceUsage] ([TedsDischargeInterviewSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

