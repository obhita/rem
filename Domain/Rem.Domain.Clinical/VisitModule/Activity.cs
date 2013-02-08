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
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// Activity refers to any task undertaken typically on behalf of a <see cref="Appointment">Patient</see>.
    /// </summary>
    [NotLayerSupertype]
    public abstract class Activity : AuditableAggregateRootBase, IPatientAccessAuditable, IHasProvenance
    {
        #region Events

        #endregion

        #region Member Variables
        
        private readonly ClinicalCase _clinicalCase;
        private readonly Visit _visit;
        private readonly DateTimeRange _activityDateTimeRange;
        private readonly ActivityType _activityType;
        private readonly Provenance _provenance;

        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        protected internal Activity ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal Activity ( Visit visit, ActivityType activityType )
        {
            Check.IsNotNull ( visit, "Visit is required." );
            Check.IsNotNull ( activityType, "Activity Type is required." );

            _visit = visit;
            _clinicalCase = visit.ClinicalCase;
            _activityType = activityType;
            _activityDateTimeRange = visit.AppointmentDateTimeRange;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        protected internal Activity(ClinicalCase clinicalCase, ActivityType activityType, Provenance provenance, DateTimeRange activityDateTimeRange)
        {
            Check.IsNotNull(clinicalCase, "Clinical Case is required.");
            Check.IsNotNull(provenance, "Provenance is required.");
            Check.IsNotNull(activityType, "Activity Type is required.");

            _clinicalCase = clinicalCase;
            _provenance = provenance;
            _activityType = activityType;
            _activityDateTimeRange = activityDateTimeRange;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the visit.
        /// </summary>
        public virtual Visit Visit
        {
            get { return _visit; }
            private set { }
        }

        /// <summary>
        /// Gets the clinical case.
        /// </summary>
        [NotNull]
        public virtual ClinicalCase ClinicalCase
        {
            get { return _clinicalCase; }
            private set{}
        }

        /// <summary>
        /// Gets the type of the activity.
        /// </summary>
        /// <value>
        /// The type of the activity.
        /// </value>
        [NotNull]
        public virtual ActivityType ActivityType
        {
            get { return _activityType; }
            private set { }
        }

        /// <summary>
        /// Gets the activity date time range.
        /// </summary>
        [NotNull]
        public virtual DateTimeRange ActivityDateTimeRange
        {
            get { return _activityDateTimeRange; }
            private set { }
        }

        #endregion

        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return ClinicalCase.Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1} on {2}", GetType().Name.SeparatePascalCaseWords(), ToString(), _visit); }
        }

        /// <summary>
        /// Gets the provenance.
        /// </summary>
        public virtual Provenance Provenance
        {
            get { return _provenance; }
            private set { }
        }

        /// <summary>
        /// Gets the provenance.
        /// </summary>
        Provenance IHasProvenance.Provenance
        {
            get { return Provenance; }
        }

        #region Public Methods

        /// <summary>
        /// Revises the activity date time range.
        /// </summary>
        /// <param name="dateTimeRange">The date time range.</param>
        public virtual void ReviseActivityDateTimeRange(DateTimeRange dateTimeRange)
        {
            Check.IsNotNull(dateTimeRange, "Activity date time range is required.");
            ActivityDateTimeRange = dateTimeRange;

            DomainRuleEngine.CreateRuleEngine<Activity, DateTimeRange>(this, () => ReviseActivityDateTimeRange)
                .WithContext(dateTimeRange)
                .Execute(() => ActivityDateTimeRange = dateTimeRange);
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
            return ActivityType.ToString ();
        }
    }
}