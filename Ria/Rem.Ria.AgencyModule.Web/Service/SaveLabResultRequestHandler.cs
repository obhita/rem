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
using System.Text;
using Agatha.Common;
using HL7Generator.Infrastructure.Table;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V251;
using NHapi.Model.V251.Group;
using NHapi.Model.V251.Message;
using NHapi.Model.V251.Segment;
using NHibernate;
using NHibernate.Criterion;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.Web.Service
{
    /// <summary>
    /// Class for handling save lab result request.
    /// </summary>
    public class SaveLabResultRequestHandler : NHibernateSessionRequestHandler<SaveLabResultRequest, SaveLabResultResponse>
    {
        #region Constants and Fields

        private readonly LabSpecimenFactory _labSpecimenFactory;
        private readonly ILabSpecimenTypeRepository _labSpecimenTypeRepository;
        private readonly ILabTestNameRepository _labTestNameRepository;
        private readonly IVisitStatusRepository _visitStatusRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLabResultRequestHandler"/> class.
        /// </summary>
        /// <param name="labSpecimenFactory">The lab specimen factory.</param>
        /// <param name="labSpecimenTypeRepository">The lab specimen type repository.</param>
        /// <param name="visitStatusRepository">The visit status repository.</param>
        /// <param name="labTestNameRepository">The lab test name repository.</param>
        public SaveLabResultRequestHandler (
            LabSpecimenFactory labSpecimenFactory,
            ILabSpecimenTypeRepository labSpecimenTypeRepository,
            IVisitStatusRepository visitStatusRepository,
            ILabTestNameRepository labTestNameRepository )
        {
            _labSpecimenFactory = labSpecimenFactory;
            _labSpecimenTypeRepository = labSpecimenTypeRepository;
            _visitStatusRepository = visitStatusRepository;
            _labTestNameRepository = labTestNameRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( SaveLabResultRequest request )
        {
            var response = CreateTypedResponse ();
            response.ErrorMessages = new List<string> ();
            var parser = new PipeParser ();

            var encoding = new UTF8Encoding ();
            var decodedMessage = encoding.GetString ( request.HL7Message );
            var hl7Message = parser.Parse ( decodedMessage );

            if ( hl7Message.Version != "2.5.1" )
            {
                response.ErrorMessages.Add ( "Lab Result is using the version other than 2.5.1" );
                return response;
            }

            var message = ( ORU_R01 )hl7Message;
            var patientResult = message.GetPATIENT_RESULT ();
            var pidSegment = patientResult.PATIENT.PID;
            var observation = patientResult.GetORDER_OBSERVATION ();
            var specimentSegment = observation.SPECIMEN.SPM;
            var obResultSegment = observation.OBSERVATION.OBX;
            var obRequest = observation.OBR;

            if ( pidSegment == null )
            {
                response.ErrorMessages.Add ( "PID segment is missing" );
                return response;
            }

            if ( pidSegment.PatientID.IDNumber.Value == null )
            {
                response.ErrorMessages.Add ( string.Format ( "Patient Id is missing" ) );
                return response;
            }

            var patient = Session.Get<Patient> ( Convert.ToInt64 ( pidSegment.PatientID.IDNumber.Value ) );
            if ( patient == null )
            {
                response.ErrorMessages.Add ( string.Format ( "Patient not found with id : {0}", pidSegment.PatientID.IDNumber.Value ) );
                return response;
            }

            var clinicalCaseCriteria =
                DetachedCriteria.For<ClinicalCase> ().SetProjection ( Projections.Id () ).Add (
                    Restrictions.Eq (
                        Projections.Property<ClinicalCase> ( p => p.Patient.Key ),
                        pidSegment.PatientID.IDNumber.Value ) );
            var checkedInStatus = _visitStatusRepository.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.CheckedIn );
            var visitCriteria =
                Session.CreateCriteria<Visit> ().Add ( Subqueries.PropertyIn ( "ClinicalCase.Key", clinicalCaseCriteria ) )
                    ////.Add(Restrictions.Eq(Projections.Property<Visit>(v => v.VisitStatus), checkedInStatus ))
                    ////TODO: Right now, the CheckedInDateTime is the time when the user click the button to change the VisitStatus. This is not right
                    .Add (
                        Restrictions.Eq ( Projections.Property<Visit> ( v => v.CheckedInDateTime ), obRequest.ObservationDateTime.Time.GetAsDate () ) );

            var visitList = visitCriteria.List ();

            if ( visitList.Count > 0 )
            {
                var visit = ( Visit )visitList[0];

                var labSpecimenType = _labSpecimenTypeRepository.GetByCodedConceptCode ( specimentSegment.SpecimenType.Identifier.Value );
                if ( labSpecimenType == null )
                {
                    labSpecimenType = new LabSpecimenType
                        {
                            CodedConceptCode = specimentSegment.SpecimenType.Identifier.Value,
                            Name = specimentSegment.SpecimenType.Text.Value,
                            EffectiveDateRange = new DateRange ( DateTime.Now, null )
                        };
                    labSpecimenType = _labSpecimenTypeRepository.MakePersistent ( labSpecimenType );
                }

                var labSpecimen = _labSpecimenFactory.CreateLabSpecimen (
                    visit );
                labSpecimen.ReviseLabSpecimenType ( labSpecimenType );

                var labFacility = new LabFacility (
                    obResultSegment.PerformingOrganizationName.OrganizationName.Value,
                    obResultSegment.PerformingOrganizationAddress.StreetAddress.StreetOrMailingAddress.Value,
                    obResultSegment.PerformingOrganizationAddress.City.Value,
                    obResultSegment.PerformingOrganizationAddress.StateOrProvince.Value,
                    obResultSegment.PerformingOrganizationAddress.ZipOrPostalCode.Value );
                labSpecimen.ReviseLabFacility ( labFacility );

                if ( specimentSegment.GetSpecimenRejectReason ().Count () > 0 )
                {
                    labSpecimen.ReviseTestNotCompletedReasonDescription ( specimentSegment.GetSpecimenRejectReason ().FirstOrDefault ().Text.Value );
                }

                var testName = _labTestNameRepository.GetByCodedConceptCode ( obRequest.UniversalServiceIdentifier.Identifier.Value );
                if ( testName == null )
                {
                    testName = new LabTestName
                        {
                            CodedConceptCode = obRequest.UniversalServiceIdentifier.Identifier.Value,
                            Name = obRequest.UniversalServiceIdentifier.Text.Value,
                            EffectiveDateRange = new DateRange ( DateTime.Now, null )
                        };
                    testName = _labTestNameRepository.MakePersistent ( testName );
                }

                CodedConcept interpretationCodeCodedConcept = null;
                if ( obResultSegment.ValueType.Value != "TX" )
                {
                    var abnormalFlagIS = obResultSegment.GetAbnormalFlags ().FirstOrDefault ();
                    if ( abnormalFlagIS != null )
                    {
                        var abnormalFlagCode = abnormalFlagIS.Value;
                        var abnormalFlag = AbnormalFlag.GetAbnormalFlagByCode ( abnormalFlagCode );
                        if ( abnormalFlag != null )
                        {
                            interpretationCodeCodedConcept = new CodedConceptBuilder ()
                                .WithCodedConceptCode ( abnormalFlagCode )
                                .WithDisplayName ( abnormalFlag.WellKnownName )
                                .WithCodeSystemIdentifier ( AbnormalFlag.CodeSystemIdentifier )
                                .WithCodeSystemName ( AbnormalFlag.CodeSystemName );
                        }
                    }
                }

                var labTest = labSpecimen.AddLabTest (
                    new LabTestInfoBuilder ()
                        .WithLabTestName ( testName )
                        .WithTestReportDate ( obRequest.ObservationDateTime.Time.GetAsDate () )
                        .WithNormalRangeDescription ( obResultSegment.ReferencesRange.Value )
                        .WithInterpretationCodedConcept ( interpretationCodeCodedConcept ) );

                var resultTestName = new CodedConceptBuilder ()
                    .WithCodedConceptCode ( obResultSegment.ObservationIdentifier.Identifier.Value )
                    .WithDisplayName ( obResultSegment.ObservationIdentifier.Text.Value );

                double? value = null;
                double tempValue;
                if ( double.TryParse ( obResultSegment.GetObservationValue ().FirstOrDefault ().Data.ToString (), out tempValue ) )
                {
                    value = tempValue;
                }

                var labResult = new LabResultBuilder ()
                    .WithLabTestResultNameCodedConcept ( resultTestName )
                    .WithUnitOfMeasureCode ( obResultSegment.Units.Identifier.Value )
                    .WithValue ( value );

                labTest.AddLabResult ( labResult );
            }
            else
            {
                response.ErrorMessages.Add (
                    string.Format (
                        "Visit not found for Patient id: {0} and Visit Date: {1}",
                        pidSegment.PatientID.IDNumber.Value,
                        obRequest.ObservationDateTime.Time.GetAsDate () ) );
            }

            if ( response.ErrorMessages.Count > 0 )
            {
                response.Exception = new ExceptionInfo ( new Exception ( "SaveLabResult failed, transaction is going to be rollback." ) );
                response.ExceptionType = ExceptionType.Unknown;
            }

            return response;
        }

        #endregion
    }
}
