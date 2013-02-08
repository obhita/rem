ALTER TABLE [ProgramModule].[ProgramCategoryLkp]
    ADD CONSTRAINT [ProgramCategoryLkp_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

