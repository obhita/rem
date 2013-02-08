ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_LivingArrangementsTypeLkp_FK] FOREIGN KEY ([LivingArrangementsTypeLkpKey]) REFERENCES [TedsModule].[LivingArrangementsTypeLkp] ([LivingArrangementsTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

