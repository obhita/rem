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
using Agatha.Common;
using NHibernate.Linq;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling get all activities by clinical case request.
    /// </summary>
    public class GetAllActivitiesByClinicalCaseRequestHandler :
        NHibernateSessionRequestHandler<GetAllActivitiesByClinicalCaseRequest, GetAllActivitiesByClinicalCaseResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetAllActivitiesByClinicalCaseRequest request )
        {
            var clinicalCaseKey = request.ClinicalCaseKey;

            var results = from activity in Session.Query<Activity> ()
                          where activity.ClinicalCase.Key == clinicalCaseKey
                          select new ActivityDto
                              {
                                  Key = activity.Key,
                                  ActivityStartDateTime = activity.ActivityDateTimeRange.StartDateTime,
                                  ActivityEndDateTime = activity.ActivityDateTimeRange.EndDateTime,
                                  ActivityType =
                                      new ActivityTypeDto
                                          {
                                              Key = activity.ActivityType.Key,
                                              Name = activity.ActivityType.Name,
                                              ShortName = activity.ActivityType.ShortName,
                                              SortOrderNumber = activity.ActivityType.SortOrderNumber,
                                              WellKnownName = activity.ActivityType.WellKnownName,
                                              CanBeScheduledIndicator = activity.ActivityType.CanBeScheduledIndicator
                                          },
                                  AppointmentStartDateTime =
                                      activity.Visit != null
                                          ? activity.Visit.AppointmentDateTimeRange.StartDateTime
                                          : activity.ActivityDateTimeRange.StartDateTime,
                                  ClinicianKey = activity.Visit.Staff != null ? activity.Visit.Staff.Key : 0,
                                  PatientKey = activity.ClinicalCase.Patient.Key,
                                  VisitKey = activity.Visit != null ? activity.Visit.Key : 0,
                                  VisitTemplateName = activity.Visit != null ? activity.Visit.Name : string.Empty,
                                  VisitStatusWellKnownName = activity.Visit != null ? activity.Visit.VisitStatus.WellKnownName : string.Empty,
                                  ProvenanceKey = activity.Provenance == null ? 0 : activity.Provenance.Key,
                                  ProvenanceAssigningAuthorityName = activity.Provenance == null ? null : activity.Provenance.TaggedDataElement.AssigningAuthorityName
                              };

            var resultsList = results.ToList ();

            var response = CreateTypedResponse ();
            response.ActivityDtos = resultsList;

            return response;
        }

        #endregion
    }
}
