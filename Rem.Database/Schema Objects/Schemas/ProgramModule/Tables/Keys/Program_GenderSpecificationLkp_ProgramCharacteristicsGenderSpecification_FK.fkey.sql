ALTER TABLE [ProgramModule].[Program]
    ADD CONSTRAINT [Program_GenderSpecificationLkp_ProgramCharacteristicsGenderSpecification_FK] FOREIGN KEY ([ProgramCharacteristicsGenderSpecificationLkpKey]) REFERENCES [ProgramModule].[GenderSpecificationLkp] ([GenderSpecificationLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

