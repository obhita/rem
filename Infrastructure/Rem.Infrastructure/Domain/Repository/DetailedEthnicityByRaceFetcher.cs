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
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Core.CommonModule.LookupBase">LookupBase</see>.
    /// </summary>
    public class DetailedEthnicityByRaceFetcher : NHibernateRepositoryBase<RaceDetailedEthnicity>, ILookupValueByRelatedKeysFetcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedEthnicityByRaceFetcher"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public DetailedEthnicityByRaceFetcher(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        #region ILookupValueByRelatedKeysFetcher Members

        /// <summary>
        /// Gets the lookup values.
        /// </summary>
        /// <param name="keys">The lookup keys.</param>
        /// <returns>An IList&lt;<see cref="LookupBase"/>LookupBase&gt;.</returns>
        public IList<LookupBase> GetLookups ( long[] keys )
        {
            var raceDetailedEthnicities = Session.CreateCriteria ( typeof ( RaceDetailedEthnicity ) )
                .SetCacheable ( true )
                .SetCacheMode ( CacheMode.Normal )
                .List<RaceDetailedEthnicity> ();

            var detailedEthnicities = raceDetailedEthnicities.Where ( p => keys.Contains ( p.Race.Key ) )
                .Select ( p => p.DetailedEthnicity );

            return detailedEthnicities.ToList<LookupBase> ();
        }

        #endregion
    }
}