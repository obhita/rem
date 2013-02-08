ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsAdmissionInterviewSubstanceUsage_PrimaryTedsAdmissionInterviewSubstanceUsage_FK] FOREIGN KEY ([PrimaryTedsAdmissionInterviewSubstanceUsageKey]) REFERENCES [TedsModule].[TedsAdmissionInterviewSubstanceUsage] ([TedsAdmissionInterviewSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

