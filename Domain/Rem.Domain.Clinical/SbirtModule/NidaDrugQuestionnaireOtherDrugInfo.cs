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

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// The NidaDrugQuestionnaireOtherDrugInfo contains information for patient other drug use.
    /// </summary>
    [Component]
    public class NidaDrugQuestionnaireOtherDrugInfo : IEquatable<NidaDrugQuestionnaireOtherDrugInfo>
    {
        private NidaDrugQuestionnaireOtherDrugInfo ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaireOtherDrugInfo"/> class.
        /// </summary>
        /// <param name="drugTypeName">Name of the drug type.</param>
        /// <param name="answerNumber">The answer number.</param>
        public NidaDrugQuestionnaireOtherDrugInfo(string drugTypeName, int? answerNumber)
        {
            if (string.IsNullOrWhiteSpace( drugTypeName ) && answerNumber.HasValue)
            {
                Check.IsNotNull(drugTypeName, "Other Drug Type name is required, when an Answer is provided for the same.");
            }

            Check.IsInRange(answerNumber, 0, 4, () => AnswerNumber);

            DrugTypeName = drugTypeName;
            AnswerNumber = answerNumber;
        }

        /// <summary>
        /// Other drug type name
        /// </summary>
        public virtual string DrugTypeName { get; private set; }

        /// <summary>
        /// Other drug type answer
        /// </summary>
        public virtual int? AnswerNumber { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( NidaDrugQuestionnaireOtherDrugInfo other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.DrugTypeName, DrugTypeName ) && other.AnswerNumber.Equals ( AnswerNumber );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        /// <filterpriority>2</filterpriority>
        /// <returns> True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.</returns>
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
            if ( obj.GetType () != typeof ( NidaDrugQuestionnaireOtherDrugInfo ) )
            {
                return false;
            }
            return Equals ( ( NidaDrugQuestionnaireOtherDrugInfo ) obj );
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
                return ( ( DrugTypeName != null ? DrugTypeName.GetHashCode () : 0 ) * 397 ) ^ ( AnswerNumber.HasValue ? AnswerNumber.Value : 0 );
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return DrugTypeName == null && AnswerNumber == null ? DrugTypeName + " Score: " + AnswerNumber : base.ToString();
        }
    }
}
