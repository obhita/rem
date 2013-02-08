
print '------------------------------------------------------------------------------------------'
print 'PatientModule_PatientPhotoTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[PatientPhotoTypeLkp] ( [PatientPhotoTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 6988216, current_timestamp, 1, current_timestamp, 1, 0, N'Passport', NULL, N'PS', N'PS', NULL, '1/1/1900',        NULL, 0, 0 )

GO
