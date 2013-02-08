ALTER TABLE [TedsModule].[TedsDischargeRecordSubstanceUsage]
    ADD CONSTRAINT [TedsDischargeRecordSubstanceUsage_TedsDischargeRecord_FK] FOREIGN KEY ([TedsDischargeRecordKey]) REFERENCES [TedsModule].[TedsDischargeRecord] ([TedsDischargeRecordKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

