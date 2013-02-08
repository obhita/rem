ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsDischargeRecordSubstanceUsage_SecondaryTedsDischargeRecordSubstanceUsage_FK] FOREIGN KEY ([SecondaryTedsDischargeRecordSubstanceUsageKey]) REFERENCES [TedsModule].[TedsDischargeRecordSubstanceUsage] ([TedsDischargeRecordSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



