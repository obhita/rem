ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsEmploymentStatusLkp_FK] FOREIGN KEY ([TedsEmploymentStatusLkpKey]) REFERENCES [TedsModule].[TedsEmploymentStatusLkp] ([TedsEmploymentStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

