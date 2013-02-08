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
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for Staff class.
    /// </summary>
    [DataContract]
    public partial class StaffDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private StaffAddressesDto _addresses;
        private StaffCredentialsDto _credentials;
        private StaffHRDto _humanResource;
        private StaffIdentifiersDto _identifiers;
        private StaffLocationAssignmentDto _locationAssignment;
        private StaffPhoneNumbersDto _phoneNumbers;
        private StaffPhotoDto _staffPhoto;
        private StaffProfileDto _staffProfile;
        private SystemAccountDto _systemAccount;
        private StaffSystemRolesDto _systemRoles;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>The addresses.</value>
        [DataMember]
        public StaffAddressesDto Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value;
                RaisePropertyChanged ( () => Addresses );
            }
        }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        [DataMember]
        public StaffCredentialsDto Credentials
        {
            get { return _credentials; }
            set
            {
                _credentials = value;
                RaisePropertyChanged ( () => Credentials );
            }
        }

        /// <summary>
        /// Gets or sets the human resource.
        /// </summary>
        /// <value>The human resource.</value>
        [DataMember]
        public StaffHRDto HumanResource
        {
            get { return _humanResource; }
            set
            {
                _humanResource = value;
                RaisePropertyChanged ( () => HumanResource );
            }
        }

        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        /// <value>The identifiers.</value>
        [DataMember]
        public StaffIdentifiersDto Identifiers
        {
            get { return _identifiers; }
            set
            {
                _identifiers = value;
                RaisePropertyChanged ( () => Identifiers );
            }
        }

        /// <summary>
        /// Gets or sets the location assignment.
        /// </summary>
        /// <value>The location assignment.</value>
        [DataMember]
        public StaffLocationAssignmentDto LocationAssignment
        {
            get { return _locationAssignment; }
            set
            {
                _locationAssignment = value;
                RaisePropertyChanged ( () => LocationAssignment );
            }
        }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>The phone numbers.</value>
        [DataMember]
        public StaffPhoneNumbersDto PhoneNumbers
        {
            get { return _phoneNumbers; }
            set
            {
                _phoneNumbers = value;
                RaisePropertyChanged ( () => PhoneNumbers );
            }
        }

        /// <summary>
        /// Gets or sets the staff photo.
        /// </summary>
        /// <value>The staff photo.</value>
        [DataMember]
        public StaffPhotoDto StaffPhoto
        {
            get { return _staffPhoto; }
            set
            {
                _staffPhoto = value;
                RaisePropertyChanged ( () => StaffPhoto );
            }
        }

        /// <summary>
        /// Gets or sets the staff profile.
        /// </summary>
        /// <value>The staff profile.</value>
        [DataMember]
        public StaffProfileDto StaffProfile
        {
            get { return _staffProfile; }
            set
            {
                _staffProfile = value;
                RaisePropertyChanged ( () => StaffProfile );
            }
        }

        /// <summary>
        /// Gets or sets the system account.
        /// </summary>
        /// <value>The system account.</value>
        [DataMember]
        public SystemAccountDto SystemAccount
        {
            get { return _systemAccount; }
            set
            {
                _systemAccount = value;
                RaisePropertyChanged ( () => SystemAccount );
            }
        }

        /// <summary>
        /// Gets or sets the system roles.
        /// </summary>
        /// <value>The system roles.</value>
        [DataMember]
        public StaffSystemRolesDto SystemRoles
        {
            get { return _systemRoles; }
            set
            {
                _systemRoles = value;
                RaisePropertyChanged ( () => SystemRoles );
            }
        }

        #endregion
    }
}
