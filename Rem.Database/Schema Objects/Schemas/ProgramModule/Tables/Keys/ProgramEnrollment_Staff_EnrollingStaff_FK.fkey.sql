ALTER TABLE [ProgramModule].[ProgramEnrollment]
    ADD CONSTRAINT [ProgramEnrollment_Staff_EnrollingStaff_FK] FOREIGN KEY ([EnrollingStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

