
print '------------------------------------------------------------------------------------------'
print 'DensAsiModule_DensAsiFreeTimeSpentTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [DensAsiModule].[DensAsiFreeTimeSpentTypeLkp] ( [DensAsiFreeTimeSpentTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,  N'Family',  N'Family', N'FM', N'FM', 1, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiFreeTimeSpentTypeLkp] ( [DensAsiFreeTimeSpentTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Friends', N'Friends', N'FR', N'FR', 2, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiFreeTimeSpentTypeLkp] ( [DensAsiFreeTimeSpentTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0,   N'Alone',   N'Alone', N'AL', N'AL', 3, '1/1/1900', NULL, 0, 0 )

GO
