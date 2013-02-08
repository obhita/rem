
print '------------------------------------------------------------------------------------------'
print 'VisitModule_VisitStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [VisitModule].[VisitStatusLkp] ( [VisitStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 8042490, current_timestamp, 1, current_timestamp, 1, 0,  N'Scheduled',  N'Scheduled', N'Scheduled', N'SCH', 1, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [VisitModule].[VisitStatusLkp] ( [VisitStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 8042491, current_timestamp, 1, current_timestamp, 1, 0, N'Checked In', N'Checked In', N'CheckedIn', N'CHI', 2, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [VisitModule].[VisitStatusLkp] ( [VisitStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 8042492, current_timestamp, 1, current_timestamp, 1, 0,    N'No Show',    N'No Show',    N'NoShow', N'NOS', 3, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [VisitModule].[VisitStatusLkp] ( [VisitStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 8042493, current_timestamp, 1, current_timestamp, 1, 0,   N'Canceled',   N'Canceled',  N'Canceled', N'CAN', 4, '1/1/1900',        NULL, 0, 0 )

GO
