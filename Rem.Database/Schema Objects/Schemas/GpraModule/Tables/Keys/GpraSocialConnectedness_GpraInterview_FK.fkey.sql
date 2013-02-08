ALTER TABLE [GpraModule].[GpraSocialConnectedness]
    ADD CONSTRAINT [GpraSocialConnectedness_GpraInterview_FK] FOREIGN KEY ([GpraInterviewKey]) REFERENCES [GpraModule].[GpraInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

