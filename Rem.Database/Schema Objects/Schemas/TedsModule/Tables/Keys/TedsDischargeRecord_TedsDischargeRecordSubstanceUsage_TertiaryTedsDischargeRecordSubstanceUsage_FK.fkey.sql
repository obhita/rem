ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsDischargeRecordSubstanceUsage_TertiaryTedsDischargeRecordSubstanceUsage_FK] FOREIGN KEY ([TertiaryTedsDischargeRecordSubstanceUsageKey]) REFERENCES [TedsModule].[TedsDischargeRecordSubstanceUsage] ([TedsDischargeRecordSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



