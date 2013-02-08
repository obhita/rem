
print '------------------------------------------------------------------------------------------'
print 'SecurityModule_SystemRoleTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [SecurityModule].[SystemRoleTypeLkp] ( [SystemRoleTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100010080031, current_timestamp, 1, current_timestamp, 1, 1, N'Primary', N'Primary', N'PRIMARY', NULL, 1, '3/21/2011', '3/21/2011', 1, 1 )

GO
