ALTER TABLE [AgencyModule].[StaffLicense]
    ADD CONSTRAINT [StaffLicense_Staff_FK] FOREIGN KEY ([StaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

