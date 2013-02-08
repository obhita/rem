ALTER TABLE [GpraModule].[GpraProblemsTreatmentRecovery]
    ADD CONSTRAINT [GpraProblemsTreatmentRecovery_GpraPsychologicalImpactLkp_FK] FOREIGN KEY ([GpraPsychologicalImpactLkpKey]) REFERENCES [GpraModule].[GpraPsychologicalImpactLkp] ([GpraPsychologicalImpactLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

