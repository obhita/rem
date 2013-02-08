ALTER TABLE [GpraModule].[GpraFollowUp]
    ADD CONSTRAINT [GpraFollowUp_GpraInterview_FK] FOREIGN KEY ([GpraInterviewKey]) REFERENCES [GpraModule].[GpraInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

