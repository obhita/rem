﻿#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.RadiologyModule
{
    /// <summary>
    /// RadiologyOrder defines a request for radiology services.
    /// </summary>
    public class RadiologyOrder : Activity
    {
        private RadiologyTestType _radiologyTestType;
        private string _note;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadiologyOrder"/> class.
        /// </summary>
        protected internal RadiologyOrder ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadiologyOrder"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal RadiologyOrder(
            Visit visit,
            ActivityType activityType )
            : base(visit, activityType)
        {
        }

        /// <summary>
        /// Gets the type of the radiology test.
        /// </summary>
        /// <value>
        /// The type of the radiology test.
        /// </value>
        public virtual RadiologyTestType RadiologyTestType
        {
            get { return _radiologyTestType; }
            private set
            {
                ApplyPropertyChange ( ref _radiologyTestType, () => RadiologyTestType, value );
            }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        public virtual string Note
        {
            get { return _note; }
            private set { ApplyPropertyChange ( ref _note, () => Note, value ); }
        }

        /// <summary>
        /// Revises the type of the radiology test.
        /// </summary>
        /// <param name="radiologyTestType">Type of the radiology test.</param>
        public virtual void ReviseRadiologyTestType(RadiologyTestType radiologyTestType)
        {
            RadiologyTestType = radiologyTestType;
        }

        /// <summary>
        /// Revises the note.
        /// </summary>
        /// <param name="note">The note.</param>
        public virtual void ReviseNote(string note)
        {
            Note = note;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _radiologyTestType == null ? base.ToString () : _radiologyTestType.ToString ();
        }
    }
}
