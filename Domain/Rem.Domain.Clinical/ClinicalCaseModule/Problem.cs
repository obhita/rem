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
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// The Problem defines a patient clinical issue.
    /// </summary>
    public class Problem : AuditableAggregateRootBase, IPatientAccessAuditable, IHasProvenance
    {
        private readonly ClinicalCase _clinicalCase;
        private CodedConcept _problemCodeCodedConcept;

        private Staff _observedByStaff;
        private DateTime? _observedDate;

        private bool? _causeOfDeathIndicator;
        private DateRange _onsetDateRange;
        private ProblemStatus _problemStatus;
        private ProblemType _problemType;
        private DateTime? _statusChangedDate;
        private Provenance _provenance;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Problem"/> class.
        /// </summary>
        protected internal Problem ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Problem"/> class.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="problemCodeCodedConcept">The problem code coded concept.</param>
        protected internal Problem (
            ClinicalCase clinicalCase,
            CodedConcept problemCodeCodedConcept )
        {
            Check.IsNotNull ( clinicalCase, "Clinical case is required." );
            Check.IsNotNull ( problemCodeCodedConcept, "Problem code coded concept is required." );

            _clinicalCase = clinicalCase;
            _problemCodeCodedConcept = problemCodeCodedConcept;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Problem"/> class.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="problemCodeCodedConcept">The problem code coded concept.</param>
        /// <param name="provenance">The provenance.</param>
        protected internal Problem(
            ClinicalCase clinicalCase,
            CodedConcept problemCodeCodedConcept,
            Provenance provenance)
        {
            Check.IsNotNull(clinicalCase, "Clinical case is required.");
            Check.IsNotNull(problemCodeCodedConcept, "Problem code coded concept is required.");
            Check.IsNotNull ( provenance, () => Provenance );

            _clinicalCase = clinicalCase;
            _problemCodeCodedConcept = problemCodeCodedConcept;
            _provenance = provenance;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the clinical case.
        /// </summary>
        [NotNull]
        public virtual ClinicalCase ClinicalCase
        {
            get { return _clinicalCase; }
            private set { }
        }

        /// <summary>
        /// Gets the problem code coded concept.
        /// </summary>
        [NotNull]
        public virtual CodedConcept ProblemCodeCodedConcept
        {
            get { return _problemCodeCodedConcept; }
            private set { ApplyPropertyChange(ref _problemCodeCodedConcept, () => ProblemCodeCodedConcept, value); }
        }

        /// <summary>
        /// Gets the problem status.
        /// </summary>
        public virtual ProblemStatus ProblemStatus
        {
            get { return _problemStatus; }
            private set { ApplyPropertyChange ( ref _problemStatus, () => ProblemStatus, value ); }
        }

        /// <summary>
        /// Gets the status changed date.
        /// </summary>
        public virtual DateTime? StatusChangedDate
        {
            get { return _statusChangedDate; }
            private set { ApplyPropertyChange(ref _statusChangedDate, () => StatusChangedDate, value); }
        }

        /// <summary>
        /// Gets the type of the problem.
        /// </summary>
        /// <value>
        /// The type of the problem.
        /// </value>
        public virtual ProblemType ProblemType
        {
            get { return _problemType; }
            private set { ApplyPropertyChange ( ref _problemType, () => ProblemType, value ); }
        }

        /// <summary>
        /// Gets the observed by staff.
        /// </summary>
        public virtual Staff ObservedByStaff
        {
            get { return _observedByStaff; }
            private set { ApplyPropertyChange(ref _observedByStaff, () => ObservedByStaff, value); }
        }

        /// <summary>
        /// Gets the observed date.
        /// </summary>
        public virtual DateTime? ObservedDate
        {
            get { return _observedDate; }
            private set { ApplyPropertyChange ( ref _observedDate, () => ObservedDate, value ); }
        }

        /// <summary>
        /// Gets the onset date range.
        /// </summary>
        public virtual DateRange OnsetDateRange
        {
            get { return _onsetDateRange; }
            private set { ApplyPropertyChange ( ref _onsetDateRange, () => OnsetDateRange, value ); }
        }

        /// <summary>
        /// Gets the cause of death indicator.
        /// </summary>
        public virtual bool? CauseOfDeathIndicator
        {
            get { return _causeOfDeathIndicator; }
            private set { ApplyPropertyChange ( ref _causeOfDeathIndicator, () => CauseOfDeathIndicator, value ); }
        }

        /// <summary>
        /// Gets the provenance.
        /// </summary>
        public virtual Provenance Provenance
        {
            get { return _provenance; }
            private set {}
        }

        #endregion

        #region IPatientAccessAuditable Members

        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return ClinicalCase.Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1} on {2}", GetType().Name.SeparatePascalCaseWords(), ToString(), _clinicalCase); }
        }

        #endregion

        Provenance IHasProvenance.Provenance
        {
            get { return Provenance; }
        }

        /// <summary>
        /// Revises the problem code.
        /// </summary>
        /// <param name="problemCodeCodedConcept">The problem code coded concept.</param>
        public virtual void ReviseProblemCode(CodedConcept problemCodeCodedConcept)
        {
            Check.IsNotNull(problemCodeCodedConcept, "Problem code coded concept is required.");
            ProblemCodeCodedConcept = problemCodeCodedConcept;
        }

        /// <summary>
        /// Updates the problem status.
        /// </summary>
        /// <param name="problemStatus">The problem status.</param>
        /// <param name="statusChangedDate">The status changed date.</param>
        public virtual void UpdateProblemStatus(ProblemStatus problemStatus, DateTime? statusChangedDate)
        {
            if ((_problemStatus == null && problemStatus != null) || (_problemStatus != null && !_problemStatus.Equals(problemStatus)))
            {
                ProblemStatus = problemStatus;
                StatusChangedDate = statusChangedDate;
            }
        }

        /// <summary>
        /// Revises the type of the problem.
        /// </summary>
        /// <param name="problemType">Type of the problem.</param>
        public virtual void ReviseProblemType(ProblemType problemType)
        {
            ProblemType = problemType;
        }

        /// <summary>
        /// Revises the observation info.
        /// </summary>
        /// <param name="observedBy">The observed by.</param>
        /// <param name="observedDate">The observed date.</param>
        public virtual void ReviseObservationInfo(Staff observedBy, DateTime? observedDate)
        {
            DomainRuleEngine.CreateRuleEngine ( this, "ReviseObservationInfoRuleSet" )
                .WithContext ( observedBy )
                .WithContext ( observedDate )
                .Execute(() =>
                {
                    ObservedByStaff = observedBy;
                    ObservedDate = observedDate;
                });
        }

        /// <summary>
        /// Revises the onset date range.
        /// </summary>
        /// <param name="onsetDateRange">The onset date range.</param>
        public virtual void ReviseOnsetDateRange(DateRange onsetDateRange)
        {
            OnsetDateRange = onsetDateRange;
        }

        /// <summary>
        /// Revises the cause of death indicator.
        /// </summary>
        /// <param name="causeOfDeathIndicator">The cause of death indicator.</param>
        public virtual void ReviseCauseOfDeathIndicator(bool? causeOfDeathIndicator)
        {
            CauseOfDeathIndicator = causeOfDeathIndicator;
        }

        #region Overrides

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>
        /// A string.
        /// </returns>
        public override string ToString ()
        {
            return string.Format("[{0}] {1}", ProblemCodeCodedConcept.CodedConceptCode, ProblemCodeCodedConcept.CodeSystemName);
        }

        #endregion
    }
}