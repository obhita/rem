ALTER TABLE [TedsModule].[TedsAdmissionInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionInterviewSubstanceUsage_FrequencyOfUseTypeLkp_FK] FOREIGN KEY ([FrequencyOfUseTypeLkpKey]) REFERENCES [TedsModule].[FrequencyOfUseTypeLkp] ([FrequencyOfUseTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

