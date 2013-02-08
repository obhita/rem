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

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// CodedConcept is a value object that provides capabilities of a coded concept in a vocabulary/terminology system.
    /// </summary>
    [Component]
    public class CodedConcept : IEquatable<CodedConcept>
    {      
        #region Fields

        private readonly string _codedConceptCode;
        private readonly string _codeSystemIdentifier;
        private readonly string _codeSystemName;
        private readonly string _codeSystemVersionNumber;
        private readonly string _displayName;
        private readonly bool _nullFlavorIndicator;
        private readonly string _originalDescription;

        #endregion

        #region Constructors

        private CodedConcept()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CodedConcept"/> class.
        /// </summary>
        /// <param name="codedConceptCode">The coded concept code.</param>
        /// <param name="codeSystemIdentifier">The code system identifier.</param>
        /// <param name="codeSystemName">Name of the code system.</param>
        /// <param name="codeSystemVersionNumber">The code system version number.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="nullFlavorIndicator">If set to <c>true</c> [null flavor indicator].</param>
        /// <param name="originalDescription">The original description.</param>
        public CodedConcept(string codedConceptCode,
            string codeSystemIdentifier,
            string codeSystemName,
            string codeSystemVersionNumber,
            string displayName,
            bool nullFlavorIndicator,
            string originalDescription)
        {
            Check.IsNotNullOrWhitespace(codedConceptCode, "Coded concept code is required.");
            _codedConceptCode = codedConceptCode;
            _codeSystemIdentifier = codeSystemIdentifier;
            _codeSystemName = codeSystemName;
            _codeSystemVersionNumber = codeSystemVersionNumber;
            _displayName = displayName;
            _nullFlavorIndicator = nullFlavorIndicator;
            _originalDescription = originalDescription;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the coded concept code.
        /// </summary>
        [NotNull]
        public string CodedConceptCode
        {
            get { return _codedConceptCode; }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get { return _displayName; }
        }

        /// <summary>
        /// Gets the code system identifier. 
        /// </summary>
        /// <value>
        /// The code system identifier.
        /// </value>
        [ColumnLength(50)]
        public string CodeSystemIdentifier
        {
            get { return _codeSystemIdentifier; }
        }

        /// <summary>
        /// Gets the code system version number.
        /// </summary>
        /// <value>
        /// The code system version number.
        /// </value>
        public string CodeSystemVersionNumber
        {
            get { return _codeSystemVersionNumber; }
        }

        /// <summary>
        /// Gets the name of the code system.
        /// </summary>
        /// <value>
        /// The name of the code system.
        /// </value>
        public string CodeSystemName
        {
            get { return _codeSystemName; }
        }

        /// <summary>
        /// Gets the original description.
        /// </summary>
        /// <value>
        /// The original description.
        /// </value>
        public string OriginalDescription
        {
            get { return _originalDescription; }
        }

        /// <summary>
        /// Gets a value indicating whether [null flavor indicator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [null flavor indicator]; otherwise, <c>false</c>.
        /// </value>
        public bool NullFlavorIndicator
        {
            get { return _nullFlavorIndicator; }
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof( CodedConcept ) )
            {
                return false;
            }
            return Equals ( ( CodedConcept )obj );
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _displayName;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( CodedConcept other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other._codeSystemIdentifier, _codeSystemIdentifier ) && Equals ( other._codeSystemName, _codeSystemName ) && Equals ( other._codeSystemVersionNumber, _codeSystemVersionNumber ) && Equals ( other._codedConceptCode, _codedConceptCode ) && Equals ( other._displayName, _displayName ) && other._nullFlavorIndicator.Equals ( _nullFlavorIndicator ) && Equals ( other._originalDescription, _originalDescription );
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode ()
        {
            unchecked
            {
                int result = ( _codeSystemIdentifier != null ? _codeSystemIdentifier.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _codeSystemName != null ? _codeSystemName.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _codeSystemVersionNumber != null ? _codeSystemVersionNumber.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _codedConceptCode != null ? _codedConceptCode.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _displayName != null ? _displayName.GetHashCode () : 0 );
                result = ( result * 397 ) ^ _nullFlavorIndicator.GetHashCode ();
                result = ( result * 397 ) ^ ( _originalDescription != null ? _originalDescription.GetHashCode () : 0 );
                return result;
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == ( CodedConcept left, CodedConcept right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator != ( CodedConcept left, CodedConcept right )
        {
            return !Equals ( left, right );
        }
    }
}