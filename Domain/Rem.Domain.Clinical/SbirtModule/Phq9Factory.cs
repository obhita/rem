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

using Pillar.Common.Utility;
using Pillar.Domain.Event;
using Rem.Domain.Clinical.SbirtModule.Event;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// The Phq9Factory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.SbirtModule.Phq9">Phq9</see>.
    /// </summary>
    public class Phq9Factory : IPhq9Factory
    {
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly IPhq9Repository _phq9Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Phq9Factory"/> class.
        /// </summary>
        /// <param name="phq9Repository">The PHQ9 repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public Phq9Factory ( IPhq9Repository phq9Repository, ILookupValueRepository lookupValueRepository )
        {
            _phq9Repository = phq9Repository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region IPhq9Factory Members
        /// <summary>
        /// If the visit does not have a Phq9, then it returns a newly created Phq9,
        /// else, it returns an existing one from the visit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A Phq9.</returns>
        public Phq9 CreatePhq9 ( Visit visit )
        {
            Check.IsNotNull(visit, "visit is required.");

            Phq9 phq9;
            var existingNidaDrugQuestionnaire = _phq9Repository.GetPhq9ByVisitKey ( visit.Key );

            if (existingNidaDrugQuestionnaire != null)
            {
                phq9 = existingNidaDrugQuestionnaire;
            }
            else
            {
                var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.Phq9 );
                phq9 = new Phq9 ( visit, activityType );

                _phq9Repository.MakePersistent ( phq9 );

                DomainEvent.Raise ( new Phq9CreatedEvent { Phq9 = phq9 } );
            }

            return phq9;
        }

        /// <summary>
        /// Destroys the PHQ9.
        /// </summary>
        /// <param name="phq9">The PHQ9.</param>
        public void DestroyPhq9 ( Phq9 phq9 )
        {
            _phq9Repository.MakeTransient ( phq9 );
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreatePhq9(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyPhq9((Phq9)activity);
        }
    }
}