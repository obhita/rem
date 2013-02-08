ALTER TABLE [GpraModule].[GpraFollowUp]
    ADD CONSTRAINT [GpraFollowUp_GpraFollowUpStatusLkp_FK] FOREIGN KEY ([GpraFollowUpStatusLkpKey]) REFERENCES [GpraModule].[GpraFollowUpStatusLkp] ([GpraFollowUpStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

