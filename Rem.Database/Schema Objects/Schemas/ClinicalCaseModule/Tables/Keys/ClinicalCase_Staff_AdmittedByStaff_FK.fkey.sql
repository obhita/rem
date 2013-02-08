ALTER TABLE [ClinicalCaseModule].[ClinicalCase]
    ADD CONSTRAINT [ClinicalCase_Staff_AdmittedByStaff_FK] FOREIGN KEY ([AdmittedByStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;





