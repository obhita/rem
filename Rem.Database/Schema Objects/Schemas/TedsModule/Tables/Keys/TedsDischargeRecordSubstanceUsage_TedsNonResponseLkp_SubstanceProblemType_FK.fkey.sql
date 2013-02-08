ALTER TABLE [TedsModule].[TedsDischargeRecordSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeRecordSubstanceUsage_TedsNonResponseLkp_SubstanceProblemType_FK] FOREIGN KEY ([SubstanceProblemTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

