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

using Pillar.Common.Extension;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// The VisitFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.VisitModule.Visit">Visit</see>.
    /// </summary>
    public class VisitFactory : IVisitFactory
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IVisitStatusRepository _visitStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitFactory"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        /// <param name="visitStatusRepository">The visit status repository.</param>
        public VisitFactory (
            IVisitRepository visitRepository,
            IVisitStatusRepository visitStatusRepository)
        {
            _visitRepository = visitRepository;
            _visitStatusRepository = visitStatusRepository;
        }

        #region IVisitFactory Members

        /// <summary>
        /// Creates the visit.
        /// </summary>
        /// <param name="staff">The staff.</param>
        /// <param name="appointmentDateTimeRange">The appointment date time range.</param>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="visitTemplate">The visit template.</param>
        /// <param name="serviceLocation">The service location.</param>
        /// <returns>
        /// A Visit.
        /// </returns>
        public Visit CreateVisit(Staff staff, 
            DateTimeRange appointmentDateTimeRange,
            ClinicalCase clinicalCase,
            VisitTemplate visitTemplate,
            Location serviceLocation )
        {
            var visitStatus = _visitStatusRepository.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled );
            var visit = new Visit (staff, appointmentDateTimeRange, clinicalCase, visitStatus, serviceLocation, visitTemplate.Name, visitTemplate.CptCode );

            _visitRepository.MakePersistent ( visit );
            return visit;
        }

        /// <summary>
        /// Destroys the visit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        public void DestroyVisit ( Visit visit )
        {
            visit.Activities.ForEach ( visit.DeleteActivity );
            _visitRepository.MakeTransient ( visit );
        }

        #endregion
    }
}
