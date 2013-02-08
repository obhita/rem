ALTER TABLE [VisitModule].[Visit]
    ADD CONSTRAINT [Visit_ClinicalCase_FK] FOREIGN KEY ([ClinicalCaseKey]) REFERENCES [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



