ALTER TABLE [AgencyModule].[AgencyContact]
    ADD CONSTRAINT [AgencyContact_Staff_ContactStaff_FK] FOREIGN KEY ([ContactStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

