ALTER TABLE [PatientModule].[SelfPayment]
    ADD CONSTRAINT [SelfPayment_Staff_CollectedByStaff_FK] FOREIGN KEY ([CollectedByStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

