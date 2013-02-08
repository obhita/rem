ALTER TABLE [ProgramModule].[ProgramOffering]
    ADD CONSTRAINT [ProgramOffering_Program_FK] FOREIGN KEY ([ProgramKey]) REFERENCES [ProgramModule].[Program] ([ProgramKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

