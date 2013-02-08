ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsNonResponseLkp_TedsEmploymentStatus_FK] FOREIGN KEY ([TedsEmploymentStatusTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

