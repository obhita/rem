
print '------------------------------------------------------------------------------------------'
print 'PatientAccountModule_PayorCoverageTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientAccountModule].[PayorCoverageTypeLkp] ( [PayorCoverageTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (  233746, current_timestamp, 1, current_timestamp, 1, 0,   N'Primary', NULL,   N'Primary', N'P', 0, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientAccountModule].[PayorCoverageTypeLkp] ( [PayorCoverageTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 4663764, current_timestamp, 1, current_timestamp, 1, 0, N'Secondary', NULL, N'Secondary', N'S', 0, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [PatientAccountModule].[PayorCoverageTypeLkp] ( [PayorCoverageTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 5252654, current_timestamp, 1, current_timestamp, 1, 0,  N'Tertiary', NULL,  N'Tertiary', N'T', 0, '1/1/1900',        NULL, 0, 0 )

GO
