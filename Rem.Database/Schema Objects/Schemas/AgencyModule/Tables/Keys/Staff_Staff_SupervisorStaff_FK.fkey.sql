ALTER TABLE [AgencyModule].[Staff]
    ADD CONSTRAINT [Staff_Staff_SupervisorStaff_FK] FOREIGN KEY ([SupervisorStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

