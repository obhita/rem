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
using ActivityType = Rem.WellKnownNames.VisitModule.ActivityType;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// The LabSpecimenFactory implements lifetime management of <see cref="T:Rem.Domain.Clinical.LabModule.LabSpecimen">LabSpecimen</see>.
    /// </summary>
    public class LabSpecimenFactory : ILabSpecimenFactory
    {
        private readonly ILabSpecimenRepository _labSpecimenRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSpecimenFactory"/> class.
        /// </summary>
        /// <param name="labSpecimenRepository">The lab specimen repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public LabSpecimenFactory(
            ILabSpecimenRepository labSpecimenRepository,
            ILookupValueRepository lookupValueRepository)
        {
            _labSpecimenRepository = labSpecimenRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #endregion

        #region Implementation of ILabSpecimenFactory

        /// <summary>
        /// Creates the lab specimen.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A LabSpecimen.</returns>
        public LabSpecimen CreateLabSpecimen(Visit visit)
        {
            var type = _lookupValueRepository.GetLookupByWellKnownName<VisitModule.ActivityType>(ActivityType.LabSpecimen);

            var labSpecimen = new LabSpecimen(visit, type);

            _labSpecimenRepository.MakePersistent(labSpecimen);

            return labSpecimen;
        }

        /// <summary>
        /// Creates the lab specimen.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        /// <returns>A LabSpecimen.</returns>
        public LabSpecimen CreateLabSpecimen(ClinicalCase clinicalCase, Provenance provenance, DateTimeRange activityDateTimeRange)
        {
            var type = _lookupValueRepository.GetLookupByWellKnownName<VisitModule.ActivityType>(ActivityType.LabSpecimen);

            var labSpecimen = new LabSpecimen(clinicalCase, type, provenance, activityDateTimeRange);

            _labSpecimenRepository.MakePersistent(labSpecimen);

            return labSpecimen;
        }

        /// <summary>
        /// Destroys the lab specimen.
        /// </summary>
        /// <param name="labSpecimen">The lab specimen.</param>
        public void DestroyLabSpecimen(LabSpecimen labSpecimen)
        {
            _labSpecimenRepository.MakeTransient(labSpecimen);
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateLabSpecimen(visit);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="provenance">The provenance.</param>
        /// <param name="activityDateTimeRange">The activity date time range.</param>
        /// <returns> An Activity. </returns>
        public Activity CreateActivity ( ClinicalCase clinicalCase, Provenance provenance, DateTimeRange activityDateTimeRange )
        {
            return CreateLabSpecimen ( clinicalCase, provenance, activityDateTimeRange );
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyLabSpecimen((LabSpecimen)activity);
        }
    }
}