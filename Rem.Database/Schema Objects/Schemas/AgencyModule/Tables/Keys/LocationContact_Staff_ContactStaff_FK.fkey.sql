ALTER TABLE [AgencyModule].[LocationContact]
    ADD CONSTRAINT [LocationContact_Staff_ContactStaff_FK] FOREIGN KEY ([ContactStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

