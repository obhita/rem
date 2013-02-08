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

using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientAlert defines a notification for a patient.
    /// </summary>
    public class PatientAlert : AuditableAggregateRootBase, IPatientAccessAuditable
    {
        private Patient _patient;
        private string _name;
        private string _note;
        private string _cdsIdentifier;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAlert"/> class.
        /// </summary>
        protected PatientAlert ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAlert"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="note">The note.</param>
        /// <param name="cdsIdentifier">The CDS identifier.</param>
        protected internal PatientAlert(Patient patient, string name, string note, string cdsIdentifier)
        {
            Check.IsNotNull ( patient, () => Patient );
            Check.IsNotNull ( name, () => Name );
            Check.IsNotNullOrWhitespace ( note, () => Note );
            Check.IsNotNullOrWhitespace ( cdsIdentifier, () => CdsIdentifier );

            _patient = patient;
            _name = name;
            _note = note;
            _cdsIdentifier = cdsIdentifier;
        }

        #endregion

        /// <summary>
        /// Gets the patient.
        /// </summary>
        [NotNull]
        public virtual Patient Patient
        {
            get { return _patient; }
            private set { ApplyPropertyChange(ref _patient, () => Patient, value); }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange(ref _name, () => Name, value); }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        [NotNull]
        public virtual string Note
        {
            get { return _note; }
            private set { ApplyPropertyChange(ref _note, () => Note, value); }
        }

        /// <summary>
        /// Gets the CDS identifier.
        /// </summary>
        [NotNull]
        public virtual string CdsIdentifier
        {
            get { return _cdsIdentifier; }
            private set { ApplyPropertyChange(ref _cdsIdentifier, () => CdsIdentifier, value); }
        }

        /// <summary>
        /// Gets the audited patient.
        /// </summary>
        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return Patient; }
        }

        /// <summary>
        /// Gets the audited context description.
        /// </summary>
        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _name;
        }
    }
}
