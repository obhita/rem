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

using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// An AllergyReaction describes an action elicited by a stimulus to an allergen.
    /// </summary>
    public class AllergyReaction : AuditableAggregateNodeBase, IAggregateNodeValueObject
    {
        private readonly Reaction _reaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllergyReaction"/> class.
        /// </summary>
        protected internal AllergyReaction ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllergyReaction"/> class.
        /// </summary>
        /// <param name="reaction">The reaction.</param>
        protected internal AllergyReaction ( Reaction reaction )
        {
            Check.IsNotNull ( reaction, "reaction is required." );
            _reaction = reaction;
        }

        /// <summary>
        /// Gets or sets the allergy.
        /// </summary>
        /// <value>
        /// The allergy.
        /// </value>
        [NotNull]
        public virtual Allergy Allergy { get; protected internal set; }

        /// <summary>
        /// Gets the reaction.
        /// </summary>
        [NotNull]
        public virtual Reaction Reaction
        {
            get { return _reaction; }
            private set { }
        }

        #region IAggregateNode interface implementation

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return Allergy; }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="allergyReaction">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>         
        public virtual bool ValuesEqual ( AllergyReaction allergyReaction )
        {
            if (allergyReaction == null)
            {
                return false;
            }

            var valuesEqual =
                Equals ( Allergy.Key, allergyReaction.Key ) &&
                Equals ( Reaction.Key, allergyReaction.Key );

            return valuesEqual;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _reaction.ToString ();
        }
    }
}