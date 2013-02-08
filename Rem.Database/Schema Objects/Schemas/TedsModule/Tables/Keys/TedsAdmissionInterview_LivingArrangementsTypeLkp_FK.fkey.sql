ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_LivingArrangementsTypeLkp_FK] FOREIGN KEY ([LivingArrangementsTypeLkpKey]) REFERENCES [TedsModule].[LivingArrangementsTypeLkp] ([LivingArrangementsTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



