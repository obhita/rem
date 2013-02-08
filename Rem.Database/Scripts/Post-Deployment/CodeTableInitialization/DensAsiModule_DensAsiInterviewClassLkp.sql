
print '------------------------------------------------------------------------------------------'
print 'DensAsiModule_DensAsiInterviewClassLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [DensAsiModule].[DensAsiInterviewClassLkp] ( [DensAsiInterviewClassLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,   N'Intake',   N'Intake', N'I', N'I', 1, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiInterviewClassLkp] ( [DensAsiInterviewClassLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Followup', N'Followup', N'F', N'F', 2, '1/1/1900', NULL, 0, 0 )

GO
