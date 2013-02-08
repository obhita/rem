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
using Pillar.Common;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientAlias defines a pseudonym for a patient.
    /// </summary>
    public class PatientAlias : PatientAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private PatientAliasType _patientAliasType;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAlias"/> class.
        /// </summary>
        protected PatientAlias ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAlias"/> class.
        /// </summary>
        /// <param name="patientAliasType">Type of the patient alias.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        public PatientAlias (
            PatientAliasType patientAliasType,
            string firstName,
            string middleName = null,
            string lastName = null )
        {
            Check.IsNotNull ( patientAliasType, "Patient alias type is required." );

            if ( string.IsNullOrWhiteSpace ( firstName ) &&
                 string.IsNullOrWhiteSpace ( middleName ) &&
                 string.IsNullOrWhiteSpace ( lastName ) )
            {
                throw new ArgumentException ( "At least one of first name, middle name, or last name must be set." );
            }

            _patientAliasType = patientAliasType;
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the first name.
        /// </summary>
        public virtual string FirstName
        {
            get { return _firstName; }
            private set { ApplyPropertyChange ( ref _firstName, () => FirstName, value ); }
        }

        /// <summary>
        /// Gets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public virtual string MiddleName
        {
            get { return _middleName; }
            private set { ApplyPropertyChange ( ref _middleName, () => MiddleName, value ); }
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        public virtual string LastName
        {
            get { return _lastName; }
            private set { ApplyPropertyChange ( ref _lastName, () => LastName, value ); }
        }

        /// <summary>
        /// Gets the type of the patient alias.
        /// </summary>
        /// <value>
        /// The type of the patient alias.
        /// </value>
        [NotNull]
        public virtual PatientAliasType PatientAliasType
        {
            get { return _patientAliasType; }
            private set { ApplyPropertyChange ( ref _patientAliasType, () => PatientAliasType, value ); }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="patientAlias">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>              
        public virtual bool ValuesEqual ( PatientAlias patientAlias )
        {
            if (patientAlias == null)
            {
                return false;
            }

            var valuesEqual =
                Equals ( FirstName, patientAlias.FirstName ) &&
                Equals ( MiddleName, patientAlias.MiddleName ) &&
                Equals ( LastName, patientAlias.LastName ) &&
                Equals ( PatientAliasType, patientAlias.PatientAliasType );

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
            return string.Format ( "({0}) - {1} {2} {3}", PatientAliasType, FirstName, MiddleName, LastName ).Trim ();
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

            return ValuesEqual(obj as PatientAlias);
        }
    }
}
