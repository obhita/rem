ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_TedsRaceLkp_FK] FOREIGN KEY ([TedsRaceLkpKey]) REFERENCES [TedsModule].[TedsRaceLkp] ([TedsRaceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



