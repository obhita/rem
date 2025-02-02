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

using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for StaffPhoneNumber class.
    /// </summary>
    [DataContract]
    public class StaffPhoneNumberDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private string _phoneExtensionNumber;
        private string _phoneNumber;
        private LookupValueDto _staffPhoneType;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the phone extension number.
        /// </summary>
        /// <value>The phone extension number.</value>
        [DataMember]
        public string PhoneExtensionNumber
        {
            get { return _phoneExtensionNumber; }
            set { ApplyPropertyChange ( ref _phoneExtensionNumber, () => PhoneExtensionNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        [DataMember]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { ApplyPropertyChange ( ref _phoneNumber, () => PhoneNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the staff key.
        /// </summary>
        /// <value>The staff key.</value>
        [DataMember]
        public long StaffKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the staff phone.
        /// </summary>
        /// <value>The type of the staff phone.</value>
        [DataMember]
        public LookupValueDto StaffPhoneType
        {
            get { return _staffPhoneType; }
            set { ApplyPropertyChange ( ref _staffPhoneType, () => StaffPhoneType, value ); }
        }

        #endregion
    }
}
