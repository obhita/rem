ALTER TABLE [AgencyModule].[StaffCollegeDegree]
    ADD CONSTRAINT [StaffCollegeDegree_Staff_FK] FOREIGN KEY ([StaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

