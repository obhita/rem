ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsNonResponseLkp_TedsEducationYearCount_FK] FOREIGN KEY ([TedsEducationYearCountTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



