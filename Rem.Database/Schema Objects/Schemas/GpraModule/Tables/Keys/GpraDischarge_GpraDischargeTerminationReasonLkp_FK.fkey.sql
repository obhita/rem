ALTER TABLE [GpraModule].[GpraDischarge]
    ADD CONSTRAINT [GpraDischarge_GpraDischargeTerminationReasonLkp_FK] FOREIGN KEY ([GpraDischargeTerminationReasonLkpKey]) REFERENCES [GpraModule].[GpraDischargeTerminationReasonLkp] ([GpraDischargeTerminationReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

