#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using C32Gen;
using C32Gen.DataTransferObject;
using NHibernate.Criterion;
using Pillar.Domain.NHibernate.Extensions;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Domain;
using Rem.WellKnownNames;
using ActivityType = Rem.WellKnownNames.VisitModule.ActivityType;
using PatientAddressType = Rem.WellKnownNames.PatientModule.PatientAddressType;
using PatientContactType = Rem.WellKnownNames.PatientModule.PatientContactType;
using PatientPhoneType = Rem.WellKnownNames.PatientModule.PatientPhoneType;
using SmokingStatus = Rem.WellKnownNames.PatientModule.SmokingStatus;
using VisitStatus = Rem.WellKnownNames.VisitModule.VisitStatus;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Factory for C32 dto.
    /// </summary>
    public class C32DtoFactory : IC32DtoFactory
    {
        #region Constants and Fields

        private readonly ISessionProvider _sessionProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="C32DtoFactory"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public C32DtoFactory ( ISessionProvider sessionProvider )
        {
            _sessionProvider = sessionProvider;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the C32 dto.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A <see cref="C32Gen.DataTransferObject.C32Dto"/></returns>
        public C32Dto CreateC32Dto ( long patientKey )
        {
            var patient = GetPatientForC32DtoGeneration ( patientKey );

            var dto = new C32Dto ();
            dto.Body = new C32BodyDto ();
            dto.Header = new C32HeaderDto ();

            var guid = new Guid ();
            var timestampInIso8601Format = DateTime.Now.ToString ( "yyyyMMddHHmm" );

            #region Header creation

            dto.Header.DocumentId = new AssigningAuthorityIdDto { Root = guid.ToString (), Extension = "1", AssigningAuthority = "FEI Systems Inc."};
            dto.Header.Title = string.Format (
                "{0} Continuity of Care Document for {1}", patient.Agency.AgencyProfile.AgencyName.LegalName, patient.Name.Complete );
            dto.Header.Version = new VersionDto { Number = 1, SetId = new IIDataTransferObject { Root = guid.ToString () } };

            //TODO: Lookup table for Confidentiality Code
            dto.Header.Confidentiality = new CodedConceptDataTransferObject { CodeSystem = Hl7Codes.Hl7ConfidentialityCode, Code = "N" };
            dto.Header.DocumentTimestamp = new ValueDataTransferObject { Value = timestampInIso8601Format };
            var personalInfoDto = new PersonalInfoDto ();
            personalInfoDto.PatientInfo = new PatientInfoDto ();
            personalInfoDto.PatientInfo.PersonId = new IIDataTransferObject { Extension = patient.Key.ToString (), Root = guid.ToString () };

            personalInfoDto.PatientInfo.PersonAddress = new AddressDto ();
            var patientHomeAddress =
                patient.Addresses.Where (
                    p => p.PatientAddressType.WellKnownName == PatientAddressType.Home ).FirstOrDefault ();
            if ( patientHomeAddress != null )
            {
                if ( !string.IsNullOrWhiteSpace ( patientHomeAddress.Address.FirstStreetAddress )
                     || !string.IsNullOrWhiteSpace ( patientHomeAddress.Address.SecondStreetAddress ) )
                {
                    personalInfoDto.PatientInfo.PersonAddress.StreetAddressLines = new List<string> ();
                    if ( !string.IsNullOrWhiteSpace ( patientHomeAddress.Address.FirstStreetAddress ) )
                    {
                        personalInfoDto.PatientInfo.PersonAddress.StreetAddressLines.Add ( patientHomeAddress.Address.FirstStreetAddress );
                    }
                    if ( !string.IsNullOrWhiteSpace ( patientHomeAddress.Address.SecondStreetAddress ) )
                    {
                        personalInfoDto.PatientInfo.PersonAddress.StreetAddressLines.Add ( patientHomeAddress.Address.SecondStreetAddress );
                    }
                }
                personalInfoDto.PatientInfo.PersonAddress.City = patientHomeAddress.Address.CityName;
                personalInfoDto.PatientInfo.PersonAddress.State = patientHomeAddress.Address.StateProvince.Name;
                personalInfoDto.PatientInfo.PersonAddress.PostalCode = patientHomeAddress.Address.PostalCode.Code;
            }

            personalInfoDto.PatientInfo.PersonPhone = new ValueDataTransferObject ();
            var patientHomePhone =
                patient.PhoneNumbers.Where (
                    p => p.PatientPhoneType.WellKnownName == PatientPhoneType.Home ).FirstOrDefault ();
            if ( patientHomePhone != null )
            {
                personalInfoDto.PatientInfo.PersonPhone.Value = patientHomePhone.PhoneNumber;
            }

            personalInfoDto.PatientInfo.PersonInfo = new PersonInfoDto
                {
                    PersonName = new PersonNameDto { Given = patient.Name.First, Family = patient.Name.Last, Suffix = patient.Name.Suffix },
                    PersonDateOfBirth =
                        new ValueDataTransferObject
                            {
                                Value = patient.Profile.BirthDate == null ? string.Empty : patient.Profile.BirthDate.Value.ToString ( "yyyyMMdd" )
                            }
                };
            if ( patient.BirthInfo != null )
            {
                personalInfoDto.PatientInfo.PersonInfo.BirthPlace = new NameDataTransferObject
                    {
                        Name =
                            string.Format (
                                "{0}, {1}, {2}",
                                patient.BirthInfo.BirthCityName,
                                patient.BirthInfo.BirthCountyArea,
                                patient.BirthInfo.BirthStateProvince )
                    };
            }

            if ( patient.Profile.PatientGender != null )
            {
                personalInfoDto.PatientInfo.PersonInfo.Gender = new CodedConceptDataTransferObject
                    {
                        Code = patient.Profile.PatientGender.AdministrativeGender.CodedConceptCode,
                        DisplayName = patient.Profile.PatientGender.AdministrativeGender.Name,
                        CodeSystem = patient.Profile.PatientGender.AdministrativeGender.CodeSystemIdentifier,
                        CodeSystemName = patient.Profile.PatientGender.AdministrativeGender.CodeSystemName
                    };
            }

            //if (patient.MaritalStatus != null)
            //{
            //    personalInfoDto.PatientInfo.PersonInfo.MaritalStatus = new CodedConceptDataTransferObject()
            //                        {
            //                            Code = "S",
            //                            DisplayName = patient.MaritalStatus.Name,
            //                            CodeSystem = "2.16.840.1.113883.5.2",
            //                            CodeSystemName = "HL7 Marital status"
            //                        };
            //}
            //if (patient.ReligiousAffiliation != null)
            //{
            //    personalInfoDto.PatientInfo.PersonInfo.ReligiousAffiliation = new CodedConceptDataTransferObject
            //                               {
            //                                   Code = "1022",
            //                                   DisplayName = patient.ReligiousAffiliation.Name,
            //                                   CodeSystem = "2.16.840.1.113883.5.1076",
            //                                   CodeSystemName = "ReligiousAffiliation"
            //                               };
            //}
            //var primaryRace =
            //    patient.Races.Where(p => (p.PrimaryIndicator != null && p.PrimaryIndicator.Value == true)).
            //        FirstOrDefault();
            ////TODO: Race CodedConceptLookupBase
            //if (primaryRace != null)
            //{
            //    personalInfoDto.PatientInfo.PersonInfo.Race = new CodedConceptDataTransferObject
            //                                                      {
            //                                                          Code = "2178-2",
            //                                                          DisplayName = primaryRace.Race.Name,
            //                                                          CodeSystem = "2.16.840.1.113883.6.238",
            //                                                          CodeSystemName = "CDC Race and Ethnicity"
            //                                                      };
            //    //TODO: Enthnicity? DetailedEnthnicity? Which one to use?
            //    var primaryRaceEthnicity = primaryRace.Race.RaceDetailedEthnicities.FirstOrDefault();
            //    if (primaryRaceEthnicity != null)
            //    {
            //        personalInfoDto.PatientInfo.PersonInfo.Ethnicity = new CodedConceptDataTransferObject
            //                                                               {
            //                                                                   Code = "2178-2",
            //                                                                   DisplayName =
            //                                                                       primaryRaceEthnicity.DetailedEthnicity.
            //                                                                       Name,
            //                                                                   CodeSystem = "2.16.840.1.113883.6.238",
            //                                                                   CodeSystemName = "CDC Race and Ethnicity"
            //                                                               };
            //    }
            //}

            dto.Header.PersonalInfo = personalInfoDto;

            //TODO: Our language on patient doesn't have these information
            var languageSpokenDto = new LanguageSpokenDto ();
            languageSpokenDto.LanguageCode = new CodeDto { Code = "en-US" };
            languageSpokenDto.ModeCode = new OriginalTextCodedConceptDto
                {
                    Code = "RWR", DisplayName = "Receive Written", CodeSystem = "2.16.840.1.113883.5.60", CodeSystemName = "LanguageAbilityMode"
                };
            languageSpokenDto.PreferenceInd = new ValueDataTransferObject { Value = "false" };
            if ( dto.Header.Languages == null )
            {
                dto.Header.Languages = new List<LanguageSpokenDto> ();
            }
            dto.Header.Languages.Add ( languageSpokenDto );

            var supportDto = new SupportDto ();

            //supportDto.Date = new OperatorDateTimeDto();

            var guardianPatientContact =
                patient.Contacts.FirstOrDefault (
                    patientContact =>
                    patientContact.ContactTypes.Any (
                        patientContactContactType =>
                        patientContactContactType.PatientContactType.WellKnownName ==
                        PatientContactType.Guardian ) );

            if ( guardianPatientContact != null )
            {
                supportDto.Guardian = new GuardianDto
                    {
                        ContactAddress =
                            new AddressDto
                                {
                                    StreetAddressLines =
                                        new List<string> { guardianPatientContact.FirstStreetAddress, guardianPatientContact.SecondStreetAddress },
                                    City = guardianPatientContact.CityName,
                                    PostalCode = guardianPatientContact.PostalCode
                                },
                        ContactName = new PersonNameDto { Given = guardianPatientContact.FirstName, Family = guardianPatientContact.LastName }
                    };

                if ( guardianPatientContact.StateProvince != null )
                {
                    supportDto.Guardian.ContactAddress.State = guardianPatientContact.StateProvince.Name;
                }
                var patientContactHomePhone =
                    guardianPatientContact.PhoneNumbers.Where (
                        p => p.PatientContactPhoneType.WellKnownName == PatientPhoneType.Home ).FirstOrDefault ();
                if ( patientContactHomePhone != null )
                {
                    supportDto.Guardian.ContactTelecom = new ValueDataTransferObject { Value = patientContactHomePhone.PhoneNumber };
                }
            }

            if ( dto.Header.Supports == null )
            {
                dto.Header.Supports = new List<SupportDto> ();
            }
            dto.Header.Supports.Add ( supportDto );

            var agency = patient.Agency;
            if ( agency != null )
            {
                var custodianDto = new CustodianDto
                    {
                        CustodianId = new IIDataTransferObject { Root = guid.ToString (), Extension = patient.Agency.Key.ToString () },
                        CustodianName = agency.AgencyProfile.AgencyName.LegalName
                    };
                dto.Header.Custodian = custodianDto;
            }

            ////TODO: Should HealthCareProviders be a collection?
            //var clinicalCase = patient.ClinicalCases.FirstOrDefault();
            //if (clinicalCase != null)
            //{
            //    var healthcareProvider = clinicalCase.PerformedByStaff;
            //    if (healthcareProvider != null)
            //    {
            //        var healthCareProvidersDto = new HealthCareProvidersDto()
            //        {
            //            HealthcareProvider = new HealthcareProviderDto()
            //            {
            //                Role = new OriginalTextCodedConceptDto()
            //                {
            //                    Code = "PCP",
            //                    CodeSystem = "2.16.840.1.113883.5.88",
            //                    OriginalText = "Primary Care Physician"
            //                },
            //                DateRange = new DateDateRangeDto() { StartDate = new ValueDataTransferObject() { Value = "19320924" }, EndDate = new ValueDataTransferObject() { Value = "20000407" } },

            //                ProviderEntity = new ProviderEntityDto()
            //                {
            //                    ProviderId = new IIDataTransferObject() { Root = "20cf14fb-b65c-4c8c-a54d-b0cca834c18c" },
            //                    ProviderName = new PersonNameDto() { Prefix = healthcareProvider.PrefixName, Given = healthcareProvider.FirstName , Family = healthcareProvider.LastName }
            //                }
            //            }
            //        };

            //        var healthProviderAgency = healthcareProvider.Agency;
            //        if (healthProviderAgency != null)
            //        {
            //            healthCareProvidersDto.HealthcareProvider.ProviderEntity.ProviderOrganizationName = healthProviderAgency.LegalName;
            //        }
            //        dto.Header.HealthCareProviders = healthCareProvidersDto;
            //    }
            //}

            // TODO: Author? Creating Machine or Person
            var informationSourceDto = new InformationSourceDto ();
            informationSourceDto.Author = new AuthorDto
                {
                    AuthorTime = new ValueDataTransferObject { Value = "20000407130000+0500" },
                    AuthorName = new PersonNameDto { Prefix = "prefix", Given = "given", Family = "family", Suffix = "suffix" },
                    Reference = new ReferenceDto
                        {
                            ReferenceDocumentId = new IIDataTransferObject { Root = "root6", Extension = "extension6" },
                            ReferenceDocumentUrl = new ValueDto { Value = "http://www.feiinfo.com" }
                        }
                };

            //informationSourceDto.InformationSourceName = new InformationSourceNameDto
            //{
            //    PersonName = new PersonNameDto { Prefix = "prefix", Given = "given", Family = "family", Suffix = "suffix" },
            //};
            dto.Header.InformationSource = informationSourceDto;

            #endregion Header creation

            #region Body creation

            foreach (var clinicalCase in patient.ClinicalCases)
            {
                foreach ( var visit in clinicalCase.Visits.Where ( p => p.VisitStatus.WellKnownName == VisitStatus.CheckedIn ) )
                {
                    #region Encounters

                    AddEncounterDto ( dto, visit );

                    #endregion Encounters

                    var visitActivities = visit.Activities.ToList ();

                    #region Encounters of BriefIntervention

                    foreach (
                        var briefIntervention in visitActivities.Where ( p => p.ActivityType.WellKnownName == ActivityType.BriefIntervention ) )
                    {
                        if ( ( briefIntervention as BriefIntervention ).TobaccoCessationCounselingIndicator.HasValue
                             && ( briefIntervention as BriefIntervention ).TobaccoCessationCounselingIndicator.Value )
                        {
                            var tobaccoCessationCounselingEncounter = AddEncounterDto ( dto, visit );
                            tobaccoCessationCounselingEncounter.EncounterType = new OriginalTextCodedConceptDto
                                {
                                    Code = "99406",
                                    CodeSystem = "2.16.840.1.113883.6.12",
                                    CodeSystemName = "CPT",
                                    DisplayName = "Tobacco Use Cessation Counseling"
                                };
                        }
                    }

                    #endregion

                    #region Problem List (aka Conditions, Diagnoses ), from Visit Problem

                    var visitProblems = visit.Problems;
                    foreach ( var visitProblem in visitProblems )
                    {
                        var problem = visitProblem.Problem;

                        AddProblem ( dto, problem, visit.AppointmentDateTimeRange.StartDateTime );
                    }

                    #endregion Problem List (aka Conditions, Diagnoses), from Visit Problem

                    #region Diagnostic Results (aka Labs)

                    foreach ( var labSpeciment in visitActivities.Where ( p => p.ActivityType.WellKnownName == ActivityType.LabSpecimen ) )
                    {
                        foreach ( var labTest in ( labSpeciment as LabSpecimen ).LabTests )
                        {
                            foreach ( var labResult in labTest.LabResults )
                            {
                                var resultDto = new ResultDto ();
                                resultDto.ResultId = new IIDataTransferObject { Root = guid.ToString (), Extension = labResult.Key.ToString () };
                                if ( labTest.LabTestInfo.TestReportDate != null )
                                {
                                    resultDto.ResultDateTime = new OperatorDateTimeDto
                                        {
                                            Date = labTest.LabTestInfo.TestReportDate.Value.ToString ( "yyyyMMdd" )
                                        };
                                }

                                resultDto.ProcedureCode = new OriginalTextCodedConceptDto
                                    {
                                        Code = labResult.LabTestResultNameCodedConcept.CodedConceptCode,
                                        CodeSystem = labResult.LabTestResultNameCodedConcept.CodeSystemIdentifier,
                                        CodeSystemName = labResult.LabTestResultNameCodedConcept.CodeSystemName,
                                        DisplayName = labResult.LabTestResultNameCodedConcept.DisplayName,
                                        OriginalText = labResult.LabTestResultNameCodedConcept.OriginalDescription
                                    };

                                if ( labTest.LabTestInfo.LabTestTypeCodedConcept != null )
                                {
                                    resultDto.ResultType = new OriginalTextCodedConceptDto
                                        {
                                            Code = labTest.LabTestInfo.LabTestTypeCodedConcept.CodedConceptCode,
                                            CodeSystem = labTest.LabTestInfo.LabTestTypeCodedConcept.CodeSystemIdentifier,
                                            CodeSystemName = labTest.LabTestInfo.LabTestTypeCodedConcept.CodeSystemName,
                                            DisplayName = labTest.LabTestInfo.LabTestTypeCodedConcept.DisplayName,
                                        };
                                }
                                else
                                {
                                    resultDto.ResultType = new OriginalTextCodedConceptDto
                                        {
                                            Code = "30313-1",
                                            CodeSystem = "2.16.840.1.113883.6.1",
                                            CodeSystemName = "LOINC",
                                            DisplayName = "HGB",
                                        };
                                }

                                resultDto.ResultStatus = labTest.LabTestInfo.LabResultStatusCodedConcept != null
                                                             ? new CodeDto { Code = labTest.LabTestInfo.LabResultStatusCodedConcept.CodedConceptCode }
                                                             : new CodeDto { Code = "completed" };

                                if ( labResult.Value != null )
                                {
                                    resultDto.ResultValue = new ResultValueDto
                                        {
                                            PhysicalQuantity =
                                                new ValueUnitDataTransferObject
                                                    {
                                                        Value = labResult.Value.Value.ToString ( "F1" ),
                                                        Unit = labResult.UnitOfMeasureCode
                                                    }
                                        };
                                }
                                if ( labTest.LabTestInfo.InterpretationCodeCodedConcept != null )
                                {
                                    resultDto.ResultInterpretation = new OriginalTextCodedConceptDto
                                        {
                                            Code = labTest.LabTestInfo.InterpretationCodeCodedConcept.CodedConceptCode,
                                            CodeSystem = labTest.LabTestInfo.InterpretationCodeCodedConcept.CodeSystemIdentifier,
                                            CodeSystemName = labTest.LabTestInfo.InterpretationCodeCodedConcept.CodeSystemName,
                                            DisplayName = labTest.LabTestInfo.InterpretationCodeCodedConcept.DisplayName,
                                        };
                                }
                                resultDto.ResultReferenceRange = labTest.LabTestInfo.NormalRangeDescription;

                                if ( dto.Body.Results == null )
                                {
                                    dto.Body.Results = new List<ResultDto> ();
                                }
                                dto.Body.Results.Add ( resultDto );
                            }
                        }
                    }

                    #endregion

                    foreach ( var vitalSignActivity in visitActivities.Where ( p => p.ActivityType.WellKnownName == ActivityType.VitalSign ) )
                    {
                        #region Vital Signs

                        var vitalSign = vitalSignActivity as VitalSign;

                        // Body Height
                        if ( vitalSign.Height != null && ( vitalSign.Height.FeetMeasure != null || vitalSign.Height.InchesMeasure != null ) )
                        {
                            var vitalSignBodyHeightDto = GetVitalSignResultDto ( visit, vitalSign );

                            var heightCmMeasure = ( ( vitalSign.Height.FeetMeasure == null
                                                          ? 0
                                                          : vitalSign.Height.FeetMeasure.Value ) * 12.0 ) +
                                                  ( ( ( vitalSign.Height.InchesMeasure == null
                                                            ? 0
                                                            : vitalSign.Height.InchesMeasure.Value ) ) * 2.54 );
                            vitalSignBodyHeightDto.ResultType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.96",
                                    CodeSystemName = "SNOMED CT",
                                    Code = "50373000",
                                    DisplayName = "Body Height"
                                };
                            vitalSignBodyHeightDto.ResultStatus = new CodeDto { Code = "completed" };
                            vitalSignBodyHeightDto.ResultValue = new ResultValueDto
                                {
                                    PhysicalQuantity =
                                        new ValueUnitDataTransferObject { Value = Math.Round ( heightCmMeasure ).ToString (), Unit = "cm" }
                                };

                            AddVitalSignResultDto ( dto, vitalSignBodyHeightDto );
                        }

                        // Body Weight
                        if ( vitalSign.WeightLbsMeasure != null )
                        {
                            var vitalSignBodyWeightDto = GetVitalSignResultDto ( visit, vitalSign );
                            vitalSignBodyWeightDto.ResultType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.96",
                                    CodeSystemName = "SNOMED CT",
                                    Code = "27113001",
                                    DisplayName = "Body Weight"
                                };
                            vitalSignBodyWeightDto.ResultStatus = new CodeDto { Code = "completed" };
                            vitalSignBodyWeightDto.ResultValue = new ResultValueDto
                                {
                                    PhysicalQuantity =
                                        new ValueUnitDataTransferObject { Value = vitalSign.WeightLbsMeasure.Value.ToString ( "0.00" ), Unit = "lbs" }
                                };

                            AddVitalSignResultDto ( dto, vitalSignBodyWeightDto );
                        }

                        // BMI
                        var bmi = vitalSign.CalculateBmi ();
                        if ( bmi != null )
                        {
                            var vitalSignBmiDto = GetVitalSignResultDto ( visit, vitalSign );
                            vitalSignBmiDto.ResultType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.96",
                                    CodeSystemName = "SNOMED CT",
                                    Code = "225171007",
                                    DisplayName = "Body mass index"
                                };
                            vitalSignBmiDto.ResultStatus = new CodeDto { Code = "completed" };
                            vitalSignBmiDto.ResultValue = new ResultValueDto
                                {
                                    PhysicalQuantity =
                                        new ValueUnitDataTransferObject { Value = Math.Round ( bmi.Value ).ToString (), Unit = "kg/m2" }
                                };

                            AddVitalSignResultDto ( dto, vitalSignBmiDto );

                            var vitalSignBmiPercentileDto = GetVitalSignResultDto ( visit, vitalSign );
                            vitalSignBmiPercentileDto.ResultType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.96",
                                    CodeSystemName = "SNOMED CT",
                                    Code = "162860001",
                                    DisplayName = "BMI percentile"
                                };
                            vitalSignBmiPercentileDto.ResultStatus = new CodeDto { Code = "completed" };
                            vitalSignBmiPercentileDto.ResultValue = new ResultValueDto
                                {
                                    PhysicalQuantity = new ValueUnitDataTransferObject ()

                                    //{ Value = Math.Round(bmi.Value).ToString(), Unit = "kg/m2" }
                                };

                            AddVitalSignResultDto ( dto, vitalSignBmiPercentileDto );
                        }

                        // Blood Pressures
                        foreach ( var bloodPressure in vitalSign.BloodPressures )
                        {
                            var vitalSignSystollicBpDto = GetVitalSignResultDto ( visit, vitalSign );
                            vitalSignSystollicBpDto.ResultId = new IIDataTransferObject { Root = bloodPressure.Key.ToString () };
                            vitalSignSystollicBpDto.ResultType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.96",
                                    CodeSystemName = "SNOMED CT",
                                    Code = "12929001",
                                    DisplayName = "Systolic BP"
                                };
                            vitalSignSystollicBpDto.ResultStatus = new CodeDto { Code = "completed" };
                            vitalSignSystollicBpDto.ResultValue = new ResultValueDto
                                {
                                    PhysicalQuantity =
                                        new ValueUnitDataTransferObject { Value = bloodPressure.SystollicMeasure.ToString (), Unit = "mm[Hg]" }
                                };

                            AddVitalSignResultDto ( dto, vitalSignSystollicBpDto );

                            var vitalSignDiastolicBpDto = GetVitalSignResultDto ( visit, vitalSign );
                            vitalSignDiastolicBpDto.ResultId = new IIDataTransferObject { Root = bloodPressure.Key.ToString () };
                            vitalSignDiastolicBpDto.ResultType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.96",
                                    CodeSystemName = "SNOMED",
                                    Code = "163031004",
                                    DisplayName = "Diastolic BP"
                                };
                            vitalSignDiastolicBpDto.ResultStatus = new CodeDto { Code = "completed" };
                            vitalSignDiastolicBpDto.ResultValue = new ResultValueDto
                                {
                                    PhysicalQuantity =
                                        new ValueUnitDataTransferObject { Value = bloodPressure.DiastollicMeasure.ToString (), Unit = "mm[Hg]" }
                                };

                            AddVitalSignResultDto ( dto, vitalSignDiastolicBpDto );
                        }

                        // Heart Rates
                        foreach ( var hearRate in vitalSign.HeartRates )
                        {
                            var vitalSignSystollicBpDto = GetVitalSignResultDto ( visit, vitalSign );
                            vitalSignSystollicBpDto.ResultId = new IIDataTransferObject { Root = hearRate.Key.ToString () };
                            vitalSignSystollicBpDto.ResultType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.96",
                                    CodeSystemName = "SNOMED",
                                    Code = "364075005",
                                    DisplayName = "Heart rate"
                                };
                            vitalSignSystollicBpDto.ResultStatus = new CodeDto { Code = "completed" };
                            vitalSignSystollicBpDto.ResultValue = new ResultValueDto
                                {
                                    PhysicalQuantity =
                                        new ValueUnitDataTransferObject
                                            {
                                                Value = hearRate.BeatsPerMinuteMeasure.ToString (),
                                                Unit = "beats per minute"
                                            }
                                };

                            AddVitalSignResultDto ( dto, vitalSignSystollicBpDto );
                        }

                        #endregion Vital Signs

                        #region Plan of Care

                        // The Dietary Consultation Order maps to the C32 using a SNOMED code.
                        if ( vitalSign.DietaryConsultationOrderIndicator != null && vitalSign.DietaryConsultationOrderIndicator.Value )
                        {
                            var plannedObservationPlannedEventDto = new PlannedEventDto
                                {
                                    PlanFreeText = "dietary consultation order",
                                    PlanId = new IIDataTransferObject { Root = vitalSign.Key.ToString () },
                                    PlannedTime =
                                        new OperatorDateTimeDto
                                            { Date = visit.AppointmentDateTimeRange.StartDateTime.AddDays ( 10 ).ToString ( "yyyyMMdd" ) },
                                    PlanType = new OriginalTextCodedConceptDto
                                        {
                                            Code = "103699006",
                                            DisplayName = "dietary consultation order",
                                            CodeSystem = "2.16.840.1.113883.6.96"
                                        }
                                };

                            //TODO: Hard coded 10 days after the visit for the planned event

                            AddPlannedEvent ( dto, plannedObservationPlannedEventDto );
                        }

                        // The Follow-up Management Plan for BMI Management maps to the c32 using a HCPCS code.
                        if ( vitalSign.BmiFollowUpPlanIndicator != null && vitalSign.BmiFollowUpPlanIndicator.Value )
                        {
                            var plannedObservationPlannedEventDto = new PlannedEventDto
                                {
                                    PlanFreeText = "BMI Management",
                                    PlanId = new IIDataTransferObject { Root = vitalSign.Key.ToString () },
                                    PlannedTime =
                                        new OperatorDateTimeDto
                                            { Date = visit.AppointmentDateTimeRange.StartDateTime.AddDays ( 10 ).ToString ( "yyyyMMdd" ) },
                                    PlanType = new OriginalTextCodedConceptDto
                                        {
                                            Code = "169411000",
                                            DisplayName = "BMI Management",
                                            CodeSystem = "2.16.840.1.113883.6.96"
                                        }
                                };

                            //TODO: Hard coded 10 days after the visit for the planned event

                            AddPlannedEvent ( dto, plannedObservationPlannedEventDto );
                        }

                        #endregion Plan of Care
                    }

                    foreach (
                        var socialHistoryActivity in visitActivities.Where ( p => p.ActivityType.WellKnownName == ActivityType.SocialHistory ) )
                    {
                        var socialHistory = socialHistoryActivity as SocialHistory;

                        #region Vital Signs and SocialHistory for Tobacco User

                        if ( socialHistory.SocialHistorySmoking != null && socialHistory.SocialHistorySmoking.SmokingStatus != null )
                        {
                            if ( socialHistory.SocialHistorySmoking.SmokingStatus.WellKnownName == SmokingStatus.EverydaySmoker ||
                                 socialHistory.SocialHistorySmoking.SmokingStatus.WellKnownName == SmokingStatus.SomedaySmoker )
                            {
                                var vitalSignTobaccoUserpDto = new ResultDto ();
                                vitalSignTobaccoUserpDto.ResultId = new IIDataTransferObject { Root = socialHistory.Key.ToString () };
                                vitalSignTobaccoUserpDto.ResultDateTime = new OperatorDateTimeDto
                                    {
                                        Date = visit.AppointmentDateTimeRange.StartDateTime.ToString ( "yyyyMMdd" )
                                    };

                                vitalSignTobaccoUserpDto.ResultType = new OriginalTextCodedConceptDto
                                    {
                                        CodeSystem = "2.16.840.1.113883.6.96",
                                        CodeSystemName = "SNOMED",
                                        Code = "160603005",
                                        DisplayName = "Tobacco User"
                                    };
                                vitalSignTobaccoUserpDto.ResultStatus = new CodeDto { Code = "completed" };

                                AddVitalSignResultDto ( dto, vitalSignTobaccoUserpDto );

                                var socialHistoryTypeDto = new OriginalTextCodedConceptDto
                                    {
                                        CodeSystem = "2.16.840.1.113883.6.96",
                                        CodeSystemName = "SNOMED CT",
                                        Code = "160603005"
                                    };
                                AddSocialHistory ( dto, visit, socialHistory, socialHistoryTypeDto );
                            }
                        }

                        #endregion Vital Signs and SocialHistory for Tobacco User
                    }

                    #region Immunization and Influenza Immunization Procedures

                    foreach (
                        var immunizationActivity in visitActivities.Where ( p => p.ActivityType.WellKnownName == ActivityType.Immunization ) )
                    {
                        var immunization = immunizationActivity as Immunization;

                        var immunizationDto = new C32Gen.DataTransferObject.ImmunizationDto ();

                        //TODO: Which value to put here?
                        immunizationDto.AdministeredDate = new ValueDataTransferObject { Value = "199911" };

                        if ( immunization.ImmunizationVaccineInfo != null && immunization.ImmunizationVaccineInfo.VaccineCodedConcept != null )
                        {
                            immunizationDto.MedicationInformations = new List<MedicationInformationDto> ();

                            var medicationInformationDto = new MedicationInformationDto ();
                            immunizationDto.MedicationInformations.Add ( medicationInformationDto );

                            medicationInformationDto.CodedProductName = new OriginalTextCodedConceptDto
                                {
                                    Code = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.CodedConceptCode,
                                    DisplayName = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.DisplayName,
                                    CodeSystem = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.CodeSystemIdentifier,
                                    CodeSystemName = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.CodeSystemName
                                };

                            //medicationInformationDto.CodedBrandName = new CodedConceptDataTransferObject() { Code = "code41", DisplayName = "displayName39", CodeSystem = "codeSystem39", CodeSystemName = "codeSystemName39" };
                            //medicationInformationDto.FreeTextProductName = "Influenza virus vaccine";
                            //medicationInformationDto.FreeTextBrandName = "freeTextBrandName0";

                            if ( immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer != null
                                 &&
                                 ( immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerCode != null
                                   || immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerName != null ) )
                            {
                                medicationInformationDto.DrugManufacturer = new OrganizationDto ();
                                if ( immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerCode != null )
                                {
                                    medicationInformationDto.DrugManufacturer.OrganizationId = new IIDataTransferObject
                                        {
                                            Root = immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerCode
                                        };
                                }

                                //medicationInformationDto.DrugManufacturer.OrganizationAddress = new AddressDto() { StreetAddress = "Street Address", City = "city", State = "state", PostalCode = "postalcode"};
                                if ( immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerName != null )
                                {
                                    medicationInformationDto.DrugManufacturer.OrganizationName = new TextNullFlavorDataTransferObject
                                        {
                                            TextValue = immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerName
                                        };
                                }

                                //medicationInformationDto.DrugManufacturer.OrganizationTelecom = new ValueDataTransferObject(){Value = "1-999-999-9999"};
                            }
                        }

                        //immunizationDto.MedicationSeriesNumber = new ValueDataTransferObject() { Value = "123456789" };
                        //immunizationDto.Provider = new OrganizationDto
                        //                               {
                        //                                   OrganizationId = new IIDataTransferObject() { Root = "root11", Extension = "extension11" },
                        //                                   OrganizationAddress = new AddressDto() { StreetAddress = "Street Address", City = "city", State = "state", PostalCode = "postalcode" },
                        //                                   OrganizationName = new TextNullFlavorDataTransferObject() { Value = "drug manufacturer" },
                        //                                   OrganizationTelecom = new ValueDataTransferObject() { Value = "1-999-999-9999" }
                        //                               };
                        //immunizationDto.Reactions = new List<OriginalTextCodedConceptDto>()
                        //    {
                        //        new OriginalTextCodedConceptDto()
                        //        {
                        //            Code = "43789009",
                        //            CodeSystem = "2.16.840.1.113883.6.96",
                        //            DisplayName = "CBC WO DIFFERENTIAL",
                        //            OriginalText = "Extract blood for CBC test"
                        //        }
                        //    };
                        //TODO:
                        if ( immunization.ImmunizationNotGivenReason != null )
                        {
                            //immunizationDto.RefusalInd = "Refusal Ind";
                            immunizationDto.RefusalReason = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = immunization.ImmunizationNotGivenReason.CodeSystemIdentifier,
                                    CodeSystemName = immunization.ImmunizationNotGivenReason.CodeSystemName,
                                    Code = immunization.ImmunizationNotGivenReason.CodedConceptCode,
                                    DisplayName = immunization.ImmunizationNotGivenReason.Name
                                };
                        }

                        if ( dto.Body.ImmunizationsDto == null )
                        {
                            dto.Body.ImmunizationsDto = new ImmunizationsDto ();
                        }
                        if ( dto.Body.ImmunizationsDto.Immunizations == null )
                        {
                            dto.Body.ImmunizationsDto.Immunizations = new List<C32Gen.DataTransferObject.ImmunizationDto> ();
                        }
                        dto.Body.ImmunizationsDto.Immunizations.Add ( immunizationDto );

                        #region Procedure of Immunization

                        var procedureDto = new ProcedureDto ();

                        procedureDto.ProcedureDateTime = new OperatorDateTimeDto
                            {
                                Date = visit.AppointmentDateTimeRange.StartDateTime.ToString ( "yyyyMMdd" )
                            };

                        procedureDto.ProcedureFreeTextType = "Influenza Vaccination";
                        procedureDto.ProcedureIds = new List<IIDataTransferObject> ();
                        procedureDto.ProcedureIds = new List<IIDataTransferObject>
                            {
                                new IIDataTransferObject { Root = immunization.Key.ToString () }
                            };
                        if ( visit != null && visit.Staff != null )
                        {
                            var providerInformationDto = new ProviderInformationDto
                                {
                                    ProviderId = new IIDataTransferObject { Root = visit.Staff.Key.ToString () },
                                    ProviderName = new PersonNameDto
                                        {
                                            Family = visit.Staff.StaffProfile.StaffName.Last,
                                            Given = visit.Staff.StaffProfile.StaffName.First,
                                            Prefix = visit.Staff.StaffProfile.StaffName.Prefix,
                                            Suffix = visit.Staff.StaffProfile.StaffName.Suffix
                                        }
                                };

                            if ( visit.Staff.Agency != null )
                            {
                                providerInformationDto.ProviderOrganizationName = new TextNullFlavorDataTransferObject
                                    {
                                        TextValue = visit.Staff.Agency.AgencyProfile.AgencyName.LegalName
                                    };
                            }

                            if ( procedureDto.ProcedureProviders == null )
                            {
                                procedureDto.ProcedureProviders = new List<ProviderInformationDto> ();
                            }

                            procedureDto.ProcedureProviders.Add ( providerInformationDto );
                        }

                        if ( immunization.ImmunizationVaccineInfo != null )
                        {
                            procedureDto.ProcedureType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.CodeSystemIdentifier,
                                    CodeSystemName = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.CodeSystemName,
                                    Code = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.CodedConceptCode,
                                    DisplayName = immunization.ImmunizationVaccineInfo.VaccineCodedConcept.DisplayName
                                };
                        }

                        if ( dto.Body.ProceduresDto == null )
                        {
                            dto.Body.ProceduresDto = new ProceduresDto ();
                        }
                        if ( dto.Body.ProceduresDto.Procedures == null )
                        {
                            dto.Body.ProceduresDto.Procedures = new List<ProcedureDto> ();
                        }
                        dto.Body.ProceduresDto.Procedures.Add ( procedureDto );

                        #endregion Procedure of Immunization
                    }

                    #endregion Immunization and Influenza Immunization Procedures

                    #region Procedures of BriefIntervention

                    foreach (
                        var briefIntervention in visitActivities.Where ( p => p.ActivityType.WellKnownName == ActivityType.BriefIntervention ) )
                    {
                        if ( ( briefIntervention as BriefIntervention ).TobaccoCessationCounselingIndicator.HasValue
                             && ( briefIntervention as BriefIntervention ).TobaccoCessationCounselingIndicator.Value )
                        {
                            var procedureFreeTextType = "Tobacco Use Cessation Counseling";
                            var procedureType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.12",
                                    CodeSystemName = "CPT",
                                    Code = "99406",
                                    DisplayName = "Tobacco Use Cessation Counseling"
                                };
                            AddProcedureForBriefIntervention (
                                dto, visit, briefIntervention as BriefIntervention, procedureFreeTextType, procedureType );
                        }

                        if ( ( briefIntervention as BriefIntervention ).NutritionCounselingIndicator.HasValue
                             && ( briefIntervention as BriefIntervention ).NutritionCounselingIndicator.Value )
                        {
                            var procedureFreeTextType = "nutrition counseling";
                            var procedureType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.12",
                                    CodeSystemName = "CPT",
                                    Code = "97802",
                                    DisplayName = "nutrition counseling"
                                };
                            AddProcedureForBriefIntervention (
                                dto, visit, briefIntervention as BriefIntervention, procedureFreeTextType, procedureType );
                        }

                        if ( ( briefIntervention as BriefIntervention ).PhysicalActivityCounselingIndicator.HasValue
                             && ( briefIntervention as BriefIntervention ).PhysicalActivityCounselingIndicator.Value )
                        {
                            var procedureFreeTextType = "physical activity counseling";
                            var procedureType = new OriginalTextCodedConceptDto
                                {
                                    CodeSystem = "2.16.840.1.113883.6.14",
                                    CodeSystemName = "HCPCS",
                                    Code = "S9451",
                                    DisplayName = "physical activity counseling"
                                };
                            AddProcedureForBriefIntervention (
                                dto, visit, briefIntervention as BriefIntervention, procedureFreeTextType, procedureType );
                        }
                    }

                    #endregion Procedures

                    //#region Social History (Smoking Status)

                    //foreach ( Activity procedureActivity in visitActivities.Where ( p => p.ActivityType.WellKnownName == ActivityType.SocialHistory ) )
                    //{
                    //}

                    //#endregion Social History (Smoking Status)
                }
            }

            #region Problem List (aka Conditions, Diagnoses )

            foreach ( var clinicalCaseForProblems in patient.ClinicalCases )
            {
                var problems = clinicalCaseForProblems.Problems;
                foreach ( var problem in problems )
                {
                    AddProblem ( dto, problem, null );
                }
            }

            #endregion Problem List (aka Conditions, Diagnoses )

            #region Allergies and Reactions

            var allergies = patient.Allergies;
            foreach ( var allergy in allergies )
            {
                var allergyDto = new AllergyDto ();

                if ( allergy.AllergyType != null )
                {
                    allergyDto.AdverseEventType = new OriginalTextCodedConceptDto
                        {
                            Code = allergy.AllergyType.CodedConceptCode,
                            CodeSystem = allergy.AllergyType.CodeSystemIdentifier,
                            DisplayName = allergy.AllergyType.Name,
                            CodeSystemName = allergy.AllergyType.CodeSystemName
                        };
                }

                allergyDto.AdverseEventDate = new OperatorDateTimeDto ();

                if ( allergy.OnsetDateRange != null && ( allergy.OnsetDateRange.StartDate != null || allergy.OnsetDateRange.EndDate != null ) )
                {
                    if ( allergy.OnsetDateRange.StartDate != null )
                    {
                        allergyDto.AdverseEventDate.StartDate = new ValueDataTransferObject
                            {
                                Value = allergy.OnsetDateRange.StartDate.Value.ToString ( "yyyyMMdd" )
                            };
                    }
                    if ( allergy.OnsetDateRange.EndDate != null )
                    {
                        allergyDto.AdverseEventDate.EndDate = new ValueDataTransferObject
                            {
                                Value = allergy.OnsetDateRange.EndDate.Value.ToString ( "yyyyMMdd" )
                            };
                    }
                }

                if ( allergy.AllergenCodedConcept != null )
                {
                    allergyDto.Product = new OriginalTextCodedConceptDto
                        {
                            CodeSystem = allergy.AllergenCodedConcept.CodeSystemIdentifier,
                            DisplayName = allergy.AllergenCodedConcept.DisplayName,
                            Code = allergy.AllergenCodedConcept.CodedConceptCode,
                            CodeSystemName = allergy.AllergenCodedConcept.CodeSystemName
                        };
                }

                var allergyDtoReactions = new List<OriginalTextCodedConceptDto> ();
                foreach ( var allergyReaction in allergy.AllergyReactions )
                {
                    allergyDtoReactions.Add (
                        new OriginalTextCodedConceptDto
                            {
                                CodeSystem = allergyReaction.Reaction.CodeSystemIdentifier,
                                DisplayName = allergyReaction.Reaction.Name,
                                Code = allergyReaction.Reaction.CodedConceptCode,
                                CodeSystemName = allergyReaction.Reaction.CodeSystemName
                            } );
                }
                allergyDto.Reactions = allergyDtoReactions;

                if ( allergy.AllergySeverityType != null )
                {
                    allergyDto.Severity = new OriginalTextCodedConceptDto
                        {
                            CodeSystem = allergy.AllergySeverityType.CodeSystemIdentifier,
                            DisplayName = allergy.AllergySeverityType.Name,
                            Code = allergy.AllergySeverityType.CodedConceptCode,
                            CodeSystemName = allergy.AllergySeverityType.CodeSystemName
                        };
                }

                if ( allergy.AllergyStatus != null )
                {
                    allergyDto.AllergyStatus = new OriginalTextCodedConceptDto
                        {
                            Code = allergy.AllergyStatus.CodedConceptCode,
                            CodeSystem = allergy.AllergyStatus.CodeSystemIdentifier,
                            DisplayName = allergy.AllergyStatus.Name,
                            CodeSystemName = allergy.AllergyStatus.CodeSystemName
                        };
                }

                if ( dto.Body.Allergies == null )
                {
                    dto.Body.Allergies = new List<AllergyDto> ();
                }
                dto.Body.Allergies.Add ( allergyDto );
            }

            #endregion Allergies and Reactions

            #region Medications

            var medications = patient.Medications;
            foreach ( var medication in medications )
            {
                var medicationDto = new C32Gen.DataTransferObject.MedicationDto ();

                //medicationDto.IndicateMedicationStopped = new ValueDataTransferObject() { Value = "20101010" };

                if ( medication.UsageDateRange != null && ( medication.UsageDateRange.StartDate != null || medication.UsageDateRange.EndDate != null ) )
                {
                    medicationDto.MedicationDateRange = new OperatorDateTimeDto ();
                    if ( medication.UsageDateRange.StartDate != null )
                    {
                        medicationDto.MedicationDateRange.StartDate = new ValueDataTransferObject
                            {
                                Value = medication.UsageDateRange.StartDate.Value.ToString ( "yyyyMMdd" )
                            };
                    }
                    if ( medication.UsageDateRange.EndDate != null )
                    {
                        medicationDto.MedicationDateRange.EndDate = new ValueDataTransferObject
                            {
                                Value = medication.UsageDateRange.EndDate.Value.ToString ( "yyyyMMdd" )
                            };
                    }
                }

                ////TODO: AdmissionTiming 
                //medicationDto.AdmissionTiming = new AdmissionTimingDto() { InstitutionSpecified = false, Period = new ValueDataTransferObject() { Value = "6", Unit = "h" } };

                if ( medication.MedicationRoute != null )
                {
                    medicationDto.Route = new OriginalTextCodedConceptDto
                        {
                            CodeSystem = medication.MedicationRoute.CodeSystemIdentifier,
                            Code = medication.MedicationRoute.CodedConceptCode,
                            DisplayName = medication.MedicationRoute.Name,
                            CodeSystemName = medication.MedicationRoute.CodeSystemName
                        };
                }

                if ( medication.MedicationDoseValue != null )
                {
                    medicationDto.Dose = new ValueUnitDataTransferObject
                        {
                            Value = medication.MedicationDoseValue.ToString (), Unit = medication.MedicationDoseUnit.ShortName
                        };
                }

                if ( medication.MedicationDoseUnit != null )
                {
                    medicationDto.Dose.Unit = medication.MedicationDoseUnit.Name;
                }

                ////TODO: Site, DoseRestriction?
                //medicationDto.Site = new CodedConceptDataTransferObject() { Code = "code31", DisplayName = "displayName29", CodeSystem = "codeSystem29", CodeSystemName = "codeSystemName29" };
                //medicationDto.DoseRestriction = new DoseRestrictionDto() { Numerator = new ValueDataTransferObject() { Value = "value63", Unit = "unit18" }, Denominator = new ValueDataTransferObject() { Value = "value64", Unit = "unit19" } };

                // ProductForm
                //medicationDto.ProductForm = new OriginalTextCodedConceptDto { CodeSystem = "2.16.840.1.113883.3.26.1.1", DisplayName = "Puff", Code = "415215001" };

                //TODO: DeliveryMethod?
                //medicationDto.DeliveryMethod = new CodedConceptDataTransferObject() { Code = "code37", DisplayName = "displayName35", CodeSystem = "codeSystem29", CodeSystemName = "codeSystemName35" };

                if ( medication.MedicationCodeCodedConcept != null )
                {
                    medicationDto.MedicationInformation = new MedicationInformationDto
                        {
                            CodedProductName =
                                new OriginalTextCodedConceptDto
                                    {
                                        CodeSystem = medication.MedicationCodeCodedConcept.CodeSystemIdentifier,
                                        DisplayName = medication.MedicationCodeCodedConcept.DisplayName,
                                        Code = medication.MedicationCodeCodedConcept.CodedConceptCode,
                                        CodeSystemName = medication.MedicationCodeCodedConcept.CodeSystemName
                                    },

                            ////TODO: The following information?
                            //CodedBrandName = new CodedConceptDataTransferObject() { Code = "code41", DisplayName = "displayName39", CodeSystem = "codeSystem39", CodeSystemName = "codeSystemName39" },
                            FreeTextProductName = medication.MedicationCodeCodedConcept.DisplayName,

                            //FreeTextBrandName = "freeTextBrandName0",
                            //DrugManufacturer = new OrganizationDto()
                            //{
                            //    OrganizationId = new IIDataTransferObject() { Root = "root11", Extension = "extension11" },
                            //    OrganizationAddress = new AddressDto() { StreetAddress = "Street Address", City = "city", State = "state", PostalCode = "postalcode" },
                            //    OrganizationName = new TextNullFlavorDataTransferObject() { Value = "drug manufacturer" },
                            //    OrganizationTelecom = new ValueDataTransferObject() { Value = "1-999-999-9999" }
                            //}
                        };
                }

                // TODO: Hard Coded
                medicationDto.TypeOfMedication = new OriginalTextCodedConceptDto
                    {
                        Code = "329505003", CodeSystem = "2.16.840.1.113883.6.96", CodeSystemName = "SNOMED CT"
                    };

                if ( medication.MedicationStatus != null )
                {
                    medicationDto.StatusOfMedication = new StatusCodedConceptDataTransferObject
                        {
                            ValueType = "CE",
                            Status = medication.MedicationStatus.Name,
                            Code = medication.MedicationStatus.CodedConceptCode,
                            DisplayName = medication.MedicationStatus.Name,
                            CodeSystem = medication.MedicationStatus.CodeSystemIdentifier,
                            CodeSystemName = medication.MedicationStatus.CodeSystemName
                        };
                }

                //TODO:
                //medicationDto.Indication = new StatusCodedConceptDataTransferObject() { Status = "normal", FreeTextRef = "freeTextRef0", Code = "code46", DisplayName = "displayName44", CodeSystem = "codeSystem44", CodeSystemName = "codeSystemName44" };

                medicationDto.Frequency = medication.FrequencyDescription;
                medicationDto.PatientInstructions = medication.InstructionsNote;

                ////TODO:
                //medicationDto.Reaction = new CodedConceptDataTransferObject() { Code = "code47", DisplayName = "displayName45", CodeSystem = "codeSystem45", CodeSystemName = "codeSystemName45" };
                //medicationDto.Vehicle = new NameDataTransferObject() { Name = "name1", Code = "code48", DisplayName = "displayName45", CodeSystem = "codeSystem45", CodeSystemName = "codeSystemName45" };
                //medicationDto.DoseIndicator = "dose";
                //medicationDto.OrderInformation = new OrderInformationDto()
                //{
                //    OrderNumber = new IIDataTransferObject() { Root = "root10", Extension = "extension10" },
                //    Fills = new ValueDataTransferObject() { Value = "value65" },
                //    QuantityOrdered = new ValueDataTransferObject() { Value = "value66", Unit = "unit20" },
                //    OrderExpirationDateTime = new ValueDataTransferObject() { Value = "value67" },
                //    OperatorDateTime = new OperatorDateTimeDto() { StartDate = new ValueDataTransferObject() { Value = "20101010" }, Operator = "operator0" }
                //};
                //medicationDto.FulfillmentInstructions = "fullfillmentinstructions";
                //medicationDto.FulfillmentHistory = new FullfillmentHistoryDto()
                //{
                //    PrescriptionNumber = new IIDataTransferObject() { Root = "root11", Extension = "extension11" },
                //    Provider = new OrganizationDto()
                //    {
                //        OrganizationId = new IIDataTransferObject() { Root = "root11", Extension = "extension11" },
                //        OrganizationAddress = new AddressDto() { StreetAddress = "Street Address", City = "city", State = "state", PostalCode = "postalcode" },
                //        OrganizationName = new TextNullFlavorDataTransferObject() { Value = "provider" },
                //        OrganizationTelecom = new ValueDataTransferObject() { Value = "1-999-999-9999" }
                //    },
                //    DispensingPharmacyLocation = new TextNullFlavorDataTransferObject(),
                //    DispenseDate = new OperatorDateTimeDto() { StartDate = new ValueDataTransferObject() { Value = "20101010" }, EndDate = new ValueDataTransferObject() { Value = "20101010" }, Operator = "operator1" },
                //    QuantityDispensed = new ValueDataTransferObject() { Value = "value87", Unit = "unit23" },
                //    FillNumber = new ValueDataTransferObject() { Value = "value28" },
                //    FillStatus = new ValueDataTransferObject() { Value = "normal" }
                //};

                if ( dto.Body.Medications == null )
                {
                    dto.Body.Medications = new List<C32Gen.DataTransferObject.MedicationDto> ();
                }
                dto.Body.Medications.Add ( medicationDto );

                #endregion Medications
            }

            #endregion

            return dto;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the patient for C32 dto generation.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.PatientModule.Patient"/></returns>
        internal Patient GetPatientForC32DtoGeneration ( long patientKey )
        {
            var session = _sessionProvider.GetSession ();
            var key = patientKey;

            ICriterion patientRootCriterion = Restrictions.Eq ( Projections.Property<Patient> ( p => p.Key ), key );
            ICriterion patientAddressRootCriterion =
                Restrictions.Eq ( Projections.Property<PatientAddress> ( p => p.Patient.Key ), key );
            ICriterion clinicalCaseRootCriterion =
                Restrictions.Eq ( Projections.Property<ClinicalCase> ( p => p.Patient.Key ), key );

            //DetachedCriteria allClinicalCaseKeys =
            //    DetachedCriteria.For<ClinicalCase> ().SetProjection ( Projections.Property<ClinicalCase> ( p => p.Key ) ).Add (
            //        clinicalCaseRootCriterion );

            //ICriterion visitRootCriterion = Subqueries.PropertyIn ( "ClinicalCase.Key", allClinicalCaseKeys );

            //DetachedCriteria allVisitKeys =
            //    DetachedCriteria.For<Visit> ().SetProjection ( Projections.Property<Visit> ( p => p.Key ) ).Add (
            //        visitRootCriterion );

            //ICriterion labSpecimenRootCriterion = Subqueries.PropertyIn ( "Visit.Key", allVisitKeys );
            //DetachedCriteria allLabSpecimenKeys =
            //    DetachedCriteria.For<LabSpecimen> ().SetProjection ( Projections.Property<LabSpecimen> ( p => p.Key ) ).Add (
            //        labSpecimenRootCriterion );

            //ICriterion labTestRootCriterion = Subqueries.PropertyIn ( "LabSpecimen.Key", allLabSpecimenKeys );

            // To use multi criteria to eagerly load multiple child collection, if the first detached criteria has child collection to query,
            // then must use .SetResultTransformer(new DistinctRootEntityResultTransformer() for the first detached criteria
            var multiCriteria = session.CreateMultiCriteria ()
                .AddDetachedCriteriaForChild<Patient, IEnumerable<PatientAddress>> (
                    patientRootCriterion,
                    p => p.Addresses,
                    null,
                    true )
                .AddDetachedCriteriaForChild<PatientAddress, Domain.Clinical.PatientModule.PatientAddressType> (
                    patientAddressRootCriterion, p => p.PatientAddressType )

                //.AddDetachedCriteriaForChild<PatientAddress, StateProvince> (
                //    patientAddressRootCriterion, p => p.Address.StateProvince )

                .AddDetachedCriteriaForChild<Patient, IEnumerable<PatientPhone>> (
                    patientRootCriterion,
                    p => p.PhoneNumbers )
                .AddDetachedCriteriaForChild<Patient, IEnumerable<PatientContact>> (
                    patientRootCriterion,
                    p => p.Contacts )
                .AddDetachedCriteriaForChild<PatientContact, PatientContactRelationshipType> (
                    patientAddressRootCriterion, p => p.PatientContactRelationshipType )
                .AddDetachedCriteriaForChild<Patient, Agency> (
                    patientRootCriterion,
                    p => p.Agency )
                .AddDetachedCriteriaForChild<Patient, IEnumerable<Allergy>> (
                    patientRootCriterion,
                    p => p.Allergies )
                .AddDetachedCriteriaForChild<Patient, IEnumerable<Medication>> (
                    patientRootCriterion,
                    p => p.Medications )
                .AddDetachedCriteriaForChild<Patient, IEnumerable<ClinicalCase>> (
                    patientRootCriterion, p => p.ClinicalCases )
                .AddDetachedCriteriaForChild<ClinicalCase, IEnumerable<Problem>> (
                    clinicalCaseRootCriterion, p => p.Problems )
                .AddDetachedCriteriaForChild<ClinicalCase, IEnumerable<Visit>> (
                    clinicalCaseRootCriterion, p => p.Visits );

            //.Add(
            //    DetachedCriteria.For<Visit>().Add(visitRootCriterion)
            //        .CreateCriteria("Activities")
            //        .CreateCriteria("ActivityType").Add(Restrictions.Eq("WellKnownName", WellKnownNames.VisitModule.ActivityType.LabSpecimen)))

            //.Add(
            //    DetachedCriteria.For<LabSpecimen>().Add(labSpecimenRootCriterion)
            //        .CreateCriteria("LabTests"))

            //.Add(DetachedCriteria.For<LabTest>().Add(labTestRootCriterion)
            //         .CreateCriteria("LabResults"));

            var results = multiCriteria.List ();

            var patients = ( IList )( results[0] );

            var patient = ( Patient )patients[0];

            return patient;
        }

        private static EncounterDto AddEncounterDto ( C32Dto dto, Visit visit )
        {
            var encounterDto = new EncounterDto ();

            //encounterDto.AdmissionType = new OriginalTextCodedConceptDto
            //{
            //    Code = "43789009",
            //    CodeSystem = "2.16.840.1.113883.6.96",
            //    DisplayName = "AdmissionType",
            //    OriginalText = "AdmissionType"
            //};
            //encounterDto.DischargeDisposition = "DischargeDisposition";
            encounterDto.EncounterDateTime = new ValueDataTransferObject
                {
                    Value = visit.AppointmentDateTimeRange.StartDateTime.ToString ( "yyyyMMdd" )
                };

            encounterDto.EncounterIds = new List<IIDataTransferObject> { new IIDataTransferObject { Root = visit.Key.ToString () } };

            if ( visit.Staff != null )
            {
                var providerInformationDto = new ProviderInformationDto ();
                providerInformationDto.ProviderId = new IIDataTransferObject { Root = visit.Staff.Key.ToString () };
                providerInformationDto.ProviderName = new PersonNameDto
                    {
                        Family = visit.Staff.StaffProfile.StaffName.Last,
                        Given = visit.Staff.StaffProfile.StaffName.First,
                        Prefix = visit.Staff.StaffProfile.StaffName.Prefix,
                        Suffix = visit.Staff.StaffProfile.StaffName.Suffix
                    };
                if ( visit.Staff.Agency != null )
                {
                    providerInformationDto.ProviderOrganizationName = new TextNullFlavorDataTransferObject
                        {
                            TextValue = visit.Staff.Agency.AgencyProfile.AgencyName.LegalName
                        };
                }

                if ( encounterDto.EncounterProviders == null )
                {
                    encounterDto.EncounterProviders = new List<ProviderInformationDto> ();
                }

                encounterDto.EncounterProviders.Add ( providerInformationDto );
            }

            if ( visit.CptCode != null )
            {
                encounterDto.EncounterType = new OriginalTextCodedConceptDto
                    {
                        Code = visit.CptCode,
                        CodeSystem = "2.16.840.1.113883.6.12",
                        CodeSystemName = "CPT"
                    };
            }

            //encounterDto.FacilityLocation = new FacilityLocationDto()
            //{
            //    LocationDuration =
            //        new ValueDataTransferObject() { Value = "LocationDuration" }
            //};
            //encounterDto.ReasonForVisit = new ReasonForVisitDto()
            //{
            //    Reason = new OriginalTextCodedConceptDto
            //    {
            //        Code = "43789009",
            //        CodeSystem = "2.16.840.1.113883.6.96",
            //        DisplayName = "EncounterType",
            //        OriginalText = "EncounterType"
            //    }
            //};

            if ( dto.Body.EncountersDto == null )
            {
                dto.Body.EncountersDto = new EncountersDto ();
            }
            if ( dto.Body.EncountersDto.Encounters == null )
            {
                dto.Body.EncountersDto.Encounters = new List<EncounterDto> ();
            }
            dto.Body.EncountersDto.Encounters.Add ( encounterDto );
            return encounterDto;
        }

        private static void AddSocialHistory (
            C32Dto dto, Visit visit, SocialHistory socialHistory, OriginalTextCodedConceptDto socialHistoryTypeDto )
        {
            var socialHistoryEntryDto = new SocialHistoryEntryDto ();
            socialHistoryEntryDto.SocialHistoryId = new IIDataTransferObject { Root = socialHistory.Key.ToString () };

            socialHistoryEntryDto.SocialHistoryTime = new OperatorDateTimeDto
                {
                    Date = visit.AppointmentDateTimeRange.StartDateTime.ToString ( "yyyyMMdd" )
                };
            socialHistoryEntryDto.SocialHistoryType = socialHistoryTypeDto;

            if ( dto.Body.SocialHistory == null )
            {
                dto.Body.SocialHistory = new C32Gen.DataTransferObject.SocialHistoryDto ();
            }
            if ( dto.Body.SocialHistory.SocialHistoryEntries == null )
            {
                dto.Body.SocialHistory.SocialHistoryEntries = new List<SocialHistoryEntryDto> ();
            }
            dto.Body.SocialHistory.SocialHistoryEntries.Add ( socialHistoryEntryDto );
        }

        private static void AddVitalSignResultDto ( C32Dto dto, ResultDto vitalSignResultDto )
        {
            if ( dto.Body.VitalSignsDto == null )
            {
                dto.Body.VitalSignsDto = new VitalSignsDto ();
            }

            if ( dto.Body.VitalSignsDto.VitalSignResults == null )
            {
                dto.Body.VitalSignsDto.VitalSignResults = new List<ResultDto> ();
            }

            dto.Body.VitalSignsDto.VitalSignResults.Add ( vitalSignResultDto );
        }

        private static ResultDto GetVitalSignResultDto ( Visit visit, VitalSign vitalSign )
        {
            var vitalSignResultDto = new ResultDto
                {
                    ResultId = new IIDataTransferObject { Root = vitalSign.Key.ToString () },
                    ResultDateTime = new OperatorDateTimeDto { Date = visit.AppointmentDateTimeRange.StartDateTime.ToString ( "yyyyMMdd" ) }
                };

            return vitalSignResultDto;
        }

        private void AddPlannedEvent ( C32Dto dto, PlannedEventDto plannedObservationPlannedEventDto )
        {
            if ( dto.Body.PlanOfCare == null )
            {
                dto.Body.PlanOfCare = new PlanOfCareDto ();
            }
            if ( dto.Body.PlanOfCare.PlannedEvents == null )
            {
                dto.Body.PlanOfCare.PlannedEvents = new List<PlannedEventDto> ();
            }
            if ( dto.Body.PlanOfCare.ElementNamesForPlannedEvents == null )
            {
                dto.Body.PlanOfCare.ElementNamesForPlannedEvents = new List<PlanOfCareDto.PlannedEventsElementNameChoiceType> ();
            }
            dto.Body.PlanOfCare.PlannedEvents.Add ( plannedObservationPlannedEventDto );
            dto.Body.PlanOfCare.ElementNamesForPlannedEvents.Add (
                PlanOfCareDto.PlannedEventsElementNameChoiceType.plannedObservation );
        }

        private void AddProblem ( C32Dto dto, Problem problem, DateTime? diagnosisDate )
        {
            var conditionDto = new ConditionDto ();
            conditionDto.DiagnosisPriority = 1;

            if ( diagnosisDate.HasValue )
            {
                // TODO: The problem date should be the DiagnosisDate
                //conditionDto.ProblemDate = new OperatorDateTimeDto { Date = diagnosisDate.Value.ToString ( "yyyyMMdd" ) };
            }
            else
            {
                if ( problem.OnsetDateRange != null && ( problem.OnsetDateRange.StartDate != null || problem.OnsetDateRange.EndDate != null ) )
                {
                    // TODO: The problem date should be the DiagnosisDate
                //    conditionDto.ProblemDate = new OperatorDateTimeDto ();

                //    if ( problem.OnsetDateRange.StartDate != null )
                //    {
                //        conditionDto.ProblemDate.StartDate = new ValueDataTransferObject
                //            {
                //                Value = problem.OnsetDateRange.StartDate.Value.ToString ( "yyyyMMdd" )
                //            };
                //    }
                //    if ( problem.OnsetDateRange.EndDate != null )
                //    {
                //        conditionDto.ProblemDate.EndDate = new ValueDataTransferObject
                //            {
                //                Value = problem.OnsetDateRange.EndDate.Value.ToString ( "yyyyMMdd" )
                //            };
                //    }
                }
            }

            if ( problem.ProblemType != null )
            {
                conditionDto.ProblemType = new OriginalTextCodedConceptDto
                    {
                        Code = problem.ProblemType.CodedConceptCode,
                        DisplayName = problem.ProblemType.Name,
                        CodeSystem = problem.ProblemType.CodeSystemIdentifier,
                        CodeSystemName = problem.ProblemType.CodeSystemName
                    };
            }

            if ( problem.ProblemCodeCodedConcept != null )
            {
                conditionDto.ProblemName = problem.ProblemCodeCodedConcept.DisplayName;
                conditionDto.ProblemCode = new OriginalTextCodedConceptDto
                    {
                        Code = problem.ProblemCodeCodedConcept.CodedConceptCode,
                        CodeSystem = problem.ProblemCodeCodedConcept.CodeSystemIdentifier,
                        DisplayName = problem.ProblemCodeCodedConcept.DisplayName,
                        CodeSystemName = problem.ProblemCodeCodedConcept.CodeSystemName
                    };
            }

            //TODO: Provider problem.ClinicalCase.PerformedByStaff
            //conditionDto.Provider = new ProviderDto() { ProviderId = new IIDataTransferObject() { Root = "npi", Extension = "npi-extension" } };

            //TODO: We don't have Age At Onset
            //conditionDto.AgeAtOnset = 100;
            //conditionDto.CauseOfDeath = new CauseOfDeathDto()
            //{
            //    ProblemCode = new CodedConceptDataTransferObject() { Code = "195967001", CodeSystem = "2.16.840.1.113883.6.96", DisplayName = "Asthma", CodeSystemName = "SNOMED-CT" },
            //    TimeOfDeath = new ValueDataTransferObject() { Value = "20101010" },
            //    AgeAtDeath = 100
            //};

            if ( problem.ProblemStatus != null )
            {
                conditionDto.ProblemStatus = new OriginalTextCodedConceptDto
                    {
                        Code = problem.ProblemStatus.CodedConceptCode,
                        CodeSystem = problem.ProblemStatus.CodeSystemIdentifier,
                        CodeSystemName = problem.ProblemStatus.CodeSystemName,
                        DisplayName = problem.ProblemStatus.Name
                    };
            }

            if ( dto.Body.Conditions == null )
            {
                dto.Body.Conditions = new List<ConditionDto> ();
            }
            dto.Body.Conditions.Add ( conditionDto );
        }

        private void AddProcedureForBriefIntervention (
            C32Dto dto, Visit visit, BriefIntervention briefIntervention, string procedureFreeTextType, OriginalTextCodedConceptDto procedureType )
        {
            var procedureDto = new ProcedureDto ();

            //procedureDto.BodySite = new OriginalTextCodedConceptDto()
            //{
            //    Code = "43789009",
            //    CodeSystem = "2.16.840.1.113883.6.96",
            //    DisplayName = "CBC WO DIFFERENTIAL",
            //    OriginalText = "Extract blood for CBC test"
            //};
            procedureDto.ProcedureDateTime = new OperatorDateTimeDto { Date = visit.AppointmentDateTimeRange.StartDateTime.ToString ( "yyyyMMdd" ) };
            procedureDto.ProcedureFreeTextType = procedureFreeTextType;
            procedureDto.ProcedureIds = new List<IIDataTransferObject> ();
            procedureDto.ProcedureIds = new List<IIDataTransferObject> { new IIDataTransferObject { Root = briefIntervention.Key.ToString () } };
            if ( visit.Staff != null )
            {
                var providerInformationDto = new ProviderInformationDto ();
                providerInformationDto.ProviderId = new IIDataTransferObject { Root = visit.Staff.Key.ToString () };
                providerInformationDto.ProviderName = new PersonNameDto
                    {
                        Family = visit.Staff.StaffProfile.StaffName.Last,
                        Given = visit.Staff.StaffProfile.StaffName.First,
                        Prefix = visit.Staff.StaffProfile.StaffName.Prefix,
                        Suffix = visit.Staff.StaffProfile.StaffName.Suffix
                    };
                if ( visit.Staff.Agency != null )
                {
                    providerInformationDto.ProviderOrganizationName = new TextNullFlavorDataTransferObject
                        {
                            TextValue = visit.Staff.Agency.AgencyProfile.AgencyName.LegalName
                        };
                }

                if ( procedureDto.ProcedureProviders == null )
                {
                    procedureDto.ProcedureProviders = new List<ProviderInformationDto> ();
                }

                procedureDto.ProcedureProviders.Add ( providerInformationDto );
            }

            procedureDto.ProcedureType = procedureType;

            //QualifierDto qualifierDto = new QualifierDto();
            //qualifierDto.Name = new CodedConceptDataTransferObject() { Code = "272741003", DisplayName = "Laterality" };
            //qualifierDto.Value = new CodedConceptDataTransferObject() { Code = "7771000", DisplayName = "Left" };
            //procedureDto.ProcedureType.Qualifiers = new List<QualifierDto>() {qualifierDto};

            if ( dto.Body.ProceduresDto == null )
            {
                dto.Body.ProceduresDto = new ProceduresDto ();
            }
            if ( dto.Body.ProceduresDto.Procedures == null )
            {
                dto.Body.ProceduresDto.Procedures = new List<ProcedureDto> ();
            }
            dto.Body.ProceduresDto.Procedures.Add ( procedureDto );
        }

        #endregion
    }
}
