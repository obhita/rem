ALTER TABLE [TedsModule].[TedsDischargeRecordSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeRecordSubstanceUsage_TedsNonResponseLkp_UseFrequencyType_FK] FOREIGN KEY ([UseFrequencyTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

