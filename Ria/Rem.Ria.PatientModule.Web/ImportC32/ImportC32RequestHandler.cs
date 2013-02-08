#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using System.Linq;
using Agatha.Common;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Extension;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientEditor;

namespace Rem.Ria.PatientModule.Web.ImportC32
{
    /// <summary>
    /// Class for handling importing c32 request.
    /// </summary>
    public class ImportC32RequestHandler :
        NHibernateSessionRequestHandler<ImportC32Request, ImportC32Response>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        private readonly IPatientDocumentRepository _patientDocumentRepository;
        private readonly IProvenanceFactory _provenanceFactory;
        private readonly IClinicalCaseRepository _clinicalCaseRepository;
        private readonly IProblemFactory _problemFactory;
        private readonly IImmunizationFactory _immunizationFactory;
        private readonly ILabSpecimenFactory _labSpecimenFactory;
        private readonly IVitalSignFactory _vitalSignFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportC32RequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="patientDocumentRepository">The patient document repository.</param>
        /// <param name="provenanceFactory">The provenance factory.</param>
        /// <param name="clinicalCaseRepository">The clinical case repository.</param>
        /// <param name="problemFactory">The problem factory.</param>
        /// <param name="immunizationFactory">The immunization factory.</param>
        /// <param name="labSpecimenFactory">The lab specimen factory.</param>
        /// <param name="vitalSignFactory">The vital sign factory.</param>
        public ImportC32RequestHandler (
            IDtoToDomainMappingHelper mappingHelper,
            IPatientDocumentRepository patientDocumentRepository,
            IProvenanceFactory provenanceFactory,
            IClinicalCaseRepository clinicalCaseRepository,
            IProblemFactory problemFactory,
            IImmunizationFactory immunizationFactory,
            ILabSpecimenFactory labSpecimenFactory,
            IVitalSignFactory vitalSignFactory
            )
        {
            _mappingHelper = mappingHelper;
            _patientDocumentRepository = patientDocumentRepository;
            _provenanceFactory = provenanceFactory;
            _clinicalCaseRepository = clinicalCaseRepository;
            _problemFactory = problemFactory;
            _immunizationFactory = immunizationFactory;
            _labSpecimenFactory = labSpecimenFactory;
            _vitalSignFactory = vitalSignFactory;
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Agatha Response.</returns>
        public override Response Handle ( ImportC32Request request )
        {
            // 1. Import Provenance
            // 2. Import medications... with Provenance data
            // 3. Update Patient document to mark it imported

            var provenance = CreateProvenance ( request.Provenance );

            var patientDocument = _patientDocumentRepository.GetByKey ( request.PatientDocumentKey );
            var patient = patientDocument.Patient;

            request.Medications.ToList ().ForEach ( dto => CreateMedication ( dto, patient, provenance ) );
            request.Allergies.ToList ().ForEach ( dto => CreateAllergy ( dto, patient, provenance ) );
            request.Problems.ToList ().ForEach ( dto => CreateProblem ( dto, patient.Key, provenance ) );
            request.Immunizations.ToList ().ForEach ( dto => CreateImmunization ( dto, patient.Key, provenance ) );
            CreateLabSpecimen ( request.LabSpecimen, patient.Key, provenance );
            CreateVitalSign ( request.VitalSign, patient.Key, provenance );

            patientDocument.ReviseC32ImportedIndicator ( true );
            var response = CreateTypedResponse ();
            return response;
        }

        #endregion

        #region Creating Methods

        private Provenance CreateProvenance ( ProvenanceDto provenanceDto )
        {
            var provenance = _provenanceFactory.CreateProvenance (
                new TaggedDataElement ( provenanceDto.AssigningAuthority, provenanceDto.Extension ), provenanceDto.SignedTimestamp );
            provenance.ReviseAssignedAuthor (
                new AssignedAuthor (
                    provenanceDto.ProviderDirectoryEntry,
                    new PersonName ( provenanceDto.PrefixName, provenanceDto.FirstName, null, provenanceDto.LastName, null ) ) );
            provenance.ReviseRepresentedOrganization (
                new RepresentedOrganization (
                    new TaggedDataElement (
                        string.IsNullOrWhiteSpace ( provenanceDto.OrganizationAssigningAuthority )
                            ? "Test"
                            : provenanceDto.OrganizationAssigningAuthority,
                        provenanceDto.OrganizationExtension ),
                    provenanceDto.OrganizationName,
                    new Phone ( provenanceDto.PhoneNumber, null ) ) );
            return provenance;
        }

        private void CreateMedication ( MedicationDto dto, Patient patient, Provenance provenance )
        {
            CodedConcept medicationCode = null;
            if ( dto.MedicationCodeCodedConcept != null )
            {
                medicationCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.MedicationCodeCodedConcept );
            }

            var medication = patient.AddMedication ( medicationCode, provenance );

            CodedConcept rootMedicationCode = null;
            if ( dto.RootMedicationCodedConcept != null )
            {
                rootMedicationCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.RootMedicationCodedConcept );
            }

            var discontinuedReason = _mappingHelper.MapLookupField<DiscontinuedReason> ( dto.DiscontinuedReason );
            var medicationStatus = dto.MedicationStatus == null
                                       ? _mappingHelper.MapLookupField<MedicationStatus> ( WellKnownNames.PatientModule.MedicationStatus.Inactive )
                                       : _mappingHelper.MapLookupField<MedicationStatus> ( dto.MedicationStatus );

            medication.ReviseOverTheCounterIndicator ( dto.OverTheCounterIndicator );
            medication.RevisePrescribingPhysicianName ( dto.PrescribingPhysicianName );
            medication.ReviseUsageDateRange ( new DateRange ( dto.StartDate, dto.EndDate ) );
            medication.ReviseDiscontinuedByPhysicianName ( dto.DiscontinuedByPhysicianName );
            medication.ReviseDiscontinuedReason ( discontinuedReason );
            medication.ReviseDiscontinuedReasonOtherDescription ( dto.DiscontinuedReasonOtherDescription );
            medication.ReviseFrequencyDescription ( dto.FrequencyDescription );
            medication.ReviseInstructionsNote ( dto.InstructionsNote );
            medication.ReviseMedicationStatus ( medicationStatus );
            medication.ReviseRootMedicationCodedConcept ( rootMedicationCode );
        }

        private void CreateAllergy ( AllergyDto dto, Patient patient, Provenance provenance )
        {
            var allergyStatus = _mappingHelper.MapLookupField<AllergyStatus> ( dto.AllergyStatus );
            CodedConcept allergenCodedConcept = null;
            if ( dto.AllergenCodedConcept != null )
            {
                allergenCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( dto.AllergenCodedConcept );
            }
            var allergy = patient.AddAllergy ( allergyStatus, allergenCodedConcept, provenance );


            var allergySeverityType = _mappingHelper.MapLookupField<AllergySeverityType> ( dto.AllergySeverityType );
            var allergyType = _mappingHelper.MapLookupField<AllergyType> ( dto.AllergyType );

            allergy.ReviseAllergySeverityType ( allergySeverityType );
            allergy.ReviseAllergyType ( allergyType );
            allergy.ReviseOnsetDateRange ( new DateRange ( dto.OnsetStartDate, dto.OnsetEndDate ) );

            // Map reactions
            var deletedReactions = allergy.AllergyReactions.Where (
                a => dto.AllergyReactions.All ( ad => ad.Key != a.Reaction.Key ) ).ToList ();
            deletedReactions.ForEach ( allergy.DeleteReaction );

            var addedReactions = dto.AllergyReactions.Where (
                a => allergy.AllergyReactions.All ( ad => ad.Reaction.Key != a.Key ) ).ToList ();
            addedReactions.ForEach ( r => allergy.AddReaction ( _mappingHelper.MapLookupField<Reaction> ( r ) ) );
        }

        private void CreateProblem ( ProblemDto dto, long patientKey, Provenance provenance )
        {
            var clinicalCase = _clinicalCaseRepository.GetActiveClinicalCaseByPatient ( patientKey );

            CodedConcept problemCode = null;
            if ( dto.ProblemCodeCodedConcept != null )
            {
                problemCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.ProblemCodeCodedConcept );
            }
            var problem = _problemFactory.CreateProblem ( clinicalCase, problemCode, provenance );

            var problemType = _mappingHelper.MapLookupField<ProblemType> ( dto.ProblemType );
            var problemStatus = _mappingHelper.MapLookupField<ProblemStatus> ( dto.ProblemStatus );

            problem.ReviseProblemType ( problemType );
            problem.ReviseOnsetDateRange ( new DateRange ( dto.OnsetStartDate, dto.OnsetEndDate ) );
            problem.UpdateProblemStatus ( problemStatus, dto.StatusChangedDate );
            problem.ReviseCauseOfDeathIndicator ( dto.CauseOfDeathIndicator );

            if ( dto.ObservedByStaff != null )
            {
                var staff = Session.Load<Staff> ( dto.ObservedByStaff.Key );
                problem.ReviseObservationInfo ( staff, dto.ObservedDate );
            }
        }

        private void CreateImmunization ( ImmunizationDto dto, long patientKey, Provenance provenance )
        {
            var clinicalCase = _clinicalCaseRepository.GetActiveClinicalCaseByPatient ( patientKey );
            var immunization = _immunizationFactory.CreateImmunization (
                clinicalCase, provenance, new DateTimeRange ( dto.ActivityStartDateTime, dto.ActivityStartDateTime ) );

            CodedConcept vaccineCodedConcept = null;
            if ( dto.VaccineCodedConcept != null )
            {
                vaccineCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( dto.VaccineCodedConcept );
            }

            var unitOfMeasure = _mappingHelper.MapLookupField<ImmunizationUnitOfMeasure> ( dto.ImmunizationUnitOfMeasure );
            var notGivenReason = _mappingHelper.MapLookupField<ImmunizationNotGivenReason> ( dto.ImmunizationNotGivenReason );

            immunization.ReviseImmunizationVaccineInfo (
                new ImmunizationVaccineInfo (
                    vaccineCodedConcept,
                    dto.VaccineLotNumber,
                    new ImmunizationVaccineManufacturer ( dto.VaccineManufacturerCode, dto.VaccineManufacturerName ) ) );

            immunization.ReviseImmunizationAdministration ( new ImmunizationAdministration ( dto.AdministeredAmount, unitOfMeasure ) );
            immunization.ReviseImmunizationNotGivenReason ( notGivenReason );
        }

        private void CreateLabSpecimen ( LabSpecimenDto dto, long patientKey, Provenance provenance )
        {
            if ( dto == null || dto.LabResults == null || dto.LabResults.Count == 0 )
            {
                return;
            }

            var clinicalCase = _clinicalCaseRepository.GetActiveClinicalCaseByPatient ( patientKey );
            var labSpecimen = _labSpecimenFactory.CreateLabSpecimen (
                clinicalCase, provenance, new DateTimeRange ( dto.ActivityStartDateTime, dto.ActivityStartDateTime ) );
            var labSpecimenType = _mappingHelper.MapLookupField<LabSpecimenType> ( dto.LabSpecimenType );

            labSpecimen.ReviseLabSpecimenType ( labSpecimenType );
            labSpecimen.ReviseLabReceivedDate ( dto.LabReceivedDate );
            labSpecimen.ReviseCollectedHereIndicator ( dto.CollectedHereIndicator );

            // TODO: This needs to be rethought when the domain for Lab is redone.
            var labTest = labSpecimen.LabTests.FirstOrDefault ( lt => lt.LabTestInfo.LabTestName.WellKnownName == dto.LabTestName.WellKnownName );
            if ( labTest == null && labSpecimen.LabTests.Count > 0 )
            {
                //right now there is only every one lab test per lab specimen?
                labSpecimen.RemoveLabTest ( labSpecimen.LabTests.ElementAt ( 0 ) );
            }
            var labTestInfo = new LabTestInfoBuilder ()
                .WithLabTestName ( _mappingHelper.MapLookupField<LabTestName> ( dto.LabTestName ) )
                .WithTestReportDate ( dto.LabTestDate )
                .WithLabTestNote ( dto.LabTestNote );
            if ( labTest == null )
            {
                labTest = labSpecimen.AddLabTest ( labTestInfo );
            }
            else
            {
                labTest.ReviseLabTestInfo ( labTestInfo );
            }

            var result = new AggregateNodeCollectionMapper<LabResultDto, LabTest, LabResult> ( dto.LabResults, labTest, labTest.LabResults )
                .MapAddedItem (
                    ( lrdto, lt ) =>
                        {
                            CodedConcept labTestResultNameCodedConcept = null;
                            if ( lrdto.LabTestResultNameCodedConcept != null )
                            {
                                labTestResultNameCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( lrdto.LabTestResultNameCodedConcept );
                            }

                            lt.AddLabResult (
                                new LabResultBuilder ()
                                    .WithLabTestResultNameCodedConcept ( labTestResultNameCodedConcept )
                                    .WithUnitOfMeasureCode ( lrdto.UnitOfMeasureCode )
                                    .WithValue ( lrdto.Value ) );
                        }
                )
                .MapChangedItem (
                    ( lrdto, lt, lr ) =>
                        {
                            lt.RemoveLabResult ( lr );
                            CodedConcept labTestResultNameCodedConcept = null;
                            if ( lrdto.LabTestResultNameCodedConcept != null )
                            {
                                labTestResultNameCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( lrdto.LabTestResultNameCodedConcept );
                            }
                            lt.AddLabResult (
                                new LabResultBuilder ()
                                    .WithLabTestResultNameCodedConcept ( labTestResultNameCodedConcept )
                                    .WithValue ( lrdto.Value )
                                    .WithUnitOfMeasureCode ( lrdto.UnitOfMeasureCode ) );
                        } )
                .MapRemovedItem ( ( lrdto, lt, lr ) => lt.RemoveLabResult ( lr ) )
                .Map ();
        }

        #region VitalSign

        private void CreateVitalSign ( VitalSignDto dto, long patientKey, Provenance provenance )
        {
            if ( dto == null )
            {
                return;
            }

            var clinicalCase = _clinicalCaseRepository.GetActiveClinicalCaseByPatient ( patientKey );
            var vitalSign = _vitalSignFactory.CreateVitalSign (
                clinicalCase, provenance, new DateTimeRange ( dto.ActivityStartDateTime, dto.ActivityEndDateTime ) );
            vitalSign.ReviseHeight ( new Height ( dto.HeightFeetMeasure, dto.HeightInchesMeasure ) );
            vitalSign.ReviseWeight ( dto.WeightLbsMeasure );

            new AggregateNodeCollectionMapper<BloodPressureDto, VitalSign, BloodPressure> (
                dto.BloodPressures, vitalSign, vitalSign.BloodPressures )
                .MapRemovedItem ( RemoveBloodPressure )
                .MapAddedItem ( AddBloodPressure )
                .MapChangedItem ( ChangeBloodPressure )
                .Map ();

            new AggregateNodeCollectionMapper<HeartRateDto, VitalSign, HeartRate> ( dto.HeartRates, vitalSign, vitalSign.HeartRates )
                .MapRemovedItem ( RemoveHeartRate )
                .MapAddedItem ( AddHeartRate )
                .MapChangedItem ( ChangeHeartRate )
                .Map ();
        }

        private static void AddBloodPressure ( BloodPressureDto bloodPressureDto, VitalSign vitalSign )
        {
            vitalSign.AddBloodPressure (
                new BloodPressure ( bloodPressureDto.SystollicMeasure.GetValueOrDefault (), bloodPressureDto.DiastollicMeasure.GetValueOrDefault () ) );
        }

        private static void AddHeartRate ( HeartRateDto heartRateDto, VitalSign vitalSign )
        {
            vitalSign.AddHeartRate ( new HeartRate ( heartRateDto.BeatsPerMinuteMeasure.GetValueOrDefault () ) );
        }

        private static void ChangeBloodPressure ( BloodPressureDto bloodPressureDto, VitalSign vitalSign, BloodPressure bloodPressure )
        {
            RemoveBloodPressure ( bloodPressureDto, vitalSign, bloodPressure );
            AddBloodPressure ( bloodPressureDto, vitalSign );
        }

        private static void ChangeHeartRate ( HeartRateDto heartRateDto, VitalSign vitalSign, HeartRate heartRate )
        {
            RemoveHeartRate ( heartRateDto, vitalSign, heartRate );
            AddHeartRate ( heartRateDto, vitalSign );
        }

        private static void RemoveBloodPressure ( BloodPressureDto bloodPressureDto, VitalSign vitalSign, BloodPressure bloodPressure )
        {
            vitalSign.RemoveBloodPressure ( bloodPressure );
        }

        private static void RemoveHeartRate ( HeartRateDto heartRateDto, VitalSign vitalSign, HeartRate heartRate )
        {
            vitalSign.RemoveHeartRate ( heartRate );
        }

        #endregion

        #endregion
    }
}
