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
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Medication is a substance or preparation used in treating disease.
    /// </summary>
    public class Medication : AuditableAggregateRootBase, IPatientAccessAuditable, IHasProvenance
    {
        private readonly Patient _patient;
        private string _discontinuedByPhysicianName;
        private DiscontinuedReason _discontinuedReason;
        private string _discontinuedReasonOtherDescription;
        private string _frequencyDescription;
        private string _instructionsNote;

        private CodedConcept _medicationCodeCodedConcept;
        private MedicationDoseUnit _medicationDoseUnit;
        private double? _medicationDoseValue;
        private MedicationRoute _medicationRoute;
        private MedicationStatus _medicationStatus;
        private bool? _overTheCounterIndicator;
        private string _prescribingPhysicianName;
        private CodedConcept _rootMedicationCodedConcept;
        private DateRange _usageDateRange;

        private Provenance _provenance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Medication"/> class.
        /// </summary>
        protected internal Medication ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Medication"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="medicationCodeCodedConcept">The medication code coded concept.</param>
        /// <param name="rootMedicationCodedConcept">The root medication coded concept.</param>
        protected internal Medication ( Patient patient, CodedConcept medicationCodeCodedConcept, CodedConcept rootMedicationCodedConcept )
        {
            Check.IsNotNull ( patient, "Patient is required." );
            Check.IsNotNull ( medicationCodeCodedConcept, () => MedicationCodeCodedConcept );
            Check.IsNotNull ( rootMedicationCodedConcept, () => RootMedicationCodedConcept );

            _patient = patient;
            _medicationCodeCodedConcept = medicationCodeCodedConcept;
            _rootMedicationCodedConcept = rootMedicationCodedConcept;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Medication"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="medicationCodeCodedConcept">The medication code coded concept.</param>
        /// <param name="provenance">The provenance.</param>
        protected internal Medication(Patient patient, CodedConcept medicationCodeCodedConcept, Provenance provenance)
        {
            Check.IsNotNull(patient, "Patient is required.");
            Check.IsNotNull(medicationCodeCodedConcept, () => MedicationCodeCodedConcept);
            Check.IsNotNull(provenance, () => Provenance);

            _patient = patient;
            _medicationCodeCodedConcept = medicationCodeCodedConcept;
            _provenance = provenance;
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
        /// Gets the root medication coded concept.
        /// </summary>
        public virtual CodedConcept RootMedicationCodedConcept
        {
            get { return _rootMedicationCodedConcept; }
            private set { ApplyPropertyChange ( ref _rootMedicationCodedConcept, () => RootMedicationCodedConcept, value ); }
        }

        /// <summary>
        /// Gets the medication code coded concept.
        /// </summary>
        [NotNull]
        public virtual CodedConcept MedicationCodeCodedConcept
        {
            get { return _medicationCodeCodedConcept; }
            private set { ApplyPropertyChange ( ref _medicationCodeCodedConcept, () => MedicationCodeCodedConcept, value ); }
        }

        /// <summary>
        /// Gets the medication route.
        /// </summary>
        public virtual MedicationRoute MedicationRoute
        {
            get { return _medicationRoute; }
            private set { ApplyPropertyChange ( ref _medicationRoute, () => MedicationRoute, value ); }
        }

        /// <summary>
        /// Gets the medication dose unit.
        /// </summary>
        public virtual MedicationDoseUnit MedicationDoseUnit
        {
            get { return _medicationDoseUnit; }
            private set { ApplyPropertyChange ( ref _medicationDoseUnit, () => MedicationDoseUnit, value ); }
        }

        /// <summary>
        /// Gets the medication dose value.
        /// </summary>
        public virtual double? MedicationDoseValue
        {
            get { return _medicationDoseValue; }
            private set { ApplyPropertyChange ( ref _medicationDoseValue, () => MedicationDoseValue, value ); }
        }

        /// <summary>
        /// Gets the over the counter indicator.
        /// </summary>
        public virtual bool? OverTheCounterIndicator
        {
            get { return _overTheCounterIndicator; }
            private set { ApplyPropertyChange ( ref _overTheCounterIndicator, () => OverTheCounterIndicator, value ); }
        }

        /// <summary>
        /// Gets the instructions note.
        /// </summary>
        public virtual string InstructionsNote
        {
            get { return _instructionsNote; }
            private set { ApplyPropertyChange ( ref _instructionsNote, () => InstructionsNote, value ); }
        }

        /// <summary>
        /// Gets the usage date range.
        /// </summary>
        public virtual DateRange UsageDateRange
        {
            get { return _usageDateRange; }
            private set { ApplyPropertyChange ( ref _usageDateRange, () => UsageDateRange, value ); }
        }

        /// <summary>
        /// Gets the medication status.
        /// </summary>
        public virtual MedicationStatus MedicationStatus
        {
            get { return _medicationStatus; }
            private set { ApplyPropertyChange ( ref _medicationStatus, () => MedicationStatus, value ); }
        }

        /// <summary>
        /// Gets the name of the prescribing physician.
        /// </summary>
        /// <value>
        /// The name of the prescribing physician.
        /// </value>
        public virtual string PrescribingPhysicianName
        {
            get { return _prescribingPhysicianName; }
            private set { ApplyPropertyChange ( ref _prescribingPhysicianName, () => PrescribingPhysicianName, value ); }
        }

        /// <summary>
        /// Gets the name of the discontinued by physician.
        /// </summary>
        /// <value>
        /// The name of the discontinued by physician.
        /// </value>
        public virtual string DiscontinuedByPhysicianName
        {
            get { return _discontinuedByPhysicianName; }
            private set { ApplyPropertyChange ( ref _discontinuedByPhysicianName, () => DiscontinuedByPhysicianName, value ); }
        }

        /// <summary>
        /// Gets the discontinued reason.
        /// </summary>
        public virtual DiscontinuedReason DiscontinuedReason
        {
            get { return _discontinuedReason; }
            private set { ApplyPropertyChange ( ref _discontinuedReason, () => DiscontinuedReason, value ); }
        }

        /// <summary>
        /// Gets the discontinued reason other description.
        /// </summary>
        public virtual string DiscontinuedReasonOtherDescription
        {
            get { return _discontinuedReasonOtherDescription; }
            private set { ApplyPropertyChange ( ref _discontinuedReasonOtherDescription, () => DiscontinuedReasonOtherDescription, value ); }
        }

        /// <summary>
        /// Gets the frequency description.
        /// </summary>
        public virtual string FrequencyDescription
        {
            get { return _frequencyDescription; }
            private set { ApplyPropertyChange ( ref _frequencyDescription, () => FrequencyDescription, value ); }
        }

        /// <summary>
        /// Gets the provenance.
        /// </summary>
        public virtual Provenance Provenance
        {
            get { return _provenance; }
            private set { ApplyPropertyChange ( ref _provenance, () => Provenance, value ); }
        }

        #region IPatientAccessAuditable Members

        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }

        #endregion


        /// <summary>
        /// Gets the provenance.
        /// </summary>
        Provenance IHasProvenance.Provenance
        {
            get { return Provenance; }
        }

        /// <summary>
        /// Revises the root medication coded concept.
        /// </summary>
        /// <param name="rootMedicationCodedConcept">The root medication coded concept.</param>
        public virtual void ReviseRootMedicationCodedConcept ( CodedConcept rootMedicationCodedConcept )
        {
            RootMedicationCodedConcept = rootMedicationCodedConcept;
        }

        /// <summary>
        /// Revises the medication code coded concept.
        /// </summary>
        /// <param name="medicationCodeCodedConcept">The medication code coded concept.</param>
        public virtual void ReviseMedicationCodeCodedConcept ( CodedConcept medicationCodeCodedConcept )
        {
            Check.IsNotNull ( medicationCodeCodedConcept, () => MedicationCodeCodedConcept );
            MedicationCodeCodedConcept = medicationCodeCodedConcept;
        }

        /// <summary>
        /// Revises the medication code coded concept.
        /// </summary>
        /// <param name="medicationRoute">The medication route.</param>
        public virtual void ReviseMedicationCodeCodedConcept ( MedicationRoute medicationRoute )
        {
            MedicationRoute = medicationRoute;
        }

        /// <summary>
        /// Revises the medication dose unit.
        /// </summary>
        /// <param name="medicationDoseUnit">The medication dose unit.</param>
        public virtual void ReviseMedicationDoseUnit ( MedicationDoseUnit medicationDoseUnit )
        {
            MedicationDoseUnit = medicationDoseUnit;
        }

        /// <summary>
        /// Revises the medication dose value.
        /// </summary>
        /// <param name="medicationDoseValue">The medication dose value.</param>
        public virtual void ReviseMedicationDoseValue ( double? medicationDoseValue )
        {
            MedicationDoseValue = medicationDoseValue;
        }

        /// <summary>
        /// Revises the over the counter indicator.
        /// </summary>
        /// <param name="overTheCounterIndicator">The over the counter indicator.</param>
        public virtual void ReviseOverTheCounterIndicator ( bool? overTheCounterIndicator )
        {
            OverTheCounterIndicator = overTheCounterIndicator;
        }

        /// <summary>
        /// Revises the instructions note.
        /// </summary>
        /// <param name="instructionsNote">The instructions note.</param>
        public virtual void ReviseInstructionsNote ( string instructionsNote )
        {
            InstructionsNote = instructionsNote;
        }

        /// <summary>
        /// Revises the usage date range.
        /// </summary>
        /// <param name="usageDateRange">The usage date range.</param>
        public virtual void ReviseUsageDateRange ( DateRange usageDateRange )
        {
            UsageDateRange = usageDateRange;
        }

        /// <summary>
        /// Revises the medication status.
        /// </summary>
        /// <param name="medicationStatus">The medication status.</param>
        public virtual void ReviseMedicationStatus ( MedicationStatus medicationStatus )
        {
            MedicationStatus = medicationStatus;
        }

        /// <summary>
        /// Revises the name of the prescribing physician.
        /// </summary>
        /// <param name="prescribingPhysicianName">Name of the prescribing physician.</param>
        public virtual void RevisePrescribingPhysicianName ( string prescribingPhysicianName )
        {
            PrescribingPhysicianName = prescribingPhysicianName;
        }

        /// <summary>
        /// Revises the name of the discontinued by physician.
        /// </summary>
        /// <param name="discontinuedByPhysicianName">Name of the discontinued by physician.</param>
        public virtual void ReviseDiscontinuedByPhysicianName ( string discontinuedByPhysicianName )
        {
            DiscontinuedByPhysicianName = discontinuedByPhysicianName;
        }

        /// <summary>
        /// Revises the discontinued reason.
        /// </summary>
        /// <param name="discontinuedReason">The discontinued reason.</param>
        public virtual void ReviseDiscontinuedReason ( DiscontinuedReason discontinuedReason )
        {
            DiscontinuedReason = discontinuedReason;
        }

        /// <summary>
        /// Revises the discontinued reason other description.
        /// </summary>
        /// <param name="discontinuedReasonOtherDescription">The discontinued reason other description.</param>
        public virtual void ReviseDiscontinuedReasonOtherDescription ( string discontinuedReasonOtherDescription )
        {
            DiscontinuedReasonOtherDescription = discontinuedReasonOtherDescription;
        }

        /// <summary>
        /// Revises the frequency description.
        /// </summary>
        /// <param name="frequencyDescription">The frequency description.</param>
        public virtual void ReviseFrequencyDescription ( string frequencyDescription )
        {
            FrequencyDescription = frequencyDescription;
        }
        
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return MedicationCodeCodedConcept.ToString ();
        }
    }
}