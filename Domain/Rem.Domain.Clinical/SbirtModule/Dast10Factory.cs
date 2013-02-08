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
    /// The Dast10Factory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.SbirtModule.Dast10">Dast10</see>.
    /// </summary>
    public class Dast10Factory : IDast10Factory
    {
        private readonly IDast10Repository _dast10Repository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dast10Factory"/> class.
        /// </summary>
        /// <param name="dast10Repository">The dast10 repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public Dast10Factory (
            IDast10Repository dast10Repository,
            ILookupValueRepository lookupValueRepository )
        {
            _dast10Repository = dast10Repository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region Implementation of IDast10Factory
        /// <summary>
        /// If the visit does not have a Dast10, then it returns a newly created Dast10,
        /// else, it returns an existing one from the visit.
        /// </summary>
        /// <param name="visit">A visit.</param>
        /// <returns>A Dast10.</returns>
        public Dast10 CreateDast10 ( Visit visit )
        {
            Check.IsNotNull(visit, "visit is required.");

            Dast10 dast10;
            var existingNidaDrugQuestionnaire = _dast10Repository.GetDast10ByVisitKey ( visit.Key );

            if (existingNidaDrugQuestionnaire != null)
            {
                dast10 = existingNidaDrugQuestionnaire;
            }
            else
            {
                var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.Dast10 );
                dast10 = new Dast10 ( visit, activityType );

                _dast10Repository.MakePersistent ( dast10 );

                DomainEvent.Raise ( new Dast10CreatedEvent { Dast10 = dast10 } );
            }

            return dast10;
        }

        /// <summary>
        /// Destroys the Dast10.
        /// </summary>
        /// <param name="dast10">The dast10.</param>
        public void DestroyDast10 ( Dast10 dast10 )
        {
            _dast10Repository.MakeTransient ( dast10 );
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateDast10(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyDast10((Dast10)activity);
        }
    }
}