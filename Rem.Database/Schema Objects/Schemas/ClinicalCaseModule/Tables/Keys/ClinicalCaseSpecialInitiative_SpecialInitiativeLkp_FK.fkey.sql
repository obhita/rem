ALTER TABLE [ClinicalCaseModule].[ClinicalCaseSpecialInitiative]
    ADD CONSTRAINT [ClinicalCaseSpecialInitiative_SpecialInitiativeLkp_FK] FOREIGN KEY ([SpecialInitiativeLkpKey]) REFERENCES [ClinicalCaseModule].[SpecialInitiativeLkp] ([SpecialInitiativeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

