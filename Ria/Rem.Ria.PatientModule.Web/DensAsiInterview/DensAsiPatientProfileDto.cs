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

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiPatientProfile class.
    /// </summary>
    public class DensAsiPatientProfileDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DateTime? _admissionDate;
        private DateTime? _birthDate;
        private DensAsiNonResponseTypeDto<bool?> _buddhismReligionIndicator;
        private string _buddhismReligionIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _christianReligionIndicator;
        private string _christianReligionIndicatorNote;
        private string _cityName;
        private LookupValueDto _densAsiInterviewClass;
        private string _densAsiInterviewClassNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _densAsiInterviewContactType;
        private string _densAsiInterviewContactTypeNote;
        private string _firstName;
        private string _firstStreetAddress;
        private bool? _hispanicOrLatinoIndicator;
        private DateTime? _interviewDate;
        private string _interviewDateNote;
        private string _lastName;
        private DensAsiNonResponseTypeDto<int?> _lastThirtyDaysControlledEnvironmentDayCount;
        private string _lastThirtyDaysControlledEnvironmentDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _lastThirtyDaysDensAsiControlledEnvironment;
        private string _lastThirtyDaysDensAsiControlledEnvironmentNote;
        private DensAsiNonResponseTypeDto<bool?> _noParticularReligiousSectIndicator;
        private string _noParticularReligiousSectIndicatorNote;
        private LookupValueDto _patientAdministrativeGender;
        private string _postalCode;
        private DensAsiNonResponseTypeDto<LookupValueDto> _preferredDensAsiReligion;
        private string _preferredDensAsiReligionNote;
        private LookupValueDto _race;
        private DensAsiNonResponseTypeDto<bool?> _residenceOwnedByYouOrFamilyIndicator;
        private string _residenceOwnedByYouOrFamilyIndicatorNote;
        private string _secondStreetAddress;
        private LookupValueDto _stateProvince;
        private DensAsiNonResponseTypeDto<TimeSpan?> _yearsAndMonthsAtCurrentAddressTimeSpan;
        private string _yearsAndMonthsAtCurrentAddressTimeSpanNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the admission date.
        /// </summary>
        /// <value>The admission date.</value>
        public virtual DateTime? AdmissionDate
        {
            get { return _admissionDate; }
            set { ApplyPropertyChange ( ref _admissionDate, () => AdmissionDate, value ); }
        }

        /// <summary>
        /// Question Number: G16
        /// </summary>
        /// <value>The birth date.</value>
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set { ApplyPropertyChange ( ref _birthDate, () => BirthDate, value ); }
        }

        /// <summary>
        /// Question Number: G102
        /// </summary>
        /// <value>The buddhism religion indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> BuddhismReligionIndicator
        {
            get { return _buddhismReligionIndicator; }
            set { ApplyPropertyChange ( ref _buddhismReligionIndicator, () => BuddhismReligionIndicator, value ); }
        }

        /// <summary>
        /// Question Number: G102
        /// </summary>
        /// <value>The buddhism religion indicator note.</value>
        public string BuddhismReligionIndicatorNote
        {
            get { return _buddhismReligionIndicatorNote; }
            set { ApplyPropertyChange ( ref _buddhismReligionIndicatorNote, () => BuddhismReligionIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: G101
        /// </summary>
        /// <value>The christian religion indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ChristianReligionIndicator
        {
            get { return _christianReligionIndicator; }
            set { ApplyPropertyChange ( ref _christianReligionIndicator, () => ChristianReligionIndicator, value ); }
        }

        /// <summary>
        /// Question Number: G101
        /// </summary>
        /// <value>The christian religion indicator note.</value>
        public string ChristianReligionIndicatorNote
        {
            get { return _christianReligionIndicatorNote; }
            set { ApplyPropertyChange ( ref _christianReligionIndicatorNote, () => ChristianReligionIndicatorNote, value ); }
        }

        /// <summary>
        /// Address type : Home
        /// </summary>
        /// <value>The name of the city.</value>
        public string CityName
        {
            get { return _cityName; }
            set { ApplyPropertyChange ( ref _cityName, () => CityName, value ); }
        }

        /// <summary>
        /// Question Number: G8
        /// </summary>
        /// <value>The dens asi interview class.</value>
        public LookupValueDto DensAsiInterviewClass
        {
            get { return _densAsiInterviewClass; }
            set { ApplyPropertyChange ( ref _densAsiInterviewClass, () => DensAsiInterviewClass, value ); }
        }

        /// <summary>
        /// Question Number: G8
        /// </summary>
        /// <value>The dens asi interview class note.</value>
        public string DensAsiInterviewClassNote
        {
            get { return _densAsiInterviewClassNote; }
            set { ApplyPropertyChange ( ref _densAsiInterviewClassNote, () => DensAsiInterviewClassNote, value ); }
        }

        /// <summary>
        /// Question Number: G9
        /// </summary>
        /// <value>The type of the dens asi interview contact.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> DensAsiInterviewContactType
        {
            get { return _densAsiInterviewContactType; }
            set { ApplyPropertyChange ( ref _densAsiInterviewContactType, () => DensAsiInterviewContactType, value ); }
        }

        /// <summary>
        /// Question Number: G9
        /// </summary>
        /// <value>The dens asi interview contact type note.</value>
        public string DensAsiInterviewContactTypeNote
        {
            get { return _densAsiInterviewContactTypeNote; }
            set { ApplyPropertyChange ( ref _densAsiInterviewContactTypeNote, () => DensAsiInterviewContactTypeNote, value ); }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get { return _firstName; }
            set { ApplyPropertyChange ( ref _firstName, () => FirstName, value ); }
        }

        /// <summary>
        /// Address type : Home
        /// </summary>
        /// <value>The first street address.</value>
        public string FirstStreetAddress
        {
            get { return _firstStreetAddress; }
            set { ApplyPropertyChange ( ref _firstStreetAddress, () => FirstStreetAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the hispanic or latino indicator.
        /// </summary>
        /// <value>The hispanic or latino indicator.</value>
        public bool? HispanicOrLatinoIndicator
        {
            get { return _hispanicOrLatinoIndicator; }
            set { ApplyPropertyChange ( ref _hispanicOrLatinoIndicator, () => HispanicOrLatinoIndicator, value ); }
        }

        /// <summary>
        /// Question Number: G5
        /// </summary>
        /// <value>The interview date.</value>
        public DateTime? InterviewDate
        {
            get { return _interviewDate; }
            set { ApplyPropertyChange ( ref _interviewDate, () => InterviewDate, value ); }
        }

        /// <summary>
        /// Question Number: G5
        /// </summary>
        /// <value>The interview date note.</value>
        public string InterviewDateNote
        {
            get { return _interviewDateNote; }
            set { ApplyPropertyChange ( ref _interviewDateNote, () => InterviewDateNote, value ); }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get { return _lastName; }
            set { ApplyPropertyChange ( ref _lastName, () => LastName, value ); }
        }

        /// <summary>
        /// Question Number: G20
        /// </summary>
        /// <value>The last thirty days controlled environment day count.</value>
        public DensAsiNonResponseTypeDto<int?> LastThirtyDaysControlledEnvironmentDayCount
        {
            get { return _lastThirtyDaysControlledEnvironmentDayCount; }
            set { ApplyPropertyChange ( ref _lastThirtyDaysControlledEnvironmentDayCount, () => LastThirtyDaysControlledEnvironmentDayCount, value ); }
        }

        /// <summary>
        /// Question Number: G20
        /// </summary>
        /// <value>The last thirty days controlled environment day count note.</value>
        public string LastThirtyDaysControlledEnvironmentDayCountNote
        {
            get { return _lastThirtyDaysControlledEnvironmentDayCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _lastThirtyDaysControlledEnvironmentDayCountNote, () => LastThirtyDaysControlledEnvironmentDayCountNote, value );
            }
        }

        /// <summary>
        /// Question Number: G19
        /// </summary>
        /// <value>The last thirty days dens asi controlled environment.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> LastThirtyDaysDensAsiControlledEnvironment
        {
            get { return _lastThirtyDaysDensAsiControlledEnvironment; }
            set { ApplyPropertyChange ( ref _lastThirtyDaysDensAsiControlledEnvironment, () => LastThirtyDaysDensAsiControlledEnvironment, value ); }
        }

        /// <summary>
        /// Question Number: G19
        /// </summary>
        /// <value>The last thirty days dens asi controlled environment note.</value>
        public string LastThirtyDaysDensAsiControlledEnvironmentNote
        {
            get { return _lastThirtyDaysDensAsiControlledEnvironmentNote; }
            set
            {
                ApplyPropertyChange (
                    ref _lastThirtyDaysDensAsiControlledEnvironmentNote, () => LastThirtyDaysDensAsiControlledEnvironmentNote, value );
            }
        }

        /// <summary>
        /// Question Number: G103
        /// </summary>
        /// <value>The no particular religious sect indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> NoParticularReligiousSectIndicator
        {
            get { return _noParticularReligiousSectIndicator; }
            set { ApplyPropertyChange ( ref _noParticularReligiousSectIndicator, () => NoParticularReligiousSectIndicator, value ); }
        }

        /// <summary>
        /// Question Number: G103
        /// </summary>
        /// <value>The no particular religious sect indicator note.</value>
        public string NoParticularReligiousSectIndicatorNote
        {
            get { return _noParticularReligiousSectIndicatorNote; }
            set { ApplyPropertyChange ( ref _noParticularReligiousSectIndicatorNote, () => NoParticularReligiousSectIndicatorNote, value ); }
        }

        /// <summary>
        /// Gets or sets the patient administrative gender.
        /// </summary>
        /// <value>The patient administrative gender.</value>
        public virtual LookupValueDto PatientAdministrativeGender
        {
            get { return _patientAdministrativeGender; }
            set { ApplyPropertyChange ( ref _patientAdministrativeGender, () => PatientAdministrativeGender, value ); }
        }

        /// <summary>
        /// Address type : Home
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode
        {
            get { return _postalCode; }
            set { ApplyPropertyChange ( ref _postalCode, () => PostalCode, value ); }
        }

        /// <summary>
        /// Question Number: G18
        /// </summary>
        /// <value>The preferred dens asi religion.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> PreferredDensAsiReligion
        {
            get { return _preferredDensAsiReligion; }
            set { ApplyPropertyChange ( ref _preferredDensAsiReligion, () => PreferredDensAsiReligion, value ); }
        }

        /// <summary>
        /// Question Number: G18
        /// </summary>
        /// <value>The preferred dens asi religion note.</value>
        public string PreferredDensAsiReligionNote
        {
            get { return _preferredDensAsiReligionNote; }
            set { ApplyPropertyChange ( ref _preferredDensAsiReligionNote, () => PreferredDensAsiReligionNote, value ); }
        }

        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        /// <value>The race of the patient.</value>
        public LookupValueDto Race
        {
            get { return _race; }
            set { ApplyPropertyChange ( ref _race, () => Race, value ); }
        }

        /// <summary>
        /// Question Number: G15
        /// </summary>
        /// <value>The residence owned by you or family indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ResidenceOwnedByYouOrFamilyIndicator
        {
            get { return _residenceOwnedByYouOrFamilyIndicator; }
            set { ApplyPropertyChange ( ref _residenceOwnedByYouOrFamilyIndicator, () => ResidenceOwnedByYouOrFamilyIndicator, value ); }
        }

        /// <summary>
        /// Question Number: G15
        /// </summary>
        /// <value>The residence owned by you or family indicator note.</value>
        public string ResidenceOwnedByYouOrFamilyIndicatorNote
        {
            get { return _residenceOwnedByYouOrFamilyIndicatorNote; }
            set { ApplyPropertyChange ( ref _residenceOwnedByYouOrFamilyIndicatorNote, () => ResidenceOwnedByYouOrFamilyIndicatorNote, value ); }
        }

        /// <summary>
        /// Address type : Home
        /// </summary>
        /// <value>The second street address.</value>
        public string SecondStreetAddress
        {
            get { return _secondStreetAddress; }
            set { ApplyPropertyChange ( ref _secondStreetAddress, () => SecondStreetAddress, value ); }
        }

        /// <summary>
        /// Address type : Home
        /// </summary>
        /// <value>The state province.</value>
        public LookupValueDto StateProvince
        {
            get { return _stateProvince; }
            set { ApplyPropertyChange ( ref _stateProvince, () => StateProvince, value ); }
        }

        /// <summary>
        /// Question Number: G14
        /// </summary>
        /// <value>The years and months at current address time span.</value>
        public DensAsiNonResponseTypeDto<TimeSpan?> YearsAndMonthsAtCurrentAddressTimeSpan
        {
            get { return _yearsAndMonthsAtCurrentAddressTimeSpan; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsAtCurrentAddressTimeSpan, () => YearsAndMonthsAtCurrentAddressTimeSpan, value ); }
        }

        /// <summary>
        /// Question Number: G14
        /// </summary>
        /// <value>The years and months at current address time span note.</value>
        public string YearsAndMonthsAtCurrentAddressTimeSpanNote
        {
            get { return _yearsAndMonthsAtCurrentAddressTimeSpanNote; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsAtCurrentAddressTimeSpanNote, () => YearsAndMonthsAtCurrentAddressTimeSpanNote, value ); }
        }

        #endregion
    }
}
