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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientConfidentialInfoBuilder provides a fluent interface for creating a patient confidential information.
    /// </summary>
    public class PatientConfidentialInfoBuilder
    {
        private string _confidentialFamilyInformationDescription;
        private bool? _sexualAbuseVictimIndicator;
        private bool? _physicalAbuseVictimIndicator;
        private bool? _domesticAbuseVictimIndicator;
        private DateTime? _registeredSexOffenderDate;
        private bool? _registeredSexOffenderIndicator;
        private DateTime? _convictedOfArsonDate;
        private bool? _convictedOfArsonIndicator;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PatientConfidentialInfoBuilder"/> to <see cref="PatientConfidentialInfo"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator PatientConfidentialInfo(PatientConfidentialInfoBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the confidential family information description.
        /// </summary>
        /// <param name="confidentialFamilyInformationDescription">The confidential family information description.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithConfidentialFamilyInformationDescription(string confidentialFamilyInformationDescription)
        {
            _confidentialFamilyInformationDescription = confidentialFamilyInformationDescription;
            return this;
        }

        /// <summary>
        /// Assigns the sexual abuse victim indicator.
        /// </summary>
        /// <param name="sexualAbuseVictimIndicator">The sexual abuse victim indicator.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithSexualAbuseVictimIndicator(bool? sexualAbuseVictimIndicator)
        {
            _sexualAbuseVictimIndicator = sexualAbuseVictimIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the physical abuse victim indicator.
        /// </summary>
        /// <param name="physicalAbuseVictimIndicator">The physical abuse victim indicator.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithPhysicalAbuseVictimIndicator(bool? physicalAbuseVictimIndicator)
        {
            _physicalAbuseVictimIndicator = physicalAbuseVictimIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the domestic abuse victim indicator.
        /// </summary>
        /// <param name="domesticAbuseVictimIndicator">The domestic abuse victim indicator.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithDomesticAbuseVictimIndicator(bool? domesticAbuseVictimIndicator)
        {
            _domesticAbuseVictimIndicator = domesticAbuseVictimIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the registered sex offender date.
        /// </summary>
        /// <param name="registeredSexOffenderDate">The registered sex offender date.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithRegisteredSexOffenderDate(DateTime? registeredSexOffenderDate)
        {
            _registeredSexOffenderDate = registeredSexOffenderDate;
            return this;
        }

        /// <summary>
        /// Assigns the registered sex offender indicator.
        /// </summary>
        /// <param name="registeredSexOffenderIndicator">The registered sex offender indicator.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithRegisteredSexOffenderIndicator(bool? registeredSexOffenderIndicator)
        {
            _registeredSexOffenderIndicator = registeredSexOffenderIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the convicted of arson date.
        /// </summary>
        /// <param name="convictedOfArsonDate">The convicted of arson date.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithConvictedOfArsonDate(DateTime? convictedOfArsonDate)
        {
            _convictedOfArsonDate = convictedOfArsonDate;
            return this;
        }

        /// <summary>
        /// Assigns the convicted of arson indicator.
        /// </summary>
        /// <param name="convictedOfArsonIndicator">The convicted of arson indicator.</param>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfoBuilder WithConvictedOfArsonIndicator(bool? convictedOfArsonIndicator)
        {
            _convictedOfArsonIndicator = convictedOfArsonIndicator;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A PatientConfidentialInfoBuilder.</returns>
        public PatientConfidentialInfo Build()
        {
            return new PatientConfidentialInfo(_confidentialFamilyInformationDescription,
                                               _sexualAbuseVictimIndicator,
                                               _physicalAbuseVictimIndicator,
                                               _domesticAbuseVictimIndicator,
                                               _registeredSexOffenderDate,
                                               _registeredSexOffenderIndicator,
                                               _convictedOfArsonDate,
                                               _convictedOfArsonIndicator);
        }
    }
}
