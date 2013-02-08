ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsNonResponseLkp_OtherPsychiatricProblemIndicator_FK] FOREIGN KEY ([OtherPsychiatricProblemIndicatorTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

