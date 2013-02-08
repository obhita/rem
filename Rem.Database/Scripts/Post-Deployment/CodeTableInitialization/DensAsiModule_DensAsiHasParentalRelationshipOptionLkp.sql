
print '------------------------------------------------------------------------------------------'
print 'DensAsiModule_DensAsiHasParentalRelationshipOptionLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [DensAsiModule].[DensAsiHasParentalRelationshipOptionLkp] ( [DensAsiHasParentalRelationshipOptionLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  1, current_timestamp, 1, current_timestamp, 1, 0,     N'Yes',			 N'Has Relationship',				  N'Y', N'Y',  1, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiHasParentalRelationshipOptionLkp] ( [DensAsiHasParentalRelationshipOptionLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  2, current_timestamp, 1, current_timestamp, 1, 0,     N'No',				 N'Does Not have Relationship',				  N'N', N'N',  2, '1/1/1900', NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiHasParentalRelationshipOptionLkp] ( [DensAsiHasParentalRelationshipOptionLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  3, current_timestamp, 1, current_timestamp, 1, 0,     N'Uncertain/I don''t know',     N'Uncertain/I don''t know', N'UI', N'UI',  3, '1/1/1900', NULL, 0, 0 )

GO
