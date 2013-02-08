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
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.VisitModule.Visit">Visit</see>.
    /// </summary>
    public class VisitRepository : NHibernateRepositoryBase<Visit>, IVisitRepository
    {
        private static readonly int RecentVisitCount = 5;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="VisitRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public VisitRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        #region Implementation of IRepository<Visit>

        /// <summary>
        /// Gets the Visit by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A Visit object.</returns>
        public Visit GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Saves a Visit.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A Visit object.</returns>
        public Visit MakePersistent ( Visit entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes a Visit.
        /// </summary>
        /// <param name="entity">The entityto be deleted.</param>
        public void MakeTransient ( Visit entity )
        {
            Helper.MakeTransient ( entity );
        }

        /// <summary>
        /// Gets the visits by clinician and date range.
        /// </summary>
        /// <param name="clinicianKey">The clinician key.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// An IList&lt;Visit&gt;.
        /// </returns>
        public IList<Visit> GetVisitsByClinicianAndDateRange ( long clinicianKey, DateTime startDate, DateTime endDate )
        {
            var visitList =
                from
                    visit in Session.Query<Visit> ()
                where
                    visit.Staff.Key == clinicianKey &&
                    visit.AppointmentDateTimeRange.StartDateTime.Date >= startDate.Date &&
                    visit.AppointmentDateTimeRange.StartDateTime.Date <= endDate.Date
                select
                    visit;

            return visitList.ToList ();
        }

        /// <summary>
        /// Gets the scheduled visits and activities by clinical case.
        /// </summary>
        /// <param name="clinicalCaseKey">The clinical case key.</param>
        /// <returns>
        /// An IList&lt;Visit&gt;.
        /// </returns>
        public IList<Visit> GetScheduledVisitsAndActivitiesByClinicalCase ( long clinicalCaseKey )
        {
            // TODO: Need to also eager fetch activity.type and status but can't figure out how.
            var date = DateTime.Now.Date;
            var scheduledVisits = from
                                      scheduledVisit in Session.Query<Visit> ()
                                  where
                                      scheduledVisit.ClinicalCase.Key == clinicalCaseKey &&
                                      scheduledVisit.AppointmentDateTimeRange.EndDateTime >= date
                                  select
                                      scheduledVisit;

            scheduledVisits = scheduledVisits
                .FetchMany ( v => v.Activities );

            return scheduledVisits.ToList ();
        }

        /// <summary>
        /// Gets the recent visits and activities by clinical case.
        /// </summary>
        /// <param name="clinicalCaseKey">The clinical case key.</param>
        /// <returns>
        /// An IList&lt;Visit&gt;.
        /// </returns>
        public IList<Visit> GetRecentVisitsAndActivitiesByClinicalCase ( long clinicalCaseKey )
        {
            // TODO: Need to also eager fetch activity.type and status but can't figure out how.
            var date = DateTime.Now.Date;
            var recentVisits = from
                                   recentVisit in Session.Query<Visit> ()
                               where
                                   recentVisit.ClinicalCase.Key == clinicalCaseKey &&
                                   recentVisit.AppointmentDateTimeRange.StartDateTime < date
                               orderby
                                   recentVisit.AppointmentDateTimeRange.StartDateTime descending
                               select
                                   recentVisit;

            recentVisits = recentVisits
                .FetchMany ( v => v.Activities );

            var limitedRecentVisits = recentVisits.Take ( RecentVisitCount );

            return limitedRecentVisits.ToList ();
        }

        /// <summary>
        /// Gets the schedulable activity types.
        /// </summary>
        /// <returns>
        /// An IList&lt;ActivityType&gt;.
        /// </returns>
        public IList<ActivityType> GetSchedulableActivityTypes()
        {
            var activityTypeList =
                from
                    activityType in Session.Query<ActivityType> ()
                where
                    activityType.CanBeScheduledIndicator
                select
                    activityType;

            return activityTypeList.ToList();
        }

        #endregion
    }
}