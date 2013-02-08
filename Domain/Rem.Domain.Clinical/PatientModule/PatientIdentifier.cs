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

using Pillar.Common;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientIdentifier defines a patient identification.
    /// </summary>
    public class PatientIdentifier : PatientAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        private bool? _activeIndicator;
        private string _description;
        private DateRange _effectiveDateRange;
        private string _identifier;
        private PatientContact _patientContact;
        private PatientIdentifierType _patientIdentifierType;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientIdentifier"/> class.
        /// </summary>
        protected PatientIdentifier ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientIdentifier"/> class.
        /// </summary>
        /// <param name="patientIdentifierType">Type of the patient identifier.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <param name="activeIndicator">The active indicator.</param>
        /// <param name="patientContact">The patient contact.</param>
        public PatientIdentifier (
            PatientIdentifierType patientIdentifierType,
            string identifier,
            string description,
            DateRange effectiveDateRange,
            bool? activeIndicator,
            PatientContact patientContact )
        {
            Check.IsNotNull ( patientIdentifierType, "Patient identifier type is required." );
            Check.IsNotNullOrWhitespace ( identifier, "Identifier is required." );

            _patientIdentifierType = patientIdentifierType;
            _identifier = identifier;
            _description = description;
            _effectiveDateRange = effectiveDateRange;
            _activeIndicator = activeIndicator;
            _patientContact = patientContact;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type of the patient identifier.
        /// </summary>
        /// <value>
        /// The type of the patient identifier.
        /// </value>
        [NotNull]
        public virtual PatientIdentifierType PatientIdentifierType
        {
            get { return _patientIdentifierType; }
            private set { ApplyPropertyChange ( ref _patientIdentifierType, () => PatientIdentifierType, value ); }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [NotNull]
        public virtual string Identifier
        {
            get { return _identifier; }
            private set { ApplyPropertyChange ( ref _identifier, () => Identifier, value ); }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public virtual string Description
        {
            get { return _description; }
            private set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }

        /// <summary>
        /// Gets the effective date range.
        /// </summary>
        public virtual DateRange EffectiveDateRange
        {
            get { return _effectiveDateRange; }
            private set { ApplyPropertyChange ( ref _effectiveDateRange, () => EffectiveDateRange, value ); }
        }

        /// <summary>
        /// Gets the active indicator.
        /// </summary>
        public virtual bool? ActiveIndicator
        {
            get { return _activeIndicator; }
            private set { ApplyPropertyChange ( ref _activeIndicator, () => ActiveIndicator, value ); }
        }

        /// <summary>
        /// Gets the patient contact.
        /// </summary>
        public virtual PatientContact PatientContact
        {
            get { return _patientContact; }
            private set { ApplyPropertyChange ( ref _patientContact, () => PatientContact, value ); }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="patientIdentifier">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>              
        public virtual bool ValuesEqual ( PatientIdentifier patientIdentifier )
        {
            if (patientIdentifier == null)
            {
                return false;
            }

            var valuesEqual =
                Equals ( PatientIdentifierType, patientIdentifier.PatientIdentifierType ) &&
                Equals ( Identifier, patientIdentifier.Identifier ) &&
                Equals ( Description, patientIdentifier.Description ) &&
                Equals ( EffectiveDateRange, patientIdentifier.EffectiveDateRange ) &&
                Equals ( ActiveIndicator, patientIdentifier.ActiveIndicator ) &&
                Equals ( PatientContact, patientIdentifier.PatientContact );
            return valuesEqual;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return Identifier;
        }

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="obj">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>               
        public virtual bool ValuesEqual(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return ValuesEqual(obj as PatientIdentifier);
        }
    }
}
