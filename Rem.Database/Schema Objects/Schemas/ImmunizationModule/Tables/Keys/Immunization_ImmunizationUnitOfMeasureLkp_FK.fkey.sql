ALTER TABLE [ImmunizationModule].[Immunization]
    ADD CONSTRAINT [Immunization_ImmunizationUnitOfMeasureLkp_FK] FOREIGN KEY ([ImmunizationUnitOfMeasureLkpKey]) REFERENCES [ImmunizationModule].[ImmunizationUnitOfMeasureLkp] ([ImmunizationUnitOfMeasureLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

