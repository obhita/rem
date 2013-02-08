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
using HL7Generator.Infrastructure;
using HL7Generator.Infrastructure.Message;
using HL7Generator.Infrastructure.Segment;
using HL7Generator.Infrastructure.Table;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V251.Message;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Domain;
using Rem.Ria.PatientModule.Web.Common;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Factory for HL7 syndromic surveillance.
    /// </summary>
    public class Hl7SyndromicSurveillanceFactory : IHl7Factory
    {
        #region Constants and Fields

        private static readonly string EventFacilityUniversalType = "NPI";
        private static readonly string LoincNameOfCodingSystem = "LN";
        private static readonly string ObservationResultStatus = "F";
        private static readonly string ObservationUnitsIdentifier = "a";
        private static readonly string UnitsOfMeasureCodeSystemName = "UCUM";
        private static readonly string UnitsOfMeasureIdentifierNumber = "21612-7";
        private static readonly string UnitsOfMeasureIdentifierText = "AGE TIME PATIENT REPORTED";
        private static readonly string UnitsOfMeasureObservationUnitsText = "YEAR";
        private static readonly string UnitsOfMeasureValueType = "NM";
        private static readonly string VisitIdentifierTypeCode = "VN";
        private readonly ISessionProvider _sessionProvider;
        private ClinicalCase _clinicalCase;
        private DateTime _messageLocalDateTime;
        private long _minimumVisitKey;
        private DateTime _minimumVisitScheduledStartDateTime;
        private Patient _patient;
        private Problem _problem;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Hl7SyndromicSurveillanceFactory"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public Hl7SyndromicSurveillanceFactory ( ISessionProvider sessionProvider )
        {
            _sessionProvider = sessionProvider;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the HL7 message.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public string GetHl7Message ( Dictionary<string, long> keyValues )
        {
            var problemKey = keyValues[HttpHandlerQueryStrings.ProblemKey];
            var session = _sessionProvider.GetSession ();

            _messageLocalDateTime = DateTime.Now;
            _problem = session.Get<Problem> ( keyValues[HttpHandlerQueryStrings.ProblemKey] );
            Check.IsNotNull ( _problem, string.Format ( "Problem not found for key {0}.", problemKey ) );

            // Derive a visit appointment date for a problem associated with multiple visits. 
            // Return the first minimum Appointment.ScheduledStartDateTime and Visit.Key where the Problem.Key is found.
            var queryResult = session.CreateQuery (
                @"select min(v.AppointmentDateTimeRange.StartDateTime) as AppointmentStartDateTime, vp.Visit.Key as VisitKey
                from VisitProblem as vp
                join vp.Visit as v
                where vp.Problem.Key = :problemKey
                group by vp.Visit.Key" )
                .SetParameter ( "problemKey", problemKey )
                .List ();

            _minimumVisitScheduledStartDateTime = ( DateTime )( ( object[] )queryResult[0] )[0];
            _minimumVisitKey = ( long )( ( object[] )queryResult[0] )[1];
            VerifyDerivedVisit ();
            _clinicalCase = _problem.ClinicalCase;
            _patient = _problem.ClinicalCase.Patient;

            var dto = new SyndromicSurveillanceDto
                {
                    MshDto = GetMshDto (),
                    PidDto = GetPidDto (),
                    EVNDto = GetEvnDto (),
                    PV1Dto = GetPv1Dto (),
                    Observations = GetObservations (),
                    PresentedProblems = GetPresentedProblems ()
                };

            var helper = IoC.CurrentContainer.Resolve<IMessageHelper<ADT_A01, SyndromicSurveillanceDto>> ();
            var factory = IoC.CurrentContainer.Resolve<MessageFactoryBase<ADT_A01, SyndromicSurveillanceDto>>();

            AbstractMessage message;
            if ( helper != null && factory != null )
            {
                message = factory.CreateMessage ( dto );
            }
            else
            {
                throw new ArgumentException ( "HL7 message helper or factory instances were not created." );
            }
            return ( new PipeParser () ).Encode ( message );
        }

        #endregion

        #region Methods

        private EVNDto GetEvnDto ()
        {
            var evnDto = new EVNDto
                {
                    EventFacilityUniversalID = _clinicalCase.ClinicalCaseProfile.InitialLocation.Key.ToString (),
                    EventFacilityUniversalType = EventFacilityUniversalType,
                    EventFaciltyNamespaceID = _clinicalCase.ClinicalCaseProfile.InitialLocation.LocationProfile.LocationName.Name,
                    RecordedDateTime = _minimumVisitScheduledStartDateTime
                };
            return evnDto;
        }

        private MshDto GetMshDto ()
        {
            var mshDto = new MshDto
                {
                    SendingFacilityNamespaceId = _clinicalCase.ClinicalCaseProfile.InitialLocation.LocationProfile.LocationName.Name,
                    SendingFacilityUniversalId = _clinicalCase.ClinicalCaseProfile.InitialLocation.Key.ToString (),
                    DateTimeOfMessage = _messageLocalDateTime,
                };
            return mshDto;
        }

        private List<OBXDto> GetObservations ()
        {
            var obx = new List<OBXDto>
                {
                    new OBXDto
                        {
                            ValueType = UnitsOfMeasureValueType,
                            IdentifierNumber = UnitsOfMeasureIdentifierNumber,
                            IdentifierText = UnitsOfMeasureIdentifierText,
                            NameOfCodingSystem = LoincNameOfCodingSystem,
                            ObservationValue = _patient.Profile.Age.ToString (),
                            ObservationUnitsIdentifier = ObservationUnitsIdentifier,
                            ObservationUnitsText = UnitsOfMeasureObservationUnitsText,
                            ObservationUnitsNameOfCodingSystem = UnitsOfMeasureCodeSystemName,
                            ObservationResultStatus = ObservationResultStatus
                        }
                };
            return obx;
        }

        private PidDto GetPidDto ()
        {
            var pidDto = new PidDto
                {
                    IdentifierTypeCodeset = IdentifierTypeCodeset.MedicalRecordNumber,

                    // BirthDate is currently not required in the domain. 
                    // If BirthDate is not provided when an Hl7 message is generated an exception will be thrown. 
                    PatientDateOfBirth = _patient.Profile.BirthDate.Value,
                    IdNumber = _patient.Key.ToString (),
                    PatientAdministrativeSex = Hl7TypeConverter.ConvertToHl7 ( _patient.Profile.PatientGender ),
                    PatientEthnicity = _patient.Ethnicity == null ? null : Hl7TypeConverter.ConvertToHl7 ( _patient.Ethnicity.Ethnicity ),
                    PatientFirstName = _patient.Name.First,
                    PatientMiddleName = _patient.Name.Middle,
                    PatientLastName = _patient.Name.Last,
                };
            return pidDto;
        }

        private List<DG1Dto> GetPresentedProblems ()
        {
            var dg1 = new List<DG1Dto>
                {
                    new DG1Dto
                        {
                            IdentifierNumber = _problem.ProblemCodeCodedConcept.CodedConceptCode,
                            ProblemCodeCodedConceptCode = _problem.ProblemCodeCodedConcept.DisplayName,
                            NameOfCodingSystem = _problem.ProblemCodeCodedConcept.CodeSystemName,
                        },
                };
            return dg1;
        }

        private PV1Dto GetPv1Dto ()
        {
            var pv1Dto = new PV1Dto
                {
                    AdmitDateTime = _minimumVisitScheduledStartDateTime,
                    VisitIdentifierNumber = _minimumVisitKey.ToString (),
                    IdentifierTypeCode = VisitIdentifierTypeCode
                };
            return pv1Dto;
        }

        private void VerifyDerivedVisit ()
        {
            if ( _minimumVisitScheduledStartDateTime == DateTime.MinValue || _minimumVisitKey == 0 )
            {
                throw new ArgumentException ( "The submitted Problem must be associated to a Visit." );
            }
        }

        #endregion
    }
}
