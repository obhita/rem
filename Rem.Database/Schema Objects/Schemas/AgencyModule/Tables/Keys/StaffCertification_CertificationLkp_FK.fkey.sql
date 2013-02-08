ALTER TABLE [AgencyModule].[StaffCertification]
    ADD CONSTRAINT [StaffCertification_CertificationLkp_FK] FOREIGN KEY ([CertificationLkpKey]) REFERENCES [AgencyModule].[CertificationLkp] ([CertificationLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

