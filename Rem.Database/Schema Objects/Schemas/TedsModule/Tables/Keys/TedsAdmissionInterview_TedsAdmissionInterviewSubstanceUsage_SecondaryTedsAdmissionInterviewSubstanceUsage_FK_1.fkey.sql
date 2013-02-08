ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsAdmissionInterviewSubstanceUsage_SecondaryTedsAdmissionInterviewSubstanceUsage_FK] FOREIGN KEY ([SecondaryTedsAdmissionInterviewSubstanceUsageKey]) REFERENCES [TedsModule].[TedsAdmissionInterviewSubstanceUsage] ([TedsAdmissionInterviewSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

