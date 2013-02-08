ALTER TABLE [ClinicalCaseModule].[ClinicalCase]
    ADD CONSTRAINT [ClinicalCase_ClinicalCaseStatusLkp_FK] FOREIGN KEY ([ClinicalCaseStatusLkpKey]) REFERENCES [ClinicalCaseModule].[ClinicalCaseStatusLkp] ([ClinicalCaseStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

