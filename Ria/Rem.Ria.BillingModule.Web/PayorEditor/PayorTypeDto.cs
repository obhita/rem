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

using Rem.Infrastructure.Service.DataTransferObject;
using Rem.WellKnownNames.PayorModule;

namespace Rem.Ria.BillingModule.Web.PayorEditor
{
    /// <summary>
    /// Data transfer object for Payor Type Profile class.
    /// </summary>
    public class PayorTypeDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private BillingForm _billingForm;

        private string _cityName;
        private string _compositeDelimiter;
        private LookupValueDto _country;
        private LookupValueDto _countyArea;
        private string _elementDelimiter;
        private string _firstStreetAddress;
        private string _ftpAddress;

        private string _interchangeReceiverNumber;
        private string _interchangeSenderNumber;
        private string _name;
        private string _phoneExtensionNumber;
        private string _phoneNumber;
        private string _postalCode;

        private string _repetitionDelimiter;
        private string _secondStreetAddress;
        private string _segmentDelimiter;
        private LookupValueDto _stateProvince;
        private string _submitterIdentifier;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the billing form.
        /// </summary>
        /// <value>The billing form.</value>
        public BillingForm BillingForm
        {
            get { return _billingForm; }
            set { ApplyPropertyChange ( ref _billingForm, () => BillingForm, value ); }
        }

        /// <summary>
        /// Gets or sets the billing office key.
        /// </summary>
        /// <value>The billing office key.</value>
        public long BillingOfficeKey { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city name.</value>
        public string CityName
        {
            get { return _cityName; }
            set { ApplyPropertyChange ( ref _cityName, () => CityName, value ); }
        }

        /// <summary>
        /// Gets or sets the composite delimiter.
        /// </summary>
        /// <value>The composite delimiter.</value>
        public string CompositeDelimiter
        {
            get { return _compositeDelimiter; }
            set { ApplyPropertyChange ( ref _compositeDelimiter, () => CompositeDelimiter, value ); }
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public LookupValueDto Country
        {
            get { return _country; }
            set { ApplyPropertyChange ( ref _country, () => Country, value ); }
        }

        /// <summary>
        /// Gets or sets the county area.
        /// </summary>
        /// <value>The county area.</value>
        public LookupValueDto CountyArea
        {
            get { return _countyArea; }
            set { ApplyPropertyChange ( ref _countyArea, () => CountyArea, value ); }
        }

        /// <summary>
        /// Gets or sets the element delimiter.
        /// </summary>
        /// <value>The element delimiter.</value>
        public string ElementDelimiter
        {
            get { return _elementDelimiter; }
            set { ApplyPropertyChange ( ref _elementDelimiter, () => ElementDelimiter, value ); }
        }

        /// <summary>
        /// Gets or sets the first street address.
        /// </summary>
        /// <value>The first street address.</value>
        public string FirstStreetAddress
        {
            get { return _firstStreetAddress; }
            set { ApplyPropertyChange ( ref _firstStreetAddress, () => FirstStreetAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the FTP address.
        /// </summary>
        /// <value>The FTP address.</value>
        public string FtpAddress
        {
            get { return _ftpAddress; }
            set { ApplyPropertyChange ( ref _ftpAddress, () => FtpAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the interchange receiver number.
        /// </summary>
        /// <value>The interchange receiver number.</value>
        public string InterchangeReceiverNumber
        {
            get { return _interchangeReceiverNumber; }
            set { ApplyPropertyChange ( ref _interchangeReceiverNumber, () => InterchangeReceiverNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the interchange sender number.
        /// </summary>
        /// <value>The interchange sender number.</value>
        public string InterchangeSenderNumber
        {
            get { return _interchangeSenderNumber; }
            set { ApplyPropertyChange ( ref _interchangeSenderNumber, () => InterchangeSenderNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the phone extension number.
        /// </summary>
        /// <value>The phone extension number.</value>
        public string PhoneExtensionNumber
        {
            get { return _phoneExtensionNumber; }
            set { ApplyPropertyChange ( ref _phoneExtensionNumber, () => PhoneExtensionNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { ApplyPropertyChange ( ref _phoneNumber, () => PhoneNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode
        {
            get { return _postalCode; }
            set { ApplyPropertyChange ( ref _postalCode, () => PostalCode, value ); }
        }

        /// <summary>
        /// Gets or sets the repetition delimiter.
        /// </summary>
        /// <value>The repetition delimiter.</value>
        public string RepetitionDelimiter
        {
            get { return _repetitionDelimiter; }
            set { ApplyPropertyChange ( ref _repetitionDelimiter, () => RepetitionDelimiter, value ); }
        }

        /// <summary>
        /// Gets or sets the second street address.
        /// </summary>
        /// <value>The second street address.</value>
        public string SecondStreetAddress
        {
            get { return _secondStreetAddress; }
            set { ApplyPropertyChange ( ref _secondStreetAddress, () => SecondStreetAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the segment delimiter.
        /// </summary>
        /// <value>The segment delimiter.</value>
        public string SegmentDelimiter
        {
            get { return _segmentDelimiter; }
            set { ApplyPropertyChange ( ref _segmentDelimiter, () => SegmentDelimiter, value ); }
        }

        /// <summary>
        /// Gets or sets the state province.
        /// </summary>
        /// <value>The state province.</value>
        public LookupValueDto StateProvince
        {
            get { return _stateProvince; }
            set { ApplyPropertyChange ( ref _stateProvince, () => StateProvince, value ); }
        }

        /// <summary>
        /// Gets or sets the submitter identifier.
        /// </summary>
        /// <value>The submitter identifier.</value>
        public string SubmitterIdentifier
        {
            get { return _submitterIdentifier; }
            set { ApplyPropertyChange ( ref _submitterIdentifier, () => SubmitterIdentifier, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString ()
        {
            return _name;
        }

        #endregion
    }
}
