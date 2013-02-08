
	
print '------------------------------------------------------------------------------------------'
print 'TedsModule_LivingArrangementsTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [TedsModule].[LivingArrangementsTypeLkp] ( [LivingArrangementsTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [Code], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0, N'Homeless', NULL, N'Homeless', N'01', NULL, 1, '1/1/1900',NULL, 0, 0 )
INSERT INTO [TedsModule].[LivingArrangementsTypeLkp] ( [LivingArrangementsTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [Code], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Dependent living', NULL, N'Dependent living', N'02', NULL, 2, '1/1/1900',NULL, 0, 0 )
INSERT INTO [TedsModule].[LivingArrangementsTypeLkp] ( [LivingArrangementsTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [Code], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0, N'Independent living', NULL, N'Independent living', N'03', NULL, 3, '1/1/1900',NULL, 0, 0 )

GO