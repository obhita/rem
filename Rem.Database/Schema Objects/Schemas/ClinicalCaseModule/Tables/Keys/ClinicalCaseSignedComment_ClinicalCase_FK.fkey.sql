ALTER TABLE [ClinicalCaseModule].[ClinicalCaseSignedComment]
    ADD CONSTRAINT [ClinicalCaseSignedComment_ClinicalCase_FK] FOREIGN KEY ([ClinicalCaseKey]) REFERENCES [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



