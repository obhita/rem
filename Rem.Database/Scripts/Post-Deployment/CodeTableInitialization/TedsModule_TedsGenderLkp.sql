print '------------------------------------------------------------------------------------------'
print 'TedsModule_TedsGenderLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [TedsModule].[TedsGenderLkp] ( [TedsGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [Code], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0, N'Male', NULL, N'Male', N'1', NULL, 1, '1/1/1900',NULL, 0, 0 )
INSERT INTO [TedsModule].[TedsGenderLkp] ( [TedsGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [Code], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Female', NULL, N'Female', N'2', NULL, 2, '1/1/1900',NULL, 0, 0 )

GO
	