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
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// StaffProfileBuilder provides a fluent interface for creating a StaffProfile.
    /// </summary>
    public class StaffProfileBuilder
    {
        private DateTime? _birthDate;
        private EmailAddress _emailAddress;
        private DateRange _employmentDateRange;
        private Gender _gender;
        private string _note;
        private string _professionalCredentialNote;
        private string _socialSecurityNumber;
        private PersonName _staffName;
        private StaffType _staffType;

        /// <summary>
        /// Performs an implicit conversion from <see cref="StaffProfileBuilder"/> to <see cref="StaffProfile"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator StaffProfile(StaffProfileBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the name of the staff.
        /// </summary>
        /// <param name="staffName">Name of the staff.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithStaffName(PersonName staffName)
        {
            _staffName = staffName;
            return this;
        }

        /// <summary>
        /// Assigns the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithGender(Gender gender)
        {
            _gender = gender;
            return this;
        }

        /// <summary>
        /// Assigns the birth date.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithBirthDate(DateTime? birthDate)
        {
            _birthDate = birthDate;
            return this;
        }

        /// <summary>
        /// Assigns the social security number.
        /// </summary>
        /// <param name="socialSecurityNumber">The social security number.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithSocialSecurityNumber(string socialSecurityNumber)
        {
            _socialSecurityNumber = socialSecurityNumber;
            return this;
        }

        /// <summary>
        /// Assigns the type of the staff.
        /// </summary>
        /// <param name="staffType">Type of the staff.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithStaffType(StaffType staffType)
        {
            _staffType = staffType;
            return this;
        }

        /// <summary>
        /// Assigns the professional credential note.
        /// </summary>
        /// <param name="professionalCredentialNote">The professional credential note.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithProfessionalCredentialNote(string professionalCredentialNote)
        {
            _professionalCredentialNote = professionalCredentialNote;
            return this;
        }

        /// <summary>
        /// Assigns the email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithEmailAddress(EmailAddress emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        /// <summary>
        /// Assigns the employment date range.
        /// </summary>
        /// <param name="employmentDateRange">The employment date range.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithEmploymentDateRange(DateRange employmentDateRange)
        {
            _employmentDateRange = employmentDateRange;
            return this;
        }

        /// <summary>
        /// Assigns the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>A StaffProfileBuilder.</returns>
        public StaffProfileBuilder WithNote(string note)
        {
            _note = note;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A StaffProfile.</returns>
        public StaffProfile Build()
        {
            return new StaffProfile(
                _staffName,
                _gender,
                _birthDate,
                _socialSecurityNumber,
                _staffType,
                _professionalCredentialNote,
                _emailAddress,
                _employmentDateRange,
                _note);
        }
    }
}