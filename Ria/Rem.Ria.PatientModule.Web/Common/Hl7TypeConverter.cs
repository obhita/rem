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

using HL7Generator.Infrastructure.Table;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;

using CodedConcept = HL7Generator.Infrastructure.CodedConcept;
using domain = Rem.Domain.Clinical.PatientModule;
using hl7 = HL7Generator.Infrastructure;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Class for converting HL7 type.
    /// </summary>
    internal static class Hl7TypeConverter
    {
        #region Methods

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="patientAddressType">Type of the patient address.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.Table.AddressTypeCodeset"/></returns>
        internal static AddressTypeCodeset ConvertToHl7 ( PatientAddressType patientAddressType )
        {
            AddressTypeCodeset addressTypeCodeset;

            if ( patientAddressType != null && patientAddressType.WellKnownName != null )
            {
                if (patientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.Home ||
                    patientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.PreviousHomeAddress)
                {
                    addressTypeCodeset = AddressTypeCodeset.Home;
                }
                else if(patientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.WorkOffice)
                {
                    addressTypeCodeset = AddressTypeCodeset.OfficeAddress;
                }
                else if(patientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.Temporary)
                {
                    addressTypeCodeset = AddressTypeCodeset.CurrentOrTemporary;
                }
                else if(patientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.Mailing ||
                    patientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.Billing)
                {
                    addressTypeCodeset = AddressTypeCodeset.Mailing;
                }
                else 
                {
                    //Homeless, Other 
                    addressTypeCodeset = AddressTypeCodeset.BadAddress;
                }
            }
            else
            {
                addressTypeCodeset = AddressTypeCodeset.BadAddress;
            }

            return addressTypeCodeset;
        }

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="patientGender">The patient gender.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.Table.GenderCodeset"/></returns>
        internal static GenderCodeset ConvertToHl7 ( PatientGender patientGender )
        {
            return patientGender != null ? ConvertToHl7 ( patientGender.AdministrativeGender ) : GenderCodeset.Unknown;
        }

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="administrativeGender">The administrative gender.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.Table.GenderCodeset"/></returns>
        internal static GenderCodeset ConvertToHl7 ( AdministrativeGender administrativeGender )
        {
            GenderCodeset genderCodeset;

            if ( administrativeGender != null && administrativeGender.WellKnownName != null )
            {
                if (administrativeGender.WellKnownName == WellKnownNames.CommonModule.AdministrativeGender.Female)
                {
                    genderCodeset = GenderCodeset.Female;
                }
                else if (administrativeGender.WellKnownName == WellKnownNames.CommonModule.AdministrativeGender.Male)
                {
                    genderCodeset = GenderCodeset.Male;
                }
                else
                {
                    genderCodeset = GenderCodeset.Unknown;
                }
            }
            else
            {
                genderCodeset = GenderCodeset.Unknown;
            }

            return genderCodeset;
        }

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="ethnicity">The ethnicity.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.Table.EthnicityCodeset"/></returns>
        internal static EthnicityCodeset ConvertToHl7 ( Ethnicity ethnicity )
        {
            // TODO: We do not have ethnicity name like 'HISPANIC OR LATINO' in corresponding table
            EthnicityCodeset ethnicityCodeset;
            if ( ethnicity != null )
            {
                switch ( ethnicity.Name.ToUpper () )
                {
                    case "NOT HISPANIC OR LATINO":
                        ethnicityCodeset = EthnicityCodeset.NotHispanicOrLatino;
                        break;
                    case "HISPANIC OR LATINO":
                        ethnicityCodeset = EthnicityCodeset.HispanicOrLatino;
                        break;

                    default:
                        ethnicityCodeset = EthnicityCodeset.Unknown;
                        break;
                }
            }
            else
            {
                ethnicityCodeset = EthnicityCodeset.Unknown;
            }
            return ethnicityCodeset;
        }

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="patientPhoneType">Type of the patient phone.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.Table.TelecommunicationUseCode"/></returns>
        internal static TelecommunicationUseCode ConvertToHl7 ( PatientPhoneType patientPhoneType )
        {
            TelecommunicationUseCode telecommunicationUseCode = null;

            if ( patientPhoneType != null && patientPhoneType.WellKnownName != null )
            {
                if (patientPhoneType.WellKnownName == WellKnownNames.PatientModule.PatientPhoneType.Home)
                {
                    telecommunicationUseCode = TelecommunicationUseCode.PrimaryResidenceNumber;
                }
                else if (patientPhoneType.WellKnownName == WellKnownNames.PatientModule.PatientPhoneType.Work)
                {
                    telecommunicationUseCode = TelecommunicationUseCode.WorkNumber;
                }
            }

            return telecommunicationUseCode;
        }

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="patientRace">The patient race.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.Table.RaceCodeset"/></returns>
        internal static RaceCodeset ConvertToHl7 ( Race patientRace )
        {
            RaceCodeset raceCodeset = null;

            if ( patientRace != null && patientRace.WellKnownName != null )
            {
                if(patientRace.WellKnownName == WellKnownNames.PatientModule.Race.AlaskaNative ||
                    patientRace.WellKnownName == WellKnownNames.PatientModule.Race.AmericanIndian)
                {
                    raceCodeset = RaceCodeset.AmericanIndianOrAlaskaNative;
                }
                else if (patientRace.WellKnownName == WellKnownNames.PatientModule.Race.Asian)
                {
                    raceCodeset = RaceCodeset.Asian;
                }
                else if (patientRace.WellKnownName == WellKnownNames.PatientModule.Race.BlackorAfricanAmerican)
                {
                    raceCodeset = RaceCodeset.BlackOrAfricanAmerican;
                }
                else if (patientRace.WellKnownName == WellKnownNames.PatientModule.Race.NativeHawaiianandOtherPacificIslander)
                {
                    raceCodeset = RaceCodeset.NativeHawaiianOrOtherPacificIslander;
                }
                else if (patientRace.WellKnownName == WellKnownNames.PatientModule.Race.White)
                {
                    raceCodeset = RaceCodeset.White;
                }
            }

            return raceCodeset;
        }

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="codedConcept">The coded concept.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.CodedConcept"/></returns>
        internal static CodedConcept ConvertToHl7 ( Domain.Core.CommonModule.CodedConcept codedConcept )
        {
            CodedConcept hl7CodedConcept = null;

            if ( codedConcept != null )
            {
                hl7CodedConcept = new CodedConcept (
                    codedConcept.CodeSystemName, codedConcept.CodeSystemIdentifier, codedConcept.CodedConceptCode, codedConcept.DisplayName );
            }

            return hl7CodedConcept;
        }

        /// <summary>
        /// Converts to HL7.
        /// </summary>
        /// <param name="codedConceptLookupBase">The coded concept lookup base.</param>
        /// <returns>A <see cref="HL7Generator.Infrastructure.CodedConcept"/></returns>
        internal static CodedConcept ConvertToHl7 ( CodedConceptLookupBase codedConceptLookupBase )
        {
            CodedConcept hl7CodedConcept = null;

            if ( codedConceptLookupBase != null )
            {
                hl7CodedConcept = new CodedConcept (
                    codedConceptLookupBase.CodeSystemName,
                    codedConceptLookupBase.CodeSystemIdentifier,
                    codedConceptLookupBase.CodedConceptCode,
                    codedConceptLookupBase.WellKnownName );
            }

            return hl7CodedConcept;
        }

        #endregion
    }
}
