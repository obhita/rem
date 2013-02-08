﻿#region License
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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientSpecialNeed defines the personalized needs of the patient.
    /// </summary>
    public class PatientSpecialNeed : PatientAggregateNodeBase, IAggregateNodeValueObject
    {
        #region Private Memebers

        private SpecialNeed _specialNeed;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientSpecialNeed"/> class.
        /// </summary>
        protected PatientSpecialNeed ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientSpecialNeed"/> class.
        /// </summary>
        /// <param name="specialNeed">The special need.</param>
        public PatientSpecialNeed ( SpecialNeed specialNeed )
        {
            Check.IsNotNull ( specialNeed, "Special need is required." );

            _specialNeed = specialNeed;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the special need.
        /// </summary>
        [NotNull]
        public virtual SpecialNeed SpecialNeed
        {
            get { return _specialNeed; }
            private set { ApplyPropertyChange ( ref _specialNeed, () => SpecialNeed, value ); }
        }

        #endregion
            
        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="patientSpecialNeed">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>         
        public virtual bool ValuesEqual ( PatientSpecialNeed patientSpecialNeed )
        {
            if (patientSpecialNeed == null)
            {
                return false;
            }

            return Equals ( SpecialNeed, patientSpecialNeed.SpecialNeed );
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return SpecialNeed.ToString ();
        }
    }
}