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
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// The TedsDischargeInterviewFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.TedsModule.TedsDischargeInterview">TedsDischargeInterview</see>.
    /// </summary>
    public class TedsDischargeInterviewFactory : ITedsDischargeInterviewFactory
    {
        private readonly ITedsDischargeInterviewRepository _tedsDischargeInterviewRepository;
        private readonly ITedsAdmissionInterviewRepository _tedsAdmissionInterviewRepository;
        private readonly ILookupValueRepository _lookupValueRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeInterviewFactory"/> class.
        /// </summary>
        /// <param name="tedsDischargeInterviewRepository">The teds discharge interview repository.</param>
        /// <param name="tedsAdmissionInterviewRepository">The teds admission interview repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public TedsDischargeInterviewFactory(
            ITedsDischargeInterviewRepository tedsDischargeInterviewRepository,
            ITedsAdmissionInterviewRepository tedsAdmissionInterviewRepository,
            ILookupValueRepository lookupValueRepository)
        {
            _tedsDischargeInterviewRepository = tedsDischargeInterviewRepository;
            _tedsAdmissionInterviewRepository = tedsAdmissionInterviewRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        /// <summary>
        /// Destroys the Teds interview.
        /// </summary>
        /// <param name="tedsDischargeInterview">The Teds interview.</param>
        public void DestroyTedsDischargeInterview(TedsDischargeInterview tedsDischargeInterview)
        {
            Check.IsNotNull(tedsDischargeInterview, "TedsDischargeInterview is required.");

            _tedsDischargeInterviewRepository.MakeTransient(tedsDischargeInterview);
        }

        /// <summary>
        /// Creates the Teds interview.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A TedsDischargeInterview.</returns>
        public TedsDischargeInterview CreateTedsDischargeInterview(Visit visit)
        {
            var activityType =
                _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.TedsDischargeInterview );
            var tedsDischargeInterview = new TedsDischargeInterview ( visit, activityType );
            tedsDischargeInterview.ReviseTedsAdmissionInterview ( GetTedsAdmissionInterview ( visit.Key ) );
            _tedsDischargeInterviewRepository.MakePersistent ( tedsDischargeInterview );

            return tedsDischargeInterview;
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateTedsDischargeInterview(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyTedsDischargeInterview((TedsDischargeInterview)activity);
        }

        private TedsAdmissionInterview GetTedsAdmissionInterview( long visitKey )
        {
            var admission = _tedsAdmissionInterviewRepository.GetByVisitKey ( visitKey );

            if (admission == null)
            {
                throw new Exception ( "Current Vist does not have Teds Admission Interview. Please create it first." );
            }

            return admission;
        }
    }
}