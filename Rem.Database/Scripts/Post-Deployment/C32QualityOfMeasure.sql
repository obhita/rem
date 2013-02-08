print '------------------------------------------------------------------------------------------'
print 'Begin Persona Data for Quality of Measure'
print 'For PopHealth reporting period 10/01/2010-12/31/2010'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - QNQF0013 Hypertension: Blood Pressure Measurement'
print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0421 Adult Weight Screening and Follow-Up' 
print '                                    - (a) Age 65+'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Henry QoM Levin - Patient Characteristic Age > 65'
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 13000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 13000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'henry.levin@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Henry', N'QoM', N'Levin', NULL, NULL, 'PUI13000',  '09/12/1932', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Henry QoM Levin -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 13000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 13000, '20110706 12:07:31.1303176 +00:00', 1 ,'20110706 12:07:31.1303176 +00:00', 1, 0, 1, '20110813 12:07:31.1303176 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'I have active hypertension.', 13000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Henry QoM Levin -> Clinical Case - 1 -> Problem - Essential hypertension, Code: 59621000'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[Problem]: ProblemKey starts at 13000 */
INSERT INTO [ClinicalCaseModule].[Problem] ([ProblemKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ProblemStatusLkpKey], [ClinicalCaseKey], [ProblemCodeCodedConceptCode], [ProblemCodeDisplayName], [ProblemCodeCodeSystemName], [ProblemCodeCodeSystemIdentifier], [ProblemCodeNullFlavorIndicator], [ProblemTypeLkpKey], [ObservedDate], [StatusChangedDate], [ObservedByStaffKey], [CauseOfDeathIndicator], [OnsetStartDate], [OnsetEndDate])VALUES (13000, '20101020 15:45:31.1303176 +00:00', 1, '20101020 15:45:31.1303176 +00:00', 1, 1, 9193667880, 13000, '59621000', 'Essential hypertension', 'SNOMED CT', '2.16.840.1.113883.6.96', 0, 5, '3/24/2011','3/24/2011', 100010020001, null, '01/01/2010', '12/30/2011')
GO

print '------------------------------------------------------------------------------------------'
print 'Appointments between 10/01/2010 and 12/30/2010'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 13000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (13000, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/10/2010 09:00', '10/10/2010 10:00', 100010020001, 'I have active hypertension.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (13001, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/11/2010 09:00', '10/11/2010 10:00', 100010020001, 'I have active hypertension.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Henry QoM Levin -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 13000 */
/* [VisitModule].[Activity]: ActivityKey starts at 13000 */
/* [VisitModule].[BloodPressure]: BloodPressureKey starts at 13000 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (13000, 13000, 8042491, 100010020003, 'Established Patient - Primary Care - Adult', '99212', '10/10/2010 09:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (13000, 13000, '20100813 11:45:31.1303176 +00:00', 1, '20100813 11:45:31.1303176 +00:00', 1, 1, '10/10/2010 09:00', '10/10/2010 10:00', 13000, 1)
INSERT INTO [VisitModule].[VitalSign] ([ActivityKey], [HeightFeetMeasure], [HeightInchesMeasure], [WeightLbsMeasure]) VALUES (13000, 6, 2.5, 206)
INSERT INTO [VisitModule].[BloodPressure] ([BloodPressureKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [SystollicMeasure], [DiastollicMeasure], [EffectiveTimestamp], [VitalSignKey]) VALUES (13000, '20100813 11:45:31.1303176 +00:00', 1, '20100813 11:45:31.1303176 +00:00', 1, 1, 132, 86, '20100813 11:45:31.1303176 +00:00', 13000)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (13001, 13000, 8042491, 100010020003, 'Established Patient - Primary Care - Adult', '99212', '10/11/2010 09:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (13001, 13000, '20100813 11:45:31.1303176 +00:00', 1, '20100813 11:45:31.1303176 +00:00', 1, 1, '10/11/2010 09:00', '10/11/2010 10:00', 13001, 1)
INSERT INTO [VisitModule].[VitalSign] ([ActivityKey], [HeightFeetMeasure], [HeightInchesMeasure], [WeightLbsMeasure]) VALUES (13001, 6, 2.5, 206)
INSERT INTO [VisitModule].[BloodPressure] ([BloodPressureKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [SystollicMeasure], [DiastollicMeasure], [EffectiveTimestamp], [VitalSignKey]) VALUES (13001, '20100813 11:45:31.1303176 +00:00', 1, '20100813 11:45:31.1303176 +00:00', 1, 1, 132, 86, '20100813 11:45:31.1303176 +00:00', 13001)
GO


print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0027 Smoking and Tobacco Use Cessation, Medical assistance' 
print '                                    - (a) Advising Smokers and Tobacco Users to Quit'
print '                                    - (b) Discussing Smoking and Tobacco Use Cessation Medications and Strategies'
print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0028 Preventive Care and Screening: Tobacco' 
print '                                    - (a) Use Assessment'
print '                                    - (b) Cessation Intervention'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Willard QoM Wilson - Patient Characteristic Age >= 17'
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 27000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES (27000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'willard.wilson@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Willard', N'QoM', N'Wilson', NULL, NULL, 'PUI27000',  '08/12/1952', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Willard QoM Wilson -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 27000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 27000, '20110706 12:07:31.1303176 +00:00', 1 ,'20110706 12:07:31.1303176 +00:00', 1, 0, 1, '20110813 12:07:31.1303176 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Tobacco Use Cessation Counseling.', 27000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Appointments between 10/01/2009 and 12/30/2010'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 27000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (27000, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '01/27/2010 09:00', '01/27/2010 10:00', 100010020001, 'I am a smoker.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (27100, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '02/27/2010 09:00', '02/27/2010 10:00', 100010020001, 'I am a smoker.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Willard QoM Wilson -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 27000 */
/* [VisitModule].[Activity]: ActivityKey starts at 27000 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (27000, 27000, 8042491, 100010020003, 'Established Patient - Primary Care - Adult', '99212', '01/27/2010 09:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (27000, 27000, '20100813 11:45:31.1303176 +00:00', 1, '20100813 11:45:31.1303176 +00:00', 1, 1, '01/27/2010 09:00', '01/27/2010 10:00', 27000, 5)
INSERT INTO [VisitModule].[SocialHistory] ([ActivityKey], [SmokingStatusLkpKey], [IsPhq2ScoreAbovePhq9ThresholdIndicator]) VALUES (27000, 1100001, 0)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (27100, 27000, 8042491, 100010020003, 'Established Patient - Primary Care - Adult', '99212', '02/27/2010 09:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (27100, 27000, '20100813 11:45:31.1303176 +00:00', 1, '20100813 11:45:31.1303176 +00:00', 1, 1, '02/27/2010 09:00', '02/27/2010 10:00', 27100, 6)
INSERT INTO [VisitModule].[BriefIntervention] ([ActivityKey], [NutritionCounselingIndicator], [PhysicalActivityCounselingIndicator], [TobaccoCessationCounselingIndicator]) VALUES (27100, NULL, NULL, 1)
GO


print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0421 Adult Weight Screening and Follow-Up' 
print '                                    - (b) Age 18-64'
print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0041 Preventive Care and Screening: Influenza Immunization for Patients >= 50 Years Old'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Feit QoM Payne  - Patient Characteristic Age 50-64'
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 421000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 421000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'feit.payne @test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Feit', N'QoM', N'Payne', NULL, NULL, 'PUI421000',  '05/13/1955', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Feit QoM Payne  -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 421000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 421000, '20100706 12:27:34.7680639 +00:00', 1 ,'20100706 12:27:34.7680639 +00:00', 1, 0, 1, '20100706 12:27:34.7680639 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'I have active hypertension.', 421000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Appointments between 10/01/2010 and 12/30/2010'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 421000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (421000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '10/14/2010 09:00', '10/14/2010 10:00', 100010020001, 'I have active hypertension.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (421001, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '10/21/2010 09:00', '10/21/2010 10:00', 100010020001, 'I have active hypertension.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Feit QoM Payne  -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 421000 */
/* [VisitModule].[Activity]: ActivityKey starts at 421000 */
/* [VisitModule].[BloodPressure]: BloodPressureKey starts at 421000 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (421000, 421000, 8042491, 100010020003, 'Established Patient - Primary Care - Adult', '99212', '10/14/2010 09:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (421000, 421000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '10/14/2010 09:00', '10/14/2010 10:00', 421000, 1)
INSERT INTO [VisitModule].[VitalSign] ([ActivityKey], [HeightFeetMeasure], [HeightInchesMeasure], [WeightLbsMeasure], [DietaryConsultationOrderIndicator], [BmiFollowUpPlanIndicator]) VALUES (421000, 6, 2.5, 289, null, 1)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (421001, 421000, 8042491, 100010020003, 'Established Patient - Primary Care - Adult', '99212', '10/21/2010 09:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (421001, 421000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '10/21/2010 09:00', '10/21/2010 10:00', 421001, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (421001, null, null, null, null, '111', 'influenza virus vaccine, live, attenuated, for intranasal use', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)
GO


print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0004 Initiation and Engagement of Alcohol and Other Drug Dependence Treatment' 
print '                                    - (a) Age 12-17 with followup within 14 days'
print '                                    - (b) Age 12-17 with followup within 14 and 30 days'
print '                                    - (e) Age 12+ with followup within 14 days'
print '                                    - (f) Age 12+ with followup within 14 and 30 days'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Driscoll QoM Young - Patient Characteristic 12 <= Age <= 17' 
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 4000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 4000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'driscoll.young@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Driscoll', N'QoM', N'Young', NULL, NULL, 'PUI4000',  '08/12/1995', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Driscoll QoM Young -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 4000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 4000, '20110706 12:07:31.1303176 +00:00', 1 ,'20110706 12:07:31.1303176 +00:00', 1, 0, 1, '20110813 12:07:31.1303176 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'I like to drink a lot.', 4000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Driscoll QoM Young -> Clinical Case - 1 -> Problem - Alcohol or drug dependence, Code: 191811004'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[Problem]: ProblemKey starts at 4000 */
INSERT INTO [ClinicalCaseModule].[Problem] ([ProblemKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ProblemStatusLkpKey], [ClinicalCaseKey], [ProblemCodeCodedConceptCode], [ProblemCodeDisplayName], [ProblemCodeCodeSystemName], [ProblemCodeCodeSystemIdentifier], [ProblemCodeNullFlavorIndicator], [ProblemTypeLkpKey], [ObservedDate], [StatusChangedDate], [ObservedByStaffKey], [CauseOfDeathIndicator], [OnsetStartDate], [OnsetEndDate])VALUES (4000, '20101020 15:45:31.1303176 +00:00', 1, '20101020 15:45:31.1303176 +00:00', 1, 1, 9193667880, 4000, '191811004', 'Alcohol or drug dependence', 'SNOMED CT', '2.16.840.1.113883.6.96', 0, 5, '3/24/2011','3/24/2011', 100010020001, null, '01/01/2010', null)
GO

print '------------------------------------------------------------------------------------------'
print 'Appointments between 10/01/2010 and 12/30/2010'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 4000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (4000, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/04/2010 09:00', '10/04/2010 10:00', 100010020001, 'I like to drink a lot.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (4001, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/05/2010 09:00', '10/05/2010 10:00', 100010020001, 'I like to drink a lot.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (4002, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/19/2010 09:00', '10/19/2010 10:00', 100010020001, 'I like to drink a lot.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (4003, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/20/2010 09:00', '10/20/2010 10:00', 100010020001, 'I like to drink a lot.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Driscoll QoM Young -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 4000 */
/* [VisitModule].[VisitProblem]: VisitProblemKey starts at 4000 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (4000, 4000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/04/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (4000, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 4000, 4000, 1, 1)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (4001, 4000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/05/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (4001, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 4000, 4001, 1, 1)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (4002, 4000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/19/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (4002, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 4000, 4002, 1, 1)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (4003, 4000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/20/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (4003, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 4000, 4003, 1, 1)
GO


print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0004 Initiation and Engagement of Alcohol and Other Drug Dependence Treatment' 
print '                                    - (c) Age 17+ with followup within 14 days'
print '                                    - (d) Age 17+ with followup within 14 and 30 days'
print '                                    - (e) Age 17+ with followup within 14 days'
print '                                    - (f) Age 17+ with followup within 14 and 30 days'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Driscoll QoM Carl - Patient Characteristic Age > 17' 
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 41000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 41000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'driscoll.carl@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Driscoll', N'QoM', N'Carl', NULL, NULL, 'PUI41000',  '08/11/1955', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Driscoll QoM Carl -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 41000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 41000, '20110706 12:07:31.1303176 +00:00', 1 ,'20110706 12:07:31.1303176 +00:00', 1, 0, 1, '20110813 12:07:31.1303176 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'I like to drink a lot.', 41000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Driscoll QoM Carl -> Clinical Case - 1 -> Problem - Alcohol or drug dependence, Code: 191811004'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[Problem]: ProblemKey starts at 41000 */
INSERT INTO [ClinicalCaseModule].[Problem] ([ProblemKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ProblemStatusLkpKey], [ClinicalCaseKey], [ProblemCodeCodedConceptCode], [ProblemCodeDisplayName], [ProblemCodeCodeSystemName], [ProblemCodeCodeSystemIdentifier], [ProblemCodeNullFlavorIndicator], [ProblemTypeLkpKey], [ObservedDate], [StatusChangedDate], [ObservedByStaffKey], [CauseOfDeathIndicator], [OnsetStartDate], [OnsetEndDate])VALUES (41000, '20101020 15:45:31.1303176 +00:00', 1, '20101020 15:45:31.1303176 +00:00', 1, 1, 9193667880, 41000, '191811004', 'Alcohol or drug dependence', 'SNOMED CT', '2.16.840.1.113883.6.96', 0, 5, '3/24/2011','3/24/2011', 100010020001, null, '01/01/2010', null)
GO

print '------------------------------------------------------------------------------------------'
print 'Appointments between 10/01/2010 and 12/30/2010'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 41000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (41000, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/04/2010 09:00', '10/04/2010 10:00', 100010020001, 'I like to drink a lot.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (41001, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/05/2010 09:00', '10/05/2010 10:00', 100010020001, 'I like to drink a lot.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (41002, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/19/2010 09:00', '10/19/2010 10:00', 100010020001, 'I like to drink a lot.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (41003, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '10/20/2010 09:00', '10/20/2010 10:00', 100010020001, 'I like to drink a lot.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Driscoll QoM Carl -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 41000 */
/* [VisitModule].[VisitProblem]: VisitProblemKey starts at 41000 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (41000, 41000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/04/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (41000, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 41000, 41000, 1, 1)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (41001, 41000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/05/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (41001, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 41000, 41001, 1, 1)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (41002, 41000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/19/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (41002, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 41000, 41002, 1, 1)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (41003, 41000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '10/20/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (41003, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 41000, 41003, 1, 1)
GO


print '------------------------------------------------------------------------------------------'
print ' Persona Data for Quality of Measure - NQF-0024 Weight Assessment and Counseling for Children and Adolescents' 
print '                                     - (a) Age 2 to 16 with BMI measurement'
print '                                     - (b) Age 2 to 16 with nutrition counseling'
print '                                     - (c) Age 2 to 16 with physical activity counseling'
print '                                     - (d) Age 2 to 10 with BMI measurement'
print '                                     - (e) Age 2 to 10 with nutrition counseling'
print '                                     - (f) Age 2 to 10 with physical activity counseling'
print '                                     - (g) Age 11 to 16 with BMI measurement'
print '                                     - (h) Age 11 to 16 with nutrition counseling'
print '                                     - (c) Age 11 to 16 with physical activity counseling'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Youngest QoM Henrietta - Patient Characteristics                                         '
print '                          Age 2 to 10                                                     '
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 24000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 24000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'youngest.henrietta@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Youngest', N'QoM', N'Henrietta', NULL, NULL, 'PUI24000',  '04/23/2008', 2173041, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print ' Youngest QoM Henrietta -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 24000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 24000, '20110706 12:07:31.1303176 +00:00', 1 ,'20110706 12:07:31.1303176 +00:00', 1, 0, 1, '20110813 12:07:31.1303176 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Regular check.', 24000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Appointment 11/24/2010 09:00 - 10:00'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 24000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (24000, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '11/24/2010 09:00', '11/24/2010 10:00', 100010020001, 'I have drinking problem.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Youngest QoM Henrietta -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 24000 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (24000, 24000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '11/24/2010 09:00')
GO

print '------------------------------------------------------------------------------------------'
print 'Youngest QoM Henrietta -> Clinical Case - 1 -> Activities'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Activity]: VisitKey starts at 24000 */

/* BMI */
INSERT INTO [VisitModule].[Activity] ( [ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES ( 24000, 24000, '20110706 12:07:31.1303176 +00:00',1,'20110706 12:07:31.1303176 +00:00',1,1,'11/24/2010 09:00', '11/24/2010 10:00', 24000, 1)
INSERT INTO [VisitModule].[VitalSign] ( [ActivityKey], [HeightFeetMeasure], [HeightInchesMeasure], [WeightLbsMeasure])VALUES (24000, 2, 2, 70)

/* Nutritional Counseling and Physical Activity Counseling */
INSERT INTO [VisitModule].[Activity] ( [ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES ( 24001, 24000, '20110706 12:07:31.1303176 +00:00',1,'20110706 12:07:31.1303176 +00:00',1,1,'11/24/2010 09:00', '11/24/2010 10:00', 24000, 6)
INSERT INTO [VisitModule].[BriefIntervention] ( [ActivityKey], [NutritionCounselingIndicator], [PhysicalActivityCounselingIndicator], [TobaccoCessationCounselingIndicator] )VALUES (24001, 1, 1, 0)
GO

print '------------------------------------------------------------------------------------------'
print ' Young QoM Henrietta - Patient Characteristics                                         '
print '                          Age 11 to 16                                                     '
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 24100 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 24100, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'young.henrietta@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Young', N'QoM', N'Henrietta', NULL, NULL, 'PUI24100',  '04/23/1998', 2173041, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print ' Young QoM Henrietta -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 24100 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 24100, '20110706 12:07:31.1303176 +00:00', 1 ,'20110706 12:07:31.1303176 +00:00', 1, 0, 1, '20110813 12:07:31.1303176 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Regular check.', 24100, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Appointment 11/24/2010 09:00 - 10:00'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 24100 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (24100, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '11/24/2010 09:00', '11/24/2010 10:00', 100010020001, 'I have drinking problem.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Young QoM Henrietta -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 24100 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (24100, 24100, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '11/24/2010 09:00')
GO

print '------------------------------------------------------------------------------------------'
print 'Young QoM Henrietta -> Clinical Case - 1 -> Activities'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Activity]: VisitKey starts at 24100 */

/* BMI */
INSERT INTO [VisitModule].[Activity] ( [ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey] ) VALUES ( 24100, 24100, '20110706 12:07:31.1303176 +00:00',1,'20110706 12:07:31.1303176 +00:00',1,1,'11/24/2010 09:00', '11/24/2010 10:00', 24100, 1)
INSERT INTO [VisitModule].[VitalSign] ( [ActivityKey], [HeightFeetMeasure], [HeightInchesMeasure], [WeightLbsMeasure])VALUES (24100, 2, 2, 70)

/* Nutritional Counseling and Physical Activity Counseling */
INSERT INTO [VisitModule].[Activity] ( [ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey] ) VALUES ( 24101, 24100, '20110706 12:07:31.1303176 +00:00',1,'20110706 12:07:31.1303176 +00:00',1,1,'11/24/2010 09:00', '11/24/2010 10:00', 24100, 6)
INSERT INTO [VisitModule].[BriefIntervention] ( [ActivityKey], [NutritionCounselingIndicator], [PhysicalActivityCounselingIndicator], [TobaccoCessationCounselingIndicator] )VALUES (24101, 1, 1, 0)
GO


print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0038 Childhood Immunization Status' 
print '									- (k) Summary1'
print '									- (a) Tetanus and Acellular Pertussis (DTAP) Vaccine'
print '									- (b) Polio (IPV) Vaccine'
print '									- (c) Measles, Mumps and Rubella (MMR) Vaccine'
print '									- (d) H Influenza Type B (HiB) Vaccine'
print '									- (e) Hepatitis B (Hep B) Vaccine'
print '									- (f) Chicken Pox (VZV) Vaccine'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Merle QoM Green - Patient Characteristic Age 1-2'
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 381000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 381000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'merle.green@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Merle', N'QoM', N'Green', NULL, NULL, 'PUI381000',  '09/01/2009', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Merle QoM Green -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 381000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 381000, '20100706 12:27:34.7680639 +00:00', 1 ,'20100706 12:27:34.7680639 +00:00', 1, 0, 1, '20100706 12:27:34.7680639 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Routine check.', 381000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Appointments between birth and 12/30/2010'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 381000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '01/10/2010 13:00', '01/10/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381001, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '04/10/2010 13:00', '04/10/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381002, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '07/10/2010 13:00', '07/10/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381003, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '12/10/2010 13:00', '12/10/2010 14:00', 100010020001 , 'Regular visit.' )
																																																																																																				
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381004, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '01/11/2010 15:00', '01/11/2010 16:00', 100010020001 , 'Regular visit.' )
																																																																																																				
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381005, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '01/11/2010 13:00', '01/11/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381006, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '04/11/2010 13:00', '04/11/2010 14:00', 100010020001 , 'Regular visit.' )
																																																																																																				
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381007, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '01/12/2010 13:00', '01/12/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381008, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '04/12/2010 13:00', '04/12/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381009, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '07/12/2010 13:00', '07/12/2010 14:00', 100010020001 , 'Regular visit.' )
																																																																																																				
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381010, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '01/13/2010 13:00', '01/13/2010 14:00', 100010020001 , 'Regular visit.' )
																																																																																																				
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381011, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '01/14/2010 13:00', '01/14/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381012, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '04/14/2010 13:00', '04/14/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381013, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '07/14/2010 13:00', '07/14/2010 14:00', 100010020001 , 'Regular visit.' )
																																																																																																				
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381014, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '01/15/2010 13:00', '01/15/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381015, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '04/15/2010 13:00', '04/15/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381016, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '07/15/2010 13:00', '07/15/2010 14:00', 100010020001 , 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (381017, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '12/15/2010 13:00', '12/15/2010 14:00', 100010020001	, 'Regular visit.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Merle QoM Green -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 381000 */
/* [VisitModule].[Activity]: ActivityKey starts at 381000 */
/* [ImmunizationModule].[Immunization]: ImmunizationKey starts at 381000 */

/* dtap */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381000, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '01/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381000, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '01/10/2010 13:00', '01/10/2010 14:00', 381000, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381000, null, null, null, null, '110', 'DTaP-hepatitis B and poliovirus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381001, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '04/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381001, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '04/10/2010 13:00', '04/10/2010 14:00', 381001, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381001, null, null, null, null, '110', 'DTaP-hepatitis B and poliovirus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381002, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '07/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381002, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '07/10/2010 13:00', '07/10/2010 14:00', 381002, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381002, null, null, null, null, '110', 'DTaP-hepatitis B and poliovirus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381003, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '12/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381003, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '12/10/2010 13:00', '12/10/2010 14:00', 381003, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381003, null, null, null, null, '110', 'DTaP-hepatitis B and poliovirus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)							 																		
																																																																																					 
/* mmr */																																																																																			 									
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381004, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '01/11/2010 15:00')																			 
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381004, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '01/11/2010 15:00', '01/11/2010 16:00', 381004, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381004, null, null, null, null, '03', 'measles, mumps and rubella virus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)							 									
																																																																																					 
/* hib */																																																																																			 
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381005, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '01/11/2010 13:00')																			 
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381005, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '01/11/2010 13:00', '01/11/2010 14:00', 381005, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381005, null, null, null, null, '120', 'diphtheria, tetanus toxoids and acellular pertussis vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)							 
																																																																																					 									
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381006, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '04/11/2010 13:00')																			 
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381006, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '04/11/2010 13:00', '04/11/2010 14:00',  381006, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381006, null, null, null, null, '120', 'diphtheria, tetanus toxoids and acellular pertussis vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)							 
																																																																																					 									
/* hepb */																																																																																			 
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381007, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '01/12/2010 13:00')																			 
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381007, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '01/12/2010 13:00', '01/12/2010 14:00',  381007, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381007, null, null, null, null, '08', 'hepatitis B vaccine, pediatric or pediatric/adolescent dosage', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)							 

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381008, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '04/12/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381008, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '04/12/2010 13:00', '04/12/2010 14:00', 381008, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381008, null, null, null, null, '08', 'hepatitis B vaccine, pediatric or pediatric/adolescent dosage', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381009, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '07/12/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381009, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1,'07/12/2010 13:00', '07/12/2010 14:00',  381009, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381009, null, null, null, null, '08', 'hepatitis B vaccine, pediatric or pediatric/adolescent dosage', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

/* vzv */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381010, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '01/13/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381010, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '01/13/2010 13:00', '01/13/2010 14:00', 381010, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381010, null, null, null, null, '21', 'varicella virus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

/* ipv */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381011, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '01/12/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381011, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '01/14/2010 13:00', '01/14/2010 14:00', 381011, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381011, null, null, null, null, '10', 'poliovirus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381012, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '04/12/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381012, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1,'04/14/2010 13:00', '04/14/2010 14:00',  381012, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381012, null, null, null, null, '10', 'poliovirus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381013, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '07/12/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381013, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '07/14/2010 13:00', '07/14/2010 14:00', 381013, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381013, null, null, null, null, '10', 'poliovirus vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

/* pcv */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381014, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '01/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381014, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '01/15/2010 13:00', '01/15/2010 14:00', 381014, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381014, null, null, null, null, '100', 'pneumococcal conjugate vaccine, 7 valent', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381015, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '04/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey],  [ClinicalCaseKey],  [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381015, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '04/15/2010 13:00', '04/15/2010 14:00', 381015, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381015, null, null, null, null, '100', 'pneumococcal conjugate vaccine, 7 valent', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381016, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '07/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381016, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '07/15/2010 13:00', '07/15/2010 14:00', 381016, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381016, null, null, null, null, '100', 'pneumococcal conjugate vaccine, 7 valent', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (381017, 381000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '12/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey],  [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (381017, 381000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '12/15/2010 13:00', '12/15/2010 14:00', 381017, 4)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (381017, null, null, null, null, '100', 'pneumococcal conjugate vaccine, 7 valent', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)
GO


print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0038 Childhood Immunization Status' 
print '									- (h) Hepatitis A (Hep A) Vaccine'
print '									- (i) Rotavirus (RV) Vaccine'
print '									- (j) Influenza (Inf) Vaccine'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Colby QoM Green - Patient Characteristic Age 1-2'
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 382000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 382000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'colby.green@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Colby', N'QoM', N'Green', NULL, NULL, 'PUI382000',  '09/01/2009', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Colby QoM Green -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 382000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 382000, '20100706 12:27:34.7680639 +00:00', 1 ,'20100706 12:27:34.7680639 +00:00', 1, 0, 1, '20100706 12:27:34.7680639 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Routine check.', 382000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Appointments between birth and 12/30/2010'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 382000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (382000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '02/10/2010 13:00', '02/10/2010 14:00', 100010020001, 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (382001, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '05/10/2010 13:00', '05/10/2010 14:00', 100010020001, 'Regular visit.' )
																																																																																																			  
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (382002, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '02/11/2010 13:00', '02/11/2010 14:00', 100010020001, 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (382003, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '05/11/2010 13:00', '05/11/2010 14:00', 100010020001, 'Regular visit.' )
																																																																																																			   
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (382004, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '05/12/2010 13:00', '05/12/2010 14:00', 100010020001, 'Regular visit.' )
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (382005, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, NULL, '07/12/2010 13:00', '07/12/2010 14:00', 100010020001, 'Regular visit.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Colby QoM Green -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 382000 */
/* [VisitModule].[Activity]: ActivityKey starts at 382000 */
/* [ImmunizationModule].[Immunization]: ImmunizationKey starts at 382000 */

/* hepa */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (382000, 382000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '02/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (382000, 382000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '02/10/2010 13:00', '02/10/2010 14:00', 382000, 9)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (382000, null, null, null, null, '83', 'hepatitis A vaccine, pediatric/adolescent dosage, 2 dose schedule', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (382001, 382000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '05/10/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (382001, 382000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '05/10/2010 13:00', '05/10/2010 14:00', 382001, 9)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (382001, null, null, null, null, '83', 'hepatitis A vaccine, pediatric/adolescent dosage, 2 dose schedule', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

/* rv */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (382002, 382000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '02/11/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (382002, 382000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '02/11/2010 13:00', '02/11/2010 14:00', 382002, 9)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (382002, null, null, null, null, '116', 'rotavirus, live, pentavalent vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (382003, 382000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '05/11/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (382003, 382000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '05/11/2010 13:00', '05/11/2010 14:00', 382003, 9)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (382003, null, null, null, null, '116', 'rotavirus, live, pentavalent vaccine', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

/* inf */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (382004, 382000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '05/12/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (382004, 382000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '05/12/2010 13:00', '05/12/2010 14:00', 382004, 9)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (382004, null, null, null, null, '15', 'influenza virus vaccine, split virus', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)

INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (382005, 382000, 8042491, 100010020003, 'Established Patient - Primary Care - Adolescent', '99212', '07/12/2010 13:00')
INSERT INTO [VisitModule].[Activity] ([ActivityKey], [ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ActivityStartDateTime], [ActivityEndDateTime], [VisitKey], [ActivityTypeLkpKey]) VALUES (382005, 382000, '20110706 12:07:31.7680639 +00:00', 1, '20110706 12:07:31.7680639 +00:00', 1, 1, '07/12/2010 13:00', '07/12/2010 14:00', 382005, 9)
INSERT INTO [ImmunizationModule].[Immunization] ([ActivityKey], [AdministeredAmount], [VaccineLotNumber], [VaccineManufacturerName], [VaccineManufacturerCode], [VaccineCodedConceptCode], [VaccineDisplayName], [VaccineCodeSystemName], [VaccineCodeSystemIdentifier], [VaccineNullFlavorIndicator], [ImmunizationUnitOfMeasureLkpKey], [ImmunizationNotGivenReasonLkpKey]) VALUES (382005, null, null, null, null, '15', 'influenza virus vaccine, split virus', 'CVX', '2.16.840.1.113883.6.59', 0, null, null)
GO


print '------------------------------------------------------------------------------------------'
print 'Persona Data for Quality of Measure - NQF0105 Anti‐depressant medication management' 
print '									- (a) Antidepressant medication dispensed for at least 84 days following first diagnosis of major depression'
print '									- (b) Antidepressant medication dispensed for at least 180 days following first diagnosis of major depression'
print '------------------------------------------------------------------------------------------'

print '------------------------------------------------------------------------------------------'
print ' Mitch QoM Hopper - Patient Characteristic Age >= 18'
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Patient]: PatientKey starts at 105000 */
INSERT INTO [PatientModule].[Patient] ([PatientKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [RevisedTimestamp], [RevisedAccountKey], [BirthFirstName], [BirthLastName], [BirthCityName], [DeathDate], [MotherFirstName], [MotherMaidenName], [PaperFileIndicator], [InterpreterNeededIndicator], [ConfidentialFamilyInformationDescription], [EmailAddress], [SexualAbuseVictimIndicator], [PhysicalAbuseVictimIndicator], [DomesticAbuseVictimIndicator], [RegisteredSexOffenderIndicator], [RegisteredSexOffenderDate], [ConvictedOfArsonIndicator], [ConvictedOfArsonDate], [AssignedPostalCode], [CustodialStatusLkpKey], [BirthStateProvinceLkpKey], [BirthCountyAreaLkpKey], [CitizenshipCountryLkpKey], [AssignedCountyAreaLkpKey], [AssignedGeographicalRegionLkpKey], [RecordStatusLkpKey], [EducationStatusLkpKey], [ImmigrationStatusLkpKey], [LanguageLkpKey], [SmokingStatusLkpKey], [AgencyKey], [FirstName], [MiddleName], [LastName], [PrefixName], [SuffixName], [UniqueIdentifier], [BirthDate], [PatientGenderLkpKey], [MaritalStatusLkpKey], [ReligiousAffiliationLkpKey], [EthnicityLkpKey], [DetailedEthnicityLkpKey] ) VALUES ( 105000, '20100706 12:27:34.7680639 +00:00', 1, '20100706 12:27:34.7680639 +00:00', 1, 1, '20100706 12:27:34.7524404 +00:00', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, N'mitch.young@test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8269629, NULL, NULL, NULL, NULL,    100010020002, N'Mitch', N'QoM', N'Hopper', NULL, NULL, 'PUI105000',  '09/01/1950', 6337881, NULL, NULL, 6327635, NULL )
GO

print '------------------------------------------------------------------------------------------'
print 'Mitch QoM Hopper -> Clinical Case - 1'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[ClinicalCase]: ClinicalCaseKey starts at 105000 */
INSERT INTO [ClinicalCaseModule].[ClinicalCase] ([ClinicalCaseKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ClinicalCaseNumber], [ClinicalCaseStartDate], [ClinicalCaseCloseDate], [ClinicalCaseNote], [ClinicalCaseClosingNote], [AdmissionDate], [AdmissionNote], [DischargeDate], [DischargeNote], [PatientPresentingProblemNote], [PatientKey], [ClinicalCaseStatusLkpKey], [ReferralTypeLkpKey], [InitialContactMethodLkpKey], [DischargeReasonLkpKey], [InitialLocationKey], [PerformedByStaffKey], [AdmittedByStaffKey], [DischargedByStaffKey] ) VALUES ( 105000, '20110706 12:07:31.1303176 +00:00', 1 ,'20110706 12:07:31.1303176 +00:00', 1, 0, 1, '20110813 12:07:31.1303176 +00:00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Patient has depression.', 105000, 258420, 4740268, 2156123, 9747937, 100010020003, 100010020001, 100010020001, 100010020001 )
GO

print '------------------------------------------------------------------------------------------'
print 'Mitch QoM Hopper -> Clinical Case - 1 -> Problem - Major Depression, Code: 14183003'
print '------------------------------------------------------------------------------------------'
/* [ClinicalCaseModule].[Problem]: ProblemKey starts at 105000 */
INSERT INTO [ClinicalCaseModule].[Problem] ([ProblemKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [ProblemStatusLkpKey], [ClinicalCaseKey], [ProblemCodeCodedConceptCode], [ProblemCodeDisplayName], [ProblemCodeCodeSystemName], [ProblemCodeCodeSystemIdentifier], [ProblemCodeNullFlavorIndicator], [ProblemTypeLkpKey], [ObservedDate], [StatusChangedDate], [ObservedByStaffKey], [CauseOfDeathIndicator], [OnsetStartDate], [OnsetEndDate])VALUES (105000, '20101020 15:45:31.1303176 +00:00', 1, '20101020 15:45:31.1303176 +00:00', 1, 1, 9193667880, 105000, '14183003', 'Chronic major depressive disorder, single episode (disorder)', 'SNOMED CT', '2.16.840.1.113883.6.96', 0, 5, '3/24/2011','3/24/2011', 100010020001, null, '01/10/2010', null)
GO

print '------------------------------------------------------------------------------------------'
print 'Appointment'
print '------------------------------------------------------------------------------------------'
/* [AgencyModule].[Appointment]: AppointmentKey starts at 105000 */
INSERT INTO [AgencyModule].[Appointment] (AppointmentKey, CreatedTimestamp, CreatedBySystemAccountKey, UpdatedTimestamp, UpdatedBySystemAccountKey, Version, SubjectDescription, AppointmentStartDatetime, AppointmentEndDatetime, StaffKey, Note ) VALUES (105000, '20110706 12:07:31.1303176 +00:00', 1, '20110706 12:07:31.1303176 +00:00', 1, 1, NULL, '01/10/2010 09:00', '01/10/2010 10:00', 100010020001, 'I have deprssion.' )
GO

print '------------------------------------------------------------------------------------------'
print 'Mitch QoM Hopper -> Clinical Case - 1 -> Checkedin Visits'
print '------------------------------------------------------------------------------------------'
/* [VisitModule].[Visit]: VisitKey starts at 105000 */
/* [VisitModule].[VisitProblem]: VisitProblemKey starts at 105000 */
INSERT INTO [VisitModule].[Visit] (AppointmentKey, ClinicalCaseKey, VisitStatusLkpKey, ServiceLocationKey, Name, CptCode, CheckedInDateTime) VALUES (105000, 105000, 8042491, 100010020003, 'Established Patient - Primary Care - Adult', '99212', '01/10/2010 09:00')
INSERT INTO [VisitModule].[VisitProblem] ([VisitProblemKey], [Version], [CreatedTimestamp], [UpdatedTimestamp], [ProblemKey], [VisitKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey]) VALUES (105000, 1, '20100813 11:45:31.1303176 +00:00', '20100813 11:45:31.1303176 +00:00', 105000, 105000, 1, 1)
GO

print '------------------------------------------------------------------------------------------'
print 'Mitch QoM Hopper -> Medications'
print '------------------------------------------------------------------------------------------'
/* [PatientModule].[Medication]: [MedicationKey] starts at 105000 */
INSERT INTO [PatientModule].[Medication] ([MedicationKey], [Version], [MedicationDoseValue], [OverTheCounterIndicator], [InstructionsNote], [PrescribingPhysicianName], [DiscontinuedByPhysicianName], [DiscontinuedReasonOtherDescription], [FrequencyDescription], [CreatedTimestamp], [UpdatedTimestamp], [PatientKey], [RootMedicationCodedConceptCode], [RootMedicationDisplayName], [RootMedicationCodeSystemName], [RootMedicationCodeSystemIdentifier], [RootMedicationNullFlavorIndicator], [MedicationCodeCodedConceptCode], [MedicationCodeDisplayName], [MedicationCodeCodeSystemName], [MedicationCodeCodeSystemIdentifier], [MedicationCodeNullFlavorIndicator], [MedicationRouteLkpKey], [MedicationDoseUnitLkpKey], [MedicationStatusLkpKey], [DiscontinuedReasonLkpKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey], [UsageStartDate], [UsageEndDate]) VALUES( 105000,  1,  null,  null,  null,  null,  null,  null,  null,  '20110706 12:07:31.7680639 +00:00',  '20110706 12:07:31.7680639 +00:00',  105000,  '72625', 'duloxetine [72625]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  '596926', 'duloxetine 20 MG Enteric Coated Capsule [596926]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  null,  null,  1,  null,  1,  1,  '20091225',  null)
INSERT INTO [PatientModule].[Medication] ([MedicationKey], [Version], [MedicationDoseValue], [OverTheCounterIndicator], [InstructionsNote], [PrescribingPhysicianName], [DiscontinuedByPhysicianName], [DiscontinuedReasonOtherDescription], [FrequencyDescription], [CreatedTimestamp], [UpdatedTimestamp], [PatientKey], [RootMedicationCodedConceptCode], [RootMedicationDisplayName], [RootMedicationCodeSystemName], [RootMedicationCodeSystemIdentifier], [RootMedicationNullFlavorIndicator], [MedicationCodeCodedConceptCode], [MedicationCodeDisplayName], [MedicationCodeCodeSystemName], [MedicationCodeCodeSystemIdentifier], [MedicationCodeNullFlavorIndicator], [MedicationRouteLkpKey], [MedicationDoseUnitLkpKey], [MedicationStatusLkpKey], [DiscontinuedReasonLkpKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey], [UsageStartDate], [UsageEndDate]) VALUES( 105001,  1,  null,  null,  null,  null,  null,  null,  null,  '20110706 12:07:31.7680639 +00:00',  '20110706 12:07:31.7680639 +00:00',  105000,  '72625', 'duloxetine [72625]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  '596926', 'duloxetine 20 MG Enteric Coated Capsule [596926]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  null,  null,  1,  null,  1,  1,  '20091231',  null)
INSERT INTO [PatientModule].[Medication] ([MedicationKey], [Version], [MedicationDoseValue], [OverTheCounterIndicator], [InstructionsNote], [PrescribingPhysicianName], [DiscontinuedByPhysicianName], [DiscontinuedReasonOtherDescription], [FrequencyDescription], [CreatedTimestamp], [UpdatedTimestamp], [PatientKey], [RootMedicationCodedConceptCode], [RootMedicationDisplayName], [RootMedicationCodeSystemName], [RootMedicationCodeSystemIdentifier], [RootMedicationNullFlavorIndicator], [MedicationCodeCodedConceptCode], [MedicationCodeDisplayName], [MedicationCodeCodeSystemName], [MedicationCodeCodeSystemIdentifier], [MedicationCodeNullFlavorIndicator], [MedicationRouteLkpKey], [MedicationDoseUnitLkpKey], [MedicationStatusLkpKey], [DiscontinuedReasonLkpKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey], [UsageStartDate], [UsageEndDate]) VALUES( 105002,  1,  null,  null,  null,  null,  null,  null,  null,  '20110706 12:07:31.7680639 +00:00',  '20110706 12:07:31.7680639 +00:00',  105000,  '72625', 'duloxetine [72625]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  '596926', 'duloxetine 20 MG Enteric Coated Capsule [596926]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  null,  null,  1,  null,  1,  1,  '20100101',  null)
INSERT INTO [PatientModule].[Medication] ([MedicationKey], [Version], [MedicationDoseValue], [OverTheCounterIndicator], [InstructionsNote], [PrescribingPhysicianName], [DiscontinuedByPhysicianName], [DiscontinuedReasonOtherDescription], [FrequencyDescription], [CreatedTimestamp], [UpdatedTimestamp], [PatientKey], [RootMedicationCodedConceptCode], [RootMedicationDisplayName], [RootMedicationCodeSystemName], [RootMedicationCodeSystemIdentifier], [RootMedicationNullFlavorIndicator], [MedicationCodeCodedConceptCode], [MedicationCodeDisplayName], [MedicationCodeCodeSystemName], [MedicationCodeCodeSystemIdentifier], [MedicationCodeNullFlavorIndicator], [MedicationRouteLkpKey], [MedicationDoseUnitLkpKey], [MedicationStatusLkpKey], [DiscontinuedReasonLkpKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey], [UsageStartDate], [UsageEndDate]) VALUES( 105003,  1,  null,  null,  null,  null,  null,  null,  null,  '20110706 12:07:31.7680639 +00:00',  '20110706 12:07:31.7680639 +00:00',  105000,  '72625', 'duloxetine [72625]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  '596926', 'duloxetine 20 MG Enteric Coated Capsule [596926]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  null,  null,  1,  null,  1,  1,  '20100112',  null)
INSERT INTO [PatientModule].[Medication] ([MedicationKey], [Version], [MedicationDoseValue], [OverTheCounterIndicator], [InstructionsNote], [PrescribingPhysicianName], [DiscontinuedByPhysicianName], [DiscontinuedReasonOtherDescription], [FrequencyDescription], [CreatedTimestamp], [UpdatedTimestamp], [PatientKey], [RootMedicationCodedConceptCode], [RootMedicationDisplayName], [RootMedicationCodeSystemName], [RootMedicationCodeSystemIdentifier], [RootMedicationNullFlavorIndicator], [MedicationCodeCodedConceptCode], [MedicationCodeDisplayName], [MedicationCodeCodeSystemName], [MedicationCodeCodeSystemIdentifier], [MedicationCodeNullFlavorIndicator], [MedicationRouteLkpKey], [MedicationDoseUnitLkpKey], [MedicationStatusLkpKey], [DiscontinuedReasonLkpKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey], [UsageStartDate], [UsageEndDate]) VALUES( 105004,  1,  null,  null,  null,  null,  null,  null,  null,  '20110706 12:07:31.7680639 +00:00',  '20110706 12:07:31.7680639 +00:00',  105000,  '72625', 'duloxetine [72625]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  '596926', 'duloxetine 20 MG Enteric Coated Capsule [596926]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  null,  null,  1,  null,  1,  1,  '20100114',  null)
INSERT INTO [PatientModule].[Medication] ([MedicationKey], [Version], [MedicationDoseValue], [OverTheCounterIndicator], [InstructionsNote], [PrescribingPhysicianName], [DiscontinuedByPhysicianName], [DiscontinuedReasonOtherDescription], [FrequencyDescription], [CreatedTimestamp], [UpdatedTimestamp], [PatientKey], [RootMedicationCodedConceptCode], [RootMedicationDisplayName], [RootMedicationCodeSystemName], [RootMedicationCodeSystemIdentifier], [RootMedicationNullFlavorIndicator], [MedicationCodeCodedConceptCode], [MedicationCodeDisplayName], [MedicationCodeCodeSystemName], [MedicationCodeCodeSystemIdentifier], [MedicationCodeNullFlavorIndicator], [MedicationRouteLkpKey], [MedicationDoseUnitLkpKey], [MedicationStatusLkpKey], [DiscontinuedReasonLkpKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey], [UsageStartDate], [UsageEndDate]) VALUES( 105005,  1,  null,  null,  null,  null,  null,  null,  null,  '20110706 12:07:31.7680639 +00:00',  '20110706 12:07:31.7680639 +00:00',  105000,  '72625', 'duloxetine [72625]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  '596926', 'duloxetine 20 MG Enteric Coated Capsule [596926]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  null,  null,  1,  null,  1,  1,  '20100115',  null)
INSERT INTO [PatientModule].[Medication] ([MedicationKey], [Version], [MedicationDoseValue], [OverTheCounterIndicator], [InstructionsNote], [PrescribingPhysicianName], [DiscontinuedByPhysicianName], [DiscontinuedReasonOtherDescription], [FrequencyDescription], [CreatedTimestamp], [UpdatedTimestamp], [PatientKey], [RootMedicationCodedConceptCode], [RootMedicationDisplayName], [RootMedicationCodeSystemName], [RootMedicationCodeSystemIdentifier], [RootMedicationNullFlavorIndicator], [MedicationCodeCodedConceptCode], [MedicationCodeDisplayName], [MedicationCodeCodeSystemName], [MedicationCodeCodeSystemIdentifier], [MedicationCodeNullFlavorIndicator], [MedicationRouteLkpKey], [MedicationDoseUnitLkpKey], [MedicationStatusLkpKey], [DiscontinuedReasonLkpKey], [CreatedBySystemAccountKey], [UpdatedBySystemAccountKey], [UsageStartDate], [UsageEndDate]) VALUES( 105006,  1,  null,  null,  null,  null,  null,  null,  null,  '20110706 12:07:31.7680639 +00:00',  '20110706 12:07:31.7680639 +00:00',  105000,  '72625', 'duloxetine [72625]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  '596926', 'duloxetine 20 MG Enteric Coated Capsule [596926]', 'RxNorm', '2.16.840.1.113883.6.88', 0,  null,  null,  1,  null,  1,  1,  '20100720',  null)
GO

print '------------------------------------------------------------------------------------------'
print 'End Persona Data for Quality of Measure'
print '------------------------------------------------------------------------------------------'
