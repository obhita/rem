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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraDemographicsSection contains patient demographics information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraDemographicsSection : GpraInterviewSectionBase
    {
        private readonly GpraNonResponseType<DateTime?> _birthDate;
        private readonly GpraNonResponseType<bool?> _ethnicGroupCentralAmericanIndicator;
        private readonly GpraNonResponseType<bool?> _ethnicGroupCubanIndicator;
        private readonly GpraNonResponseType<bool?> _ethnicGroupDominicanIndicator;
        private readonly GpraNonResponseType<bool?> _ethnicGroupMexicanIndicator;
        private readonly GpraNonResponseType<bool?> _ethnicGroupOtherIndicator;
        private readonly GpraNonResponseType<bool?> _ethnicGroupPuertoRicanIndicator;
        private readonly GpraNonResponseType<bool?> _ethnicGroupSouthAmericanIndicator;
        private readonly string _ethnicGroupSpecificationNote;
        private readonly GpraNonResponseType<GpraPatientGender> _gpraPatientGender;
        private readonly string _gpraPatientGenderSpecificationNote;
        private readonly GpraNonResponseType<bool?> _hispanicLatinoIndicator;
        private readonly GpraNonResponseType<bool?> _raceAlaskaNativeIndicator;
        private readonly GpraNonResponseType<bool?> _raceAmericanIndianIndicator;
        private readonly GpraNonResponseType<bool?> _raceAsianIndicator;
        private readonly GpraNonResponseType<bool?> _raceBlackAfricanAmericanIndicator;
        private readonly GpraNonResponseType<bool?> _raceNativeHawaiianOtherPacificIslanderIndicator;
        private readonly GpraNonResponseType<bool?> _raceWhiteIndicator;
        private readonly GpraNonResponseType<bool?> _veteranIndicator;

        private GpraDemographicsSection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraDemographicsSection"/> class.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="ethnicGroupCentralAmericanIndicator">The ethnic group central american indicator.</param>
        /// <param name="ethnicGroupCubanIndicator">The ethnic group cuban indicator.</param>
        /// <param name="ethnicGroupDominicanIndicator">The ethnic group dominican indicator.</param>
        /// <param name="ethnicGroupMexicanIndicator">The ethnic group mexican indicator.</param>
        /// <param name="ethnicGroupOtherIndicator">The ethnic group other indicator.</param>
        /// <param name="ethnicGroupPuertoRicanIndicator">The ethnic group puerto rican indicator.</param>
        /// <param name="ethnicGroupSouthAmericanIndicator">The ethnic group south american indicator.</param>
        /// <param name="ethnicGroupSpecificationNote">The ethnic group specification note.</param>
        /// <param name="gpraPatientGender">The gpra patient gender.</param>
        /// <param name="gpraPatientGenderSpecificationNote">The gpra patient gender specification note.</param>
        /// <param name="hispanicLatinoIndicator">The hispanic latino indicator.</param>
        /// <param name="raceAlaskaNativeIndicator">The race alaska native indicator.</param>
        /// <param name="raceAmericanIndianIndicator">The race american indian indicator.</param>
        /// <param name="raceAsianIndicator">The race asian indicator.</param>
        /// <param name="raceBlackAfricanAmericanIndicator">The race black african american indicator.</param>
        /// <param name="raceNativeHawaiianOtherPacificIslanderIndicator">The race native hawaiian other pacific islander indicator.</param>
        /// <param name="raceWhiteIndicator">The race white indicator.</param>
        /// <param name="veteranIndicator">The veteran indicator.</param>
        public GpraDemographicsSection(GpraNonResponseType<DateTime?> birthDate,
                                       GpraNonResponseType<bool?> ethnicGroupCentralAmericanIndicator,
                                       GpraNonResponseType<bool?> ethnicGroupCubanIndicator,
                                       GpraNonResponseType<bool?> ethnicGroupDominicanIndicator,
                                       GpraNonResponseType<bool?> ethnicGroupMexicanIndicator,
                                       GpraNonResponseType<bool?> ethnicGroupOtherIndicator,
                                       GpraNonResponseType<bool?> ethnicGroupPuertoRicanIndicator,
                                       GpraNonResponseType<bool?> ethnicGroupSouthAmericanIndicator,
                                       string ethnicGroupSpecificationNote,
                                       GpraNonResponseType<GpraPatientGender> gpraPatientGender,
                                       string gpraPatientGenderSpecificationNote,
                                       GpraNonResponseType<bool?> hispanicLatinoIndicator,
                                       GpraNonResponseType<bool?> raceAlaskaNativeIndicator,
                                       GpraNonResponseType<bool?> raceAmericanIndianIndicator,
                                       GpraNonResponseType<bool?> raceAsianIndicator,
                                       GpraNonResponseType<bool?> raceBlackAfricanAmericanIndicator,
                                       GpraNonResponseType<bool?> raceNativeHawaiianOtherPacificIslanderIndicator,
                                       GpraNonResponseType<bool?> raceWhiteIndicator,
                                       GpraNonResponseType<bool?> veteranIndicator)
        {
            _birthDate = birthDate;

            if (ethnicGroupCentralAmericanIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => EthnicGroupCentralAmericanIndicator).Contains(ethnicGroupCentralAmericanIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Ethnic Group Central American Indicator.", ethnicGroupCentralAmericanIndicator.GpraNonResponse.Name), "EthnicGroupCentralAmericanIndicator");
            }
            _ethnicGroupCentralAmericanIndicator = ethnicGroupCentralAmericanIndicator;

            if (ethnicGroupCubanIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => EthnicGroupCubanIndicator).Contains(ethnicGroupCubanIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Ethnic Group Cuban Indicator.", ethnicGroupCubanIndicator.GpraNonResponse.Name), "EthnicGroupCubanIndicator");
            }
            _ethnicGroupCubanIndicator = ethnicGroupCubanIndicator;

            if (ethnicGroupDominicanIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => EthnicGroupCubanIndicator).Contains(ethnicGroupDominicanIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Ethnic Group Dominican Indicator.", ethnicGroupDominicanIndicator.GpraNonResponse.Name), "EthnicGroupDominicanIndicator");
            }
            _ethnicGroupDominicanIndicator = ethnicGroupDominicanIndicator;

            if (ethnicGroupMexicanIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => EthnicGroupMexicanIndicator).Contains(ethnicGroupMexicanIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Ethnic Group Mexican Indicator.", ethnicGroupMexicanIndicator.GpraNonResponse.Name), "EthnicGroupMexicanIndicator");
            }
            _ethnicGroupMexicanIndicator = ethnicGroupMexicanIndicator;

            if (ethnicGroupOtherIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => EthnicGroupOtherIndicator).Contains(ethnicGroupOtherIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Ethnic Group Other Indicator.", ethnicGroupOtherIndicator.GpraNonResponse.Name), "EthnicGroupOtherIndicator");
            }
            _ethnicGroupOtherIndicator = ethnicGroupOtherIndicator;

            if (ethnicGroupPuertoRicanIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => EthnicGroupPuertoRicanIndicator).Contains(ethnicGroupPuertoRicanIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Ethnic Group Puerto Rican Indicator.", ethnicGroupPuertoRicanIndicator.GpraNonResponse.Name), "EthnicGroupPuertoRicanIndicator");
            }
            _ethnicGroupPuertoRicanIndicator = ethnicGroupPuertoRicanIndicator;

            if (ethnicGroupSouthAmericanIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => EthnicGroupSouthAmericanIndicator).Contains(ethnicGroupSouthAmericanIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Ethnic Group South Asian Indicator.", ethnicGroupSouthAmericanIndicator.GpraNonResponse.Name), "EthnicGroupSouthAsianIndicator");
            }
            _ethnicGroupSouthAmericanIndicator = ethnicGroupSouthAmericanIndicator;

            _ethnicGroupSpecificationNote = ethnicGroupSpecificationNote;
            _gpraPatientGender = gpraPatientGender;
            _gpraPatientGenderSpecificationNote = gpraPatientGenderSpecificationNote;

            if (hispanicLatinoIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => HispanicLatinoIndicator).Contains(hispanicLatinoIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Hispanic Latino Indicator.", hispanicLatinoIndicator.GpraNonResponse.Name), "HispanicLatinoIndicator");
            }
            _hispanicLatinoIndicator = hispanicLatinoIndicator;

            if (raceAlaskaNativeIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => RaceAlaskaNativeIndicator).Contains(raceAlaskaNativeIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Race Alaska Native Indicator.", raceAlaskaNativeIndicator.GpraNonResponse.Name), "RaceAlaskaNativeIndicator");
            }
            _raceAlaskaNativeIndicator = raceAlaskaNativeIndicator;

            if (raceAmericanIndianIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => RaceAmericanIndianIndicator).Contains(raceAmericanIndianIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Race American Indian Indicator.", raceAmericanIndianIndicator.GpraNonResponse.Name), "RaceAmericanIndianIndicator");
            }
            _raceAmericanIndianIndicator = raceAmericanIndianIndicator;

            if (raceAsianIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => RaceAsianIndicator).Contains(raceAsianIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Race Asian Indicator.", raceAsianIndicator.GpraNonResponse.Name), "RaceAsianIndicator");
            }
            _raceAsianIndicator = raceAsianIndicator;

            if (raceBlackAfricanAmericanIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => RaceBlackAfricanAmericanIndicator).Contains(raceBlackAfricanAmericanIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Race Black African American Indicator.", raceBlackAfricanAmericanIndicator.GpraNonResponse.Name), "RaceBlackAfricanAmericanIndicator");
            }
            _raceBlackAfricanAmericanIndicator = raceBlackAfricanAmericanIndicator;

            if (raceNativeHawaiianOtherPacificIslanderIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => RaceNativeHawaiianOtherPacificIslanderIndicator).Contains(raceNativeHawaiianOtherPacificIslanderIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Race Native Hawaiian Other Pacific Islander Indicator.", raceNativeHawaiianOtherPacificIslanderIndicator.GpraNonResponse.Name), "RaceNativeHawaiianOtherPacificIslanderIndicator");
            }
            _raceNativeHawaiianOtherPacificIslanderIndicator = raceNativeHawaiianOtherPacificIslanderIndicator;

            if (raceWhiteIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => RaceWhiteIndicator).Contains(raceWhiteIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Race White Indicator.", raceWhiteIndicator.GpraNonResponse.Name), "RaceWhiteIndicator");
            }
            _raceWhiteIndicator = raceWhiteIndicator;

            _veteranIndicator = veteranIndicator;
        }

        /// <summary>
        /// Gets the gpra patient gender.
        /// Question 1: What is your gender?
        /// </summary>
        public virtual GpraNonResponseType<GpraPatientGender> GpraPatientGender
        {
            get { return _gpraPatientGender; }
            private set { }
        }

        /// <summary>
        /// Gets the gpra patient gender specification note.
        /// Question 1 (contd): Other (Specify)
        /// </summary>
        public virtual string GpraPatientGenderSpecificationNote
        {
            get { return _gpraPatientGenderSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating hispanic latino.
        /// Question 2: Are you Hispanic or Latino? [IF YES] What ethnic group do you consider yourself? Please answer yes or no for each of the following. You may say yes to more than one.
        /// </summary>
        public virtual GpraNonResponseType<bool?> HispanicLatinoIndicator
        {
            get { return _hispanicLatinoIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group central american.
        /// Question 2(contd): Central American
        /// </summary>
        public virtual GpraNonResponseType<bool?> EthnicGroupCentralAmericanIndicator
        {
            get { return _ethnicGroupCentralAmericanIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group Cuban.
        /// Question 2(contd): Cuban
        /// </summary>
        public virtual GpraNonResponseType<bool?> EthnicGroupCubanIndicator
        {
            get { return _ethnicGroupCubanIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group Dominican.
        /// Question 2(contd): Dominican
        /// </summary>
        public virtual GpraNonResponseType<bool?> EthnicGroupDominicanIndicator
        {
            get { return _ethnicGroupDominicanIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group Mexican.
        /// Question 2(contd): Mexican
        /// </summary>
        public virtual GpraNonResponseType<bool?> EthnicGroupMexicanIndicator
        {
            get { return _ethnicGroupMexicanIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group Puerto Rican.
        /// Question 2(contd): Puerto Rican
        /// </summary>
        public virtual GpraNonResponseType<bool?> EthnicGroupPuertoRicanIndicator
        {
            get { return _ethnicGroupPuertoRicanIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group South American.
        /// Question 2(contd): South American
        /// </summary>
        public virtual GpraNonResponseType<bool?> EthnicGroupSouthAmericanIndicator
        {
            get { return _ethnicGroupSouthAmericanIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group Other.
        /// Question 2(contd): Other (Specify)
        /// </summary>
        public virtual GpraNonResponseType<bool?> EthnicGroupOtherIndicator
        {
            get { return _ethnicGroupOtherIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ethnic group specification note.
        /// Question 2(contd): Other specification
        /// </summary>
        public virtual string EthnicGroupSpecificationNote
        {
            get { return _ethnicGroupSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating race black African American.
        /// Question 3: What is your race? Please answer yes or no for each of the following. You may say yes to more than one. Black or African American
        /// </summary>
        public virtual GpraNonResponseType<bool?> RaceBlackAfricanAmericanIndicator
        {
            get { return _raceBlackAfricanAmericanIndicator; }
            private set  { }
        }

        /// <summary>
        /// Gets a boolean value indicating race Asian.
        /// Question 3 (contd): Asian
        /// </summary>
        public virtual GpraNonResponseType<bool?> RaceAsianIndicator
        {
            get { return _raceAsianIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating race native Hawaiian other Pacific Islander.
        /// Question 3 (contd): Native Hawaiian or other Pacific Islander
        /// </summary>
        public virtual GpraNonResponseType<bool?> RaceNativeHawaiianOtherPacificIslanderIndicator
        {
            get { return _raceNativeHawaiianOtherPacificIslanderIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating race Alaska native.
        /// Question 3 (contd): Alaska Native
        /// </summary>
        public virtual GpraNonResponseType<bool?> RaceAlaskaNativeIndicator
        {
            get { return _raceAlaskaNativeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating race White.
        /// Question 3 (contd): White
        /// </summary>
        public virtual GpraNonResponseType<bool?> RaceWhiteIndicator
        {
            get { return _raceWhiteIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating race American Indian.
        /// Question 3 (contd): American Indian
        /// </summary>
        public virtual GpraNonResponseType<bool?> RaceAmericanIndianIndicator
        {
            get { return _raceAmericanIndianIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the birth date.
        /// Question 4: What is your date of birth?
        /// </summary>
        public virtual GpraNonResponseType<DateTime?> BirthDate
        {
            get { return _birthDate; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating veteran.
        /// Question 5: Are you a veteran?
        /// </summary>
        public virtual GpraNonResponseType<bool?> VeteranIndicator
        {
            get { return _veteranIndicator; }
            private set { }
        }

                /// <summary>
        /// Gets the possible Gpra non response well known names for this interview
        /// section.
        /// </summary>
        /// <typeparam name="TProperty">The property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see> list of WellKNownNames.
        /// </returns>
        public override IEnumerable<string> GetPossibleGpraNonResponseWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            IEnumerable<string> possibleGpraNonResponseWellKnownNames;
            string propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => GpraPatientGender )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => HispanicLatinoIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EthnicGroupCentralAmericanIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EthnicGroupCubanIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EthnicGroupDominicanIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EthnicGroupMexicanIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EthnicGroupPuertoRicanIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EthnicGroupSouthAmericanIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EthnicGroupOtherIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => RaceBlackAfricanAmericanIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => RaceAsianIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => RaceNativeHawaiianOtherPacificIslanderIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => RaceAlaskaNativeIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => RaceWhiteIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => RaceAmericanIndianIndicator ) )
            {
                possibleGpraNonResponseWellKnownNames = new List<string>
                    {
                        WellKnownNames.GpraModule.GpraNonResponse.Refused
                    };
            }
            else if ( propertyName == PropertyUtil.ExtractPropertyName ( () => VeteranIndicator ) )
            {
                possibleGpraNonResponseWellKnownNames = new List<string>
                    {
                        WellKnownNames.GpraModule.GpraNonResponse.Refused,
                        WellKnownNames.GpraModule.GpraNonResponse.DontKnow
                    };
            }
            else
            {
                possibleGpraNonResponseWellKnownNames = base.GetPossibleGpraNonResponseWellKnownNames ( propertyExpression );
            }

            return possibleGpraNonResponseWellKnownNames;
        }

                /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary ()
        {
            return new Dictionary<string, IEnumerable<string>>
                {
                    { PropertyUtil.ExtractPropertyName ( () => GpraPatientGender ), GetPossibleGpraNonResponseWellKnownNames ( () => GpraPatientGender ) },
                    { PropertyUtil.ExtractPropertyName ( () => HispanicLatinoIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => HispanicLatinoIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => EthnicGroupCentralAmericanIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => EthnicGroupCentralAmericanIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => EthnicGroupCubanIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => EthnicGroupCubanIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => EthnicGroupDominicanIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => EthnicGroupDominicanIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => EthnicGroupMexicanIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => EthnicGroupMexicanIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => EthnicGroupPuertoRicanIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => EthnicGroupPuertoRicanIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => EthnicGroupSouthAmericanIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => EthnicGroupSouthAmericanIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => EthnicGroupOtherIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => EthnicGroupOtherIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => RaceBlackAfricanAmericanIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => RaceBlackAfricanAmericanIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => RaceAsianIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => RaceAsianIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => RaceNativeHawaiianOtherPacificIslanderIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => RaceNativeHawaiianOtherPacificIslanderIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => RaceAlaskaNativeIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => RaceAlaskaNativeIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => RaceWhiteIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => RaceWhiteIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => RaceAmericanIndianIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => RaceAmericanIndianIndicator ) },
                    { PropertyUtil.ExtractPropertyName ( () => VeteranIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => VeteranIndicator ) }
                };
        }
    }
}