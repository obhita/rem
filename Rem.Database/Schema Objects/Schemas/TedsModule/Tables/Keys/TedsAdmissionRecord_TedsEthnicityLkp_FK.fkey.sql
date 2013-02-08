ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsEthnicityLkp_FK] FOREIGN KEY ([TedsEthnicityLkpKey]) REFERENCES [TedsModule].[TedsEthnicityLkp] ([TedsEthnicityLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

