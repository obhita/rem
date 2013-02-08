ALTER TABLE [TedsModule].[TedsDischargeInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeInterviewSubstanceUsage_FrequencyOfUseTypeLkp_FK] FOREIGN KEY ([FrequencyOfUseTypeLkpKey]) REFERENCES [TedsModule].[FrequencyOfUseTypeLkp] ([FrequencyOfUseTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

