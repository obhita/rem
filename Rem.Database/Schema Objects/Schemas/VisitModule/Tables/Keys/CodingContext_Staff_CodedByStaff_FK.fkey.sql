ALTER TABLE [VisitModule].[CodingContext]
    ADD CONSTRAINT [CodingContext_Staff_CodedByStaff_FK] FOREIGN KEY ([CodedByStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

