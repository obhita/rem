
print '------------------------------------------------------------------------------------------'
print 'PatientModule_PatientAliasTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[PatientAliasTypeLkp] ( [PatientAliasTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 261700, current_timestamp, 1, current_timestamp, 1, 0,    N'nickname', NULL, N'CK', N'CK', 0, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientModule].[PatientAliasTypeLkp] ( [PatientAliasTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 261701, current_timestamp, 1, current_timestamp, 1, 0, N'Maiden Name', NULL, N'MN', N'MN', 0, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientModule].[PatientAliasTypeLkp] ( [PatientAliasTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 261702, current_timestamp, 1, current_timestamp, 1, 0,       N'Alias', NULL, N'AL', N'AL', 0, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientModule].[PatientAliasTypeLkp] ( [PatientAliasTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 261703, current_timestamp, 1, current_timestamp, 1, 0, N'Street Name', NULL, N'ST', N'ST', 0, '1/1/1900',        NULL, 0, 0 )

GO
