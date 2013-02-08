ALTER TABLE [AgencyModule].[StaffLicense]
    ADD CONSTRAINT [StaffLicense_LicenseLkp_FK] FOREIGN KEY ([LicenseLkpKey]) REFERENCES [AgencyModule].[LicenseLkp] ([LicenseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

