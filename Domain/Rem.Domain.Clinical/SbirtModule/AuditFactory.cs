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

using System.Linq;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// The AuditFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.SbirtModule.AuditFactory">AuditFactory</see>.
    /// </summary>
    public class AuditFactory : IAuditFactory
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditFactory"/> class.
        /// </summary>
        /// <param name="auditRepository">The audit repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public AuditFactory (
            IAuditRepository auditRepository,
            ILookupValueRepository lookupValueRepository )
        {
            _auditRepository = auditRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region Implementation of IAuditFactory

        /// <summary>
        /// Creates the audit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>
        /// An Audit.
        /// </returns>
        public Audit CreateAudit ( Visit visit)
        {
            // TODO: This checks only the currently submitted session activities. Need to implement a solution that addresses multiple session concurrency issues.
            var auditFirst = visit.Activities.FirstOrDefault(a => a.ActivityType.WellKnownName == WellKnownNames.VisitModule.ActivityType.Audit);
            if (auditFirst != null)
            {
                return auditFirst as Audit;
            }

            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType>(WellKnownNames.VisitModule.ActivityType.Audit);
            var audit = new Audit(visit, activityType);

            _auditRepository.MakePersistent(audit);

            return audit;
        }

        /// <summary>
        /// Destroys the Audit.
        /// </summary>
        /// <param name="audit">The audit.</param>
        public void DestroyAudit ( Audit audit )
        {
            _auditRepository.MakeTransient(audit);
        }

        #endregion

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>An Activity.</returns>
        public Activity CreateActivity(Visit visit)
        {
            return CreateAudit(visit);
        }

        /// <summary>
        /// Destroys the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void DestroyActivity(Activity activity)
        {
            DestroyAudit((Audit)activity);
        }
    }
}
