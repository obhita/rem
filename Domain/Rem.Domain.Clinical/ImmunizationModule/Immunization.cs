#region License

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

using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.ImmunizationModule
{
    /// <summary>
    /// Immunization defines the creation of immunity (as by vaccination) usually against a particular disease.
    /// </summary>
    public class Immunization : Activity
    {
        private ImmunizationVaccineInfo _immunizationVaccineInfo;
        private ImmunizationAdministration _immunizationAdministration;
        private ImmunizationNotGivenReason _immunizationNotGivenReason;

        /// <summary>
        /// Initializes a new instance of the <see cref="Immunization"/> class.
        /// </summary>
        protected internal Immunization ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Immunization"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal Immunization ( Visit visit, ActivityType activityType )
            : base ( visit, activityType )
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Immunization"/> class.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        protected internal Immunization(ClinicalCase clinicalCase, ActivityType activityType, Provenance provenance, DateTimeRange activityDateTimeRange)
            : base(clinicalCase, activityType, provenance, activityDateTimeRange)
        {
        }

        /// <summary>
        /// Gets the immunization vaccine info.
        /// </summary>
        public virtual ImmunizationVaccineInfo ImmunizationVaccineInfo
        {
            get { return _immunizationVaccineInfo; }
            private set { ApplyPropertyChange ( ref _immunizationVaccineInfo, () => ImmunizationVaccineInfo, value ); }
        }

        /// <summary>
        /// Gets the immunization administration.
        /// </summary>
        public virtual ImmunizationAdministration ImmunizationAdministration
        {
            get { return _immunizationAdministration; }
            private set { ApplyPropertyChange ( ref _immunizationAdministration, () => ImmunizationAdministration, value ); }
        }

        /// <summary>
        /// Gets the immunization not given reason.
        /// </summary>
        public virtual ImmunizationNotGivenReason ImmunizationNotGivenReason
        {
            get { return _immunizationNotGivenReason; }
            private set { ApplyPropertyChange ( ref _immunizationNotGivenReason, () => ImmunizationNotGivenReason, value ); }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return ImmunizationVaccineInfo == null || ImmunizationVaccineInfo.VaccineCodedConcept == null
                       ? base.ToString ()
                       : ImmunizationVaccineInfo.VaccineCodedConcept.DisplayName;
        }

        /// <summary>
        /// Revises the immunization vaccine info.
        /// </summary>
        /// <param name="immunizationVaccineInfo">
        /// The immunization vaccine info.
        /// </param>
        public virtual void ReviseImmunizationVaccineInfo ( ImmunizationVaccineInfo immunizationVaccineInfo )
        {
            ImmunizationVaccineInfo = immunizationVaccineInfo;
        }

        /// <summary>
        /// Revises the immunization administration.
        /// </summary>
        /// <param name="immunizationAdministration">
        /// The immunization administration.
        /// </param>
        public virtual void ReviseImmunizationAdministration ( ImmunizationAdministration immunizationAdministration )
        {
            ImmunizationAdministration = immunizationAdministration;
        }

        /// <summary>
        /// Revises the immunization not given reason.
        /// </summary>
        /// <param name="immunizationNotGivenReason">
        /// The immunization not given reason.
        /// </param>
        public virtual void ReviseImmunizationNotGivenReason ( ImmunizationNotGivenReason immunizationNotGivenReason )
        {
            ImmunizationNotGivenReason = immunizationNotGivenReason;
        }
    }
}
