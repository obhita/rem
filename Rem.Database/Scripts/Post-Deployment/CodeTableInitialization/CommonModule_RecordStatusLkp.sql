
print '------------------------------------------------------------------------------------------'
print 'CommonModule_RecordStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [CommonModule].[RecordStatusLkp] ( [RecordStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  436227, current_timestamp, 1, current_timestamp, 1, 0, N'InComplete', NULL, N'InComplete', N'I', 0, '1/1/1900',       NULL, 0, 0 )
INSERT INTO [CommonModule].[RecordStatusLkp] ( [RecordStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 5098408, current_timestamp, 1, current_timestamp, 1, 0,  N'Not Valid', NULL,   N'NotValid', N'N', 0, '1/1/1900',       NULL, 0, 0 )
INSERT INTO [CommonModule].[RecordStatusLkp] ( [RecordStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 8269629, current_timestamp, 1, current_timestamp, 1, 0,   N'Complete', NULL,   N'Complete', N'C', 0, '1/1/1900',       NULL, 0, 0 )

GO
