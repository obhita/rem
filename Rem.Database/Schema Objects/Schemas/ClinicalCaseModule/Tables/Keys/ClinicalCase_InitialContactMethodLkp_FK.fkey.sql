ALTER TABLE [ClinicalCaseModule].[ClinicalCase]
    ADD CONSTRAINT [ClinicalCase_InitialContactMethodLkp_FK] FOREIGN KEY ([InitialContactMethodLkpKey]) REFERENCES [ClinicalCaseModule].[InitialContactMethodLkp] ([InitialContactMethodLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

