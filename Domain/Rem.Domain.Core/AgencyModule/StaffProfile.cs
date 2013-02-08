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
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The StaffProfile defines general profile information for a staff.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class StaffProfile
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="StaffProfile"/> class from being created.
        /// </summary>
        private StaffProfile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffProfile"/> class.
        /// </summary>
        /// <param name="staffName">Name of the staff.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="socialSecurityNumber">The social security number.</param>
        /// <param name="staffType">Type of the staff.</param>
        /// <param name="professionalCredentialNote">The professional credential note.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="employmentDateRange">The employment date range.</param>
        /// <param name="note">The note.</param>
        protected internal StaffProfile(
            PersonName staffName,
            Gender gender,
            DateTime? birthDate,
            string socialSecurityNumber,
            StaffType staffType,
            string professionalCredentialNote,
            EmailAddress emailAddress,
            DateRange employmentDateRange,
            string note)
        {
            Check.IsNotNull(staffName, () => StaffName);

            StaffName = staffName;
            Gender = gender;
            BirthDate = birthDate;
            SocialSecurityNumber = socialSecurityNumber;
            StaffType = staffType;
            ProfessionalCredentialNote = professionalCredentialNote;
            EmailAddress = emailAddress;
            EmploymentDateRange = employmentDateRange;
            Note = note;
        }

        /// <summary>
        /// Gets the name of the staff.
        /// </summary>
        /// <value>
        /// The name of the staff.
        /// </value>
        [NotNull]
        public virtual PersonName StaffName { get; private set; }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        public virtual Gender Gender { get; private set; }

        /// <summary>
        /// Gets the birth date.
        /// </summary>
        public virtual DateTime? BirthDate { get; private set; }

        /// <summary>
        /// Gets the social security number.
        /// </summary>
        public virtual string SocialSecurityNumber { get; private set; }

        /// <summary>
        /// Gets the type of the staff.
        /// </summary>
        /// <value>
        /// The type of the staff.
        /// </value>
        public virtual StaffType StaffType { get; private set; }

        /// <summary>
        /// Gets the professional credential note.
        /// </summary>
        public virtual string ProfessionalCredentialNote { get; private set; }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public virtual EmailAddress EmailAddress { get; private set; }

        /// <summary>
        /// Gets the employment date range.
        /// </summary>
        public virtual DateRange EmploymentDateRange { get; private set; }

        /// <summary>
        /// Gets the note.
        /// </summary>
        public virtual string Note { get; private set; }
    }
}