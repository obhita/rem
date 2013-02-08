ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsRaceLkp_FK] FOREIGN KEY ([TedsRaceLkpKey]) REFERENCES [TedsModule].[TedsRaceLkp] ([TedsRaceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

