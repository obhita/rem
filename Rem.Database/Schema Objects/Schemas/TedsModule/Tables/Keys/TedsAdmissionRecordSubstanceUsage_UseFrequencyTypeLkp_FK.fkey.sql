ALTER TABLE [TedsModule].[TedsAdmissionRecordSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionRecordSubstanceUsage_UseFrequencyTypeLkp_FK] FOREIGN KEY ([UseFrequencyTypeLkpKey]) REFERENCES [TedsModule].[UseFrequencyTypeLkp] ([UseFrequencyTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

