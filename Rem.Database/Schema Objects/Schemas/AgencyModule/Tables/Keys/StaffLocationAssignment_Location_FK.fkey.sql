ALTER TABLE [AgencyModule].[StaffLocationAssignment]
    ADD CONSTRAINT [StaffLocationAssignment_Location_FK] FOREIGN KEY ([LocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

