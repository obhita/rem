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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Agatha.Common;
using C32Gen;
using C32Gen.DataTransferObject;
using Health.Direct.Xdm;
using Ionic.Zip;
using Pillar.Common.Collections;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using ImmunizationDto = C32Gen.DataTransferObject.ImmunizationDto;
using MedicationDto = C32Gen.DataTransferObject.MedicationDto;

namespace Rem.Ria.PatientModule.Web.ImportC32
{
    /// <summary>
    ///  Class for handling importing c32 request.
    /// </summary>
    public class GetDataFromC32RequestHandler :
        NHibernateSessionRequestHandler<GetDataFromC32Request, GetDataFromC32Response>
    {
        #region Constants and Fields

        private const string Utf8Encoding = "UTF-8";
        private const string StandardDateFormat = "yyyyMMdd";
        private const string LongDateFormat = "yyyyMMddHHmmsszzz";
        private const string ShortDateFormat = "yyyyMM";
        private const string BodyWeight = "Body Weight";
        private const string BodyHeight = "Body Height";
        private const string SystolicBP = "Systolic BP";
        private const string DiastolicBP = "Diastolic BP";
        private const string HeartRate = "Heart rate";
        private const string DietaryConsultationOrderIndicatorEventCode = "103699006";
        private const string BmiFollowUpPlanIndicatorEventCode = "169411000";

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly ICodedConceptLookupBaseRepository _codedConceptLookupBaseRepository;
        private readonly IPatientDocumentRepository _patientDocumentRepository;
        private readonly IC32Builder _c32Builder;

        private long _patientKey;

        #endregion

        #region Constructors and Destructors


        /// <summary>
        /// Initializes a new instance of the <see cref="GetDataFromC32RequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="codedConceptLookupBaseRepository">The coded concept lookup base repository.</param>
        /// <param name="patientDocumentRepository">The patient document repository.</param>
        /// <param name="c32Builder">The C32 builder.</param>
        public GetDataFromC32RequestHandler (
            IDtoToDomainMappingHelper mappingHelper,
            ICodedConceptLookupBaseRepository codedConceptLookupBaseRepository,
            IPatientDocumentRepository patientDocumentRepository,
            IC32Builder c32Builder )
        {
            _mappingHelper = mappingHelper;
            _codedConceptLookupBaseRepository = codedConceptLookupBaseRepository;
            _patientDocumentRepository = patientDocumentRepository;
            _c32Builder = c32Builder;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetDataFromC32Request request )
        {
            // get the patient external doc
            var provenance = new ProvenanceDto ();
            var patientExternalDocument = GetPatientExternalDocument ( request.PatientDocumentKey, ref provenance );
            var c32Dto = GetC32Dto ( patientExternalDocument );
            
            GetProvenanceFromC32Dto ( c32Dto, ref provenance );

            var medications = GetMedicationsFromC32Dto ( c32Dto );
            var allergies = GetAllergiesFromC32Dto ( c32Dto );
            var problems = GetProblemsFromC32Dto ( c32Dto );
            var immunizations = GetImmunizationsFromC32Dto ( c32Dto );
            var labSpecimen = GetLabSpecimenFromC32Dto ( c32Dto );
            var vitalSign = GetVitalSignFromC32Dto ( c32Dto );

            var response = CreateTypedResponse ();
            response.Medications = medications;
            response.Provenance = provenance;
            response.Allergies = allergies;
            response.Problems = problems;
            response.Immunizations = immunizations;
            response.LabSpecimen = labSpecimen;
            response.VitalSign = vitalSign;

            return response;
        }

        #endregion

        #region Methods

        private string GetPatientExternalDocument ( long patientDocumentKey, ref ProvenanceDto provenance )
        {
            var result = string.Empty;

            var patientDocument = _patientDocumentRepository.GetByKey ( patientDocumentKey );
            if ( patientDocument == null )
            {
                return result;
            }

            if (patientDocument.PatientDocumentType.WellKnownName == WellKnownNames.PatientModule.PatientDocumentType.XDM)
            {
                // parse XDM doc
                var packager = XDMZipPackager.Default;
                var xdmFile = ZipFile.Read ( patientDocument.Document, null, Encoding.GetEncoding ( Utf8Encoding ) );

                var documentPackage = packager.Unpackage ( xdmFile );

                // TODO: make sure extracting the correct data from metadata
                if (documentPackage.SubmissionTime.HasValue)
                {
                    provenance.SignedTimestamp = documentPackage.SubmissionTime.Value;
                }

                //Only handle first document at this moment
                var doc = documentPackage.Documents.FirstOrDefault();
                if (doc != null)
                {
                    if ( doc.PatientID != null )
                    {
                        provenance.AssigningAuthority = doc.PatientID.AssigningAuthority;
                    }

                    if ( doc.Author != null )
                    {
                        if (doc.Author.Person != null)
                        {
                            provenance.FirstName = doc.Author.Person.First;
                            provenance.LastName = doc.Author.Person.Last;
                            provenance.PrefixName = doc.Author.Person.Prefix;
                        }

                        if (doc.Author.Institutions != null)
                        {
                            provenance.OrganizationAssigningAuthority = doc.Author.Institutions.FirstOrDefault().AssigningAuthority;
                            provenance.OrganizationName = doc.Author.Institutions.FirstOrDefault().Name;
                        }

                        if (doc.Author.TelecomAddress.Email != null)
                        {
                            provenance.Extension = doc.Author.TelecomAddress.Email.Host;
                            provenance.OrganizationExtension = provenance.Extension;
                        }
                        provenance.PhoneNumber = doc.Author.TelecomAddress.Phone;
                    }
                    result = doc.DocumentString;
                }
            }
            
            if (patientDocument.PatientDocumentType.WellKnownName == WellKnownNames.PatientModule.PatientDocumentType.C32)
            {
                _patientKey = patientDocument.Patient.Key;

                if ( patientDocument.Document != null && patientDocument.Document.Length > 0 )
                {
                    result = Encoding.GetEncoding ( Utf8Encoding ).GetString ( patientDocument.Document );
                }
            }

            return result;
        }

        private C32Dto GetC32Dto ( string c32File )
        {
            if ( string.IsNullOrEmpty ( c32File ) )
            {
                return null;
            }

            var dto = _c32Builder.BuildC32Dto ( c32File );

            return dto;
        }

        #region Medication

        private List<PatientDashboard.MedicationDto> GetMedicationsFromC32Dto ( C32Dto c32Dto )
        {
            var medications = new List<PatientDashboard.MedicationDto> ();

            if ( c32Dto == null || c32Dto.Body == null || c32Dto.Body.Medications == null )
            {
                return medications;
            }

            foreach ( MedicationDto dto in c32Dto.Body.Medications )
            {
                var medication = CreateMedication ( dto );

                medications.Add ( medication );
            }

            return medications;
        }

        private PatientDashboard.MedicationDto CreateMedication ( MedicationDto dto )
        {
            var result = new PatientDashboard.MedicationDto ();

            result.PatientKey = _patientKey;

            var startDate = GetStartDate ( dto );
            if ( startDate != DateTime.MinValue )
            {
                result.StartDate = startDate;
            }

            var endDate = GetEndDate ( dto );
            if ( endDate != DateTime.MinValue )
            {
                result.EndDate = endDate;
            }

            result.FrequencyDescription = dto.Frequency;
            result.InstructionsNote = dto.PatientInstructions;
            result.MedicationStatus = GetMedicationStatus ( dto.StatusOfMedication );
            result.MedicationCodeCodedConcept = GetMedicationCodeCodedConcept ( dto.MedicationInformation );

            // TODO: 
            // Get these data:
            //DiscontinuedByPhysicianName,
            //DiscontinuedReason,
            //DiscontinuedReasonOtherDescription,
            //OverTheCounterIndicator,
            //PrescribingPhysicianName,
            //RootMedicationCodedConcept

            return result;
        }

        private DateTime? GetStartDate ( MedicationDto dto )
        {
            var startDate = DateTime.MinValue;

            if ( dto != null && dto.MedicationDateRange != null && dto.MedicationDateRange.StartDate != null )
            {
                startDate = GetDate ( dto.MedicationDateRange.StartDate.Value );
            }

            return startDate;
        }

        private DateTime GetEndDate ( MedicationDto dto )
        {
            var endDate = DateTime.MinValue;

            if ( dto != null && dto.MedicationDateRange != null && dto.MedicationDateRange.EndDate != null )
            {
                endDate = GetDate ( dto.MedicationDateRange.EndDate.Value );
            }

            return endDate;
        }

        private DateTime GetDate(string dateString, string dateFormat = StandardDateFormat)
        {
            var result = DateTime.MinValue;

            if ( !string.IsNullOrWhiteSpace (  dateString ) )
            {
                DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            }

            return result;
        }

        private DateTime? GetNullableDate ( string dateString )
        {
            DateTime? date = null;

            var temp = DateTime.MinValue;
            if ( !string.IsNullOrWhiteSpace ( dateString ) )
            {
                DateTime.TryParseExact(dateString, StandardDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp);

                if ( temp != DateTime.MinValue )
                {
                    date = temp;
                }
            }

            return date;
        }

        private LookupValueDto GetMedicationStatus ( StatusCodedConceptDataTransferObject status )
        {
            LookupValueDto result = null;

            MedicationStatus lookup = null;

            if ( status != null )
            {
                if ( !string.IsNullOrEmpty ( status.Status ) )
                {
                    lookup = _mappingHelper.MapLookupFieldByName<MedicationStatus> ( status.Status );
                }
                else if ( !string.IsNullOrEmpty ( status.DisplayName ) )
                {
                    lookup = _mappingHelper.MapLookupFieldByName<MedicationStatus> ( status.DisplayName );
                }
            }

            if ( lookup != null )
            {
                result = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup );
            }

            return result;
        }

        private CodedConceptDto GetMedicationCodeCodedConcept ( MedicationInformationDto dto )
        {
            if ( dto == null || dto.CodedProductName == null )
            {
                return null;
            }

            var result = new CodedConceptDto ()
                {
                    CodeSystemIdentifier = dto.CodedProductName.CodeSystem,
                    DisplayName = dto.CodedProductName.DisplayName,
                    CodedConceptCode = dto.CodedProductName.Code,
                    CodeSystemName = dto.CodedProductName.CodeSystemName
                };

            return result;
        }

        private CodedConceptLookupValueDto GetCodedConceptDto ( Type type, string codedConcepCode )
        {
            var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode ( type, codedConcepCode );

            return AutoMapper.Mapper.Map<CodedConceptLookupBase, CodedConceptLookupValueDto> ( lookup );
        }

        private int? GetNullableIntValue( string intString)
        {
            int temp = 0;

            int.TryParse ( intString, out temp );
            if( temp == 0 )
            {
                return null;
            }

            return temp;
        }

        private decimal? GetNullableDecimalValue( string decimalString)
        {
            decimal temp = 0;

            decimal.TryParse ( decimalString, out temp );
            
            if( temp == 0 )
            {
                return null;
            }

            return temp;
        }

        private double? GetNullableDoubleValue(string doubleString)
        {
            double temp = 0;

            double.TryParse(doubleString, out temp);

            if (temp.Equals(0))
            {
                return null;
            }

            return temp;
        }

        #endregion

        #region Provenace

        private void GetProvenanceFromC32Dto ( C32Dto c32Dto, ref ProvenanceDto provenance)
        {
            if ( c32Dto != null && c32Dto.Header != null )
            {
                var header = c32Dto.Header;

                if ( header.DocumentId != null )
                {
                    var documentId = header.DocumentId;
                    provenance.AssigningAuthority = documentId.AssigningAuthority;
                    provenance.Extension = documentId.Extension;
                }

                if ( header.DocumentTimestamp != null && !string.IsNullOrEmpty ( header.DocumentTimestamp.Value ) )
                {
                    var signedTimestamp = DateTimeOffset.MinValue;
                    DateTimeOffset.TryParseExact (
                        header.DocumentTimestamp.Value,
                        LongDateFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal,
                        out signedTimestamp );
                    provenance.SignedTimestamp = signedTimestamp;
                }

                if ( header.InformationSource.Author != null && header.InformationSource.Author.AuthorName != null )
                {
                    var authorName = header.InformationSource.Author.AuthorName;
                    provenance.PrefixName = authorName.Prefix;
                    provenance.FirstName = authorName.Given;
                    provenance.LastName = authorName.Family;
                }

                if ( header.InformationSource != null && header.InformationSource.InformationSourceName != null )
                {
                    var sourceName = header.InformationSource.InformationSourceName;

                    if ( sourceName.OrganizationName != null )
                    {
                        provenance.OrganizationName = sourceName.OrganizationName.TextValue;
                    }

                    if ( sourceName.OrganizationId != null )
                    {
                        var organizationId = sourceName.OrganizationId;
                        provenance.OrganizationAssigningAuthority = organizationId.AssigningAuthority;
                        provenance.OrganizationExtension = organizationId.Extension;
                    }

                    if ( sourceName.OrganizationTelecom != null )
                    {
                        provenance.PhoneNumber = sourceName.OrganizationTelecom.Value;
                    }
                }
            }
        }

        #endregion

        #region Allergy

        private List<PatientEditor.AllergyDto> GetAllergiesFromC32Dto ( C32Dto c32Dto )
        {
            var allergies = new List<PatientEditor.AllergyDto> ();

            if ( c32Dto != null && c32Dto.Body.Allergies != null )
            {
                foreach ( var a in c32Dto.Body.Allergies )
                {
                    var allergy = GetAllergyDto ( a );

                    allergies.Add ( allergy );
                }
            }

            return allergies;
        }

        private PatientEditor.AllergyDto GetAllergyDto ( AllergyDto dto )
        {
            var allergy = new PatientEditor.AllergyDto ();

            if ( dto.AdverseEventType != null && !string.IsNullOrEmpty ( dto.AdverseEventType.Code ) )
            {
                var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode<AllergyType> ( dto.AdverseEventType.Code );
                if ( lookup != null )
                {
                    allergy.AllergyType = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup );
                }
            }

            if ( dto.AdverseEventDate != null )
            {
                if ( dto.AdverseEventDate.StartDate != null && string.IsNullOrEmpty ( dto.AdverseEventDate.StartDate.Value ) )
                {
                    allergy.OnsetStartDate = GetNullableDate ( dto.AdverseEventDate.StartDate.Value );
                }

                if ( dto.AdverseEventDate.EndDate != null && string.IsNullOrEmpty ( dto.AdverseEventDate.EndDate.Value ) )
                {
                    allergy.OnsetEndDate = GetNullableDate ( dto.AdverseEventDate.EndDate.Value );
                }
            }

            if ( dto.Product != null )
            {
                var product = dto.Product;
                allergy.AllergenCodedConcept = new CodedConceptDto
                    {
                        CodeSystemIdentifier = product.CodeSystem,
                        DisplayName = product.DisplayName,
                        CodedConceptCode = product.Code,
                        CodeSystemName = product.CodeSystemName
                    };
            }

            if ( dto.Severity != null && !string.IsNullOrEmpty ( dto.Severity.Code ) )
            {
                var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode<AllergySeverityType> ( dto.Severity.Code );
                if ( lookup != null )
                {
                    allergy.AllergySeverityType = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup );
                }
            }

            if ( dto.AllergyStatus != null && !string.IsNullOrEmpty ( dto.AllergyStatus.Code ) )
            {
                var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode<AllergyStatus> ( dto.AllergyStatus.Code );
                if ( lookup != null )
                {
                    allergy.AllergyStatus = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup );
                }
            }

            if ( dto.Reactions != null )
            {
                allergy.AllergyReactions = new ObservableCollection<LookupValueDto> ();

                foreach ( var r in dto.Reactions )
                {
                    if ( string.IsNullOrEmpty ( r.Code ) )
                    {
                        continue;
                    }

                    var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode<Reaction> ( r.Code );
                    if ( lookup != null )
                    {
                        allergy.AllergyReactions.Add ( AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup ) );
                    }
                }
            }

            return allergy;
        }

        #endregion

        #region Problem

        private List<ProblemDto> GetProblemsFromC32Dto ( C32Dto c32Dto )
        {
            var problems = new List<ProblemDto> ();

            if ( c32Dto != null && c32Dto.Body.Conditions != null )
            {
                foreach ( var c in c32Dto.Body.Conditions )
                {
                    var problem = GetProblemDto ( c );

                    problems.Add ( problem );
                }
            }

            return problems;
        }

        private ProblemDto GetProblemDto ( ConditionDto condition )
        {
            var problem = new ProblemDto ();

            if ( condition.ProblemCode != null )
            {
                problem.ProblemCodeCodedConcept = new CodedConceptDto
                    {
                        CodedConceptCode = condition.ProblemCode.Code,
                        CodeSystemIdentifier = condition.ProblemCode.CodeSystem,
                        DisplayName = condition.ProblemCode.DisplayName,
                        CodeSystemName = condition.ProblemCode.CodeSystemName,
                    };
            }

            if ( condition.ProblemStatus != null && !string.IsNullOrEmpty ( condition.ProblemStatus.Code ) )
            {
                var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode<ProblemStatus> ( condition.ProblemStatus.Code );
                if ( lookup != null )
                {
                    problem.ProblemStatus = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup );
                }
            }

            if ( condition.ProblemType != null && !string.IsNullOrEmpty ( condition.ProblemType.Code ) )
            {
                var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode<ProblemType> ( condition.ProblemType.Code );
                if ( lookup != null )
                {
                    problem.ProblemType = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup );
                }
            }

            // TODO
            //Upon to C32DtoFactory, currently, C32dto does not have the following values
            //AssociatedIndicator,CauseOfDeathIndicator,ClinicalCaseKey,ObservedByStaff,ObservedDate,OnsetEndDate,OnsetStartDate,StatusChangedDate 

            return problem;
        }

        #endregion

        #region Immunization

        private List<PatientDashboard.ImmunizationDto> GetImmunizationsFromC32Dto ( C32Dto c32Dto )
        {
            var immunizations = new List<PatientDashboard.ImmunizationDto> ();

            if ( c32Dto != null && c32Dto.Body != null && c32Dto.Body.ImmunizationsDto != null && c32Dto.Body.ImmunizationsDto.Immunizations != null )
            {
                foreach ( var i in c32Dto.Body.ImmunizationsDto.Immunizations )
                {
                    var immunization = GetImmunizationDto ( i );

                    immunizations.Add ( immunization );
                }
            }

            return immunizations;
        }

        private PatientDashboard.ImmunizationDto GetImmunizationDto ( ImmunizationDto immunization )
        {
            var immunizationDto = new PatientDashboard.ImmunizationDto ();

            if ( immunization.MedicationInformations != null && immunization.MedicationInformations.Count > 0 )
            {
                var medicationInformationDto = immunization.MedicationInformations[0];

                var codedProductName = medicationInformationDto.CodedProductName;
                if ( codedProductName != null )
                {
                    immunizationDto.VaccineCodedConcept = new CodedConceptDto
                        {
                            CodedConceptCode = codedProductName.Code,
                            CodeSystemIdentifier = codedProductName.CodeSystem,
                            DisplayName = codedProductName.DisplayName,
                            CodeSystemName = codedProductName.CodeSystemName,
                        };
                }

                var drugManufacturer = medicationInformationDto.DrugManufacturer;
                if ( drugManufacturer != null && drugManufacturer.OrganizationId != null )
                {
                    immunizationDto.VaccineManufacturerCode = drugManufacturer.OrganizationId.Root;
                }
                if ( drugManufacturer != null && drugManufacturer.OrganizationName != null )
                {
                    immunizationDto.VaccineManufacturerName = drugManufacturer.OrganizationName.TextValue;
                }
            }

            if ( immunization.RefusalReason != null && !string.IsNullOrEmpty ( immunization.RefusalReason.Code ) )
            {
                var lookup = _codedConceptLookupBaseRepository.GetLookupByCodedConceptCode<ImmunizationNotGivenReason> ( immunization.RefusalReason.Code );
                if ( lookup != null )
                {
                    immunizationDto.ImmunizationNotGivenReason = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( lookup );
                }
            }

            if (!string.IsNullOrEmpty(immunization.AdministeredDate.Value))
            {
                var administeredDate = GetDate ( immunization.AdministeredDate.Value, ShortDateFormat );
                if (administeredDate != DateTime.MinValue)
                {
                    immunizationDto.ActivityStartDateTime = administeredDate;
                    immunizationDto.ActivityEndDateTime = administeredDate;
                }
            }

            //TODO;
            //Get the following values from C32Dto
            //VaccineLotNumber
            //AdministeredAmount
            //ImmunizationUnitOfMeasure

            return immunizationDto;
        }

        #endregion

        #region Lab Specimen
        
        private LabSpecimenDto GetLabSpecimenFromC32Dto (C32Dto c32Dto)
        {
            if( c32Dto == null || c32Dto.Body == null || c32Dto.Body.Results == null || c32Dto.Body.Results.Count <= 0)
            {
                return null;
            }

            var labSpecimenDto = CreateEmptyLabSpecimenDto();

            foreach ( var result in c32Dto.Body.Results )
            {
                if( !labSpecimenDto.LabTestDate.HasValue)
                {
                    labSpecimenDto.LabTestDate = GetLabTestDate(result);
                }

                var labResultDto = CreateLabResultDto(result);
                if(labResultDto != null)
                {
                    labSpecimenDto.LabResults.Add ( labResultDto );
                }
            }

            if(labSpecimenDto.LabTestDate.HasValue)
            {
                labSpecimenDto.ActivityStartDateTime
                    = labSpecimenDto.ActivityEndDateTime
                      = labSpecimenDto.LabTestDate.Value;
            }

            return labSpecimenDto;
        }

        private LabSpecimenDto CreateEmptyLabSpecimenDto()
        {
            var labSpecimenDto = new LabSpecimenDto ();

            //This is the temp code because C32dto does not have specimen type and test name type
            var specimenType =
                _codedConceptLookupBaseRepository.GetLookupByWellKnownName<LabSpecimenType> (
                    WellKnownNames.PatientModule.LabSpecimenType.Unknown );
            if ( specimenType != null )
            {
                labSpecimenDto.LabSpecimenType = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( specimenType );
            }

            var testNameType =
                _codedConceptLookupBaseRepository.GetLookupByWellKnownName<LabTestName> ( WellKnownNames.PatientModule.LabTestName.ImportedTest );
            if ( testNameType != null )
            {
                labSpecimenDto.LabTestName = AutoMapper.Mapper.Map<LookupBase, LookupValueDto> ( testNameType );
            }

            return labSpecimenDto;
        }

        private LabResultDto CreateLabResultDto( ResultDto resultDto)
        {
            var labResultDto = new LabResultDto ();

            if (resultDto.ProcedureCode != null)
            {
                labResultDto.LabTestResultNameCodedConcept = new CodedConceptDto
                    {
                        CodedConceptCode = resultDto.ProcedureCode.Code,
                        CodeSystemIdentifier = resultDto.ProcedureCode.CodeSystem,
                        CodeSystemName = resultDto.ProcedureCode.CodeSystemName,
                        DisplayName = resultDto.ProcedureCode.DisplayName,
                        OriginalDescription = resultDto.ProcedureCode.OriginalText
                    };
            }

            if (resultDto.ResultValue != null && resultDto.ResultValue.PhysicalQuantity != null)
            {
                var physicalQuantity = resultDto.ResultValue.PhysicalQuantity;
                labResultDto.UnitOfMeasureCode = physicalQuantity.Unit;
                var result = 0D;
                if ( !string.IsNullOrEmpty ( physicalQuantity.Value ) )
                {
                    double.TryParse ( physicalQuantity.Value, out result );
                }
                labResultDto.Value = result;
            }

            return labResultDto;
        }

        private DateTime? GetLabTestDate( ResultDto resultDto)
        {
            var dateString = resultDto.ResultDateTime != null ? resultDto.ResultDateTime.Date : string.Empty;
            var date = GetNullableDate ( dateString );
            return date;
        }

        #endregion

        #region Vital Sign

        private VitalSignDto GetVitalSignFromC32Dto( C32Dto c32Dto)
        {
            if (c32Dto == null || c32Dto.Body == null || c32Dto.Body.VitalSignsDto == null || c32Dto.Body.VitalSignsDto.VitalSignResults == null || c32Dto.Body.VitalSignsDto.VitalSignResults.Count <= 0)
            {
                return null;
            }

            var vitalSignResults = c32Dto.Body.VitalSignsDto.VitalSignResults;

            var vitalSignDto = new VitalSignDto ();
            var activityStartDate = DateTime.MinValue;
            vitalSignDto.WeightLbsMeasure = GetWeightLbs(vitalSignResults, ref activityStartDate);
            vitalSignDto.BloodPressures = GetBloodPressures(vitalSignResults, ref  activityStartDate);
            vitalSignDto.HeartRates = GetHeartRates(vitalSignResults, ref activityStartDate);
            CalculateHeight(vitalSignDto, vitalSignResults, ref activityStartDate);

            //if (c32Dto.Body.PlanOfCare != null && c32Dto.Body.PlanOfCare.PlannedEvents != null)
            //{
            //    SetDietaryConsultationOrderIndicator ( vitalSignDto, c32Dto.Body.PlanOfCare.PlannedEvents );
            //    SetBmiFollowUpPlanIndicator ( vitalSignDto, c32Dto.Body.PlanOfCare.PlannedEvents );
            //}

            if (activityStartDate != DateTime.MinValue)
            {
                vitalSignDto.ActivityStartDateTime = vitalSignDto.ActivityEndDateTime = activityStartDate;
            }
            else
            {
                vitalSignDto.ActivityStartDateTime = vitalSignDto.ActivityEndDateTime = DateTime.Now;
            }

            return vitalSignDto;
        }

        private double? GetWeightLbs(List<ResultDto> vitalSignResults, ref DateTime activityStartDate)
        {
            var bodyWeight = vitalSignResults.FirstOrDefault ( vsr => vsr.ResultType.DisplayName == BodyWeight );

            SetActivityDate ( bodyWeight, ref activityStartDate );
            
            if ( bodyWeight != null && bodyWeight.ResultValue != null && bodyWeight.ResultValue.PhysicalQuantity != null
                    && !string.IsNullOrEmpty ( bodyWeight.ResultValue.PhysicalQuantity.Value ) )
            {
                return GetNullableDoubleValue ( bodyWeight.ResultValue.PhysicalQuantity.Value );
            }

            return null;
        }

        private SoftDeleteObservableCollection<BloodPressureDto> GetBloodPressures(List<ResultDto> vitalSignResults, ref DateTime activityStartDate)
        {
            var bloodPressureDtos = new SoftDeleteObservableCollection<BloodPressureDto> ();

            var systolicBPResults = vitalSignResults.FindAll ( vsr => vsr.ResultType.DisplayName == SystolicBP );
            var diastolicBPResults = vitalSignResults.FindAll ( vsr => vsr.ResultType.DisplayName == DiastolicBP );

            var minCount = systolicBPResults.Count > diastolicBPResults.Count ? diastolicBPResults.Count : systolicBPResults.Count;
            if ( minCount >= 1 )
            {
                for ( int i = 1; i <= minCount; i++ )
                {
                    SetActivityDate(systolicBPResults[i - 1], ref activityStartDate);
                    var bloodPressureDto = CreateBloodPressureDto ( systolicBPResults[i - 1], diastolicBPResults[i - 1] );
                    if ( bloodPressureDto != null )
                    {
                        bloodPressureDtos.Add ( bloodPressureDto );
                    }
                }
            }

            if (systolicBPResults.Count > minCount)
            {
                for( int i = minCount + 1; i <= systolicBPResults.Count; i ++)
                {
                    SetActivityDate(systolicBPResults[i - 1], ref activityStartDate);
                    var bloodPressureDto = CreateBloodPressureDto(systolicBPResults[i - 1], null);
                    if(bloodPressureDto != null )
                    {
                        bloodPressureDtos.Add(bloodPressureDto);
                    }
                }
            }

            if (diastolicBPResults.Count > minCount)
            {
                for (int i = minCount + 1; i <= diastolicBPResults.Count; i++)
                {
                    SetActivityDate(diastolicBPResults[i - 1], ref activityStartDate);
                    var bloodPressureDto = CreateBloodPressureDto( null, diastolicBPResults[i - 1]);
                    if (bloodPressureDto != null)
                    {
                        bloodPressureDtos.Add(bloodPressureDto);
                    }
                }
            }

            return bloodPressureDtos;
        }

        private BloodPressureDto CreateBloodPressureDto(ResultDto systolicBp, ResultDto diastolicBp)
        {
            if (systolicBp.ResultType.DisplayName != SystolicBP || diastolicBp.ResultType.DisplayName != DiastolicBP)
            {
                return null;
            }

            var bloodPressureDto = new BloodPressureDto ();

            if (systolicBp != null && systolicBp.ResultValue != null && systolicBp.ResultValue.PhysicalQuantity != null)
            {
                bloodPressureDto.SystollicMeasure = GetNullableIntValue(systolicBp.ResultValue.PhysicalQuantity.Value);
            }

            if ( diastolicBp != null && diastolicBp.ResultValue != null && diastolicBp.ResultValue.PhysicalQuantity != null)
            {
                bloodPressureDto.DiastollicMeasure = GetNullableIntValue(diastolicBp.ResultValue.PhysicalQuantity.Value);
            }

            return bloodPressureDto;
        }

        private SoftDeleteObservableCollection<HeartRateDto> GetHeartRates(List<ResultDto> vitalSignResults, ref DateTime activityStartDate)
        {
            var heartRateDtos = new SoftDeleteObservableCollection<HeartRateDto>();

            var heartRateResults = vitalSignResults.FindAll(vsr => vsr.ResultType.DisplayName == HeartRate);

            foreach ( var heartRateResult in heartRateResults )
            {
                SetActivityDate(heartRateResult, ref activityStartDate);

                if( heartRateResult.ResultValue != null && heartRateResult.ResultValue.PhysicalQuantity != null && !string.IsNullOrEmpty ( heartRateResult.ResultValue.PhysicalQuantity.Value ))
                {
                    var dto = new HeartRateDto ()
                        {
                            BeatsPerMinuteMeasure = GetNullableIntValue ( heartRateResult.ResultValue.PhysicalQuantity.Value )
                        };
                    heartRateDtos.Add(dto);
                }
            }

            return heartRateDtos;
        }

        private void CalculateHeight(VitalSignDto vitalSignDto, List<ResultDto> vitalSignResults, ref DateTime activityStartDate)
        {
            var heightResult = vitalSignResults.FirstOrDefault ( vsr => vsr.ResultType.DisplayName == BodyHeight );

            SetActivityDate ( heightResult, ref activityStartDate );

            if ( heightResult != null && heightResult.ResultValue != null && heightResult.ResultValue.PhysicalQuantity != null
                 && !string.IsNullOrEmpty ( heightResult.ResultValue.PhysicalQuantity.Value ) )
            {
                var height = GetNullableDoubleValue ( heightResult.ResultValue.PhysicalQuantity.Value );
                if ( height.HasValue )
                {
                    var feet = Math.Floor ( height.Value / 12 );
                    var inches = ( ( height.Value - (feet * 12 )) / 2.54 );

                    vitalSignDto.HeightFeetMeasure = ( ( int )feet );
                    vitalSignDto.HeightInchesMeasure = Math.Round ( inches, 1 );
                }
            }
        }

        private void SetActivityDate(ResultDto resultDto, ref DateTime activityStartDate)
        {
            if ( activityStartDate == DateTime.MinValue && resultDto != null && resultDto.ResultDateTime != null
                 && !string.IsNullOrEmpty ( resultDto.ResultDateTime.Date ) )
            {
                activityStartDate = GetDate ( resultDto.ResultDateTime.Date );
            }
        }

        private void SetDietaryConsultationOrderIndicator(VitalSignDto vitalSignDto, List<PlannedEventDto> plannedEventDtos)
        {
            var dietaryConsultationOrderIndicatorEvent =
                plannedEventDtos.FirstOrDefault ( ped => ped.PlanType.Code == DietaryConsultationOrderIndicatorEventCode );
            if( dietaryConsultationOrderIndicatorEvent != null )
            {
                vitalSignDto.DietaryConsultationOrderIndicator = true;
            }
        }

        private void SetBmiFollowUpPlanIndicator(VitalSignDto vitalSignDto, List<PlannedEventDto> plannedEventDtos)
        {
            var bmiFollowUpPlanIndicatorEvent =
                plannedEventDtos.FirstOrDefault(ped => ped.PlanType.Code == BmiFollowUpPlanIndicatorEventCode);
            if (bmiFollowUpPlanIndicatorEvent != null)
            {
                vitalSignDto.BmiFollowUpPlanIndicator = true;
            }
        }

        #endregion

        #endregion
    }
}
