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
using Rem.Ria.PatientModule.Web.PatientEditor;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for PatientSummary class.
    /// </summary>
    public class PatientSummaryDto : KeyedDataTransferObject
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the patient home address.
        /// </summary>
        /// <value>The patient home address.</value>
        public PatientAddressDto PatientHomeAddress { get; set; }

        /// <summary>
        /// Gets or sets the patient phone numbers.
        /// </summary>
        /// <value>The patient phone numbers.</value>
        public PatientPhoneNumbersDto PatientPhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the patient gender.
        /// </summary>
        /// <value>The patient gender.</value>
        public LookupValueDto Gender { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public string UniqueIdentifier { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get
            {
                var name = new StringBuilder();
                name.Append(string.IsNullOrWhiteSpace(FirstName) ? string.Empty : FirstName + " ");
                name.Append(string.IsNullOrWhiteSpace(MiddleName) ? string.Empty : MiddleName + " ");
                name.Append(string.IsNullOrWhiteSpace(LastName) ? string.Empty : LastName);
                return name.ToString();
            }
        }

        /// <summary>
        /// Gets the full patient home address.
        /// </summary>
        public string FullPatientHomeAddress
        {
            get
            {
                if (PatientHomeAddress != null)
                {
                    return string.Format (
                        "{0}, {1} {2} {3}",
                        PatientHomeAddress.FirstStreetAddress,
                        PatientHomeAddress.CityName,
                        PatientHomeAddress.StateProvince.ShortName,
                        PatientHomeAddress.PostalCode );
                }
                return null;
            }
        }

        #endregion
    }
}
