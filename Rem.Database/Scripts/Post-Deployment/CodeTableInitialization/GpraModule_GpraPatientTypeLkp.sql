
print '------------------------------------------------------------------------------------------'
print 'GpraModule_GpraPatientTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [GpraModule].[GpraPatientTypeLkp] ( [GpraPatientTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 1,   N'Treatment Client',   N'Treatment Client', NULL, NULL, 1, '3/31/2011',       NULL, 0, 0 )
INSERT INTO [GpraModule].[GpraPatientTypeLkp] ( [GpraPatientTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 1, N'Client in Recovery', N'Client in Recovery', NULL, NULL, 2, '3/31/2011',       NULL, 0, 0 )

GO
