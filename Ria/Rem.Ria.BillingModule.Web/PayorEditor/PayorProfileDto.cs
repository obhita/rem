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
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.BillingModule.Web.PayorEditor
{
    /// <summary>
    /// Data transfer object for PayorProfile class.
    /// </summary>
    public class PayorProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private DateTime? _effectiveDate;

        private string _electronicTransmitterIdentificationNumber;

        private string _emailAddress;
        private DateTime? _endDate;

        private string _name;

        private string _payorIdentifier;

        private SoftDeleteObservableCollection<PayorTypeDto> _payorTypes;

        private PayorTypeDto _primariPayorType;
        private string _websiteAddress;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the billing office key.
        /// </summary>
        /// <value>The billing office key.</value>
        public long BillingOfficeKey { get; set; }

        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>The effective date.</value>
        public DateTime? EffectiveDate
        {
            get { return _effectiveDate; }
            set { ApplyPropertyChange ( ref _effectiveDate, () => EffectiveDate, value ); }
        }

        /// <summary>
        /// Gets or sets the electronic transmitter identification number.
        /// </summary>
        /// <value>The electronic transmitter identification number.</value>
        public string ElectronicTransmitterIdentificationNumber
        {
            get { return _electronicTransmitterIdentificationNumber; }
            set { ApplyPropertyChange ( ref _electronicTransmitterIdentificationNumber, () => ElectronicTransmitterIdentificationNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { ApplyPropertyChange ( ref _emailAddress, () => EmailAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
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
        /// Gets or sets the payor identifier.
        /// </summary>
        /// <value>The payor identifier.</value>
        public string PayorIdentifier
        {
            get { return _payorIdentifier; }
            set { ApplyPropertyChange ( ref _payorIdentifier, () => PayorIdentifier, value ); }
        }

        /// <summary>
        /// Gets or sets the payor types.
        /// </summary>
        /// <value>The payor types.</value>
        public SoftDeleteObservableCollection<PayorTypeDto> PayorTypes
        {
            get { return _payorTypes; }
            set
            {
                _payorTypes = value;
                RaisePropertyChanged ( () => PayorTypes );
            }
        }

        /// <summary>
        /// Gets the type of the primary payor.
        /// </summary>
        /// <value>The type of the primary payor.</value>
        public PayorTypeDto PrimaryPayorType
        {
            get { return _primariPayorType; }
            set { ApplyPropertyChange ( ref _primariPayorType, () => PrimaryPayorType, value ); }
        }

        /// <summary>
        /// Gets or sets the website address.
        /// </summary>
        /// <value>The website address.</value>
        public string WebsiteAddress
        {
            get { return _websiteAddress; }
            set { ApplyPropertyChange ( ref _websiteAddress, () => WebsiteAddress, value ); }
        }

        #endregion
    }
}
