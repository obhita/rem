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

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// AgencyContactBuilder provides a fluent interface for creating a AgencyContact.
    /// </summary>
    public class AgencyContactBuilder
    {
        private AgencyContactType _agencyContactType;
        private Staff _contactStaff;
        private DateTime? _effectiveStartDate;
        private bool _statusIndicator;
        private bool _alternativeContactIndicator;

        /// <summary>
        /// Performs an implicit conversion from <see cref="AgencyContactBuilder"/> to <see cref="AgencyContact"/>.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator AgencyContact(AgencyContactBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the type of the agency contact.
        /// </summary>
        /// <param name="agencyContactType">
        /// Type of the agency contact.
        /// </param>
        /// <returns>
        /// An AgencyContact.
        /// </returns>
        public AgencyContactBuilder WithAgencyContactType(AgencyContactType agencyContactType)
        {
            _agencyContactType = agencyContactType;
            return this;
        }

        /// <summary>
        /// Assigns the contact staff.
        /// </summary>
        /// <param name="contactStaff">
        /// The contact staff.
        /// </param>
        /// <returns>
        /// An AgencyContact.
        /// </returns>
        public AgencyContactBuilder WithContactStaff(Staff contactStaff)
        {
            _contactStaff = contactStaff;
            return this;
        }

        /// <summary>
        /// Assigns the effective start date.
        /// </summary>
        /// <param name="effectiveStartDate">
        /// The effective start date.
        /// </param>
        /// <returns>
        /// An AgencyContact.
        /// </returns>
        public AgencyContactBuilder WithEffectiveStartDate(DateTime? effectiveStartDate)
        {
            _effectiveStartDate = effectiveStartDate;
            return this;
        }

        /// <summary>
        /// Assigns the status indicator.
        /// </summary>
        /// <param name="statusIndicator">
        /// If set to <c>true</c> [status indicator].
        /// </param>
        /// <returns>
        /// An AgencyContact.
        /// </returns>
        public AgencyContactBuilder WithStatusIndicator(bool statusIndicator)
        {
            _statusIndicator = statusIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the alternative contact indicator.
        /// </summary>
        /// <param name="alternativeContactIndicator">
        /// If set to <c>true</c> [alternative contact indicator].
        /// </param>
        /// <returns>
        /// An AgencyContact.
        /// </returns>
        public AgencyContactBuilder WithAlternativeContactIndicator(bool alternativeContactIndicator)
        {
            _alternativeContactIndicator = alternativeContactIndicator;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An AgencyContact.
        /// </returns>
        public AgencyContact Build()
        {
            return new AgencyContact(_agencyContactType, _contactStaff, _effectiveStartDate, _statusIndicator, _alternativeContactIndicator);
        }
    }
}