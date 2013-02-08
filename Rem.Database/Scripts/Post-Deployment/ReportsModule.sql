print '----------------------------------------------------------------------------------------------------'
print 'ReportsModule'
print '----------------------------------------------------------------------------------------------------'

print '----------------------------------------------------------------------------------------------------'
print 'Report'
print '----------------------------------------------------------------------------------------------------'
INSERT INTO [ReportsModule].[Report] ( [ReportKey] ,[Version] ,[Name] ,[Description] ,[ResourceName] ,[CreatedTimestamp] ,[UpdatedTimestamp] ,[CreatedBySystemAccountKey] ,[UpdatedBySystemAccountKey]) VALUES (1, 1, 'Activities Performed', 'Displays the different activities performed on various patients, along with visit name and checkin date.', 'VisitModule.ActivitiesPerformed', current_timestamp, current_timestamp, 1, 1)
INSERT INTO [ReportsModule].[Report] ( [ReportKey] ,[Version] ,[Name] ,[Description] ,[ResourceName] ,[CreatedTimestamp] ,[UpdatedTimestamp] ,[CreatedBySystemAccountKey] ,[UpdatedBySystemAccountKey]) VALUES (2, 1, 'Activities Performed on a patient', 'Displays the different activities performed on a given patient, along with visit name and checkin date.', 'VisitModule.ActivitiesPerformedByPatientKey', current_timestamp, current_timestamp, 1, 1)
