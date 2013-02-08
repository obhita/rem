ALTER TABLE [ProgramModule].[Program]
    ADD CONSTRAINT [Program_CapacityTypeLkp_FK] FOREIGN KEY ([CapacityTypeLkpKey]) REFERENCES [ProgramModule].[CapacityTypeLkp] ([CapacityTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

