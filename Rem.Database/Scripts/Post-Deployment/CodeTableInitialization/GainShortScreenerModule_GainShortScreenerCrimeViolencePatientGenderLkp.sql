
print '------------------------------------------------------------------------------------------'
print 'GainShortScreenerModule_GainPatientGenderLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [GainShortScreenerModule].[GainShortScreenerCrimeViolencePatientGenderLkp] ( [GainShortScreenerCrimeViolencePatientGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 1,   N'Male',    N'Male',  N'M', NULL, 1, '3/31/2011', NULL, 0, 0 )
INSERT INTO [GainShortScreenerModule].[GainShortScreenerCrimeViolencePatientGenderLkp] ( [GainShortScreenerCrimeViolencePatientGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 1, N'Female',  N'Female',  N'F', NULL, 2, '3/31/2011', NULL, 0, 0 )
INSERT INTO [GainShortScreenerModule].[GainShortScreenerCrimeViolencePatientGenderLkp] ( [GainShortScreenerCrimeViolencePatientGenderLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 4, current_timestamp, 1, current_timestamp, 1, 1,  N'Other',   N'Other',  N'O', NULL, 4, '3/31/2011', NULL, 0, 0 )

GO
