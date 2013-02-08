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
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// The ClinicalCaseSignedComment defines a staff signed clinical case comment.
    /// </summary>
    public class ClinicalCaseSignedComment : ClinicalCaseAggregateNodeBase, IAggregateNodeValueObject
    {
        private readonly string _signedNote;
        private readonly DateTimeOffset _signedTimestamp;
        private readonly Staff _staff;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseSignedComment"/> class.
        /// </summary>
        protected ClinicalCaseSignedComment()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseSignedComment"/> class.
        /// </summary>
        /// <param name="staff">The staff.</param>
        /// <param name="signedTimestamp">The signed timestamp.</param>
        /// <param name="signedNote">The signed note.</param>
        public ClinicalCaseSignedComment (
            Staff staff,
            DateTimeOffset signedTimestamp,
            string signedNote )
        {
            Check.IsNotNull ( staff, "Staff is required." );
            Check.IsNotNull ( signedTimestamp, "Signed timestamp is required." );
            Check.IsNotNullOrWhitespace ( signedNote, "Signed note is required." );

            _staff = staff;
            _signedTimestamp = signedTimestamp;
            _signedNote = signedNote;
        }

        /// <summary>
        /// Gets the staff.
        /// </summary>
        [NotNull]
        public virtual Staff Staff
        {
            get { return _staff; }
            private set { }
        }

        /// <summary>
        /// Gets the signed timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset SignedTimestamp
        {
            get { return _signedTimestamp; }
            private set { }
        }

        /// <summary>
        /// Gets the signed note.
        /// </summary>
        [NotNull]
        public virtual string SignedNote
        {
            get { return _signedNote; }
            private set { }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format ( "{0} - (Signed by: '{1}' on '{2}')", SignedNote, Staff, SignedTimestamp  );
        }
    }
}