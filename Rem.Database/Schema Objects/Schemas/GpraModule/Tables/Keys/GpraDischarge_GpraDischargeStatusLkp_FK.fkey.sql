ALTER TABLE [GpraModule].[GpraDischarge]
    ADD CONSTRAINT [GpraDischarge_GpraDischargeStatusLkp_FK] FOREIGN KEY ([GpraDischargeStatusLkpKey]) REFERENCES [GpraModule].[GpraDischargeStatusLkp] ([GpraDischargeStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

