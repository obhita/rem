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

namespace Rem.Domain.Clinical.ProgramModule
{
    /// <summary>
    /// ProgramCharacteristics defines elements of program. 
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof ( TypeNamePlusPropertyNameAsColumnNameNamingStrategy ) )]
    public class ProgramCharacteristics : IEquatable<ProgramCharacteristics>
    {
        private readonly AgeGroup _ageGroup;
        private readonly GenderSpecification _genderSpecification;
        private readonly TreatmentApproach _treatmentApproach;
        private readonly ProgramCategory _programCategory;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramCharacteristics"/> class.
        /// </summary>
        /// <param name="ageGroup">The age group.</param>
        /// <param name="genderSpecification">The gender specification.</param>
        /// <param name="treatmentApproach">The treatment approach.</param>
        /// <param name="programCategory">The program category.</param>
        public ProgramCharacteristics ( AgeGroup ageGroup,
                                        GenderSpecification genderSpecification,
                                        TreatmentApproach treatmentApproach,
                                        ProgramCategory programCategory )
            : this ()
        {
            Check.IsNotNull ( ageGroup, "Age Group is required." );
            Check.IsNotNull ( genderSpecification, "Gender Specification is required." );
            Check.IsNotNull ( treatmentApproach, "Treatment Approach is required." );
            Check.IsNotNull ( programCategory, "Program Category is required." );

            _ageGroup = ageGroup;
            _genderSpecification = genderSpecification;
            _treatmentApproach = treatmentApproach;
            _programCategory = programCategory;
        }

        private ProgramCharacteristics ()
        {
        }

        #endregion

        /// <summary>
        /// Gets the age group.
        /// </summary>
        [NotNull]
        public virtual AgeGroup AgeGroup
        {
            get { return _ageGroup; }
            private set { }
        }

        /// <summary>
        /// Gets the gender specification.
        /// </summary>
        [NotNull]
        public virtual GenderSpecification GenderSpecification
        {
            get { return _genderSpecification; }
            private set { }
        }

        /// <summary>
        /// Gets the treatment approach.
        /// </summary>
        [NotNull]
        public virtual TreatmentApproach TreatmentApproach
        {
            get { return _treatmentApproach; }
            private set { }
        }

        /// <summary>
        /// Gets the program category.
        /// </summary>
        [NotNull]
        public virtual ProgramCategory ProgramCategory
        {
            get { return _programCategory; }
            private set { }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(ProgramCharacteristics left, ProgramCharacteristics right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(ProgramCharacteristics left, ProgramCharacteristics right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof ( ProgramCharacteristics ) )
            {
                return false;
            }
            return Equals ( ( ProgramCharacteristics ) obj );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals ( ProgramCharacteristics other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other._ageGroup, _ageGroup ) && Equals ( other._genderSpecification, _genderSpecification ) && Equals ( other._treatmentApproach, _treatmentApproach ) && Equals ( other._programCategory, _programCategory );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode ()
        {
            unchecked
            {
                int result = ( _ageGroup != null ? _ageGroup.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _genderSpecification != null ? _genderSpecification.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _treatmentApproach != null ? _treatmentApproach.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _programCategory != null ? _programCategory.GetHashCode () : 0 );
                return result;
            }
        }
    }
}