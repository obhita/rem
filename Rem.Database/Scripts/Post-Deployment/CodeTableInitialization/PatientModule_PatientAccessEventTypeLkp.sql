
print '------------------------------------------------------------------------------------------'
print 'PatientModule_PatientAccessEventTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[PatientAccessEventTypeLkp] ( [PatientAccessEventTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100000, current_timestamp, 1, current_timestamp, 1, 0, N'Insert', N'Insert', N'Insert', N'Insert', NULL, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientModule].[PatientAccessEventTypeLkp] ( [PatientAccessEventTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100001, current_timestamp, 1, current_timestamp, 1, 0, N'Update', N'Update', N'Update', N'Update', NULL, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientModule].[PatientAccessEventTypeLkp] ( [PatientAccessEventTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100002, current_timestamp, 1, current_timestamp, 1, 0, N'Delete', N'Delete', N'Delete', N'Delete', NULL, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientModule].[PatientAccessEventTypeLkp] ( [PatientAccessEventTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100003, current_timestamp, 1, current_timestamp, 1, 0,   N'Read',   N'Read',   N'Read',   N'Read', NULL, '1/1/1900',        NULL, 0, 0 )

GO
