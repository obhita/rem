
print '------------------------------------------------------------------------------------------'
print 'GpraModule_GpraDetoxificationLocationLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [GpraModule].[GpraDetoxificationLocationLkp] ( [GpraDetoxificationLocationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 1,        N'A. Hospital Inpatient',        N'A. Hospital Inpatient', NULL, NULL, 1, '3/31/2011',       NULL, 0, 0 )
INSERT INTO [GpraModule].[GpraDetoxificationLocationLkp] ( [GpraDetoxificationLocationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 1, N'B. Free Standing Residential', N'B. Free Standing Residential', NULL, NULL, 1, '3/31/2011',       NULL, 0, 0 )
INSERT INTO [GpraModule].[GpraDetoxificationLocationLkp] ( [GpraDetoxificationLocationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 1, N'C. Ambulatory Detoxification', N'C. Ambulatory Detoxification', NULL, NULL, 1, '3/31/2011',       NULL, 0, 0 )

GO
