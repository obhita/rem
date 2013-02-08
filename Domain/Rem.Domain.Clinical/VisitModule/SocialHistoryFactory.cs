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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// The SocialHistoryFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.VisitModule.SocialHistory">SocialHistory</see>.
    /// </summary>
    public class SocialHistoryFactory : ISocialHistoryFactory
    {
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly ISocialHistoryRepository _socialHistoryRepository;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialHistoryFactory"/> class.
        /// </summary>
        /// <param name="socialHistoryRepository">The social history repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public SocialHistoryFactory (
            ISocialHistoryRepository socialHistoryRepository,
            ILookupValueRepository lookupValueRepository )
        {
            _socialHistoryRepository = socialHistoryRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #endregion

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return WellKnownNames.VisitModule.ActivityType.SocialHistory; }
        }

        /// <summary>
        /// Gets the type of the registration.
        /// </summary>
        /// <value>
        /// The type of the registration.
        /// </value>
        public Type RegistrationType
        {
            get { return typeof(IActivityFactory); }
        }

        #region ISocialHistoryFactory Members

        /// <summary>
        /// If the visit does not have a SocialHistory, then it returns a newly created SocialHistory,
        /// else, it returns an existing one from the visit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A SocialHistory.</returns>
        public SocialHistory CreateSocialHistory ( Visit visit )
        {
            Check.IsNotNull ( visit, "visit is required." );

            //NOTE : There can be only one 'Social History' per visit.
            SocialHistory socialHistory;

            var existingSocialHistory = _socialHistoryRepository.GetSocialHistoryInVisit(visit.Key);

            if (existingSocialHistory != null)
            {
                socialHistory = existingSocialHistory;
            }
            else
            {
                var type = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.SocialHistory );
                socialHistory = new SocialHistory ( visit, type );

                _socialHistoryRepository.MakePersistent ( socialHistory );
            }
            return socialHistory;
        }

        /// <summary>
        /// Destroys the social history.
        /// </summary>
        /// <param name="socialHistory">The social history.</param>
        public void DestroySocialHistory ( SocialHistory socialHistory )
        {
            _socialHistoryRepository.MakeTransient ( socialHistory );
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity ( Visit visit )
        {
            return CreateSocialHistory ( visit );
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity ( Activity activity )
        {
            DestroySocialHistory((SocialHistory) activity);
        }
    }
}