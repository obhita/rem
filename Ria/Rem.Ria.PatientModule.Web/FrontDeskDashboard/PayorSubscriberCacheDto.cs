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
using System.Text;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for payor subscriber.
    /// </summary>
    public partial class PayorSubscriberCacheDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private AddressDto _address;

        private DateTime? _birthDate;

        private string _firstName;

        private LookupValueDto _administrativeGender;

        private string _lastName;

        private string _middleName;

        private LookupValueDto _payorSubscriberRelationshipCacheType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorSubscriberCacheDto"/> class.
        /// </summary>
        public PayorSubscriberCacheDto ()
        {
            Address = new AddressDto ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public AddressDto Address
        {
            get { return _address; }
            set { ApplyPropertyChange ( ref _address, () => Address, value ); }
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set { ApplyPropertyChange ( ref _birthDate, () => BirthDate, value ); }
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
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get
            {
                var nameBuilder = new StringBuilder ();
                nameBuilder.Append ( string.IsNullOrEmpty ( FirstName ) ? string.Empty : FirstName + " " );
                nameBuilder.Append ( string.IsNullOrEmpty ( MiddleName ) ? string.Empty : MiddleName + " " );
                nameBuilder.Append ( string.IsNullOrEmpty ( LastName ) ? string.Empty : LastName + " " );
                return nameBuilder.ToString ();
            }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public LookupValueDto AdministrativeGender
        {
            get { return _administrativeGender; }
            set { ApplyPropertyChange ( ref _administrativeGender, () => AdministrativeGender, value ); }
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
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        public string MiddleName
        {
            get { return _middleName; }
            set { ApplyPropertyChange ( ref _middleName, () => MiddleName, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the payor subscriber relationship.
        /// </summary>
        /// <value>The type of the payor subscriber relationship.</value>
        public LookupValueDto PayorSubscriberRelationshipCacheType
        {
            get { return _payorSubscriberRelationshipCacheType; }
            set { ApplyPropertyChange ( ref _payorSubscriberRelationshipCacheType, () => PayorSubscriberRelationshipCacheType, value ); }
        }

        #endregion
    }
}
