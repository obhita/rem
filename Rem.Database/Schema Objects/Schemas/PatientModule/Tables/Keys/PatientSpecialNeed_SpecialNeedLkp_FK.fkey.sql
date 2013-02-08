ALTER TABLE [PatientModule].[PatientSpecialNeed]
    ADD CONSTRAINT [PatientSpecialNeed_SpecialNeedLkp_FK] FOREIGN KEY ([SpecialNeedLkpKey]) REFERENCES [PatientModule].[SpecialNeedLkp] ([SpecialNeedLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

