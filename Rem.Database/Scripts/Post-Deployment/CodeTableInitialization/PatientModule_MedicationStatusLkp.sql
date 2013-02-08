
print '------------------------------------------------------------------------------------------'
print 'PatientModule_MedicationStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[MedicationStatusLkp] ( [MedicationStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,   N'Active', NULL,   N'Active', NULL, NULL, '1/1/2011',  '1/1/2011', 0, 0,  N'55561003' )
INSERT INTO [PatientModule].[MedicationStatusLkp] ( [MedicationStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Inactive', NULL, N'Inactive', NULL, NULL, '1/1/2011',  '1/1/2011', 0, 0, N'421139008' )

GO
