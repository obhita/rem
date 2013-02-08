ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_IncomeSourceTypeLkp_FK] FOREIGN KEY ([IncomeSourceTypeLkpKey]) REFERENCES [TedsModule].[IncomeSourceTypeLkp] ([IncomeSourceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

