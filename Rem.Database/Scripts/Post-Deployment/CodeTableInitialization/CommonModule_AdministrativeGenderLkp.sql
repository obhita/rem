
print '------------------------------------------------------------------------------------------'
print 'CommonModule_AdministrativeGenderLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [CommonModule].[AdministrativeGenderLkp] ( [AdministrativeGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,             N'Male', NULL,   N'M',  N'M ', 1, '1/1/1900',       NULL, 0, 0, N'M' )
INSERT INTO [CommonModule].[AdministrativeGenderLkp] ( [AdministrativeGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0,           N'Female', NULL,   N'F',  N'F ', 2, '1/1/1900',       NULL, 0, 0, N'F' )
INSERT INTO [CommonModule].[AdministrativeGenderLkp] ( [AdministrativeGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0, N'Undifferentiated', NULL, N'UND', N'UND', 3, '1/1/1900',       NULL, 0, 0, N'UND' )

GO
