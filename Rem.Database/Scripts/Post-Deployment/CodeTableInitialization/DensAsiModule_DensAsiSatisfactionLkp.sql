
print '------------------------------------------------------------------------------------------'
print 'DensAsiModule_DensAsiSatisfactionLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [DensAsiModule].[DensAsiSatisfactionLkp] ( [DensAsiSatisfactionLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  1, current_timestamp, 1, current_timestamp, 1, 0,     N'Yes',			 N'Is Satisfied',				  N'Y', N'Y',  1, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiSatisfactionLkp] ( [DensAsiSatisfactionLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  2, current_timestamp, 1, current_timestamp, 1, 0,     N'No',				 N'Not Satisfied',				  N'N', N'N',  2, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiSatisfactionLkp] ( [DensAsiSatisfactionLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  3, current_timestamp, 1, current_timestamp, 1, 0,     N'Indifferent',     N'Not Satisfied or UnSatisfied', N'I', N'I',  3, '1/1/1900', NULL, 0, 0 )

GO
