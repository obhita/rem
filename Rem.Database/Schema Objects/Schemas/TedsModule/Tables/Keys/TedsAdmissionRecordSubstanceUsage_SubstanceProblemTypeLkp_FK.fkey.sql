ALTER TABLE [TedsModule].[TedsAdmissionRecordSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionRecordSubstanceUsage_SubstanceProblemTypeLkp_FK] FOREIGN KEY ([SubstanceProblemTypeLkpKey]) REFERENCES [TedsModule].[SubstanceProblemTypeLkp] ([SubstanceProblemTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

