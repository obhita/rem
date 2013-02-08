ALTER TABLE [ClinicalCaseModule].[ClinicalCaseSpecialInitiative]
    ADD CONSTRAINT [ClinicalCaseSpecialInitiative_ClinicalCase_FK] FOREIGN KEY ([ClinicalCaseKey]) REFERENCES [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



