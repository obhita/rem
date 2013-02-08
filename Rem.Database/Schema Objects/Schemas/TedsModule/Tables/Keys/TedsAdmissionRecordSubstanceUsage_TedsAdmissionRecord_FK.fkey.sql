ALTER TABLE [TedsModule].[TedsAdmissionRecordSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionRecordSubstanceUsage_TedsAdmissionRecord_FK] FOREIGN KEY ([TedsAdmissionRecordKey]) REFERENCES [TedsModule].[TedsAdmissionRecord] ([TedsAdmissionRecordKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

