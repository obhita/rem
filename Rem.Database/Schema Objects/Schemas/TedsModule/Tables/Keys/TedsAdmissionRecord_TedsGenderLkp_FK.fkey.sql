ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsGenderLkp_FK] FOREIGN KEY ([TedsGenderLkpKey]) REFERENCES [TedsModule].[TedsGenderLkp] ([TedsGenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

