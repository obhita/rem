ALTER TABLE [ProgramModule].[ProgramEnrollment]
    ADD CONSTRAINT [ProgramEnrollment_ProgramOffering_FK] FOREIGN KEY ([ProgramOfferingKey]) REFERENCES [ProgramModule].[ProgramOffering] ([ProgramOfferingKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

