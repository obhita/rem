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
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraDemographics class.
    /// </summary>
    public class GpraDemographicsDto : GpraDtoBase
    {
        #region Constants and Fields

        private GpraNonResponseTypeDto<DateTime?> _birthDate;
        private GpraNonResponseTypeDto<bool?> _ethnicGroupCentralAmericanIndicator;
        private GpraNonResponseTypeDto<bool?> _ethnicGroupCubanIndicator;
        private GpraNonResponseTypeDto<bool?> _ethnicGroupDominicanIndicator;
        private GpraNonResponseTypeDto<bool?> _ethnicGroupMexicanIndicator;
        private GpraNonResponseTypeDto<bool?> _ethnicGroupOtherIndicator;
        private GpraNonResponseTypeDto<bool?> _ethnicGroupPuertoRicanIndicator;
        private GpraNonResponseTypeDto<bool?> _ethnicGroupSouthAmericanIndicator;
        private string _ethnicGroupSpecificationNote;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraPatientGender;
        private string _gpraPatientGenderSpecificationNote;
        private GpraNonResponseTypeDto<bool?> _hispanicLatinoIndicator;
        private GpraNonResponseTypeDto<bool?> _raceAlaskaNativeIndicator;
        private GpraNonResponseTypeDto<bool?> _raceAmericanIndianIndicator;
        private GpraNonResponseTypeDto<bool?> _raceAsianIndicator;
        private GpraNonResponseTypeDto<bool?> _raceBlackAfricanAmericanIndicator;
        private GpraNonResponseTypeDto<bool?> _raceNativeHawaiianOtherPacificIslanderIndicator;
        private GpraNonResponseTypeDto<bool?> _raceWhiteIndicator;
        private GpraNonResponseTypeDto<bool?> _veteranIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question 4: What is your date of birth?
        /// </summary>
        /// <value>The birth date.</value>
        public GpraNonResponseTypeDto<DateTime?> BirthDate
        {
            get { return _birthDate; }
            set { ApplyPropertyChange ( ref _birthDate, () => BirthDate, value ); }
        }

        /// <summary>
        /// Question 2(contd): Central American
        /// </summary>
        /// <value>The ethnic group central american indicator.</value>
        public GpraNonResponseTypeDto<bool?> EthnicGroupCentralAmericanIndicator
        {
            get { return _ethnicGroupCentralAmericanIndicator; }
            set { ApplyPropertyChange ( ref _ethnicGroupCentralAmericanIndicator, () => EthnicGroupCentralAmericanIndicator, value ); }
        }

        /// <summary>
        /// Question 2(contd): Cuban
        /// </summary>
        /// <value>The ethnic group cuban indicator.</value>
        public GpraNonResponseTypeDto<bool?> EthnicGroupCubanIndicator
        {
            get { return _ethnicGroupCubanIndicator; }
            set { ApplyPropertyChange ( ref _ethnicGroupCubanIndicator, () => EthnicGroupCubanIndicator, value ); }
        }

        /// <summary>
        /// Question 2(contd): Dominican
        /// </summary>
        /// <value>The ethnic group dominican indicator.</value>
        public GpraNonResponseTypeDto<bool?> EthnicGroupDominicanIndicator
        {
            get { return _ethnicGroupDominicanIndicator; }
            set { ApplyPropertyChange ( ref _ethnicGroupDominicanIndicator, () => EthnicGroupDominicanIndicator, value ); }
        }

        /// <summary>
        /// Question 2(contd): Mexican
        /// </summary>
        /// <value>The ethnic group mexican indicator.</value>
        public GpraNonResponseTypeDto<bool?> EthnicGroupMexicanIndicator
        {
            get { return _ethnicGroupMexicanIndicator; }
            set { ApplyPropertyChange ( ref _ethnicGroupMexicanIndicator, () => EthnicGroupMexicanIndicator, value ); }
        }

        /// <summary>
        /// Question 2(contd): Other (Specify)
        /// </summary>
        /// <value>The ethnic group other indicator.</value>
        public GpraNonResponseTypeDto<bool?> EthnicGroupOtherIndicator
        {
            get { return _ethnicGroupOtherIndicator; }
            set { ApplyPropertyChange ( ref _ethnicGroupOtherIndicator, () => EthnicGroupOtherIndicator, value ); }
        }

        /// <summary>
        /// Question 2(contd): Puerto Rican
        /// </summary>
        /// <value>The ethnic group puerto rican indicator.</value>
        public GpraNonResponseTypeDto<bool?> EthnicGroupPuertoRicanIndicator
        {
            get { return _ethnicGroupPuertoRicanIndicator; }
            set { ApplyPropertyChange ( ref _ethnicGroupPuertoRicanIndicator, () => EthnicGroupPuertoRicanIndicator, value ); }
        }

        /// <summary>
        /// Question 2(contd): South American
        /// </summary>
        /// <value>The ethnic group south american indicator.</value>
        public GpraNonResponseTypeDto<bool?> EthnicGroupSouthAmericanIndicator
        {
            get { return _ethnicGroupSouthAmericanIndicator; }
            set { ApplyPropertyChange ( ref _ethnicGroupSouthAmericanIndicator, () => EthnicGroupSouthAmericanIndicator, value ); }
        }

        /// <summary>
        /// Question 2(contd): Other specification
        /// </summary>
        /// <value>The ethnic group specification note.</value>
        public string EthnicGroupSpecificationNote
        {
            get { return _ethnicGroupSpecificationNote; }
            set { ApplyPropertyChange ( ref _ethnicGroupSpecificationNote, () => EthnicGroupSpecificationNote, value ); }
        }

        /// <summary>
        /// Question 1: What is your gender?
        /// </summary>
        /// <value>The gpra patient gender.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraPatientGender
        {
            get { return _gpraPatientGender; }
            set { ApplyPropertyChange ( ref _gpraPatientGender, () => GpraPatientGender, value ); }
        }

        /// <summary>
        /// Question 1 (contd): Other (Specify)
        /// </summary>
        /// <value>The gpra patient gender specification note.</value>
        public string GpraPatientGenderSpecificationNote
        {
            get { return _gpraPatientGenderSpecificationNote; }
            set { ApplyPropertyChange ( ref _gpraPatientGenderSpecificationNote, () => GpraPatientGenderSpecificationNote, value ); }
        }

        /// <summary>
        /// Question 2: Are you Hispanic or Latino? [IF YES] What ethnic group do you consider yourself? Please answer yes or no for each of the following. You may say yes to more than one.
        /// </summary>
        /// <value>The hispanic latino indicator.</value>
        public GpraNonResponseTypeDto<bool?> HispanicLatinoIndicator
        {
            get { return _hispanicLatinoIndicator; }
            set { ApplyPropertyChange ( ref _hispanicLatinoIndicator, () => HispanicLatinoIndicator, value ); }
        }

        /// <summary>
        /// Question 3 (contd): Alaska Native
        /// </summary>
        /// <value>The race alaska native indicator.</value>
        public GpraNonResponseTypeDto<bool?> RaceAlaskaNativeIndicator
        {
            get { return _raceAlaskaNativeIndicator; }
            set { ApplyPropertyChange ( ref _raceAlaskaNativeIndicator, () => RaceAlaskaNativeIndicator, value ); }
        }

        /// <summary>
        /// Question 3 (contd): American Indian
        /// </summary>
        /// <value>The race american indian indicator.</value>
        public GpraNonResponseTypeDto<bool?> RaceAmericanIndianIndicator
        {
            get { return _raceAmericanIndianIndicator; }
            set { ApplyPropertyChange ( ref _raceAmericanIndianIndicator, () => RaceAmericanIndianIndicator, value ); }
        }

        /// <summary>
        /// Question 3 (contd): Asian
        /// </summary>
        /// <value>The race asian indicator.</value>
        public GpraNonResponseTypeDto<bool?> RaceAsianIndicator
        {
            get { return _raceAsianIndicator; }
            set { ApplyPropertyChange ( ref _raceAsianIndicator, () => RaceAsianIndicator, value ); }
        }

        /// <summary>
        /// Question 3: What is your race? Please answer yes or no for each of the following. You may say yes to more than one. Black or African American
        /// </summary>
        /// <value>The race black african american indicator.</value>
        public GpraNonResponseTypeDto<bool?> RaceBlackAfricanAmericanIndicator
        {
            get { return _raceBlackAfricanAmericanIndicator; }
            set { ApplyPropertyChange ( ref _raceBlackAfricanAmericanIndicator, () => RaceBlackAfricanAmericanIndicator, value ); }
        }

        /// <summary>
        /// Question 3 (contd): Native Hawaiian or other Pacific Islander
        /// </summary>
        /// <value>The race native hawaiian other pacific islander indicator.</value>
        public GpraNonResponseTypeDto<bool?> RaceNativeHawaiianOtherPacificIslanderIndicator
        {
            get { return _raceNativeHawaiianOtherPacificIslanderIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _raceNativeHawaiianOtherPacificIslanderIndicator, () => RaceNativeHawaiianOtherPacificIslanderIndicator, value );
            }
        }

        /// <summary>
        /// Question 3 (contd): White
        /// </summary>
        /// <value>The race white indicator.</value>
        public GpraNonResponseTypeDto<bool?> RaceWhiteIndicator
        {
            get { return _raceWhiteIndicator; }
            set { ApplyPropertyChange ( ref _raceWhiteIndicator, () => RaceWhiteIndicator, value ); }
        }

        /// <summary>
        /// Question 5: Are you a veteran?
        /// </summary>
        /// <value>The veteran indicator.</value>
        public GpraNonResponseTypeDto<bool?> VeteranIndicator
        {
            get { return _veteranIndicator; }
            set { ApplyPropertyChange ( ref _veteranIndicator, () => VeteranIndicator, value ); }
        }

        #endregion
    }
}
