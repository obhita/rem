
print '------------------------------------------------------------------------------------------'
print 'GpraModule_GpraDischargeStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [GpraModule].[GpraDischargeStatusLkp] ( [GpraDischargeStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 1, N' Completion/Graduate', N' Completion/Graduate', N' CompletionGraduate', NULL, 1, '8/20/2011',       NULL, 1, 0 )
INSERT INTO [GpraModule].[GpraDischargeStatusLkp] ( [GpraDischargeStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 1,         N' Termination',         N' Termination',        N' Termination', NULL, 2, '8/20/2011',       NULL, 1, 0 )

GO
