ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsServiceTypeLkp_FK] FOREIGN KEY ([TedsServiceTypeLkpKey]) REFERENCES [TedsModule].[TedsServiceTypeLkp] ([TedsServiceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

