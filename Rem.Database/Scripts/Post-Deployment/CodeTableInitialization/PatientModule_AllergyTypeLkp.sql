
print '------------------------------------------------------------------------------------------'
print 'PatientModule_AllergyTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,              N'Propensity to adverse reactions', NULL,              N'Propensity to adverse reactions', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'420134006' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Propensity to adverse reactions to substance', NULL, N'Propensity to adverse reactions to substance', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'418038007' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0,      N'Propensity to adverse reactions to drug', NULL,      N'Propensity to adverse reactions to drug', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'419511003' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 4, current_timestamp, 1, current_timestamp, 1, 0,      N'Propensity to adverse reactions to food', NULL,      N'Propensity to adverse reactions to food', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'418471000' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 5, current_timestamp, 1, current_timestamp, 1, 0,                         N'Allergy to substance', NULL,                             N'SubstanceAllergy', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'419199007' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 6, current_timestamp, 1, current_timestamp, 1, 0,                                 N'Drug Allergy', NULL,                                  N'DrugAllergy', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'416098002' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 7, current_timestamp, 1, current_timestamp, 1, 0,                                 N'Food Allergy', NULL,                                  N'FoodAllergy', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'414285001' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 8, current_timestamp, 1, current_timestamp, 1, 0,                             N'Drug Intolerance', NULL,                              N'DrugIntolerance', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0,  N'59037007' )
INSERT INTO [PatientModule].[AllergyTypeLkp] ( [AllergyTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator], [CodedConceptCode] ) VALUES ( 9, current_timestamp, 1, current_timestamp, 1, 0,                             N'Food Intolerance', NULL,                              N'FoodIntolerance', NULL, NULL, '1/1/2011',  '1/1/2011', 1, 0, N'235719002' )

GO