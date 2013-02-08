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

using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ImmunizationModule
{
    /// <summary>
    /// The ImmunizationFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.ImmunizationModule.Immunization">Immunization</see>.
    /// </summary>
    public class ImmunizationFactory : IImmunizationFactory
    {
        private readonly IImmunizationRepository _immunizationRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmunizationFactory"/> class.
        /// </summary>
        /// <param name="immunizationRepository">The immunization repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public ImmunizationFactory (
            IImmunizationRepository immunizationRepository,
            ILookupValueRepository lookupValueRepository )
        {
            _immunizationRepository = immunizationRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        /// <summary>
        /// Creates the immunization.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Immunization.</returns>
        public Immunization CreateImmunization ( Visit visit )
        {
            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.Immunization );
            var immunization = new Immunization ( visit, activityType );

            _immunizationRepository.MakePersistent ( immunization );

            return immunization;
        }

        /// <summary>
        /// Creates the immunization.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        /// <returns>An immunization.</returns>
        public Immunization CreateImmunization(ClinicalCase clinicalCase, Provenance provenance, DateTimeRange activityDateTimeRange)
        {
            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType>(WellKnownNames.VisitModule.ActivityType.Immunization);
            var immunization = new Immunization(clinicalCase, activityType, provenance, activityDateTimeRange);

            _immunizationRepository.MakePersistent(immunization);

            return immunization;
        }

        /// <summary>
        /// Destroys the immunization.
        /// </summary>
        /// <param name="immunization">The immunization.</param>
        public void DestroyImmunization ( Immunization immunization )
        {
            _immunizationRepository.MakeTransient ( immunization );
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateImmunization(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyImmunization((Immunization)activity);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(ClinicalCase clinicalCase, Provenance provenance, DateTimeRange activityDateTimeRange)
        {
            return CreateImmunization(clinicalCase, provenance, activityDateTimeRange);
        }
    }
}