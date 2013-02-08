
print '------------------------------------------------------------------------------------------'
print 'ProgramModule_CapacityTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [ProgramModule].[CapacityTypeLkp] ( [CapacityTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100, current_timestamp, 1, current_timestamp, 1, 1, N'Beds', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [ProgramModule].[CapacityTypeLkp] ( [CapacityTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 101, current_timestamp, 1, current_timestamp, 1, 1,      N'Slots', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )

GO
