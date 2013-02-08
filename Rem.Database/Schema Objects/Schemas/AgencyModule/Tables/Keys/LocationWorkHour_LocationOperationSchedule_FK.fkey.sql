ALTER TABLE [AgencyModule].[LocationWorkHour]
    ADD CONSTRAINT [LocationWorkHour_LocationOperationSchedule_FK] FOREIGN KEY ([LocationOperationScheduleKey]) REFERENCES [AgencyModule].[LocationOperationSchedule] ([LocationOperationScheduleKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

