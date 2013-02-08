
print '------------------------------------------------------------------------------------------'
print 'DensAsiModule_DensAsiPatientRatingLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [DensAsiModule].[DensAsiPatientRatingLkp] ( [DensAsiPatientRatingLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,   N'Not at all',   N'Not at all',     N'NotAtAll', N'0', 1, '1/1/1900',       NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiPatientRatingLkp] ( [DensAsiPatientRatingLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0,     N'Slightly',     N'Slightly',     N'Slightly', N'1', 2, '1/1/1900',       NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiPatientRatingLkp] ( [DensAsiPatientRatingLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0,   N'Moderately',   N'Moderately',   N'Moderately', N'2', 3, '1/1/1900',       NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiPatientRatingLkp] ( [DensAsiPatientRatingLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 4, current_timestamp, 1, current_timestamp, 1, 0, N'Considerably', N'Considerably', N'Considerably', N'3', 4, '1/1/1900',       NULL, 0, 0 )
INSERT INTO [DensAsiModule].[DensAsiPatientRatingLkp] ( [DensAsiPatientRatingLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 5, current_timestamp, 1, current_timestamp, 1, 0,    N'Extremely',    N'Extremely',    N'Extremely', N'4', 5, '1/1/1900',       NULL, 0, 0 )

GO
