ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsDischargeRecordSubstanceUsage_PrimaryTedsDischargeRecordSubstanceUsage_FK] FOREIGN KEY ([PrimaryTedsDischargeRecordSubstanceUsageKey]) REFERENCES [TedsModule].[TedsDischargeRecordSubstanceUsage] ([TedsDischargeRecordSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



