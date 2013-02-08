ALTER TABLE [AgencyModule].[StaffCollegeDegree]
    ADD CONSTRAINT [StaffCollegeDegree_CollegeDegreeLkp_FK] FOREIGN KEY ([CollegeDegreeLkpKey]) REFERENCES [AgencyModule].[CollegeDegreeLkp] ([CollegeDegreeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

