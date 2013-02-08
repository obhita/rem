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

using System.Runtime.Serialization;
using System.Text;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Data transfer object for PatientAddress class.
    /// </summary>
    [DataContract]
    public class PatientAddressDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private string _cityName;
        private bool? _confidentialIndicator;
        private LookupValueDto _country;
        private LookupValueDto _countyArea;
        private string _firstStreetAddress;
        private LookupValueDto _patientAddressType;
        private string _postalCode;
        private string _secondStreetAddress;
        private LookupValueDto _stateProvince;
        private int? _yearsOfStayNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>The name of the city.</value>
        [DataMember]
        public string CityName
        {
            get { return _cityName; }
            set { ApplyPropertyChange ( ref _cityName, () => CityName, value ); }
        }

        /// <summary>
        /// Gets or sets the confidential indicator.
        /// </summary>
        /// <value>The confidential indicator.</value>
        [DataMember]
        public bool? ConfidentialIndicator
        {
            get { return _confidentialIndicator; }
            set { ApplyPropertyChange ( ref _confidentialIndicator, () => ConfidentialIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        [DataMember]
        public LookupValueDto Country
        {
            get { return _country; }
            set { ApplyPropertyChange ( ref _country, () => Country, value ); }
        }

        /// <summary>
        /// Gets or sets the county area.
        /// </summary>
        /// <value>The county area.</value>
        [DataMember]
        public LookupValueDto CountyArea
        {
            get { return _countyArea; }
            set { ApplyPropertyChange ( ref _countyArea, () => CountyArea, value ); }
        }

        /// <summary>
        /// Gets or sets the first street address.
        /// </summary>
        /// <value>The first street address.</value>
        [DataMember]
        public string FirstStreetAddress
        {
            get { return _firstStreetAddress; }
            set { ApplyPropertyChange ( ref _firstStreetAddress, () => FirstStreetAddress, value ); }
        }

        /// <summary>
        /// Gets the full address.
        /// </summary>
        public string FullAddress
        {
            get
            {
                var fullAddress = new StringBuilder ();

                if ( !string.IsNullOrWhiteSpace ( FirstStreetAddress ) )
                {
                    fullAddress.AppendLine ( FirstStreetAddress.Trim () );
                }

                var isMultiStreetLine = !string.IsNullOrWhiteSpace ( SecondStreetAddress.Trim () );

                if ( isMultiStreetLine && !string.IsNullOrWhiteSpace ( FirstStreetAddress ) )
                {
                    fullAddress.AppendLine ( SecondStreetAddress );
                }

                fullAddress.Append ( CityName + ", " + StateProvince.Name + " " + PostalCode );
                return fullAddress.ToString ();
            }
        }

        /// <summary>
        /// Gets or sets the type of the patient address.
        /// </summary>
        /// <value>The type of the patient address.</value>
        [DataMember]
        public LookupValueDto PatientAddressType
        {
            get { return _patientAddressType; }
            set { ApplyPropertyChange ( ref _patientAddressType, () => PatientAddressType, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        [DataMember]
        public string PostalCode
        {
            get { return _postalCode; }
            set { ApplyPropertyChange ( ref _postalCode, () => PostalCode, value ); }
        }

        /// <summary>
        /// Gets or sets the second street address.
        /// </summary>
        /// <value>The second street address.</value>
        [DataMember]
        public string SecondStreetAddress
        {
            get { return _secondStreetAddress; }
            set { ApplyPropertyChange ( ref _secondStreetAddress, () => SecondStreetAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the state province.
        /// </summary>
        /// <value>The state province.</value>
        [DataMember]
        public LookupValueDto StateProvince
        {
            get { return _stateProvince; }
            set { ApplyPropertyChange ( ref _stateProvince, () => StateProvince, value ); }
        }

        /// <summary>
        /// Gets or sets the years of stay number.
        /// </summary>
        /// <value>The years of stay number.</value>
        [DataMember]
        public int? YearsOfStayNumber
        {
            get { return _yearsOfStayNumber; }
            set { ApplyPropertyChange ( ref _yearsOfStayNumber, () => YearsOfStayNumber, value ); }
        }

        #endregion
    }
}
