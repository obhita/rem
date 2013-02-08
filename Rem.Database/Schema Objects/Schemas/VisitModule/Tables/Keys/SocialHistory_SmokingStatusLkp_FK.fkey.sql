ALTER TABLE [VisitModule].[SocialHistory]
    ADD CONSTRAINT [SocialHistory_SmokingStatusLkp_FK] FOREIGN KEY ([SmokingStatusLkpKey]) REFERENCES [PatientModule].[SmokingStatusLkp] ([SmokingStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

