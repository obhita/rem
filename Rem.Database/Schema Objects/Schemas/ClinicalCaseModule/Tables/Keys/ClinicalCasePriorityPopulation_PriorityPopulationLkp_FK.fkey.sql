ALTER TABLE [ClinicalCaseModule].[ClinicalCasePriorityPopulation]
    ADD CONSTRAINT [ClinicalCasePriorityPopulation_PriorityPopulationLkp_FK] FOREIGN KEY ([PriorityPopulationLkpKey]) REFERENCES [ClinicalCaseModule].[PriorityPopulationLkp] ([PriorityPopulationLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

