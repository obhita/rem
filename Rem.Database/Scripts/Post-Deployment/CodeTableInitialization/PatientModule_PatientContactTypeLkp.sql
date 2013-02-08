
print '------------------------------------------------------------------------------------------'
print 'PatientModule_PatientContactTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[PatientContactTypeLkp] ( [PatientContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES (  431961, current_timestamp, 1, current_timestamp, 1, 0,             N'Agent', NULL,            N'Agent',      N'AGNT', 0, '1/1/1900',        NULL, 0, 0,      N'AGNT' )
INSERT INTO [PatientModule].[PatientContactTypeLkp] ( [PatientContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 1334673, current_timestamp, 1, current_timestamp, 1, 0,          N'Guardian', NULL,         N'Guardian',     N'GUARD', 0, '1/1/1900',        NULL, 0, 0,     N'GUARD' )
INSERT INTO [PatientModule].[PatientContactTypeLkp] ( [PatientContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 1444698, current_timestamp, 1, current_timestamp, 1, 0,         N'Caregiver', NULL,        N'Caregiver', N'CAREGIVER', 0, '1/1/1900',        NULL, 0, 0, N'CAREGIVER' )
INSERT INTO [PatientModule].[PatientContactTypeLkp] ( [PatientContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 2874818, current_timestamp, 1, current_timestamp, 1, 0,       N'Next of kin', NULL,        N'NextOfKin',       N'NOK', 0, '1/1/1900',        NULL, 0, 0,       N'NOK' )
INSERT INTO [PatientModule].[PatientContactTypeLkp] ( [PatientContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 5584230, current_timestamp, 1, current_timestamp, 1, 0, N'Emergency Contact', NULL, N'EmergencyContact',      N'ECON', 0, '1/1/1900',        NULL, 0, 0,      N'ECON' )
INSERT INTO [PatientModule].[PatientContactTypeLkp] ( [PatientContactTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 9193667, current_timestamp, 1, current_timestamp, 1, 0,          N'Personal', NULL,         N'Personal',       N'PRS', 0, '1/1/1900',        NULL, 0, 0,       N'PRS' )

GO
