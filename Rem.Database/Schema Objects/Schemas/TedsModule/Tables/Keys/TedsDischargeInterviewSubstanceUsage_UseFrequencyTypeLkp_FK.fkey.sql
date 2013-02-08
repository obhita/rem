ALTER TABLE [TedsModule].[TedsDischargeInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeInterviewSubstanceUsage_UseFrequencyTypeLkp_FK] FOREIGN KEY ([UseFrequencyTypeLkpKey]) REFERENCES [TedsModule].[UseFrequencyTypeLkp] ([UseFrequencyTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

