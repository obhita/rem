	
print '------------------------------------------------------------------------------------------'
print 'TedsModule_ClientTransactionTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [TedsModule].[ClientTransactionTypeLkp] ( [ClientTransactionTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [Code], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0, N'Admission', NULL, N'Admission', N'A', NULL, 1, '1/1/1900',NULL, 0, 0 )
INSERT INTO [TedsModule].[ClientTransactionTypeLkp] ( [ClientTransactionTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [Code], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Transfer / Change in service', NULL, N'Transfer / Change in service', N'T', NULL, 2, '1/1/1900',NULL, 0, 0 )

GO