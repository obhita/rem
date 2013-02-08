
print '------------------------------------------------------------------------------------------'
print 'TedsModule_TedsBatchStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [TedsModule].[TedsBatchStatusLkp] ( [TedsBatchStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100, current_timestamp, 1, current_timestamp, 1, 1, N'Pending',    N'Batch created but not submitted',   N'Pending', NULL, NULL, '3/31/2011',        NULL, 0, 0 )
INSERT INTO [TedsModule].[TedsBatchStatusLkp] ( [TedsBatchStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 102, current_timestamp, 1, current_timestamp, 1, 1, N'Submitted',  N'Batch submitted and locked down', N'Submitted', NULL, NULL, '3/31/2011',        NULL, 0, 0 )

GO
