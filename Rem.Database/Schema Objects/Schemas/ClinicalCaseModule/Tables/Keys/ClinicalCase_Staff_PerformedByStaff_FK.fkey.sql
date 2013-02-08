ALTER TABLE [ClinicalCaseModule].[ClinicalCase]
    ADD CONSTRAINT [ClinicalCase_Staff_PerformedByStaff_FK] FOREIGN KEY ([PerformedByStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;





