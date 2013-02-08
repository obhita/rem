ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsServiceTypeLkp_FK] FOREIGN KEY ([TedsServiceTypeLkpKey]) REFERENCES [TedsModule].[TedsServiceTypeLkp] ([TedsServiceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

