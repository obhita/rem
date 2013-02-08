ALTER TABLE [ProgramModule].[Program]
    ADD CONSTRAINT [Program_ProgramCategoryLkp_ProgramCharacteristicsProgramCategory_FK] FOREIGN KEY ([ProgramCharacteristicsProgramCategoryLkpKey]) REFERENCES [ProgramModule].[ProgramCategoryLkp] ([ProgramCategoryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

