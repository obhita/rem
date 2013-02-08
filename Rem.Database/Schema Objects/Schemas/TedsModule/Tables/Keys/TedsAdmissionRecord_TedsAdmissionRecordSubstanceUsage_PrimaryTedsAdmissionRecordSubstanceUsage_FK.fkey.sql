ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsAdmissionRecordSubstanceUsage_PrimaryTedsAdmissionRecordSubstanceUsage_FK] FOREIGN KEY ([PrimaryTedsAdmissionRecordSubstanceUsageKey]) REFERENCES [TedsModule].[TedsAdmissionRecordSubstanceUsage] ([TedsAdmissionRecordSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

