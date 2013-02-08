
print '------------------------------------------------------------------------------------------'
print 'PatientModule_PayorCoverageCacheTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[PayorCoverageCacheTypeLkp] ([PayorCoverageCacheTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [EffectiveStartDate], [EffectiveEndDate], [SortOrderNumber], [SystemOwnedIndicator], [DefaultIndicator]) VALUES (233746,  current_timestamp, 1, current_timestamp, 1, 0,     N'Primary',  NULL, N'Primary', N'P', '19000101', NULL, 0,    0, 0)
INSERT INTO [PatientModule].[PayorCoverageCacheTypeLkp] ([PayorCoverageCacheTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [EffectiveStartDate], [EffectiveEndDate], [SortOrderNumber], [SystemOwnedIndicator], [DefaultIndicator]) VALUES (4663764, current_timestamp, 1, current_timestamp, 1, 0,   N'Secondary',  NULL, N'Secondary', N'S', '19000101', NULL, 0,    0, 0)
INSERT INTO [PatientModule].[PayorCoverageCacheTypeLkp] ([PayorCoverageCacheTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [EffectiveStartDate], [EffectiveEndDate], [SortOrderNumber], [SystemOwnedIndicator], [DefaultIndicator]) VALUES (5252654, current_timestamp, 1, current_timestamp, 1, 0,    N'Tertiary',  NULL, N'Tertiary', N'T', '19000101', NULL, 0,    0, 0)

GO

