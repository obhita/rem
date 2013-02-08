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

using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.RadiologyModule
{
    /// <summary>
    /// The RadiologyOrderFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.RadiologyModule.RadiologyOrder">RadiologyOrder</see>.
    /// </summary>
    public class RadiologyOrderFactory : IRadiologyOrderFactory
    {
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly IRadiologyOrderRepository _radiologyOrderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadiologyOrderFactory"/> class.
        /// </summary>
        /// <param name="radiologyOrderRepository">The radiology order repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public RadiologyOrderFactory (
            IRadiologyOrderRepository radiologyOrderRepository,
            ILookupValueRepository lookupValueRepository )
        {
            _radiologyOrderRepository = radiologyOrderRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region IRadiologyOrderFactory Members

        /// <summary>
        /// Creates the radiology order.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A <see cref="T:Rem.Domain.Clinical.RadiologyModule.RadiologyOrder">RadiologyOrder</see>.</returns>
        public RadiologyOrder CreateRadiologyOrder ( Visit visit )
        {
            var type = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.RadiologyOrder );
            var radiology = new RadiologyOrder ( visit, type );

            _radiologyOrderRepository.MakePersistent ( radiology );

            return radiology;
        }

        /// <summary>
        /// Destroys the radiology order.
        /// </summary>
        /// <param name="radiologyOrder">The radiology order.</param>
        public void DestroyRadiologyOrder ( RadiologyOrder radiologyOrder )
        {
            _radiologyOrderRepository.MakeTransient ( radiologyOrder );
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateRadiologyOrder(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The Activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyRadiologyOrder((RadiologyOrder)activity);
        }
    }
}