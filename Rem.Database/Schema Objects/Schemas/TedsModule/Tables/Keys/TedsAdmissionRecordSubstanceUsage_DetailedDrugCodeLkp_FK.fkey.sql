ALTER TABLE [TedsModule].[TedsAdmissionRecordSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionRecordSubstanceUsage_DetailedDrugCodeLkp_FK] FOREIGN KEY ([DetailedDrugCodeLkpKey]) REFERENCES [TedsModule].[DetailedDrugCodeLkp] ([DetailedDrugCodeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

