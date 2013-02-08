ALTER TABLE [TedsModule].[TedsDischargeInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeInterviewSubstanceUsage_TedsNonResponseLkp_UseFrequencyType_FK] FOREIGN KEY ([UseFrequencyTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

