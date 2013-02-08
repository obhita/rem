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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientProfileBuilder provides a fluent interface for creating patient profile information.
    /// </summary>
    public class PatientProfileBuilder
    {
        private PatientGender _patientGender;
        private DateTime? _birthDate;
        private DateTime? _deathDate;
        private ContactPreference _contactPreference;
        private EmailAddress _emailAddress;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PatientProfileBuilder"/> to <see cref="PatientProfile"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator PatientProfile(PatientProfileBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the patient gender.
        /// </summary>
        /// <param name="patientGender">The patient gender.</param>
        /// <returns>A PatientProfileBuilder.</returns>
        public PatientProfileBuilder WithPatientGender ( PatientGender patientGender )
        {
            _patientGender = patientGender;
            return this;
        }

        /// <summary>
        /// Assigns the birth date.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        /// <returns>A PatientProfileBuilder.</returns>
        public PatientProfileBuilder WithBirthDate ( DateTime? birthDate )
        {
            _birthDate = birthDate;
            return this;
        }

        /// <summary>
        /// Assigns the death date.
        /// </summary>
        /// <param name="deathDate">The death date.</param>
        /// <returns>A PatientProfileBuilder.</returns>
        public PatientProfileBuilder WithDeathDate ( DateTime? deathDate )
        {
            _deathDate = deathDate;
            return this;
        }

        /// <summary>
        /// Assigns the contact preference.
        /// </summary>
        /// <param name="contactPreference">The contact preference.</param>
        /// <returns>A PatientProfileBuilder.</returns>
        public PatientProfileBuilder WithContactPreference ( ContactPreference contactPreference )
        {
            _contactPreference = contactPreference;
            return this;
        }

        /// <summary>
        /// Assigns the email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>A PatientProfileBuilder.</returns>
        public PatientProfileBuilder WithEmailAddress ( EmailAddress emailAddress )
        {
            _emailAddress = emailAddress;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A PatientProfile.</returns>
        public PatientProfile Build()
        {
            return new PatientProfile ( 
                _patientGender,
                _birthDate,
                _deathDate,
                _contactPreference,
                _emailAddress );
        }
    }
}
