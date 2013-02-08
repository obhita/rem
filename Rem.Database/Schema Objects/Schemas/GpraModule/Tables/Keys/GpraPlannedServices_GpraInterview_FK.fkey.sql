ALTER TABLE [GpraModule].[GpraPlannedServices]
    ADD CONSTRAINT [GpraPlannedServices_GpraInterview_FK] FOREIGN KEY ([GpraInterviewKey]) REFERENCES [GpraModule].[GpraInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

