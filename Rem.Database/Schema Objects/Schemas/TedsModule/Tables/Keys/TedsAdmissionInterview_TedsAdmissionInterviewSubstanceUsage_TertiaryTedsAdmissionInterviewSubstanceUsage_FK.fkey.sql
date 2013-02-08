ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsAdmissionInterviewSubstanceUsage_TertiaryTedsAdmissionInterviewSubstanceUsage_FK] FOREIGN KEY ([TertiaryTedsAdmissionInterviewSubstanceUsageKey]) REFERENCES [TedsModule].[TedsAdmissionInterviewSubstanceUsage] ([TedsAdmissionInterviewSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

