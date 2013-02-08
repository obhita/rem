
print '------------------------------------------------------------------------------------------'
print 'TedsModule_TedsNonResponseLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [TedsModule].[TedsNonResponseLkp] ( [TedsNonResponseLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0, N'Not applicable', NULL, N'NotApplicable', N'NA', 3, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [TedsModule].[TedsNonResponseLkp] ( [TedsNonResponseLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0,        N'Unknown', NULL,       N'Unknown', N'U',  1, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [TedsModule].[TedsNonResponseLkp] ( [TedsNonResponseLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0,  N'Not collected', NULL,  N'NotCollected', N'NC', 2, '1/1/1900',        NULL, 0, 0 )

GO
