ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsAdmissionInterview_FK] FOREIGN KEY ([TedsAdmissionInterviewKey]) REFERENCES [TedsModule].[TedsAdmissionInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



