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

using Pillar.Domain;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientContactContactType defines a patient contact association type.
    /// </summary>
    public class PatientContactContactType : PatientContactAggregateNodeBase
    {
        #region Private members

        private PatientContactType _patientContactType;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContactContactType"/> class.
        /// </summary>
        protected internal PatientContactContactType ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContactContactType"/> class.
        /// </summary>
        /// <param name="patientContact">The patient contact.</param>
        /// <param name="contactType">Type of the contact.</param>
        protected internal PatientContactContactType ( PatientContact patientContact, PatientContactType contactType ) : base(patientContact)
        {
            PatientContactType = contactType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the patient contact.
        /// </summary>
        /// <value>
        /// The type of the patient contact.
        /// </value>
        [NotNull]
        public virtual PatientContactType PatientContactType
        {
            get { return _patientContactType; }
            private set { _patientContactType = value; }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return PatientContactType == null ? Key.ToString () : PatientContactType.ToString ();
        }
    }
}