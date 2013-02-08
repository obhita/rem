ALTER TABLE [ImmunizationModule].[Immunization]
    ADD CONSTRAINT [Immunization_ImmunizationNotGivenReasonLkp_FK] FOREIGN KEY ([ImmunizationNotGivenReasonLkpKey]) REFERENCES [ImmunizationModule].[ImmunizationNotGivenReasonLkp] ([ImmunizationNotGivenReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

