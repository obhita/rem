ALTER TABLE [BillingOfficeModule].[BillingOffice]
    ADD CONSTRAINT [BillingOffice_Staff_AdministratorStaff_FK] FOREIGN KEY ([AdministratorStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

