ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_LivingArrangementsTypeLkp_FK] FOREIGN KEY ([LivingArrangementsTypeLkpKey]) REFERENCES [TedsModule].[LivingArrangementsTypeLkp] ([LivingArrangementsTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

