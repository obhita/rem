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

using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.GainShortScreenerModule
{
    /// <summary>
    /// The GainShortScreenerFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.GainShortScreenerModule.GainShortScreener">GainShortScreener</see>.
    /// </summary>
    public class GainShortScreenerFactory : IGainShortScreenerFactory
    {
        private readonly IGainShortScreenerRepository _gainShortScreenerRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreenerFactory"/> class.
        /// </summary>
        /// <param name="gainShortScreenerRepository">The gain short screener repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public GainShortScreenerFactory (
            IGainShortScreenerRepository gainShortScreenerRepository,
            ILookupValueRepository lookupValueRepository )
        {
            _gainShortScreenerRepository = gainShortScreenerRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region Implementation of IGainShortScreenerFactory

        /// <summary>
        /// Creates the gain short screener.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A GainShortScreener.</returns>
        public GainShortScreener CreateGainShortScreener ( Visit visit )
        {
            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.GainShortScreener );
            var gainShortScreener = new GainShortScreener ( visit, activityType );

            _gainShortScreenerRepository.MakePersistent ( gainShortScreener );
            return gainShortScreener;
        }

        /// <summary>
        /// Destroys the GainShortScreener.
        /// </summary>
        /// <param name="gainShortScreener">The gain short screener.</param>
        public void DestroyGainShortScreener ( GainShortScreener gainShortScreener )
        {
            _gainShortScreenerRepository.MakeTransient ( gainShortScreener );
        }

        #endregion

        /// <summary>
        /// Creates the GainShortScreener activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateGainShortScreener(visit);
        }

        /// <summary>
        /// Destroys the GainShortScreener activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyGainShortScreener((GainShortScreener)activity);
        }
    }
}