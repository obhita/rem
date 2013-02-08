ALTER TABLE [AgencyModule].[StaffTrainingCourse]
    ADD CONSTRAINT [StaffTrainingCourse_TrainingCourseLkp_FK] FOREIGN KEY ([TrainingCourseLkpKey]) REFERENCES [AgencyModule].[TrainingCourseLkp] ([TrainingCourseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

