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

using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// The BriefInterventionFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.VisitModule.Activity">Activity</see>.
    /// </summary>
    public class BriefInterventionFactory : IBriefInterventionFactory
    {
        private readonly IBriefInterventionRepository _briefInterventionRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BriefInterventionFactory"/> class.
        /// </summary>
        /// <param name="briefInterventionRepository">The brief intervention repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public BriefInterventionFactory (
            IBriefInterventionRepository briefInterventionRepository,
            ILookupValueRepository lookupValueRepository)
        {
            _briefInterventionRepository = briefInterventionRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region IBriefInterventionFactory Members

        /// <summary>
        /// Creates the brief intervention.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A BriefIntervention.</returns>
        public BriefIntervention CreateBriefIntervention ( Visit visit )
        {
            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.BriefIntervention );
            var briefIntervention = new BriefIntervention ( visit, activityType );

            _briefInterventionRepository.MakePersistent ( briefIntervention );

            return briefIntervention;
        }

        /// <summary>
        /// Destroys the brief intervention.
        /// </summary>
        /// <param name="briefIntervention">The brief intervention.</param>
        public void DestroyBriefIntervention ( BriefIntervention briefIntervention )
        {
            _briefInterventionRepository.MakeTransient ( briefIntervention );
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateBriefIntervention(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyBriefIntervention((BriefIntervention)activity);
        }
    }
}