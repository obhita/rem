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
using System.Web.Mvc;
using Agatha.Common;
using Rem.Infrastructure.Mvc.UserContext;
using Rem.Infrastructure.Service;
using Rem.Mvc.Models;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.VisitModule;

namespace Rem.Mvc.Controllers
{
    /// <summary>
    /// Class for controlling patient dashboard.
    /// </summary>
    public class PatientDashboardController : UserContextControllerBase
    {
        #region Constants and Fields

        private readonly IRequestDispatcherFactory _requestDispatcherFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDashboardController"/> class.
        /// </summary>
        /// <param name="currentUserContextService">The current user context service.</param>
        /// <param name="requestDispatcherFactory">The request dispatcher factory.</param>
        public PatientDashboardController ( ICurrentUserContextService currentUserContextService, IRequestDispatcherFactory requestDispatcherFactory )
            : base ( currentUserContextService )
        {
            _requestDispatcherFactory = requestDispatcherFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the type of the view name from activity.
        /// </summary>
        /// <param name="activityTypeWellKnownName">Name of the activity type well known.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public static string GetViewNameFromActivityType ( string activityTypeWellKnownName )
        {
            var wellKnownNameActionNameDictionary = new Dictionary<string, string> ();

            wellKnownNameActionNameDictionary.Add ( ActivityType.VitalSign, "EditVitals" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.LabSpecimen, "EditLabResults" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.RadiologyOrder, "EditRadiologyOrder" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.Immunization, "EditImmunization" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.SocialHistory, "EditSocialHistory" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.BriefIntervention, "EditBriefIntervention" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.AuditC, "EditAuditC" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.Phq9, "EditPhq9" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.IndividualCounseling, "EditIndividualCounseling" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.Dast10, "EditDast10" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.GpraInterview, "EditGpraInterview" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.DensAsiInterview, "EditDensAsiInterview" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.GainShortScreener, "EditGainShortScreener" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.Audit, "EditAudit" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.NidaDrugQuestionnaire, "EditNidaDrugQuestionnaire" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.TedsAdmissionInterview, "EditTedsAdmissionInterview" );
            wellKnownNameActionNameDictionary.Add ( ActivityType.TedsDischargeInterview, "EditTedsDischargeInterview" );

            string actionName = null;

            if ( wellKnownNameActionNameDictionary.ContainsKey ( activityTypeWellKnownName ) )
            {
                actionName = wellKnownNameActionNameDictionary[activityTypeWellKnownName];
            }

            return actionName;
        }

        //
        // GET: /PatientDashboard/

        //public ActionResult GetAllActivitiesByClinicalCase(long id)
        //{
        //    //TODO: DI in costructor or use a Func<IRequestHandler> parameter in the constructor, bind the constructor parameter when set up in DI container
        //    var requestDispatcherFactory = IoC.Container.Resolve<IRequestDispatcherFactory>();

        //    var patientId = id;

        //    //Get the ClinicalCases by the patient key
        //    var requestDispatcher = requestDispatcherFactory.CreateRequestDispatcher();
        //    requestDispatcher.Add(new GetAllClinicalCasesByPatientRequest { PatientKey = patientId });
        //    var getAllClinicalCasesByPatientResponse = requestDispatcher.Get<GetAllClinicalCasesByPatientResponse>();
        //    var clinicalCaseList = getAllClinicalCasesByPatientResponse.ClinicalCases;

        //    var firstClinicalCase = clinicalCaseList.FirstOrDefault();

        //    IEnumerable<ActivityDto> activityDtos = null;

        //    if (firstClinicalCase != null)
        //    {
        //        requestDispatcher = requestDispatcherFactory.CreateRequestDispatcher();
        //        requestDispatcher.Add(new GetAllActivitiesByClinicalCaseRequest() { ClinicalCaseKey = firstClinicalCase.Key });

        //        var getAllActivitiesByClinicalCaseResponse = requestDispatcher.Get<GetAllActivitiesByClinicalCaseResponse>();
        //        activityDtos = getAllActivitiesByClinicalCaseResponse.ActivityDtos;
        //    }

        //    return View(activityDtos);
        //}

        /// <summary>
        /// Edits the social history.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="patientId">The patient id.</param>
        /// <returns>A <see cref="System.Web.Mvc.ActionResult"/></returns>
        public ActionResult EditSocialHistory ( long id, long patientId )
        {
            var socialHistoryId = id;

            var requestDispatcher = _requestDispatcherFactory.CreateRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<PatientSummaryDto> { Key = patientId } );
            requestDispatcher.Add ( new GetDtoRequest<SocialHistoryDto> { Key = socialHistoryId } );

            var getPatientSummaryResponse = requestDispatcher.Get<DtoResponse<PatientSummaryDto>> ();
            var patientSummary = getPatientSummaryResponse.DataTransferObject;

            var response = requestDispatcher.Get<DtoResponse<SocialHistoryDto>> ();
            var socialHistoryDto = response.DataTransferObject;

            var model = new EditSocialHistoryModel
                { PatientKey = patientSummary.Key, PatientName = patientSummary.DisplayName, SocialHistory = socialHistoryDto };

            return View ( model );
        }

        /// <summary>
        /// Edits the social history.
        /// </summary>
        /// <param name="editSocialHistoryModel">The edit social history model.</param>
        /// <returns>A <see cref="System.Web.Mvc.ActionResult"/></returns>
        [HttpPost]
        public ActionResult EditSocialHistory ( EditSocialHistoryModel editSocialHistoryModel )
        {
            var requestDispatcher = _requestDispatcherFactory.CreateRequestDispatcher ();
            requestDispatcher.Add ( new SaveDtoRequest<SocialHistoryDto> { DataTransferObject = editSocialHistoryModel.SocialHistory } );

            var response = requestDispatcher.Get<DtoResponse<SocialHistoryDto>> ();

            if ( response.Exception != null )
            {
                ViewBag.ErrorMessage = "Errors occured when trying to save PHQ-2.";
                return View ( editSocialHistoryModel );
            }

            editSocialHistoryModel.SocialHistory = response.DataTransferObject;

            return RedirectToAction ( "GetAllClinicalCasesWithActivitiesForSelectedClinicalCase", new { id = editSocialHistoryModel.PatientKey } );
        }

        /// <summary>
        /// Edits the vitals.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A <see cref="System.Web.Mvc.ActionResult"/></returns>
        public ActionResult EditVitals ( long id )
        {
            return View ();
        }

        /// <summary>
        /// Gets all clinical cases with activities for selected clinical case.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A <see cref="System.Web.Mvc.ActionResult"/></returns>
        public ActionResult GetAllClinicalCasesWithActivitiesForSelectedClinicalCase ( long id )
        {
            var patientId = id;

            //Get the ClinicalCases by the patient key
            var requestDispatcher = _requestDispatcherFactory.CreateRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<PatientSummaryDto> { Key = patientId } );
            requestDispatcher.Add ( new GetAllClinicalCasesByPatientRequest { PatientKey = patientId } );

            var getPatientSummaryResponse = requestDispatcher.Get<DtoResponse<PatientSummaryDto>> ();
            var patientSummary = getPatientSummaryResponse.DataTransferObject;

            var getAllClinicalCasesByPatientResponse = requestDispatcher.Get<GetAllClinicalCasesByPatientResponse> ();
            var clinicalCaseList = getAllClinicalCasesByPatientResponse.ClinicalCases;

            var model = new GetAllClinicalCasesWithActivitiesForSelectedClinicalCaseModel
                { PatientKey = patientSummary.Key, PatientName = patientSummary.DisplayName, ClinicalCases = clinicalCaseList };

            var firstClinicalCase = clinicalCaseList.FirstOrDefault ();

            if ( firstClinicalCase != null )
            {
                requestDispatcher = _requestDispatcherFactory.CreateRequestDispatcher ();
                requestDispatcher.Add ( new GetAllActivitiesByClinicalCaseRequest { ClinicalCaseKey = firstClinicalCase.Key } );

                var getAllActivitiesByClinicalCaseResponse = requestDispatcher.Get<GetAllActivitiesByClinicalCaseResponse> ();
                IEnumerable<ActivityDto> activities = getAllActivitiesByClinicalCaseResponse.ActivityDtos;
                model.SelectedClinicalCaseKey = firstClinicalCase.Key;
                model.Activities = activities;
            }

            return View ( model );
        }

        #endregion
    }
}
