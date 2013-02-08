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

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for StaffSummary class.
    /// </summary>
    public partial class StaffSummaryDto : KeyedDataTransferObject
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>The addresses.</value>
        [DataMember]
        public ObservableCollection<StaffAddressSummaryDto> Addresses { get; set; }

        /// <summary>
        /// Gets the name of the complete.
        /// </summary>
        public string CompleteName
        {
            get { return LastName + ", " + FirstName + " " + ProfessionalCredentialNote ?? string.Empty; }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        [DataMember]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        /// <value>The identifiers.</value>
        [DataMember]
        public ObservableCollection<StaffIdentifierSummaryDto> Identifiers { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        [DataMember]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>The phone numbers.</value>
        [DataMember]
        public ObservableCollection<StaffPhoneSummaryDto> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the professional credential note.
        /// </summary>
        /// <value>The professional credential note.</value>
        [DataMember]
        public string ProfessionalCredentialNote { get; set; }

        /// <summary>
        /// Gets or sets the staff photo.
        /// </summary>
        /// <value>The staff photo.</value>
        [DataMember]
        public StaffPhotoDto StaffPhoto { get; set; }

        /// <summary>
        /// Gets or sets the type of the staff.
        /// </summary>
        /// <value>The type of the staff.</value>
        [DataMember]
        public string StaffType { get; set; }

        /// <summary>
        /// Gets or sets the name of the suffix.
        /// </summary>
        /// <value>The name of the suffix.</value>
        [DataMember]
        public string SuffixName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the supervisor staff.
        /// </summary>
        /// <value>The first name of the supervisor staff.</value>
        [DataMember]
        public string SupervisorStaffFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the supervisor staff.
        /// </summary>
        /// <value>The last name of the supervisor staff.</value>
        [DataMember]
        public string SupervisorStaffLastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the title.
        /// </summary>
        /// <value>The name of the title.</value>
        [DataMember]
        public string TitleName { get; set; }

        #endregion
    }
}
