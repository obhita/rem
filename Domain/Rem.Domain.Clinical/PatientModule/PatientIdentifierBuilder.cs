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

using Pillar.Domain.Primitives;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientIdentifierBuilder provides a fluent interface for creating patient identifier.
    /// </summary>
    public class PatientIdentifierBuilder
    {
        private bool? _activeIndicator;
        private string _description;
        private DateRange _effectiveDateRange;
        private string _identifier;
        private PatientContact _patientContact;
        private PatientIdentifierType _patientIdentifierType;

        /// <summary>
        /// Assigns the type of the patient identifier.
        /// </summary>
        /// <param name="patientIdentifierType">Type of the patient identifier.</param>
        /// <returns>A PatientIdentifierBuilder.</returns>
        public PatientIdentifierBuilder WithPatientIdentifierType ( PatientIdentifierType patientIdentifierType )
        {
            _patientIdentifierType = patientIdentifierType;
            return this;
        }

        /// <summary>
        /// Assigns the identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>A PatientIdentifierBuilder.</returns>
        public PatientIdentifierBuilder WithIdentifier ( string identifier )
        {
            _identifier = identifier;
            return this;
        }

        /// <summary>
        /// Assigns the description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>A PatientIdentifierBuilder.</returns>
        public PatientIdentifierBuilder WithDescription ( string description )
        {
            _description = description;
            return this;
        }

        /// <summary>
        /// Assigns the effective date range.
        /// </summary>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <returns>A PatientIdentifierBuilder.</returns>
        public PatientIdentifierBuilder WithEffectiveDateRange ( DateRange effectiveDateRange )
        {
            _effectiveDateRange = effectiveDateRange;
            return this;
        }

        /// <summary>
        /// Assigns the active indicator.
        /// </summary>
        /// <param name="activeIndicator">The active indicator.</param>
        /// <returns>A PatientIdentifierBuilder.</returns>
        public PatientIdentifierBuilder WithActiveIndicator ( bool? activeIndicator )
        {
            _activeIndicator = activeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the patient contact.
        /// </summary>
        /// <param name="patientContact">The patient contact.</param>
        /// <returns>A PatientIdentifierBuilder.</returns>
        public PatientIdentifierBuilder WithPatientContact ( PatientContact patientContact )
        {
            _patientContact = patientContact;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A PatientIdentifier.</returns>
        public PatientIdentifier Build ()
        {
            return new PatientIdentifier (
                _patientIdentifierType,
                _identifier,
                _description,
                _effectiveDateRange,
                _activeIndicator,
                _patientContact );
        }
    }
}