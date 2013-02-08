ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsNonResponseLkp_TedsEducationYearCount_FK] FOREIGN KEY ([TedsEducationYearCountTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

