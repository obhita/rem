ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsEmploymentStatusLkp_FK] FOREIGN KEY ([TedsEmploymentStatusLkpKey]) REFERENCES [TedsModule].[TedsEmploymentStatusLkp] ([TedsEmploymentStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



