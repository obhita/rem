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

using System;
using Pillar.Common.Utility;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraInterviewFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.GpraModule.GpraInterview">GpraInterview</see>.
    /// </summary>
    public class GpraInterviewFactory : IGpraInterviewFactory
    {
        private readonly IGpraInterviewRepository _gpraInterviewRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterviewFactory"/> class.
        /// </summary>
        /// <param name="gpraInterviewRepository">The Gpra interview repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public GpraInterviewFactory(IGpraInterviewRepository gpraInterviewRepository,
                                            ILookupValueRepository lookupValueRepository)
        {
            _gpraInterviewRepository = gpraInterviewRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return WellKnownNames.VisitModule.ActivityType.GpraInterview; }
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

        /// <summary>
        /// Destroys the Gpra interview.
        /// </summary>
        /// <param name="gpraInterview">The Gpra interview.</param>
        public void DestroyGpraInterview(GpraInterview gpraInterview)
        {
            Check.IsNotNull(gpraInterview, "GpraInterview is required.");
            
            _gpraInterviewRepository.MakeTransient(gpraInterview);
        }

        /// <summary>
        /// Creates the Gpra interview.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A GpraInterview.</returns>
        public GpraInterview CreateGpraInterview(Visit visit )
        {
            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType>(WellKnownNames.VisitModule.ActivityType.GpraInterview);
            var gpraInterview = new GpraInterview ( visit, activityType );

            _gpraInterviewRepository.MakePersistent ( gpraInterview );

            return gpraInterview;
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity ( Visit visit )
        {
            return CreateGpraInterview ( visit );
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyGpraInterview ( ( GpraInterview ) activity );
        }
    }
}