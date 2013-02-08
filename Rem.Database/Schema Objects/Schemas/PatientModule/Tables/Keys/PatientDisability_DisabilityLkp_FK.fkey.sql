ALTER TABLE [PatientModule].[PatientDisability]
    ADD CONSTRAINT [PatientDisability_DisabilityLkp_FK] FOREIGN KEY ([DisabilityLkpKey]) REFERENCES [PatientModule].[DisabilityLkp] ([DisabilityLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

