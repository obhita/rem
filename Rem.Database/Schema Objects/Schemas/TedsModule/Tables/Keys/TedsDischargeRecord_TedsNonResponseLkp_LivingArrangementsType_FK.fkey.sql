ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsNonResponseLkp_LivingArrangementsType_FK] FOREIGN KEY ([LivingArrangementsTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

