ALTER TABLE [AgencyModule].[LocationWorkHour]
	ADD CONSTRAINT [LocationWorkHour_DayOfWeek_CHK] 
	CHECK 
	(
	[DayOfWeekEnum] = 'Sunday' OR 
	[DayOfWeekEnum] = 'Monday' OR 
	[DayOfWeekEnum] = 'Tuesday' OR 
	[DayOfWeekEnum] = 'Wednesday' OR 
	[DayOfWeekEnum] = 'Thursday' OR
	[DayOfWeekEnum] = 'Friday' OR 
	[DayOfWeekEnum] = 'Saturday'
	)
