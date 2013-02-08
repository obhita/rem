ALTER TABLE [ProgramModule].[ProgramEnrollment]
    ADD CONSTRAINT [ProgramEnrollment_DisenrollReasonLkp_FK] FOREIGN KEY ([DisenrollReasonLkpKey]) REFERENCES [ProgramModule].[DisenrollReasonLkp] ([DisenrollReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

