ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsAdmissionRecordSubstanceUsage_TertiaryTedsAdmissionRecordSubstanceUsage_FK] FOREIGN KEY ([TertiaryTedsAdmissionRecordSubstanceUsageKey]) REFERENCES [TedsModule].[TedsAdmissionRecordSubstanceUsage] ([TedsAdmissionRecordSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

