ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsAdmissionRecordSubstanceUsage_SecondaryTedsAdmissionRecordSubstanceUsage_FK] FOREIGN KEY ([SecondaryTedsAdmissionRecordSubstanceUsageKey]) REFERENCES [TedsModule].[TedsAdmissionRecordSubstanceUsage] ([TedsAdmissionRecordSubstanceUsageKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

