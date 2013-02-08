ALTER TABLE [TedsModule].[TedsDischargeRecordSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeRecordSubstanceUsage_UseFrequencyTypeLkp_FK] FOREIGN KEY ([UseFrequencyTypeLkpKey]) REFERENCES [TedsModule].[UseFrequencyTypeLkp] ([UseFrequencyTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

