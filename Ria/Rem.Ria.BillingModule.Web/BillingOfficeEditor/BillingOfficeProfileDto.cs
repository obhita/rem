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

using System;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.BillingModule.Web.BillingOfficeEditor
{
    /// <summary>
    /// Data transfer object for BillingOfficeProfile class.
    /// </summary>
    public class BillingOfficeProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private DateTime? _effectiveDate;
        private string _emailAddress;

        private DateTime? _endDate;

        private string _name;

        #endregion

        #region Public Properties

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

        #endregion
    }
}
