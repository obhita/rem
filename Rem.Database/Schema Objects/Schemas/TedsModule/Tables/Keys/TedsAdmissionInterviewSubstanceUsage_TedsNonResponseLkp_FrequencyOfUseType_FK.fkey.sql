ALTER TABLE [TedsModule].[TedsAdmissionInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionInterviewSubstanceUsage_TedsNonResponseLkp_FrequencyOfUseType_FK] FOREIGN KEY ([FrequencyOfUseTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

