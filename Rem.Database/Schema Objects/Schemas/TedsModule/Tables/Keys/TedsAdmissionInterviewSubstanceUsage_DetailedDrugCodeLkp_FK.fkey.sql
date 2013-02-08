ALTER TABLE [TedsModule].[TedsAdmissionInterviewSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionInterviewSubstanceUsage_DetailedDrugCodeLkp_FK] FOREIGN KEY ([DetailedDrugCodeLkpKey]) REFERENCES [TedsModule].[DetailedDrugCodeLkp] ([DetailedDrugCodeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

