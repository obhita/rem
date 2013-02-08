ALTER TABLE [VisitModule].[Visit]
    ADD CONSTRAINT [Visit_VisitStatusLkp_FK] FOREIGN KEY ([VisitStatusLkpKey]) REFERENCES [VisitModule].[VisitStatusLkp] ([VisitStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

