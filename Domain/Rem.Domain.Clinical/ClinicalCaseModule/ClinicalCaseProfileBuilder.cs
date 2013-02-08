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
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// ClinicalCaseProfileBuilder provides a fluent interface for creating a ClinicalCaseProfile.
    /// </summary>
    public class ClinicalCaseProfileBuilder
    {
        private Location _initialLocation;
        private DateTime? _clinicalCaseStartDate;
        private Staff _performedByStaff;
        private string _patientPresentingProblemNote;
        private ReferralType _referralType;
        private InitialContactMethod _initialContactMethod;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ClinicalCaseProfileBuilder"/> to <see cref="ClinicalCaseProfile"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ClinicalCaseProfile(ClinicalCaseProfileBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the initial location.
        /// </summary>
        /// <param name="initialLocation">The initial location.</param>
        /// <returns>A ClinicalCaseProfileBuilder.</returns>
        public ClinicalCaseProfileBuilder WithInitialLocation(Location initialLocation)
        {
            _initialLocation = initialLocation;
            return this;
        }

        /// <summary>
        /// Assigns the clinical case start date.
        /// </summary>
        /// <param name="clinicalCaseStartDate">The clinical case start date.</param>
        /// <returns>A ClinicalCaseProfileBuilder.</returns>
        public ClinicalCaseProfileBuilder WithClinicalCaseStartDate(DateTime? clinicalCaseStartDate)
        {
            _clinicalCaseStartDate = clinicalCaseStartDate;
            return this;
        }

        /// <summary>
        /// Assigns the performed by staff.
        /// </summary>
        /// <param name="performedByStaff">The performed by staff.</param>
        /// <returns>A ClinicalCaseProfileBuilder.</returns>
        public ClinicalCaseProfileBuilder WithPerformedByStaff(Staff performedByStaff)
        {
            _performedByStaff = performedByStaff;
            return this;
        }

        /// <summary>
        /// Assigns the patient presenting problem note.
        /// </summary>
        /// <param name="patientPresentingProblemNote">The patient presenting problem note.</param>
        /// <returns>A ClinicalCaseProfileBuilder.</returns>
        public ClinicalCaseProfileBuilder WithPatientPresentingProblemNote(string patientPresentingProblemNote)
        {
            _patientPresentingProblemNote = patientPresentingProblemNote;
            return this;
        }

        /// <summary>
        /// Assigns the type of the referral.
        /// </summary>
        /// <param name="referralType">Type of the referral.</param>
        /// <returns>A ClinicalCaseProfileBuilder.</returns>
        public ClinicalCaseProfileBuilder WithReferralType(ReferralType referralType)
        {
            _referralType = referralType;
            return this;
        }

        /// <summary>
        /// Assigns the initial contact method.
        /// </summary>
        /// <param name="initialContactMethod">The initial contact method.</param>
        /// <returns>A ClinicalCaseProfileBuilder.</returns>
        public ClinicalCaseProfileBuilder WithInitialContactMethod(InitialContactMethod initialContactMethod)
        {
            _initialContactMethod = initialContactMethod;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A ClinicalCaseProfile.</returns>
        public ClinicalCaseProfile Build()
        {
            return new ClinicalCaseProfile(
                _initialLocation,
                _clinicalCaseStartDate,
                _performedByStaff,
                _patientPresentingProblemNote,
                _referralType,
                _initialContactMethod);
        }
    }
}