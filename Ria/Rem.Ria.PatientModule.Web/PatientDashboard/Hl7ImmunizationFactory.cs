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
using System.Linq;
using HL7Generator.Infrastructure;
using HL7Generator.Infrastructure.Group;
using HL7Generator.Infrastructure.Message;
using HL7Generator.Infrastructure.Segment;
using HL7Generator.Infrastructure.Table;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V251.Message;
using NHibernate.Criterion;
using Pillar.Common.InversionOfControl;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Domain;
using Rem.Ria.PatientModule.Web.Common;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Factory for HL7 immunization.
    /// </summary>
    public class Hl7ImmunizationFactory : IHl7Factory
    {
        #region Constants and Fields

        private readonly ISessionProvider _sessionProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Hl7ImmunizationFactory"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public Hl7ImmunizationFactory ( ISessionProvider sessionProvider )
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
            var activityKey = keyValues[HttpHandlerQueryStrings.ActivityKey];

            var activity = GetActivityForHl7Download ( activityKey );

            if ( activity == null )
            {
                throw new ArgumentException ( "Unable to retrieve activity to create HL7 message" );
            }

            var rootDto = new HL7Generator.Infrastructure.Message.ImmunizationDto
                {
                    MshDto = GetMshDto ( activity ),
                    PidDto = GetPidDto ( activity ),
                    Orders = GetOrderDtos ( activity )
                };

            var helper = IoC.CurrentContainer.Resolve<IMessageHelper<VXU_V04, HL7Generator.Infrastructure.Message.ImmunizationDto>>();

            var factory = IoC.CurrentContainer.Resolve<MessageFactoryBase<VXU_V04, HL7Generator.Infrastructure.Message.ImmunizationDto>>();

            AbstractMessage message;

            if ( helper != null && factory != null )
            {
                message = factory.CreateMessage ( rootDto );
            }
            else
            {
                throw new ArgumentException ( "Unable to get helper/factory instance to create HL7 message" );
            }

            return ( new PipeParser () ).Encode ( message );
        }

        #endregion

        #region Methods

        private static MshDto GetMshDto ( Activity activity )
        {
            var clinicalCase = activity.ClinicalCase;
            return new MshDto
                {
                    SendingApplicationNamespaceId = "EHR Application",
                    SendingFacilityNamespaceId = clinicalCase.ClinicalCaseProfile.InitialLocation.LocationProfile.LocationName.Name,
                    ReceivingApplicationNamespaceId = "PH Application",
                    ReceivingFacilityNamespaceId = "PH Facility"
                };
        }

        private static List<VxuV4OrderDto> GetOrderDtos ( Activity activity )
        {
            var rxaDto = GetRxaDto ( activity );

            return new List<VxuV4OrderDto>
                {
                    new VxuV4OrderDto ( new OrcDto (), rxaDto )
                };
        }

        private static PidDto GetPidDto ( Activity activity )
        {
            var patient = activity.ClinicalCase.Patient;

            var pidDto = new PidDto { IdentifierTypeCodeset = IdentifierTypeCodeset.MedicalRecordNumber };

            if ( patient.Profile.BirthDate.HasValue )
            {
                pidDto.PatientDateOfBirth = patient.Profile.BirthDate.Value;
            }

            pidDto.IdNumber = patient.Key.ToString ();
            pidDto.PatientFirstName = patient.Name.First;
            if ( !string.IsNullOrWhiteSpace ( patient.Name.Middle ) )
            {
                pidDto.PatientMiddleName = patient.Name.Middle;
            }

            pidDto.PatientLastName = patient.Name.Last;

            var patientAddress = patient.Addresses.FirstOrDefault ();
            if ( patientAddress != null )
            {
                pidDto.PatientCity = patientAddress.Address.CityName;
                pidDto.PatientState = patientAddress.Address.StateProvince.ShortName;
                pidDto.PatientStreetAddress = ( string.IsNullOrWhiteSpace ( patientAddress.Address.FirstStreetAddress )
                                                    ? string.Empty
                                                    : patientAddress.Address.FirstStreetAddress )
                                              +
                                              ( string.IsNullOrWhiteSpace ( patientAddress.Address.SecondStreetAddress )
                                                    ? string.Empty
                                                    : " " + patientAddress.Address.SecondStreetAddress );
                pidDto.PatientZipCode = patientAddress.Address.PostalCode.Code;
                pidDto.PatientAddressType = Hl7TypeConverter.ConvertToHl7 ( patientAddress.PatientAddressType );
            }

            pidDto.PatientAdministrativeSex = Hl7TypeConverter.ConvertToHl7 ( patient.Profile.PatientGender );
            if ( patient.Ethnicity != null )
            {
                pidDto.PatientEthnicity = Hl7TypeConverter.ConvertToHl7 ( patient.Ethnicity.Ethnicity );
            }

            var patientPhoneNumber = patient.PhoneNumbers.FirstOrDefault ();
            if ( patientPhoneNumber != null )
            {
                if ( patientPhoneNumber.PhoneNumber != null )
                {
                    var phoneNumber = patientPhoneNumber.PhoneNumber.Replace ( "-", string.Empty );

                    pidDto.PatientHomeTelephoneAreaCityCode = phoneNumber.Substring ( 0, 3 );
                    pidDto.PatientHomeTelephoneLocalNumber = phoneNumber.Substring ( 3, phoneNumber.Length - 3 );
                }

                pidDto.PatientTelecommunicationUseCode = Hl7TypeConverter.ConvertToHl7 ( patientPhoneNumber.PatientPhoneType );
            }

            var patientRace = patient.PrimaryPatientRace;
            if ( patientRace != null )
            {
                pidDto.PatientRace = Hl7TypeConverter.ConvertToHl7 ( patientRace.Race );
            }
            return pidDto;
        }

        private static RxaDto GetRxaDto ( Activity activity )
        {
            var immunization = ( Immunization )activity;

            var rxaDto = new RxaDto ();

            if ( immunization.ImmunizationAdministration != null )
            {
                if ( immunization.ImmunizationAdministration.AdministeredAmount.HasValue )
                {
                    rxaDto.AdministeredAmount = immunization.ImmunizationAdministration.AdministeredAmount.Value.ToString ();
                }

                rxaDto.AdministredUnits = Hl7TypeConverter.ConvertToHl7 ( immunization.ImmunizationAdministration.ImmunizationUnitOfMeasure );
            }

            if ( immunization.ImmunizationVaccineInfo != null )
            {
                rxaDto.AdministeredCode = Hl7TypeConverter.ConvertToHl7 ( immunization.ImmunizationVaccineInfo.VaccineCodedConcept );

                if ( immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer != null )
                {
                    if ( !string.IsNullOrWhiteSpace ( immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerCode ) )
                    {
                        rxaDto.SubstanceManufacturer =
                            VaccineManufacturer.GetVaccineManufacturerByCode (
                                immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerCode );
                    }
                    else if (
                        !string.IsNullOrWhiteSpace (
                            immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerName ) )
                    {
                        rxaDto.SubstanceManufacturer =
                            VaccineManufacturer.GetVaccineManufacturerByName (
                                immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerName );
                    }
                    else if (
                        ( !string.IsNullOrWhiteSpace (
                            immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerCode )
                          ||
                          !string.IsNullOrWhiteSpace (
                              immunization.ImmunizationVaccineInfo.ImmunizationVaccineManufacturer.VaccineManufacturerName ) ) )
                    {
                        rxaDto.SubstanceManufacturer = VaccineManufacturer.UnknownManufacturer;
                    }
                }

                rxaDto.SubstanceLotNumber = immunization.ImmunizationVaccineInfo.VaccineLotNumber;
            }

            if (activity.Visit != null && activity.Visit.CheckedInDateTime.HasValue)
            {
                rxaDto.AdministrationDate = activity.Visit.CheckedInDateTime.Value;
            }
            return rxaDto;
        }

        private Activity GetActivityForHl7Download ( long activityKey )
        {
            var session = _sessionProvider.GetSession ();

            var activityQuery = session.CreateCriteria<Activity> ( "act" )
                .Add ( Restrictions.Eq ( "act.Key", activityKey ) );

            Activity activity = null;
            var activitiyQueryResult = activityQuery.List ();
            if ( activitiyQueryResult != null && activitiyQueryResult.Count > 0 )
            {
                activity = ( Activity )activitiyQueryResult[0];
            }

            return activity;
        }

        #endregion
    }
}
