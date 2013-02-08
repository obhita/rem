ALTER TABLE [TedsModule].[TedsBatch]
    ADD CONSTRAINT [TedsBatch_Staff_ExtractedStaff_FK] FOREIGN KEY ([ExtractedStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

