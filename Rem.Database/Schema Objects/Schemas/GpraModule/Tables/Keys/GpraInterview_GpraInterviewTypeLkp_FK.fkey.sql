ALTER TABLE [GpraModule].[GpraInterview]
    ADD CONSTRAINT [GpraInterview_GpraInterviewTypeLkp_FK] FOREIGN KEY ([GpraInterviewTypeLkpKey]) REFERENCES [GpraModule].[GpraInterviewTypeLkp] ([GpraInterviewTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

