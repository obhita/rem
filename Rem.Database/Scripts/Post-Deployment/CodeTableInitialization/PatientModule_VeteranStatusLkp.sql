
print '------------------------------------------------------------------------------------------'
print 'PatientModule_VeteranStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[VeteranStatusLkp] ( [VeteranStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1365222, current_timestamp, 1, current_timestamp, 1, 0,  N'Active Duty', NULL,  N'AD',  N'AD',    1, '1/1/1900', NULL, 0, 0 )
INSERT INTO [PatientModule].[VeteranStatusLkp] ( [VeteranStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1716702, current_timestamp, 1, current_timestamp, 1, 0,      N'Retired', NULL,   N'RT', N'RT',    3, '1/1/1900', NULL, 0, 0 )
INSERT INTO [PatientModule].[VeteranStatusLkp] ( [VeteranStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2836008, current_timestamp, 1, current_timestamp, 1, 0,     N'Reserves', NULL, N'REV',  N'EV',    2, '1/1/1900', NULL, 0, 0 )
INSERT INTO [PatientModule].[VeteranStatusLkp] ( [VeteranStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 8111866, current_timestamp, 1, current_timestamp, 1, 0,      N'Unknown', NULL,  N'UN',  N'UN', 1000, '1/1/1900', NULL, 0, 0 )

GO
