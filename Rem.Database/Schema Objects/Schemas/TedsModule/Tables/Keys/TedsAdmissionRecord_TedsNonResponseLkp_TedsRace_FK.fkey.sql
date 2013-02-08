ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsNonResponseLkp_TedsRace_FK] FOREIGN KEY ([TedsRaceTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

