
print '------------------------------------------------------------------------------------------'
print 'GpraModule_GpraNonResponseLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [GpraModule].[GpraNonResponseLkp] ( [GpraNonResponseLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 1,     N'Refused',     N'Refused',  N'Refused', N'RF', 0, '1/1/2011',       NULL, 1, 0 )
INSERT INTO [GpraModule].[GpraNonResponseLkp] ( [GpraNonResponseLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 1, N'Don''t Know', N'Don''t Know', N'DontKnow', N'DK', 1, '1/1/2011',       NULL, 1, 0 )

GO
