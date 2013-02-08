ALTER TABLE [GpraModule].[GpraInterview]
    ADD CONSTRAINT [GpraInterview_GpraPatientTypeLkp_FK] FOREIGN KEY ([GpraPatientTypeLkpKey]) REFERENCES [GpraModule].[GpraPatientTypeLkp] ([GpraPatientTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

