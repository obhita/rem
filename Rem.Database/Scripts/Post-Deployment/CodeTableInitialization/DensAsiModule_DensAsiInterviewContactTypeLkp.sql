
print '------------------------------------------------------------------------------------------'
print 'DensAsiModule_DensAsiInterviewContactTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [DensAsiModule].[DensAsiInterviewContactTypeLkp] ( [DensAsiInterviewContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0, N'In Person', N'In Person', N'I', N'I', 1, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiInterviewContactTypeLkp] ( [DensAsiInterviewContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Telephone', N'Telephone', N'P', N'P', 2, '1/1/1900', NULL, 0, 0 )

GO
