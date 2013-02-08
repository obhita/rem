﻿#region License

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

namespace Rem.Ria.BillingModule.Web.BillingOfficeEditor
{
    /// <summary>
    /// Data transfer object for BillingOfficePhone class.
    /// </summary>
    public class BillingOfficePhoneDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private LookupValueDto _billingOfficePhoneType;

        private string _extension;
        private string _phoneNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the type of the billing office phone.
        /// </summary>
        /// <value>The type of the billing office phone.</value>
        public LookupValueDto BillingOfficePhoneType
        {
            get { return _billingOfficePhoneType; }
            set { ApplyPropertyChange ( ref _billingOfficePhoneType, () => BillingOfficePhoneType, value ); }
        }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>The extension.</value>
        public string Extension
        {
            get { return _extension; }
            set { ApplyPropertyChange ( ref _extension, () => Extension, value ); }
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

        #endregion
    }
}
