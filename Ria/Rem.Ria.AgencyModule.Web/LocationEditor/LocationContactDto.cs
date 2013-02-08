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
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Data transfer object for LocationContact class.
    /// </summary>
    public class LocationContactDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private bool _alternativeContactIndicator;
        private StaffNameDto _contactStaff;
        private DateTime? _endDate;
        private LookupValueDto _locationContactType;
        private DateTime? _startDate;
        private bool _statusIndicator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationContactDto"/> class.
        /// </summary>
        public LocationContactDto ()
        {
            _statusIndicator = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [alternative contact indicator].
        /// </summary>
        /// <value><c>true</c> if [alternative contact indicator]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool AlternativeContactIndicator
        {
            get { return _alternativeContactIndicator; }
            set { ApplyPropertyChange ( ref _alternativeContactIndicator, () => AlternativeContactIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the contact staff.
        /// </summary>
        /// <value>The contact staff.</value>
        [DataMember]
        public StaffNameDto ContactStaff
        {
            get { return _contactStaff; }
            set { ApplyPropertyChange ( ref _contactStaff, () => ContactStaff, value ); }
        }

        /// <summary>
        /// Gets or sets the effective end date.
        /// </summary>
        /// <value>The effective end date.</value>
        [DataMember]
        public DateTime? EffectiveEndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EffectiveEndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the effective start date.
        /// </summary>
        /// <value>The effective start date.</value>
        [DataMember]
        public DateTime? EffectiveStartDate
        {
            get { return _startDate; }
            set { ApplyPropertyChange ( ref _startDate, () => EffectiveStartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the location contact.
        /// </summary>
        /// <value>The type of the location contact.</value>
        [DataMember]
        public LookupValueDto LocationContactType
        {
            get { return _locationContactType; }
            set { ApplyPropertyChange ( ref _locationContactType, () => LocationContactType, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [status indicator].
        /// </summary>
        /// <value><c>true</c> if [status indicator]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool StatusIndicator
        {
            get { return _statusIndicator; }
            set { ApplyPropertyChange ( ref _statusIndicator, () => StatusIndicator, value ); }
        }

        #endregion
    }
}
