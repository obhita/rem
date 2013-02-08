ALTER TABLE [ProgramModule].[Program]
    ADD CONSTRAINT [Program_TreatmentApproachLkp_ProgramCharacteristicsTreatmentApproach_FK] FOREIGN KEY ([ProgramCharacteristicsTreatmentApproachLkpKey]) REFERENCES [ProgramModule].[TreatmentApproachLkp] ([TreatmentApproachLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

