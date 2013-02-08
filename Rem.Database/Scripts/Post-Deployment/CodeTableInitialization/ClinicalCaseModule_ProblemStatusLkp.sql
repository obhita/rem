
print '------------------------------------------------------------------------------------------'
print 'ClinicalCaseModule_ProblemStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [ClinicalCaseModule].[ProblemStatusLkp] ( [ProblemStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 9193667880, current_timestamp, 1, current_timestamp, 1, 0,   N'Active', NULL, N'Active',   N'Active',   NULL, '6/11/2010', '6/11/2010', 0, 0,   N'55561003' )
INSERT INTO [ClinicalCaseModule].[ProblemStatusLkp] ( [ProblemStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 9193667881, current_timestamp, 1, current_timestamp, 1, 0, N'Inactive', NULL, N'Inactive', N'Inactive', NULL, '6/11/2010', '6/11/2010', 0, 0,   N'73425007' )
INSERT INTO [ClinicalCaseModule].[ProblemStatusLkp] ( [ProblemStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 9193667882, current_timestamp, 1, current_timestamp, 1, 0, N'Resolved', NULL, N'Resolved', N'Resolved', NULL, '6/11/2010', '6/11/2010', 0, 0,   N'413322009' )

GO
