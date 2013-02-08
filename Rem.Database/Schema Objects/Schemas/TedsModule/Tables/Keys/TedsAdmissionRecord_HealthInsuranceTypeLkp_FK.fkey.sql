ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_HealthInsuranceTypeLkp_FK] FOREIGN KEY ([HealthInsuranceTypeLkpKey]) REFERENCES [TedsModule].[HealthInsuranceTypeLkp] ([HealthInsuranceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

