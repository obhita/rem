
print '------------------------------------------------------------------------------------------'
print 'GpraModule_GpraSexualActivityLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [GpraModule].[GpraSexualActivityLkp] ( [GpraSexualActivityLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 1,                  N'Yes',                  N'Yes',          N'Yes', NULL, 1, '3/31/2011',       NULL, 0, 0 )
INSERT INTO [GpraModule].[GpraSexualActivityLkp] ( [GpraSexualActivityLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 1,                   N'No',                   N'No',           N'No', NULL, 2, '3/31/2011',       NULL, 0, 0 )
INSERT INTO [GpraModule].[GpraSexualActivityLkp] ( [GpraSexualActivityLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 1, N'Not Permitted to Ask', N'Not Permitted to Ask', N'NotPermitted', NULL, 3, '3/31/2011',       NULL, 0, 0 )

GO
