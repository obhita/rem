ALTER TABLE [AgencyModule].[Staff]
    ADD CONSTRAINT [Staff_StaffPhoto_FK] FOREIGN KEY ([StaffPhotoKey]) REFERENCES [AgencyModule].[StaffPhoto] ([StaffPhotoKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

