ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsEmploymentStatusLkp_FK] FOREIGN KEY ([TedsEmploymentStatusLkpKey]) REFERENCES [TedsModule].[TedsEmploymentStatusLkp] ([TedsEmploymentStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

