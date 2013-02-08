ALTER TABLE [ProgramModule].[Program]
    ADD CONSTRAINT [Program_AgeGroupLkp_ProgramCharacteristicsAgeGroup_FK] FOREIGN KEY ([ProgramCharacteristicsAgeGroupLkpKey]) REFERENCES [ProgramModule].[AgeGroupLkp] ([AgeGroupLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

