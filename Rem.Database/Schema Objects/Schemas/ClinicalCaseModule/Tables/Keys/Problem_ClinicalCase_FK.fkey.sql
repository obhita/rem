﻿ALTER TABLE [ClinicalCaseModule].[Problem]
    ADD CONSTRAINT [Problem_ClinicalCase_FK] FOREIGN KEY ([ClinicalCaseKey]) REFERENCES [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

