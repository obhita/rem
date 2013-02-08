
print '------------------------------------------------------------------------------------------'
print 'DensAsiModule_DensAsiIncompleteInterviewReasonLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [DensAsiModule].[DensAsiIncompleteInterviewReasonLkp] ( [DensAsiIncompleteInterviewReasonLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,        N'Patient Terminated',        N'Patient Terminated', N'T', N'T', 1, '8/19/2011', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiIncompleteInterviewReasonLkp] ( [DensAsiIncompleteInterviewReasonLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0,           N'Patient Refused',           N'Patient Refused', N'R', N'R', 2, '8/19/2011', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiIncompleteInterviewReasonLkp] ( [DensAsiIncompleteInterviewReasonLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0, N'Patient unable to respond', N'Patient unable to respond', N'U', N'U', 3, '8/19/2011', NULL, 0, 0 )

GO
