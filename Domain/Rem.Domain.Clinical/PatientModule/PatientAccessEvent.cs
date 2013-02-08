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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PatientAccessEvent defines an event where access to system resources was made on the behalf of a patient.
    /// </summary>
    public class PatientAccessEvent : AuditableAggregateRootBase
    {
        private readonly Patient _patient;
        private readonly PatientAccessEventType _patientAccessEventType;
        private string _note;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccessEvent"/> class.
        /// </summary>
        protected PatientAccessEvent ( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccessEvent"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="patientAccessEventType">Type of the patient access event.</param>
        /// <param name="auditedContextDescription">The audited context description.</param>
        /// <param name="note">The note.</param>
        public PatientAccessEvent (
            Patient patient,
            PatientAccessEventType patientAccessEventType,
            string auditedContextDescription,
            string note )
        {
            Check.IsNotNull ( patient, "Patient is required." );
            Check.IsNotNull ( patientAccessEventType, "Patient access event is required." );
            Check.IsNotNull(auditedContextDescription, "Audited context description is required.");
            Check.IsNotNullOrWhitespace ( note, "Note is required." );

            _patient = patient;
            AuditedContextDescription = auditedContextDescription;
            _patientAccessEventType = patientAccessEventType;
            _note = note;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        [NotNull]
        public virtual Patient Patient
        {
            get { return _patient; }
            private set { }
        }

        /// <summary>
        /// Gets the type of the patient access event.
        /// </summary>
        [NotNull]
        public virtual PatientAccessEventType PatientAccessEventType
        {
            get { return _patientAccessEventType; }
            private set { }
        }

        /// <summary>
        /// Gets the audited context description.
        /// </summary>
        [NotNull]
        public virtual string AuditedContextDescription { get; private set; }

        /// <summary>
        /// Gets the note.
        /// </summary>
        [NotNull]
        public virtual string Note
        {
            get { return _note; }
            private set {  }
        }

        /// <summary>
        /// Gets the type of the aggregate root.
        /// </summary>
        /// <value>
        /// The type of the aggregate root.
        /// </value>
        public virtual string AggregateRootTypeName { get; set; }

        /// <summary>
        /// Gets the aggregate root key.
        /// </summary>
        public virtual long? AggregateRootKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the aggregate node.
        /// </summary>
        /// <value>
        /// The type of the aggregate node.
        /// </value>
        public virtual string AggregateNodeTypeName { get; set; }

        /// <summary>
        /// Gets or sets the aggregate node key.
        /// </summary>
        /// <value>
        /// The aggregate node key.
        /// </value>
        public virtual long? AggregateNodeKey { get; set; }

        /// <summary>
        /// Appends the note.
        /// </summary>
        /// <param name="note">The note.</param>
        public virtual void AppendNote ( string note )
        {
            Check.IsNotNullOrWhitespace ( note, "Note cannot be null." );
            _note += Environment.NewLine + note;
        }
    }
}