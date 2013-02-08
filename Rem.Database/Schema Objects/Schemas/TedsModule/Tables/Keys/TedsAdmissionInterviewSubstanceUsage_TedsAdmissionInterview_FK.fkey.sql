ALTER TABLE [TedsModule].[TedsAdmissionInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionInterviewSubstanceUsage_TedsAdmissionInterview_FK] FOREIGN KEY ([TedsAdmissionInterviewKey]) REFERENCES [TedsModule].[TedsAdmissionInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

