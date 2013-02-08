
print '------------------------------------------------------------------------------------------'
print 'PatientModule_AllergyStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[AllergyStatusLkp] ( [AllergyStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,           N'Active', NULL,         N'Active', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0,  N'55561003' )
INSERT INTO [PatientModule].[AllergyStatusLkp] ( [AllergyStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0,    N'Prior History', NULL,   N'PriorHistory', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'392521001' )
INSERT INTO [PatientModule].[AllergyStatusLkp] ( [AllergyStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0, N'No Longer Active', NULL, N'NoLongerActive', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0,  N'73425007' )

GO
