ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_ReligiousAffiliationLkp_FK] FOREIGN KEY ([ReligiousAffiliationLkpKey]) REFERENCES [PatientModule].[ReligiousAffiliationLkp] ([ReligiousAffiliationLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

