ALTER TABLE [ClinicalCaseModule].[Problem]
    ADD CONSTRAINT [Problem_Staff_ObservedByStaff_FK] FOREIGN KEY ([ObservedByStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

