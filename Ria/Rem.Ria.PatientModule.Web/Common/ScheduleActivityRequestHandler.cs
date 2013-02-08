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

using Agatha.Common;
using AutoMapper;
using Pillar.Common.Utility;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Class for handling schedule activity request.
    /// </summary>
    public class ScheduleActivityRequestHandler : NHibernateSessionRequestHandler<ScheduleActivityRequest, ScheduleActivityResponse>
    {
        #region Constants and Fields

        private readonly IActivitySchedulerService _activityScheduler;
        private ScheduleActivityResponse _response;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleActivityRequestHandler"/> class.
        /// </summary>
        /// <param name="activityScheduler">The activity scheduler.</param>
        public ScheduleActivityRequestHandler ( IActivitySchedulerService activityScheduler )
        {
            _activityScheduler = activityScheduler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( ScheduleActivityRequest request )
        {
            _response = CreateTypedResponse ();
            _response.Activity = new ActivityDto ();

            ActivityType activityType;

            if ( TryGetActivityType ( request.ActivityType, out activityType ) )
            {
                var activity = _activityScheduler.ScheduleActivity ( request.VisitKey, activityType );
                _response.Activity = Mapper.Map<Activity, ActivityDto> ( activity );
            }

            return _response;
        }

        #endregion

        #region Methods

        private bool TryGetActivityType ( ActivityTypeDto activityTypeDto, out ActivityType activityType )
        {
            var result = true;

            Check.IsNotNull ( activityTypeDto.Key, "ActivityType Key is required for creation request." );

            activityType = Session.Get<ActivityType> ( activityTypeDto.Key );

            if ( activityType == null )
            {
                _response.Activity.AddDataErrorInfo ( new DataErrorInfo ( "Activity type requested for creation was not found.", ErrorLevel.Error ) );
                result = false;
            }

            return result;
        }

        #endregion
    }
}
