ALTER TABLE [GpraModule].[GpraPlannedServices]
    ADD CONSTRAINT [GpraPlannedServices_GpraDetoxificationLocationLkp_ModalityGpraDetoxificationLocation_FK] FOREIGN KEY ([ModalityGpraDetoxificationLocationLkpKey]) REFERENCES [GpraModule].[GpraDetoxificationLocationLkp] ([GpraDetoxificationLocationLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

