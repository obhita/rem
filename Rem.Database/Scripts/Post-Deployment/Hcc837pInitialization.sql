-- =============================================
-- Script Template
-- =============================================
print '------------------------------------------------------------------------------------------'
print 'Begin test Data for 837 profession generation'
print '------------------------------------------------------------------------------------------'


print '------------------------------------------------------------------------------------------'
print 'Activity and sub-activity data for visit 13000'
print '------------------------------------------------------------------------------------------'
INSERT INTO [VisitModule].[Activity]([ActivityKey],[ClinicalCaseKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[VisitKey],[ActivityTypeLkpKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ActivityStartDateTime],[ActivityEndDateTime]) VALUES(1001,13000,1,'2010-08-13','2010-08-13',13000,4,1,1,'2010-10-10','2010-10-10')
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (1001, null, null, null, null, '111', 'influenza virus vaccine, live, attenuated, for intranasal use', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Activity]([ActivityKey],[ClinicalCaseKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[VisitKey],[ActivityTypeLkpKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ActivityStartDateTime],[ActivityEndDateTime]) VALUES(1002,13000,1,'2010-08-13','2010-08-13',13000,6,1,1,'2010-10-10','2010-10-10')
INSERT INTO [VisitModule].[BriefIntervention] ([ActivityKey], [NutritionCounselingIndicator], [PhysicalActivityCounselingIndicator], [TobaccoCessationCounselingIndicator]) VALUES (1002, NULL, NULL, 1)

INSERT INTO [VisitModule].[Activity]([ActivityKey],[ClinicalCaseKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[VisitKey],[ActivityTypeLkpKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ActivityStartDateTime],[ActivityEndDateTime]) VALUES(1003,13000,1,'2010-08-13','2010-08-13',13000,5,1,1,'2010-10-10','2010-10-10')
INSERT INTO [VisitModule].[SocialHistory] ([ActivityKey], [SmokingStatusLkpKey], [IsPhq2ScoreAbovePhq9ThresholdIndicator]) VALUES (1003, 1100001, 0)
GO

print '------------------------------------------------------------------------------------------'
print 'Visit problems for visit 13001 and 421000 '
print '------------------------------------------------------------------------------------------'

INSERT INTO [VisitModule].[VisitProblem]([VisitProblemKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[ProblemKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES(1003,1,'2010-08-13','2010-08-13',105000,13001,1,1)           
INSERT INTO [VisitModule].[VisitProblem]([VisitProblemKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[ProblemKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES(1004,1,'2010-08-13','2010-08-13',100010080025,421000,1,1)
GO

print '------------------------------------------------------------------------------------------'
print 'Coding context Data '
print '------------------------------------------------------------------------------------------'

/* [VisitModule].[CodingContext]: CodingContextKey starts at 1001 */
INSERT INTO [VisitModule].[CodingContext] ([CodingContextKey],[Version],[CodedDate],[CodingStatusEnum],[ErrorNote],[CreatedTimestamp],[UpdatedTimestamp],[CodedByStaffKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (1001, 01,'02/29/2012 10:00','ReceivedByBilling','','20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',100010020001,13000,1,1)
INSERT INTO [VisitModule].[CodingContext] ([CodingContextKey],[Version],[CodedDate],[CodingStatusEnum],[ErrorNote],[CreatedTimestamp],[UpdatedTimestamp],[CodedByStaffKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (1002, 01,'02/29/2012 10:00','ReceivedByBilling','','20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',100010020001,13000,1,1)
INSERT INTO [VisitModule].[CodingContext] ([CodingContextKey],[Version],[CodedDate],[CodingStatusEnum],[ErrorNote],[CreatedTimestamp],[UpdatedTimestamp],[CodedByStaffKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (1003, 01,'02/29/2012 10:00','ReceivedByBilling','','20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',100010020001,100010081003,1,1)
INSERT INTO [VisitModule].[CodingContext] ([CodingContextKey],[Version],[CodedDate],[CodingStatusEnum],[ErrorNote],[CreatedTimestamp],[UpdatedTimestamp],[CodedByStaffKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (1004, 01,'02/29/2012 10:00','ReceivedByBilling','','20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',100010020001,13001,1,1)
INSERT INTO [VisitModule].[CodingContext] ([CodingContextKey],[Version],[CodedDate],[CodingStatusEnum],[ErrorNote],[CreatedTimestamp],[UpdatedTimestamp],[CodedByStaffKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (1005, 01,'02/29/2012 10:00','ReceivedByBilling','','20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',100010020001,421000,1,1)
GO

print '------------------------------------------------------------------------------------------'
print 'procedure Data '
print '------------------------------------------------------------------------------------------'

/* [VisitModule].[Procedure]: procedureKey starts at 1001 -- TODO: using procedures, problem and diagnosis */
INSERT INTO [VisitModule].[Procedure] ([ProcedureKey],[Version],[ProcedureTypeEnum],[CreatedTimestamp],[UpdatedTimestamp],[ActivityKey],[CodingContextKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ProcedureCodeCodedConceptCode],[ProcedureCodeDisplayName],[ProcedureCodeCodeSystemIdentifier],[ProcedureCodeCodeSystemVersionNumber],[ProcedureCodeCodeSystemName],[ProcedureCodeOriginalDescription],[ProcedureCodeNullFlavorIndicator],[BillingCount],[FirstModifierCodeNullFlavorIndicator],[SecondModifierCodeNullFlavorIndicator],[ThirdModifierCodeNullFlavorIndicator],[FourthModifierCodeNullFlavorIndicator]) VALUES (1001,1,'Activity','02/28/2012 10:00','02/28/2012 10:00',13000,1001,1,1,'99212','Established Patient - Primary Care - Adult','99212',1,'Established Patient - Primary Care - Adult',null,0,1,1,1,1,1)
INSERT INTO [VisitModule].[Procedure] ([ProcedureKey],[Version],[ProcedureTypeEnum],[CreatedTimestamp],[UpdatedTimestamp],[ActivityKey],[CodingContextKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ProcedureCodeCodedConceptCode],[ProcedureCodeDisplayName],[ProcedureCodeCodeSystemIdentifier],[ProcedureCodeCodeSystemVersionNumber],[ProcedureCodeCodeSystemName],[ProcedureCodeOriginalDescription],[ProcedureCodeNullFlavorIndicator],[BillingCount],[FirstModifierCodeNullFlavorIndicator],[SecondModifierCodeNullFlavorIndicator],[ThirdModifierCodeNullFlavorIndicator],[FourthModifierCodeNullFlavorIndicator]) VALUES (1002,1,'Activity','02/28/2012 10:00','02/28/2012 10:00',1001,1001,1,1,'99213','Test data','99213 Procedure',1,'Test',null,0,1,1,1,1,1)
INSERT INTO [VisitModule].[Procedure] ([ProcedureKey],[Version],[ProcedureTypeEnum],[CreatedTimestamp],[UpdatedTimestamp],[ActivityKey],[CodingContextKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ProcedureCodeCodedConceptCode],[ProcedureCodeDisplayName],[ProcedureCodeCodeSystemIdentifier],[ProcedureCodeCodeSystemVersionNumber],[ProcedureCodeCodeSystemName],[ProcedureCodeOriginalDescription],[ProcedureCodeNullFlavorIndicator],[BillingCount],[FirstModifierCodeNullFlavorIndicator],[SecondModifierCodeNullFlavorIndicator],[ThirdModifierCodeNullFlavorIndicator],[FourthModifierCodeNullFlavorIndicator]) VALUES (1003,1,'Activity','02/28/2012 10:00','02/28/2012 10:00',1002,1002,1,1,'99214','Test procedure', '99214 procedure', 1,'test procedure',null,0,1,1,1,1,1)
INSERT INTO [VisitModule].[Procedure] ([ProcedureKey],[Version],[ProcedureTypeEnum],[CreatedTimestamp],[UpdatedTimestamp],[ActivityKey],[CodingContextKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ProcedureCodeCodedConceptCode],[ProcedureCodeDisplayName],[ProcedureCodeCodeSystemIdentifier],[ProcedureCodeCodeSystemVersionNumber],[ProcedureCodeCodeSystemName],[ProcedureCodeOriginalDescription],[ProcedureCodeNullFlavorIndicator],[BillingCount],[FirstModifierCodeNullFlavorIndicator],[SecondModifierCodeNullFlavorIndicator],[ThirdModifierCodeNullFlavorIndicator],[FourthModifierCodeNullFlavorIndicator]) VALUES (1004,1,'Activity','02/28/2012 10:00','02/28/2012 10:00',1003,1002,1,1,'99215','Test procedure 99215','99215 procedure',1,'test procedure 5',null,0,1,1,1,1,1)
INSERT INTO [VisitModule].[Procedure] ([ProcedureKey],[Version],[ProcedureTypeEnum],[CreatedTimestamp],[UpdatedTimestamp],[ActivityKey],[CodingContextKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ProcedureCodeCodedConceptCode],[ProcedureCodeDisplayName],[ProcedureCodeCodeSystemIdentifier],[ProcedureCodeCodeSystemVersionNumber],[ProcedureCodeCodeSystemName],[ProcedureCodeOriginalDescription],[ProcedureCodeNullFlavorIndicator],[BillingCount],[FirstModifierCodeNullFlavorIndicator],[SecondModifierCodeNullFlavorIndicator],[ThirdModifierCodeNullFlavorIndicator],[FourthModifierCodeNullFlavorIndicator]) VALUES (1005,1,'Activity','02/28/2012 10:00','02/28/2012 10:00',100010081005,1003,1,1,'99216','Test data','99216 Procedure',1,'Test',null,0,1,1,1,1,1)
INSERT INTO [VisitModule].[Procedure] ([ProcedureKey],[Version],[ProcedureTypeEnum],[CreatedTimestamp],[UpdatedTimestamp],[ActivityKey],[CodingContextKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ProcedureCodeCodedConceptCode],[ProcedureCodeDisplayName],[ProcedureCodeCodeSystemIdentifier],[ProcedureCodeCodeSystemVersionNumber],[ProcedureCodeCodeSystemName],[ProcedureCodeOriginalDescription],[ProcedureCodeNullFlavorIndicator],[BillingCount],[FirstModifierCodeNullFlavorIndicator],[SecondModifierCodeNullFlavorIndicator],[ThirdModifierCodeNullFlavorIndicator],[FourthModifierCodeNullFlavorIndicator]) VALUES (1006,1,'Activity','02/28/2012 10:00','02/28/2012 10:00',13001,1004,1,1,'99217','Test procedure', '99217 procedure', 1,'test procedure',null,0,1,1,1,1,1)
INSERT INTO [VisitModule].[Procedure] ([ProcedureKey],[Version],[ProcedureTypeEnum],[CreatedTimestamp],[UpdatedTimestamp],[ActivityKey],[CodingContextKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[ProcedureCodeCodedConceptCode],[ProcedureCodeDisplayName],[ProcedureCodeCodeSystemIdentifier],[ProcedureCodeCodeSystemVersionNumber],[ProcedureCodeCodeSystemName],[ProcedureCodeOriginalDescription],[ProcedureCodeNullFlavorIndicator],[BillingCount],[FirstModifierCodeNullFlavorIndicator],[SecondModifierCodeNullFlavorIndicator],[ThirdModifierCodeNullFlavorIndicator],[FourthModifierCodeNullFlavorIndicator]) VALUES (1007,1,'Activity','02/28/2012 10:00','02/28/2012 10:00',421000,1005,1,1,'99218','Test procedure 99218','99218 procedure',1,'test procedure 8',null,0,1,1,1,1,1)

GO

print '------------------------------------------------------------------------------------------'
print 'Billing Office Data '
print '------------------------------------------------------------------------------------------'

/* BillingOffice*/
INSERT INTO [BillingOfficeModule].[BillingOffice]([BillingOfficeKey], [Name], [Version],[ElectronicTransmitterIdentificationNumber],[CreatedTimestamp],[UpdatedTimestamp],[AdministratorStaffKey],[AgencyKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES(1001, 'Safe Billing Office',1,'BO ETIN 456','20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',100010020001,100010020002,1,1)
GO

/* Insert a home address to patient Henry Levin, Tad Young, and Payne Feit */
INSERT INTO [PatientModule].[PatientAddress] ([PatientAddressKey],[Version],[ConfidentialIndicator],[YearsOfStayNumber],[CreatedTimestamp],[UpdatedTimestamp],[PatientAddressTypeLkpKey],[PatientKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[FirstStreetAddress],[SecondStreetAddress],[CityName],[CountyAreaLkpKey],[StateProvinceLkpKey],[CountryLkpKey],[PostalCode]) VALUES (1001,1,NULL,5,'2012-02-28 14:20:16.4303283 -05:00','2012-02-28 14:20:16.4303283 -05:00',2386448,13000,1,1,'123 Main St.',NULL,'Baltimore',NULL,5409183,NULL,'21046')
INSERT INTO [PatientModule].[PatientAddress] ([PatientAddressKey],[Version],[ConfidentialIndicator],[YearsOfStayNumber],[CreatedTimestamp],[UpdatedTimestamp],[PatientAddressTypeLkpKey],[PatientKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[FirstStreetAddress],[SecondStreetAddress],[CityName],[CountyAreaLkpKey],[StateProvinceLkpKey],[CountryLkpKey],[PostalCode]) VALUES (1002,1,NULL,5,'2012-02-28 14:20:16.4303283 -05:00','2012-02-28 14:20:16.4303283 -05:00',2386448,100010060006,1,1,'32047 Sunrise Path',NULL,'Columbia',NULL,5409183,NULL,'21046')
INSERT INTO [PatientModule].[PatientAddress] ([PatientAddressKey],[Version],[ConfidentialIndicator],[YearsOfStayNumber],[CreatedTimestamp],[UpdatedTimestamp],[PatientAddressTypeLkpKey],[PatientKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[FirstStreetAddress],[SecondStreetAddress],[CityName],[CountyAreaLkpKey],[StateProvinceLkpKey],[CountryLkpKey],[PostalCode]) VALUES (1003,1,NULL,5,'2012-02-28 14:20:16.4303283 -05:00','2012-02-28 14:20:16.4303283 -05:00',2386448,421000,1,1,'32047 Sunrise Path',NULL,'Columbia',NULL,5409183,NULL,'21046')
GO


print '------------------------------------------------------------------------------------------'
print 'PayorType Data '
print '------------------------------------------------------------------------------------------'

INSERT INTO [PayorModule].[PayorType]([PayorTypeKey],[Version],[Name],[BillingFormEnum],[SubmitterIdentifier],[CreatedTimestamp],[UpdatedTimestamp],[BillingOfficeKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[InterchangeReceiverNumber],[InterchangeSenderNumber],[CompositeDelimiter],[ElementDelimiter],[SegmentDelimiter],[RepetitionDelimiter],[BillingFirstStreetAddress],[BillingSecondStreetAddress],[BillingCityName],[BillingCountyAreaLkpKey],[BillingStateProvinceLkpKey],[BillingCountryLkpKey],[PostalCode],[BillingPhoneNumber],[BillingPhoneExtensionNumber],[FtpHostValue],[FtpUserName],[FtpPassCode])
     VALUES (100101, 2, 'Emdeon','Hcc837P','Test Submitter ID','2012-04-12','2012-04-20',1001, 1, 100010080030,'IRN 123', 'ISN 321', ':','*','~','^', '123 First St.', NULL,'Nashville',NULL,8844888,NULL, 54411,NULL, NULL,'ftp://emdon.com', NULL, NULL)
INSERT INTO [PayorModule].[PayorType]([PayorTypeKey],[Version],[Name],[BillingFormEnum],[SubmitterIdentifier],[CreatedTimestamp],[UpdatedTimestamp],[BillingOfficeKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[InterchangeReceiverNumber],[InterchangeSenderNumber],[CompositeDelimiter],[ElementDelimiter],[SegmentDelimiter],[RepetitionDelimiter],[BillingFirstStreetAddress],[BillingSecondStreetAddress],[BillingCityName],[BillingCountyAreaLkpKey],[BillingStateProvinceLkpKey],[BillingCountryLkpKey],[PostalCode],[BillingPhoneNumber],[BillingPhoneExtensionNumber],[FtpHostValue],[FtpUserName],[FtpPassCode])
     VALUES (100102, 2, 'Medicare', 'Hcc837P','Medicare2012', '2012-04-12','2012-04-20',1001, 1, 100010080030,'Medicare2012','MedicareGov', ':','*','~','^', '900 Medicare Path', NULL,'Rockville',NULL,5409183,NULL, 20853,NULL, NULL,'ftp://medicare.org',NULL, NULL)
GO

print '------------------------------------------------------------------------------------------'
print 'Payor Data '
print '------------------------------------------------------------------------------------------'

/* Payor */
INSERT INTO [PayorModule].[Payor] ([PayorKey],[Version],[ElectronicTransmitterIdentificationNumber],[Name],[PayorIdentifier],[WebsiteAddress],[CreatedTimestamp],[UpdatedTimestamp],[BillingOfficeKey],[PrimaryPayorTypeMemberKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveEndDate],[EffectiveStartDate],[EmailAddress]) VALUES(1001,1,'TestETIN123','Test Insurance','Test PayorIdentifier',null,'20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',1001,null,1,1,null,'20120220 10:45:31.1303176 +00:00',null)
INSERT INTO [PayorModule].[Payor] ([PayorKey],[Version],[ElectronicTransmitterIdentificationNumber],[Name],[PayorIdentifier],[WebsiteAddress],[CreatedTimestamp],[UpdatedTimestamp],[BillingOfficeKey],[PrimaryPayorTypeMemberKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveEndDate],[EffectiveStartDate],[EmailAddress]) VALUES(1002,1,'MedicareETIN123A','Medicare Part A','Medicare A',null,'20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',1001,null,1,1,null,'20120220 10:45:31.1303176 +00:00',null)
INSERT INTO [PayorModule].[Payor] ([PayorKey],[Version],[ElectronicTransmitterIdentificationNumber],[Name],[PayorIdentifier],[WebsiteAddress],[CreatedTimestamp],[UpdatedTimestamp],[BillingOfficeKey],[PrimaryPayorTypeMemberKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveEndDate],[EffectiveStartDate],[EmailAddress]) VALUES(1003,1,'KaiserEtin03','Kaiser Permanente','Kaiser',null,'20120220 10:45:31.1303176 +00:00','20120220 10:45:31.1303176 +00:00',1001,null,1,1,null,'20120220 10:45:31.1303176 +00:00',null)
GO

print '------------------------------------------------------------------------------------------'
print 'Payor Type member Data '
print '------------------------------------------------------------------------------------------'

INSERT INTO [PayorModule].[PayorTypeMember]([PayorTypeMemberKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[PayorKey],[PayorTypeKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (100102,1,'20120412 10:45:31.1303176 +00:00','20120412 10:45:31.1303176 +00:00',1001,100101,1,1)
INSERT INTO [PayorModule].[PayorTypeMember]([PayorTypeMemberKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[PayorKey],[PayorTypeKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (100103,1,'20120412 10:45:31.1303176 +00:00','20120412 10:45:31.1303176 +00:00',1002,100102,1,1)
INSERT INTO [PayorModule].[PayorTypeMember]([PayorTypeMemberKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[PayorKey],[PayorTypeKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (100104,1,'20120412 10:45:31.1303176 +00:00','20120412 10:45:31.1303176 +00:00',1003,100101,1,1)
GO

print '------------------------------------------------------------------------------------------'
print 'Set primary payor type'
print '------------------------------------------------------------------------------------------'

UPDATE [PayorModule].[Payor] SET [PrimaryPayorTypeMemberKey] = 100102 WHERE [PayorKey] = 1001
UPDATE [PayorModule].[Payor] SET [PrimaryPayorTypeMemberKey] = 100103 WHERE [PayorKey] = 1002
UPDATE [PayorModule].[Payor] SET [PrimaryPayorTypeMemberKey] = 100102 WHERE [PayorKey] = 1003
GO

print '------------------------------------------------------------------------------------------'
print 'Payor Coverage Data '
print '------------------------------------------------------------------------------------------'

/* PayorCoverageCache --TODO: payorCacke key has to match the payor key (seems wrong)*/
INSERT INTO [PatientModule].[PayorCoverageCache]([PayorCoverageCacheKey],[Version],[MemberNumber],[CreatedTimestamp],[UpdatedTimestamp],[PatientKey],[PayorCacheKey],[PayorCoverageCacheTypeLkpKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveEndDate],[EffectiveStartDate],[BirthDate],[AdministrativeGenderLkpKey],[PayorSubscriberRelationshipCacheTypeLkpKey],[FirstStreetAddress],[SecondStreetAddress],[CityName],[CountyAreaLkpKey],[StateProvinceLkpKey],[CountryLkpKey],[PostalCode],[PrefixName],[FirstName],[MiddleName],[LastName],[SuffixName]) VALUES ( 1001,1,'ZSE4402','2012-02-28 14:35:51.4185283 -05:00',	'2012-02-28 14:35:51.4185283 -05:00',13000,1001,233746,100010080030,100010080030,'2013-01-01','2009-01-01','1932-09-12',1,2,'123 Main Street',NULL,'Baltimore',NULL,5409183,NULL,'21046',NULL,'Henry', 'QoM', 'Levin',NULL)
INSERT INTO [PatientModule].[PayorCoverageCache]([PayorCoverageCacheKey],[Version],[MemberNumber],[CreatedTimestamp],[UpdatedTimestamp],[PatientKey],[PayorCacheKey],[PayorCoverageCacheTypeLkpKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveEndDate],[EffectiveStartDate],[BirthDate],[AdministrativeGenderLkpKey],[PayorSubscriberRelationshipCacheTypeLkpKey],[FirstStreetAddress],[SecondStreetAddress],[CityName],[CountyAreaLkpKey],[StateProvinceLkpKey],[CountryLkpKey],[PostalCode],[PrefixName],[FirstName],[MiddleName],[LastName],[SuffixName]) VALUES ( 1002,1,'MDC2120422','2012-02-28 14:35:51.4185283 -05:00',	'2012-02-28 14:35:51.4185283 -05:00',100010060006,1002,233746,100010080030,100010080030,'2013-01-01','2009-01-01','1996-03-15',1,2,'32047 Sunrise Path',NULL,'Columbia',NULL,5409183,NULL,'21046',NULL,'Tad', null, 'Young',NULL)
INSERT INTO [PatientModule].[PayorCoverageCache]([PayorCoverageCacheKey],[Version],[MemberNumber],[CreatedTimestamp],[UpdatedTimestamp],[PatientKey],[PayorCacheKey],[PayorCoverageCacheTypeLkpKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveEndDate],[EffectiveStartDate],[BirthDate],[AdministrativeGenderLkpKey],[PayorSubscriberRelationshipCacheTypeLkpKey],[FirstStreetAddress],[SecondStreetAddress],[CityName],[CountyAreaLkpKey],[StateProvinceLkpKey],[CountryLkpKey],[PostalCode],[PrefixName],[FirstName],[MiddleName],[LastName],[SuffixName]) VALUES ( 1003,1,'TEST PolicyID','2012-02-28 14:35:51.4185283 -05:00',	'2012-02-28 14:35:51.4185283 -05:00',421000,1003,233746,100010080030,100010080030,'2013-01-01','2009-01-01','1996-03-15',1,2,'32047 Sunrise Path',NULL,'Columbia',NULL,5409183,NULL,'21046',NULL,'Payne', 'QoM', 'Feit',NULL)
GO

print '------------------------------------------------------------------------------------------'
print 'Visit problem Data '
print '------------------------------------------------------------------------------------------'

/* VisitModule.VisitProblem */ -- may need more
INSERT INTO [VisitModule].[VisitProblem]  ([VisitProblemKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[ProblemKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (1001,1,'2010-08-13 11:45:31.1303176 +00:00','2010-08-13 11:45:31.1303176 +00:00',13000,13000,1,1)
INSERT INTO [VisitModule].[VisitProblem]  ([VisitProblemKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[ProblemKey],[VisitKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey]) VALUES (1002,1,'2010-08-13 11:45:31.1303176 +00:00','2010-08-13 11:45:31.1303176 +00:00',13000,100010081003,1,1)
GO

print '------------------------------------------------------------------------------------------'
print 'Agency Staff Identifier Data '
print '------------------------------------------------------------------------------------------'

/* [AgencyModule].[StaffIdentifier] NPI and Tax #  for Staff Leo Smith */
INSERT INTO [AgencyModule].[StaffIdentifier] ([StaffIdentifierKey],[Version],[IdentifierNumber],[CreatedTimestamp],[UpdatedTimestamp],[StaffIdentifierTypeLkpKey],[StaffKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveStartDate],[EffectiveEndDate]) VALUES (1001,1,123456789,'2012-03-01 14:28:22.8480674 -05:00','2012-03-01 14:28:22.8480674 -05:00',2374637,100010020001,1,1,'2000-01-01','2020-01-01')
INSERT INTO [AgencyModule].[StaffIdentifier] ([StaffIdentifierKey],[Version],[IdentifierNumber],[CreatedTimestamp],[UpdatedTimestamp],[StaffIdentifierTypeLkpKey],[StaffKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveStartDate],[EffectiveEndDate]) VALUES (1002,1,987653210,'2012-03-01 14:28:22.8480674 -05:00','2012-03-01 14:28:22.8480674 -05:00',2374638,100010020001,1,1,'2000-01-01','2020-01-01')
INSERT INTO [AgencyModule].[StaffIdentifier] ([StaffIdentifierKey],[Version],[IdentifierNumber],[CreatedTimestamp],[UpdatedTimestamp],[StaffIdentifierTypeLkpKey],[StaffKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[EffectiveStartDate],[EffectiveEndDate]) VALUES (1003,1,456879152,'2012-03-01 14:28:22.8480674 -05:00','2012-03-01 14:28:22.8480674 -05:00',2374639,100010020001,1,1,'2000-01-01','2020-01-01')
GO

print '------------------------------------------------------------------------------------------'
print 'Agency billing address Data '
print '------------------------------------------------------------------------------------------'

/* Add an Agency Billing address*/
INSERT INTO [AgencyModule].[AgencyAddressAndPhone]([AgencyAddressAndPhoneKey],[Version],[CreatedTimestamp],[UpdatedTimestamp],[AgencyKey],[CreatedBySystemAccountKey],[UpdatedBySystemAccountKey],[AgencyAddressTypeLkpKey],[FirstStreetAddress],[SecondStreetAddress],[CityName],[CountyAreaLkpKey],[StateProvinceLkpKey],[CountryLkpKey],[PostalCode]) VALUES (1001,1,'2012-03-01 14:26:21.4130000 +00:00','2012-03-01 14:26:21.4130000 +00:00',100010020002,1,1,7752229,'123 Safe Harbor Way',NULL,'Columbia',NULL,5409183,NULL,'21046-1234')
GO

print '------------------------------------------------------------------------------------------'
print 'Agency identifier Data '
print '------------------------------------------------------------------------------------------'

/* Add an agency identifier record with agency identifier type = Federal Tax ID and NPi  */
INSERT INTO [AgencyModule].[AgencyIdentifier] ( AgencyIdentifierKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, [Version], IdentifierNumber, AgencyIdentifierTypeLkpKey, AgencyKey, EffectiveStartDate, EffectiveEndDate ) VALUES ( 1001, current_timestamp, 1, current_timestamp, 1, 1, '9945433346', 2374638, 100010020002, '2/15/2008', null )
GO
