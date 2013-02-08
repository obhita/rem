ALTER TABLE [ClinicalCaseModule].[ClinicalCase]
    ADD CONSTRAINT [ClinicalCase_DischargeReasonLkp_FK] FOREIGN KEY ([DischargeReasonLkpKey]) REFERENCES [ClinicalCaseModule].[DischargeReasonLkp] ([DischargeReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

