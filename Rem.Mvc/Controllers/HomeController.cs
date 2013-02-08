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
using System.Web.Mvc;
using Agatha.Common;
using Rem.Infrastructure.Mvc.UserContext;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.PatientModule.Web.ClinicianDashboard;

namespace Rem.Mvc.Controllers
{
    /// <summary>
    /// Class for controlling home.
    /// </summary>
    public class HomeController : UserContextControllerBase
    {
        #region Constants and Fields

        private readonly IRequestDispatcherFactory _requestDispatcherFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="currentUserContextService">The current user context service.</param>
        /// <param name="requestDispatcherFactory">The request dispatcher factory.</param>
        public HomeController ( ICurrentUserContextService currentUserContextService, IRequestDispatcherFactory requestDispatcherFactory )
            : base ( currentUserContextService )
        {
            _requestDispatcherFactory = requestDispatcherFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>A <see cref="System.Web.Mvc.ActionResult"/></returns>
        public ActionResult Index ()
        {
            var requestDispatcher = _requestDispatcherFactory.CreateRequestDispatcher ();
                //IoC.Container.Resolve<IRequestDispatcher>();

            requestDispatcher.Add ( new GetClinicianPatientsRequest { ClinicianKey = StaffContext.Key } );

            var response = requestDispatcher.Get<GetClinicianPatientsResponse> ();

            IEnumerable<ClinicianPatientDto> patientList = null;

            if ( response.Exception == null )
            {
                patientList = response.PatientList;
            }
            else
            {
                // Notify exception
            }

            //SocialHistoryDto socialHistory = new SocialHistoryDto();
            //socialHistory.VisitKey = 4000;
            //socialHistory.Phq2FeelingDownAnswerNumber = 1;
            //socialHistory.Phq2LittleInterestInDoingThingsAnswerNumber = 2;
            //socialHistory.Phq2Score = 3;

            //var request = new SaveDtoRequest<SocialHistoryDto> ();
            //request.DataTransferObject = socialHistory;

            //requestDispatcher.Add(request);

            //var responseResult = requestDispatcher.Get<DtoResponse<SocialHistoryDto>> ();

            return View ( patientList );
        }

        #endregion
    }
}
