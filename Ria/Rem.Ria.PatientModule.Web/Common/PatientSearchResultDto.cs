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
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.PatientEditor;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for PatientSearchResult class.
    /// </summary>
    [DataContract]
    public partial class PatientSearchResultDto : AbstractDataTransferObject
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientSearchResultDto"/> class.
        /// </summary>
        public PatientSearchResultDto ()
        {
            Addresses = new SoftDeleteObservableCollection<PatientAddressDto> ();
            PhoneNumbers = new SoftDeleteObservableCollection<PatientPhoneDto>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>The addresses.</value>
        [DataMember]
        public SoftDeleteObservableCollection<PatientAddressDto> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        [DataMember]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key for the search.</value>
        [DataMember]
        public long Key { get; set; }

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
        /// Gets or sets the name of the patient gender.
        /// </summary>
        /// <value>The name of the patient gender.</value>
        [DataMember]
        public string PatientGenderName { get; set; }

        /// <summary>
        /// Gets or sets the patient gender.
        /// </summary>
        /// <value>
        /// The patient gender.
        /// </value>
        [DataMember]
        public LookupValueDto PatientGender { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>The phone numbers.</value>
        [DataMember]
        public SoftDeleteObservableCollection<PatientPhoneDto> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the name of the prefix.
        /// </summary>
        /// <value>The name of the prefix.</value>
        [DataMember]
        public string PrefixName { get; set; }

        /// <summary>
        /// Gets or sets the name of the suffix.
        /// </summary>
        /// <value>The name of the suffix.</value>
        [DataMember]
        public string SuffixName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        [DataMember]
        public string UniqueIdentifier { get; set; }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var nameBuilder = new StringBuilder();
            nameBuilder.Append(string.IsNullOrWhiteSpace(PrefixName) ? string.Empty : PrefixName.Trim() + " ");
            nameBuilder.Append(string.IsNullOrWhiteSpace(FirstName) ? string.Empty : FirstName.Trim() + " ");
            nameBuilder.Append(string.IsNullOrWhiteSpace(MiddleName) ? string.Empty : MiddleName.Trim() + " ");
            nameBuilder.Append(string.IsNullOrWhiteSpace(LastName) ? string.Empty : LastName.Trim());
            nameBuilder.Append(string.IsNullOrWhiteSpace(SuffixName) ? string.Empty : " " + SuffixName.Trim());

            return nameBuilder.ToString().Trim();
        }
    }
}
