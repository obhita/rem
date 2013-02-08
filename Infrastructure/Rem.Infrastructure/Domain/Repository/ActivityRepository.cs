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
using NHibernate.Linq;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Defines repository services for the <see cref="T:Rem.Domain.Clinical.VisitModule.Activity">Activity</see>
    /// </summary>
    public class ActivityRepository : NHibernateRepositoryBase<Activity>, IActivityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public ActivityRepository (ISessionProvider sessionProvider) : base(sessionProvider)
        {
        }

        #region Implementation of IActivityRepository

        /// <summary>
        /// Gets the activities by clinical case.
        /// </summary>
        /// <param name="clinicalCaseKey">The clinical case key.</param>
        /// <returns>
        /// An IList&lt;Activity&gt;.
        /// </returns>
        public IList<Activity> GetActivitiesByClinicalCase ( long clinicalCaseKey )
        {
            var activities = from activity in Session.Query<Activity> ()
                             where activity.Visit.ClinicalCase.Key == clinicalCaseKey
                             select activity;

            activities = activities.Fetch(a => a.Visit); //.ThenFetch ( v => v );

            return activities.ToList ();
        }

        #endregion

        #region Implementation of IRepository<Activity>

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>An Activity object.</returns>
        public Activity GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Saves a activity.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>An Activity object.</returns>
        public Activity MakePersistent ( Activity entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes a activity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( Activity entity )
        {
            Helper.MakeTransient ( entity );
        }

        #endregion
    }
}